using System;
using System.Collections;
using System.Collections.Generic;

namespace ZVS.Global.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Проверяет, что перечисляемое не инициализировано или не имеет элементов.
        /// </summary>
        /// <param name="source">Исходное перечисляемое.</param>
        /// <returns><see langword="true"/>, если перечисляемое не инициализировано или не имеет элементов, иначе <see langword="false"/>.</returns>
        public static bool IsNullOrEmpty(this IEnumerable source)
        {
            if (source == null)
                return true;

            IEnumerator enumerator = source.GetEnumerator();
            enumerator.Reset();
            return !enumerator.MoveNext();
        }

        /// <summary>
        /// Выборка уникальных значений, по ключу.
        /// </summary>
        /// <typeparam name="TSource">Тип перечисляемых элементов.</typeparam>
        /// <typeparam name="TKey">Тип ключа.</typeparam>
        /// <param name="source">Исходное перечисляемое.</param>
        /// <param name="keySelector">Ключ для выборки уникальных значений из перечисляемого</param>
        /// <remarks><i><b>Примечание:</b> Используется отложенный итератор <see langword="yield"/> для перечисляемого.<br/>
        /// Для полной выборки используйте методы расширения ToList() или ToArray().<br/>
        /// Если необходим составной ключ, то воспользуйтесь анонимным типом (new { key1, key2 })</i><br/></remarks>
        /// <returns>Уникальные значения по ключу.</returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var keys = new HashSet<TKey>();

            foreach (var item in source)
            {
                var key = keySelector(item);

                if (!keys.Contains(key))
                {
                    keys.Add(key);
                    yield return item;
                }
            }
        }

        /// <summary>
        /// Проверяет за один проход, что все или хотя бы один элемент в перечислении удовлетворяют условию.
        /// </summary>
        /// <typeparam name="TSource">Тип элементов перечисления.</typeparam>
        /// <param name="source">Элементы исходного перечисления.</param>
        /// <param name="predicate">Условие для проверки элементов.</param>
        /// <param name="all">Результат, если все вхождения удовлетворяют условию.</param>
        /// <param name="any">Результат, если хотя бы одно вхождение удовлетворяет условию.</param>
        public static void AllAny<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, out bool all, out bool any)
        {
            if (source.IsNullOrEmpty())
            {
                all = false;
                any = false;
                return;
            }

            any = false;
            all = true;
            foreach (var item in source)
            {
                if (predicate(item))
                    any = true;
                else
                    all = false;

                if (any && !all)
                    break;
            }
        }
    }
}
