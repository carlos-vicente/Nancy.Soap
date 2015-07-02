using System;
using System.Threading.Tasks;
using AutoMapper;
using WSDL;
using WSDL.Serialization;

namespace Nancy.SOAP
{
    public class SoapService<T> : ISoapService<T> where T : class
    {
        private readonly IGenerator _wsdlGenerator;
        private readonly IMappingEngine _engine;

        public SoapService(IGenerator wsdlGenerator, IMappingEngine engine)
        {
            _wsdlGenerator = wsdlGenerator;
            _engine = engine;
        }

        public async Task<Definition> GetContractDefinition(string endpoint)
        {
            // invoke wsdl generator with contract
            var definition = await _wsdlGenerator.GetWebServiceDefinition(
                typeof(T),
                endpoint);

            // map wsdl defition to xml serializable definition
            return _engine
                .Map<WSDL.Models.Definition, Definition>(definition);
        }

        public Task InvokeContractMethod()
        {
            //TODO: amongst other things, invoke contract implementation, 
            // therefor T must exist

            throw new NotImplementedException();
        }
    }
}