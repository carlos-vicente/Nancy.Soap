using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Nancy.Responses.Negotiation;
using SOAP.Dispatching;
using WSDL;

namespace Nancy.SOAP
{
    public abstract class SoapNancyModule<T> 
        : NancyModule where T : class
    {
        private readonly T _service;
        private readonly IMappingEngine _engine;
        private readonly IGenerator _wsdlGenerator;
        private readonly IDispatcher<T> _dispatcher;

        protected SoapNancyModule(
            string path,
            T service,
            IMappingEngine engine,
            IGenerator wsdlGenerator,
            IDispatcher<T> dispatcher)
            : base(path)
        {
            _service = service;
            _engine = engine;
            _wsdlGenerator = wsdlGenerator;
            _dispatcher = dispatcher;

            Get["/wsdl", true] = GetWsdl;
            Post["/", true] = InvokeOperation;
        }

        protected async Task<dynamic> GetWsdl(dynamic parameters, CancellationToken token)
        {
            // invoke wsdl generator with contract
            var definition = await _wsdlGenerator.GetWebServiceDefinition(typeof (T), "TODO");

            var serializable = _engine
                .Map<WSDL.Models.Definition, WSDL.Serialization.Definition>(definition);

            return Negotiate
                .WithAllowedMediaRange(new MediaRange("application/xml"))
                .WithModel(serializable);
        }

        protected async Task<dynamic> InvokeOperation(dynamic parameters, CancellationToken token)
        {
            // get soap envelop from body

            // get soap body into specific operation

            // use dispatcher to invoke operation on service

            // return soap envelop with response and OK message
            //      if something goes wrong with the invocation then throw error back to the client
            return this.Negotiate.WithStatusCode(HttpStatusCode.OK);
        }
    }
}
