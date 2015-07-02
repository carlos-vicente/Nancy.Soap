using System;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ComplexService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ComplexService.svc or ComplexService.svc.cs at the Solution Explorer and start debugging.
    public class ComplexService : IComplexService
    {
        public DoWorkResponse DoWork(DoWorkRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
