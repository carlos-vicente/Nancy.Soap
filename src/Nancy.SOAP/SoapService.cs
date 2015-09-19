using System.Threading.Tasks;
using AutoMapper;
using SOAP.Dispatching;
using WSDL;
using WSDL.Serialization;

namespace Nancy.SOAP
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
            return _engine
                .Map<WSDL.Models.Definition, Definition>(definition);
        }

        //public async Task<global::SOAP.Serialization.Response> InvokeContractMethod(
        //    global::SOAP.Serialization.Request request)
        //{
        //    // map serialization to models
        //    //var modelRequest = _engine
        //    //    .Map<
        //    //        global::SOAP.Serialization.Request,
        //    //        global::SOAP.Models.Request>(request);

        //    var modelRequest = new global::SOAP.Models.Request
        //    {
        //        Name = request.Name,
        //        Parameters = request.Parameters
        //    };

        //    // invoke dispatcher
        //    var modelResponse = await _dispatcher.InvokeMethod(modelRequest);

        //    // map models to serialization
        //    //return _engine
        //    //    .Map<
        //    //        global::SOAP.Models.Response,
        //    //        global::SOAP.Serialization.Response>(modelResponse);

        //    return new global::SOAP.Serialization.Response
        //    {
        //        Name = modelResponse.Name,
        //        Content = modelResponse.Content
        //    };
        //}
    }
}