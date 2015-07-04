using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService
{
    public class WorkItems
    {
        public string Item { get; set; }
    }

    public class DoWorkRequest
    {
        public int Identifier { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public List<WorkItems> Items { get; set; }
    }

    public class DoWorkResponse
    {
        public Guid WorkId { get; set; }
    }


    [ServiceContract]
    public interface IComplexService
    {
        [OperationContract]
        DoWorkResponse DoWork(DoWorkRequest request);
    }
}
