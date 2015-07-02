using System.Net;
using System.Web.Services.Description;

namespace Nancy.SOAP.Visualization
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = parameters =>
            {
                var request = WebRequest.Create("http://localhost:12008/web/wsdl");
                var responseStream = request
                    .GetResponse()
                    .GetResponseStream();
                
                var myDescription = ServiceDescription
                    .Read(responseStream);

                

                return this.Negotiate
                    .WithModel(myDescription)
                    .WithView("Index");
            };
        }
    }
}