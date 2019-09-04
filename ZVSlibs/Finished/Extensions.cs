using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace ZVSlibs.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Метод сравнения объектов на равенство значений для одноименных полей и свойств.
        /// </summary>
        /// <param name="A">Объект А.</param>
        /// <param name="B">Объект Б</param>
        /// <returns>Равны обекты или нет по одноименным полям и свойствам.</returns>
        public static bool Equal(object A, object B)
        {
            Type typeA = A.GetType();
            Type typeB = B.GetType();
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

        /// <summary>
        /// Метод с пока что не определенной куда-то по***нью
        /// </summary>
        public static void SomeShit()
        {
            //получить всех наследников от класса
            var classes = typeof(HttpContent).Assembly.ExportedTypes.Where(t => typeof(HttpContent).IsAssignableFrom(t));
        }
    }
}