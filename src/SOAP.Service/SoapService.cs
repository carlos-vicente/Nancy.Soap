using System.Threading.Tasks;
using AutoMapper;
using SOAP.Dispatching;
using WSDL;
using WSDL.Serialization;
using SOAP.Serialization;
using SOAP.Models;

namespace SOAP.Service
{
    public class SoapService<T> : ISoapService<T>
        where T : class
    {
        private readonly IGenerator _wsdlGenerator;
        private readonly IMappingEngine _engine;
        private readonly IDispatcher _dispatcher;

        public SoapService(
            IGenerator wsdlGenerator,
            IMappingEngine engine,
            IDispatcher dispatcher)
        {
            _wsdlGenerator = wsdlGenerator;
            _engine = engine;
            _dispatcher = dispatcher;
        }

        public async Task<Definition> GetContractDefinition(string endpoint)
        {
            // invoke wsdl generator with contract
            var definition = await _wsdlGenerator
                .GetWebServiceDefinition(
                    typeof (T),
                    endpoint)
                .ConfigureAwait(false);

            // map wsdl defition to xml serializable definition
            return _engine.Map<WSDL.Models.Definition, Definition>(definition);
        }

        public async Task<Envelope> InvokeContractMethod(
            string soapAction, 
            Envelope request)
        {
            // map serialization to models
            var modelRequest = _engine.Map<Envelope, Request>(request);

            // invoke dispatcher
            var modelResponse = await _dispatcher
                .InvokeMethod(modelRequest)
                .ConfigureAwait(false);

            // map models to serialization
            return _engine.Map<Response, Envelope>(modelResponse);
        }
    }
}