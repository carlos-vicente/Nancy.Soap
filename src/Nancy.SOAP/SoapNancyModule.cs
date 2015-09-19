using System.Threading;
using System.Threading.Tasks;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;
using SOAP.Serialization;

namespace Nancy.SOAP
{
    public abstract class SoapNancyModule<T> 
        : NancyModule
        where T : class
    {
        private const string RootRoute = "/";

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
                this.Context.Request.Url.SiteBase,
                this.ModulePath);

            var serializable = await _soapService
                .GetContractDefinition(serviceEndpoint)
                .ConfigureAwait(false);

            // negoticate with client an xml response
            return Negotiate
                .WithAllowedMediaRange(new MediaRange("application/xml"))
                .WithModel(serializable);
        }

        protected async Task<dynamic> InvokeOperation(
            dynamic parameters, 
            CancellationToken token)
        {
            var headers = this.Request.Headers;

            var soapAction = headers[""];

            // get soap envelop from body
            var envelop = this.Bind<Envelope>(new BindingConfig
            {
                BodyOnly = true
            });
            

            // get soap body into specific operation

            // use dispatcher to invoke operation on service

            // return soap envelop with response and OK message
            //      if something goes wrong with the invocation then throw error back to the client
            return this.Negotiate.WithStatusCode(HttpStatusCode.OK);
        }
    }
}
