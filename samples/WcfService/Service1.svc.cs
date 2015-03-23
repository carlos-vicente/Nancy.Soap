using System;

namespace WcfService
{
    public class Service : IContract
    {
        //public string GetData(int value)
        //{
        //    return string.Format("You entered: {0}", value);
        //}

        //public CompositeType GetDataUsingDataContract(CompositeType composite)
        //{
        //    if (composite == null)
        //    {
        //        throw new ArgumentNullException("composite");
        //    }
        //    if (composite.BoolValue)
        //    {
        //        composite.StringValue += "Suffix";
        //    }
        //    return composite;
        //}

        public void OperationNoReturnNoParameters()
        {
            throw new NotImplementedException();
        }

        public void OperationNoReturnWithParameters(int p1)
        {
            throw new NotImplementedException();
        }

        public string OperationWithReturnAndParameters(int p1, string p2)
        {
            throw new NotImplementedException();
        }
    }
}
