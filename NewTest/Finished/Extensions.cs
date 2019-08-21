using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ZVSlibs.Extensions
{
    public static class Extensions
    {
        public static bool Equal<TypeA, TypeB>(TypeA A, TypeB B)
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

        /// <summary>
        /// Переводит массив строк, содержащий только числа, в массив чисел
        /// </summary>
        /// <param name="strings">Строковые значения чисел</param>
        /// <returns>Массив чисел</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="FormatException"/>
        /// <exception cref="OverflowException"/>
        public static int[] ParseToInt(this string[] strings)
        {
            if (strings.Contains(""))
                return new int[0];

            return strings.Select(x => int.Parse(x)).ToArray();
        }

        public static string ToStr(this int[] numbers)
        {
            if (numbers.Length > 0)
            {
                StringBuilder res = new StringBuilder();
                foreach (var n in numbers)
                {
                    res.Append($"{n}.");
                }
                return res.Remove(res.Length - 1, 1).ToString();
            }
            else return "";
        }
    }
}
