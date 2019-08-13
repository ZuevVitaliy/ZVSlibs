using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            FieldInfo[] fieldsA = typeA.GetFields();
            FieldInfo[] fieldsB = typeB.GetFields();
            bool equalFields = (from a in fieldsA
                                from b in fieldsB
                                where a.Name == b.Name
                                select a.GetValue(A).Equals(b.GetValue(B))).All(x => x == true);
            PropertyInfo[] propertiesA = typeA.GetProperties();
            PropertyInfo[] propertiesB = typeB.GetProperties();
            bool equalProperties = (from a in propertiesA
                                    from b in propertiesB
                                    where a.Name == b.Name
                                    select a.GetValue(A).Equals(b.GetValue(B))).All(x => x == true);
            return equalFields && equalProperties;
        }
    }
}
