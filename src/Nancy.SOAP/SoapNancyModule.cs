using System;
using System.Threading;
using System.Threading.Tasks;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;
using SOAP.Serialization;
using SOAP.Service;
using System.Linq;

namespace Nancy.SOAP
{
    public abstract class SoapNancyModule<T> 
        : NancyModule
        where T : class
    {
        private const string RootRoute = "/";
        private static readonly MediaRange XmlMedia = new MediaRange("application/xml");

        private readonly ISoapService<T> _soapService;

        protected SoapNancyModule(
            string path,
            ISoapService<T> soapService)
            : base(path)
        {
            _soapService = soapService;

            Get[RootRoute, true] = GetWsdl;
            Post[RootRoute, true] = InvokeOperation;
        }

        protected async Task<dynamic> GetWsdl(
            dynamic parameters, 
            CancellationToken token)
        {
            var serviceEndpoint = string.Format(
                "{0}/{1}",
                Context.Request.Url.SiteBase,
                ModulePath);

            var serviceDefinition = await _soapService
                .GetContractDefinition(serviceEndpoint)
                .ConfigureAwait(false);

            // negoticate with client an xml response
            return Negotiate
                .WithStatusCode(HttpStatusCode.OK)
                .WithModel(serviceDefinition)
                .WithAllowedMediaRange(XmlMedia);
        }

        protected async Task<dynamic> InvokeOperation(
            dynamic parameters, 
            CancellationToken token)
        {
            var headers = this.Request.Headers;

            // the SOAPAction header is optional (there is a possibility of doing method routing by request)
            // Consider using an implicit rule: first try to use SOAPAction, if it is empty, try to use request
            var soapAction = headers["SOAPAction"]
                .SingleOrDefault();

            // get soap envelop from body
            var request = this.Bind<Envelope>(
                new BindingConfig // TODO: BindAndValidate -> does it use FluentValidations???
                {
                    BodyOnly = true
                });

            Envelope response;

            try
            {
                response = await _soapService
                    .InvokeContractMethod(soapAction, request)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return this.Negotiate
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithAllowedMediaRange(XmlMedia)
                    //.WithModel(response) // create an response envelope with fault body
                    ;
            }
                        
            // return soap envelop with response and OK message
            //      if something goes wrong with the invocation then throw error back to the client
            return this.Negotiate
                .WithStatusCode(HttpStatusCode.OK)
                .WithModel(response)
                .WithAllowedMediaRange(XmlMedia);
        }
    }
}
