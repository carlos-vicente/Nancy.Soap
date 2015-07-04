using Fixie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WSDL.Tests.Fixie
{
    public class CustomConvention : Convention
    {
        public CustomConvention()
        {
            Classes
                .NameEndsWith("Tests");

            Methods
                .Where(method => method.IsVoid());

            Parameters
                .Add<FromInputAttributes>();
        }

        class FromInputAttributes : ParameterSource
        {
            public IEnumerable<object[]> GetParameters(MethodInfo method)
            {
                return method
                    .GetCustomAttributes<InputAttribute>(true)
                    .Select(input => input.Parameters);
            }
        }
    }
}
