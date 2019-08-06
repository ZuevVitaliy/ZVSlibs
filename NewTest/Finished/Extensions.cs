using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTest
{
    public static class Extensions
    {
        private static bool Equal<TypeA, TypeB>(TypeA A, TypeB B)
        {
            Type typeA = typeof(TypeA);
            Type typeB = typeof(TypeB);
            var fieldsA = typeA.GetFields();
            var fieldsB = typeB.GetFields();
            return (from a in fieldsA
                    from b in fieldsB
                    where a.Name == b.Name
                    select a.GetValue(A).Equals(b.GetValue(B))).All(x => x == true);
        }
    }
}
