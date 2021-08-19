using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ZVS.Global.Extensions
{
    /// <summary>
    /// Класс расширений
    /// </summary>
    public static class CommonExtensions
    {
        /// <summary>
        /// Метод сравнения объектов на равенство значений для одноименных полей и свойств.
        /// </summary>
        /// <param name="A">Объект А.</param>
        /// <param name="B">Объект Б</param>
        /// <returns>Равны обекты или нет по одноименным полям и свойствам.</returns>
        public static bool EqualBySameData(object A, object B, Type castingType = null)
        {
            Type commonType = A.GetType();

            Type[] standartTypes = {
                typeof(bool),
                typeof(byte),
                typeof(sbyte),
                typeof(char),
                typeof(decimal),
                typeof(double),
                typeof(float),
                typeof(int),
                typeof(uint),
                typeof(short),
                typeof(ulong),
                typeof(string),
                typeof(ushort),
                typeof(long),
            };

            if (standartTypes.Any(type => type == commonType))
            {
                return A.Equals(B);
            }

            FieldInfo[] fieldsA;
            FieldInfo[] fieldsB;
            PropertyInfo[] propertiesA;
            PropertyInfo[] propertiesB;

            if (castingType != null)
            {
                if (!(A.GetType().IsAssignableFrom(castingType) || !B.GetType().IsAssignableFrom(castingType)))
                    return false;

                fieldsA = fieldsB = castingType.GetFields();
                propertiesA = propertiesB = castingType.GetProperties();
            }
            else
            {
                Type typeA = A.GetType();
                Type typeB = B.GetType();
                fieldsA = typeA.GetFields();
                fieldsB = typeB.GetFields();
                propertiesA = typeA.GetProperties();
                propertiesB = typeB.GetProperties();
            }
            bool isEqualsFields = (from a in fieldsA
                                   join b in fieldsB on a.Name equals b.Name
                                   select a.GetValue(A).EqualsCustom(b.GetValue(B))).All(x => x);
            bool isEqualsProperties = (from a in propertiesA
                                       join b in propertiesB on a.Name equals b.Name
                                       select a.GetValue(A).EqualsCustom(b.GetValue(B))).All(x => x);
            return isEqualsFields && isEqualsProperties;
        }

        /// <summary>
        /// Метод сравнения объектов на равенство
        /// </summary>
        /// <param name="A">Объект А.</param>
        /// <param name="B">Объект Б</param>
        /// <returns>Равны обекты или нет.</returns>
        public static bool EqualsCustom(this object A, object B)
        {
            return A.GetType() == B.GetType() && EqualBySameData(A, B);
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
            return type.Assembly.ExportedTypes.Where(type.IsAssignableFrom);
        }

        /// <summary>
        /// Получить имя свойства или поля.
        /// </summary>
        /// <param name="propertyOrFieldExpression">Вызываемое свойство или поле.</param>
        /// <returns></returns>
        public static string NameOf(Expression<Func<object>> propertyOrFieldExpression)
        {
            if (propertyOrFieldExpression.Body is UnaryExpression unary)
            {
                return (unary.Operand as MemberExpression)?.Member.Name;
            }

            return (propertyOrFieldExpression.Body as MemberExpression)?.Member.Name;
        }

        /// <summary>
        /// Получить имя метода.
        /// </summary>
        /// <param name="methodExpression">Вызываемый метод.</param>
        /// <returns></returns>
        public static string NameOf(Expression<Action> methodExpression)
        {
            if (methodExpression.Body is MethodCallExpression method)
            {
                return method.Method.Name;
            }
            return null;
        }
    }
}