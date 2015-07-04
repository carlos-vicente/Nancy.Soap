using System.Threading;
using System.Threading.Tasks;
using Nancy.Responses.Negotiation;
using Nancy.Routing;
using System.Linq;

namespace Nancy.SOAP
{
    public abstract class SoapNancyModule<T> 
        : NancyModule where T : class
    {
        private readonly ISoapService<T> _soapService;

        protected SoapNancyModule(
            string path, 
            ISoapService<T> soapService)
            : base(path)
        {
            _soapService = soapService;

            //TODO: which one?
            Get["/wsdl", true] = GetWsdl;
            //Get["/", true] = GetWsdl;

            Post["/", true] = InvokeOperation;
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
            // get soap envelop from body

            await Task.Run(() => { }, token);

            // get soap body into specific operation

            // use dispatcher to invoke operation on service

            // return soap envelop with response and OK message
            //      if something goes wrong with the invocation then throw error back to the client
            return this.Negotiate.WithStatusCode(HttpStatusCode.OK);
        }
    }
}
