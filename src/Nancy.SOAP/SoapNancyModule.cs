using System;
using System.Threading;
using System.Threading.Tasks;
using SOAP.Serialization.Dispatching;
using WSDL.Gen;

namespace Nancy.SOAP
{
    public abstract class SoapNancyModule<T> 
        : NancyModule where T : class
    {
        private readonly T _service;
        private readonly IWsdlGenerator _wsdlGenerator;
        private readonly IDispatcher<T> _dispatcher;

        protected SoapNancyModule(
            string module,
            T service,
            IWsdlGenerator wsdlGenerator,
            IDispatcher<T> dispatcher)
            : base(module)
        {
            _service = service;
            _wsdlGenerator = wsdlGenerator;
            _dispatcher = dispatcher;

            Get["/wsdl", true] = GetWsdl;
            Post["/", true] = InvokeOperation;
        }

        protected async Task<dynamic> GetWsdl(dynamic parameters, CancellationToken token)
        {
            // invoke wsdl generator with contract
            var definition = await _wsdlGenerator.GetWebServiceDefinition(typeof (T));

            return Response.AsXml(definition);

            //return Negotiate
            //    .WithContentType("application/wsdl+xml")
            //    .WithModel(definition);
        }

        protected Task<dynamic> InvokeOperation(dynamic parameters, CancellationToken token)
        {
            // get soap envelop from body

            // get soap body into specific operation

            // use dispatcher to invoke operation on service

            // return soap envelop with response and OK message
            //      if something goes wrong with the invocation then throw error back to the client
            throw new NotImplementedException();
        }
    }
}
