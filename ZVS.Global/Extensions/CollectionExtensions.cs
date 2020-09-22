using System;
using System.Collections;
using System.Collections.Generic;

namespace ZVS.Global.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Добавить в коллекцию вхождения из другой коллекции.
        /// </summary>
        /// <typeparam name="T">Тип коллекции.</typeparam>
        /// <param name="collection">Исходная коллекция.</param>
        /// <param name="otherEnumerable">Добавляемые вхождения.</param>
        /// <returns>Если вхождения добавились, то true, иначе false (обычно это если или принимаемая, или передаваемая коллекция null)</returns>
        public static bool AddRange<T>(this ICollection<T> collection, IEnumerable<T> otherEnumerable)
        {
            if (collection != null && otherEnumerable != null)
            {
                foreach (var item in otherEnumerable)
                {
                    collection.Add(item);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Удаляет из коллекции все совпадющие вхождения из другой коллекции по ссылке.
        /// </summary>
        /// <typeparam name="T">Тип членов коллекции.</typeparam>
        /// <param name="collection">Исходная коллекция.</param>
        /// <param name="otherEnumerable">Удаляемые вхождения.</param>
        /// <returns>Если удалились все вхождения, то true, иначе false.</returns>
        public static bool RemoveRange<T>(this ICollection<T> collection, IEnumerable<T> otherEnumerable)
        {
            if (collection != null && otherEnumerable != null)
            {
                bool result = true;
                foreach (var item in otherEnumerable)
                {
                    if (!collection.Remove(item)) result = false;
                }

                return result;
            }
            return false;
        }

        /// <summary>
        /// Копирование текущей коллекции (Создание копии коллекции с другой ссылкой, но не элементов).
        /// </summary>
        /// <typeparam name="T">Тип элементов коллекции.</typeparam>
        /// <param name="collection">Копируемая коллекция.</param>
        /// <returns>Коллекция того же типа, но с другой ссылкой.</returns>
        public static ICollection<T> CloneCollection<T>(this ICollection<T> collection)
        {
            var result = (ICollection<T>)Activator.CreateInstance(collection.GetType());
            result.AddRange(collection);
            return result;
        }

        /// <summary>
        /// Абсолютное копирование текущей коллекции (Создание копии коллекции с другой ссылкой, включая элементы).
        /// </summary>
        /// <typeparam name="T">Тип элементов коллекции.</typeparam>
        /// <param name="collection">Копируемая коллекция</param>
        /// <returns>Коллекция того же типа, но с другой ссылкой как самой коллекции, так и каждого элемента.</returns>
        public static ICollection<T> CloneCollectionAbsolute<T>(this ICollection<T> collection) where T : ICloneable
        {
            var result = (ICollection<T>)Activator.CreateInstance(collection.GetType());
            foreach (var item in collection)
            {
                result.Add((T)item.Clone());
            }
            return result;
        }

        /// <summary>
        /// Проверяет перечисление на наличие ссылки или элементов в ней.
        /// </summary>
        /// <param name="target">Исходное перечисление.</param>
        /// <returns>Если коллекция пустая или не имеет ссылки, то false, иначе true.</returns>
        public static bool IsNullOrEmpty(this IEnumerable target)
        {
            if (target == null) return true;
            IEnumerator enumerator = target.GetEnumerator();
            enumerator.Reset();
            return !enumerator.MoveNext();
        }

        /// <summary>
        /// Выборка уникальных значений по заданному ключу.<br/>
        /// <i>Если необходим составной ключ, то воспользуйтесь анонимным типом (new { key1, key2 })</i><br/>
        /// <b>!Важно: используется отложенный итератор!</b>
        /// </summary>
        /// <typeparam name="TSource">Тип элементов перечисления.</typeparam>
        /// <typeparam name="TKey">Тип ключа.</typeparam>
        /// <param name="source">Элементы исходного перечисления.</param>
        /// <param name="keySelector">Функция извлечения ключа элемента.</param>
        /// <returns>Новое перечисление с уникальными значениями по заданному ключу.</returns>
        public static IEnumerable<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        /// <summary>
        /// Удаление дубликатов значений из коллекции по заданному ключу.<br/>
        /// <i>Если необходим составной ключ, то воспользуйтесь анонимным типом (new { key1, key2 })</i>
        /// </summary>
        /// <typeparam name="TSource">Тип элементов коллекции.</typeparam>
        /// <typeparam name="TKey">Тип ключа.</typeparam>
        /// <param name="source">Элементы коллекции.</param>
        /// <param name="keySelector">Функция извлечения ключа элемента.</param>
        public static void Distinct<TSource, TKey>(this ICollection<TSource> source, Func<TSource, TKey> keySelector)
        {
            List<TSource> deletingElements = new List<TSource>(source.Count / 2);
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (var element in source)
            {
                if (!seenKeys.Add(keySelector(element)))
                {
                    deletingElements.Add(element);
                }
            }

            foreach (var element in deletingElements)
            {
                source.Remove(element);
            }
        }

        /// <summary>
        /// Объединение двух наборов уникальными значениями по заданному ключу.<br/>
        /// <i>Если необходим составной ключ, то воспользуйтесь анонимным типом (new { key1, key2 })</i><br/>
        /// <b>!Важно: используется отложенный итератор!</b>
        /// </summary>
        /// <typeparam name="TSource">Тип элементов перечисления.</typeparam>
        /// <typeparam name="TKey">Тип ключа.</typeparam>
        /// <param name="source">Элементы исходного перечисления.</param>
        /// <param name="second">Элементы второго перечисления.</param>
        /// <param name="keySelector">Функция извлечения ключа элемента.</param>
        /// <returns>Новое объединенное перечисление с уникальными значениями по заданному ключу.</returns>
        public static IEnumerable<TSource> Union<TSource, TKey>(this IEnumerable<TSource> source,
            IEnumerable<TSource> second, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
            foreach (TSource element in second)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        /// <summary>
        /// Объединение уникальных значений в текущую коллекцию по заданному ключу.<br/>
        /// <i>Если необходим составной ключ, то воспользуйтесь анонимным типом (new { key1, key2 })</i>
        /// </summary>
        /// <typeparam name="TSource">Тип элементов коллекции.</typeparam>
        /// <typeparam name="TKey">Тип ключа.</typeparam>
        /// <param name="source">Элементы исходной коллекции.</param>
        /// <param name="second">Элементы второго перечисления.</param>
        /// <param name="keySelector">Функция извлечения ключа элемента.</param>
        public static void Union<TSource, TKey>(this ICollection<TSource> source,
            IEnumerable<TSource> second, Func<TSource, TKey> keySelector)
        {
            List<TSource> deletingElements = new List<TSource>(source.Count / 2);
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (var element in source)
            {
                if (!seenKeys.Add(keySelector(element)))
                {
                    deletingElements.Add(element);
                }
            }
            foreach (var element in deletingElements)
            {
                source.Remove(element);
            }
            foreach (var element in second)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    source.Add(element);
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
        public static void AllAny<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate,
            out bool all, out bool any)
        {
            if (source.IsNullOrEmpty())
            {
                all = false;
                any = false;
                return;
            }

            all = true;
            any = false;
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
