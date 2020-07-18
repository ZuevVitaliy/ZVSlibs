using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace ZVS.Global.Extensions
{
    /// <summary>
    /// Класс расширений
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Метод сравнения объектов на равенство значений для одноименных полей и свойств.
        /// </summary>
        /// <param name="A">Объект А.</param>
        /// <param name="B">Объект Б</param>
        /// <returns>Равны обекты или нет по одноименным полям и свойствам.</returns>
        public static bool EqualBySameData(object A, object B, Type castingType = null)
        {
            Type typeA;
            Type typeB;
            FieldInfo[] fieldsA;
            FieldInfo[] fieldsB;
            PropertyInfo[] propertiesA;
            PropertyInfo[] propertiesB;

            if (castingType != null)
            {
                if (!(A.GetType().IsAssignableFrom(castingType) || !(B.GetType().IsAssignableFrom(castingType))))
                    return false;

                fieldsA = fieldsB = castingType.GetFields();
                propertiesA = propertiesB = castingType.GetProperties();
            }
            else
            {
                typeA = A.GetType();
                typeB = B.GetType();
                fieldsA = typeA.GetFields();
                fieldsB = typeB.GetFields();
                propertiesA = typeA.GetProperties();
                propertiesB = typeB.GetProperties();
            }
            bool equalFields = (from a in fieldsA
                                from b in fieldsB
                                where a.Name == b.Name
                                select a.GetValue(A).Equals(b.GetValue(B))).All(x => x);
            bool equalProperties = (from a in propertiesA
                                    from b in propertiesB
                                    where a.Name == b.Name
                                    select a.GetValue(A).Equals(b.GetValue(B))).All(x => x);
            return equalFields && equalProperties;
        }

        /// <summary>
        /// Метод сравнения объектов на равенство
        /// </summary>
        /// <param name="A">Объект А.</param>
        /// <param name="B">Объект Б</param>
        /// <returns>Равны обекты или нет.</returns>
        public static bool Equal(object A, object B)
        {
            if (A.GetType() != B.GetType()) return false;
            return EqualBySameData(A, B);
        }

        /// <summary>
        /// Переводит массив строк, содержащий числа, в массив чисел
        /// </summary>
        /// <param name="strings">Строковые значения чисел</param>
        /// <returns>Массив чисел</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="FormatException"/>
        /// <exception cref="OverflowException"/>
        public static double[] ParseToInt(this string[] strings)
        {
            var result = new List<double>();
            foreach (var str in strings)
            {
                bool isParsed = double.TryParse(str, out double number);
                if (isParsed) result.Add(number);
            }
            return result.ToArray();
        }

        /// <summary>
        /// Получить всех наследников от типа.
        /// </summary>
        public static IEnumerable<Type> GetInherits(this Type type)
        {
            return type.Assembly.ExportedTypes.Where(t => t.IsAssignableFrom(type));
        }

        /// <summary>
        /// Получить всех предков от типа.
        /// </summary>
        public static IEnumerable<Type> GetAncestors(this Type type)
        {
            return type.Assembly.ExportedTypes.Where(t => type.IsAssignableFrom(t));
        }
    }
}