using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ZVS.Global.Comparers;

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
        public static IEnumerable<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.Distinct(new KeyComparer<TSource, TKey>(keySelector));
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


        /// <summary>
        /// Проверка на совпадение элементов двух перечисляемых, игнорируя порядок этих элементов и наличие дублей.
        /// <br><i>Для сравнения элементов используется <see cref="HashSet{T}"/>.</i></br>
        /// </summary>
        /// <param name="thisEnumerable">Текущее перечисляемое.</param>
        /// <param name="otherEnumerable">Сравниваемое перечисляемое.</param>
        /// <returns>Если перечисляемые содержат одинаковые ключевые элементы, то <see langword="true"/>, иначе <see langword="false"/>.</returns>
        public static bool SequenceEqualIgnoreOrderAndDuplicates<TValue>(this IEnumerable<TValue> thisEnumerable, IEnumerable<TValue> otherEnumerable)
        {
            return thisEnumerable.SequenceEqualIgnoreOrderAndDuplicates<object, TValue>(otherEnumerable, null);
        }

        /// <summary>
        /// Проверка на совпадение элементов двух перечисляемых, игнорируя порядок этих элементов и наличие дублей.
        /// <br><i>Для сравнения элементов используется <see cref="HashSet{T}"/>.</i></br>
        /// </summary>
        /// <param name="thisEnumerable">Текущее перечисляемое.</param>
        /// <param name="otherEnumerable">Сравниваемое перечисляемое.</param>
        /// <param name="keySelector">Функция выборки ключа.</param>
        /// <returns>Если перечисляемые содержат одинаковые ключевые элементы, то <see langword="true"/>, иначе <see langword="false"/>.</returns>
        public static bool SequenceEqualIgnoreOrderAndDuplicates<TKey, TValue>(this IEnumerable<TValue> thisEnumerable, IEnumerable<TValue> otherEnumerable, Func<TValue, TKey> keySelector)
        {
            if (thisEnumerable == null && otherEnumerable == null)
                return true;
            if (thisEnumerable == null || otherEnumerable == null)
                return false;

            if (keySelector == null)
            {
                var thisSet = thisEnumerable.ToHashSet();
                var otherSet = otherEnumerable.ToHashSet();

                return thisSet.SetEquals(otherSet);
            }
            else
            {
                var thisSet = thisEnumerable.Select(keySelector).ToHashSet();
                var otherSet = otherEnumerable.Select(keySelector).ToHashSet();

                return thisSet.SetEquals(otherSet);
            }
        }

        /// <summary>
        /// Проверка на совпадение элементов двух перечисляемых, игнорируя порядок этих элементов.
        /// </summary>
        /// <param name="thisEnumerable">Текущее перечисляемое.</param>
        /// <param name="otherEnumerable">Сравниваемое перечисляемое.</param>
        /// <returns>Если перечисляемые содержат одинаковые ключевые элементы, то <see langword="true"/>, иначе <see langword="false"/>.</returns>
        public static bool SequenceEqualIgnoreOrder<TValue>(this IEnumerable<TValue> thisEnumerable, IEnumerable<TValue> otherEnumerable)
        {
            return thisEnumerable.SequenceEqualIgnoreOrder<object, TValue>(otherEnumerable, null);
        }

        /// <summary>
        /// Проверка на совпадение элементов двух перечисляемых, игнорируя порядок этих элементов.
        /// </summary>
        /// <param name="thisEnumerable">Текущее перечисляемое.</param>
        /// <param name="otherEnumerable">Сравниваемое перечисляемое.</param>
        /// <param name="keySelector">Функция выборки ключа.</param>
        /// <returns>Если перечисляемые содержат одинаковые ключевые элементы, то <see langword="true"/>, иначе <see langword="false"/>.</returns>
        public static bool SequenceEqualIgnoreOrder<TKey, TValue>(this IEnumerable<TValue> thisEnumerable, IEnumerable<TValue> otherEnumerable, Func<TValue, TKey> keySelector)
        {
            if (thisEnumerable == null && otherEnumerable == null)
                return true;
            if (thisEnumerable == null || otherEnumerable == null)
                return false;

            if (keySelector == null)
            {
                var thisOrderedEnumerable = thisEnumerable.OrderBy(x => x.GetHashCode());
                var otherOrderedEnumerable = otherEnumerable.OrderBy(x => x.GetHashCode());

                return thisOrderedEnumerable.SequenceEqual(otherOrderedEnumerable);
            }
            else
            {
                var thisOrderedEnumerable = thisEnumerable.Select(keySelector).OrderBy(x => x.GetHashCode());
                var otherOrderedEnumerable = otherEnumerable.Select(keySelector).OrderBy(x => x.GetHashCode());

                return thisOrderedEnumerable.SequenceEqual(otherOrderedEnumerable);
            }
        }

    }
}
