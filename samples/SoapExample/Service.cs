namespace SoapExample
{
    public class Service : IService
    {
        public void DoSomething()
        {
            
        }

        public string DoSomethingElse(int value1, string value2)
        {
            return string.Format("Something else with {0} and {1}", value1, value2);
        }
    }
}