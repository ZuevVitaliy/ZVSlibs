using System;
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
                var newCollection = collection.CloneCollection();
                foreach (var item in otherEnumerable)
                {
                    if (!newCollection.Remove(item)) result = false;
                }
                collection.Clear();
                collection.AddRange(newCollection);

                return result;
            }
            return false;
        }

        /// <summary>
        /// Копирование текущей коллекции (Создание копии коллекции с другой ссылкой, но не элементов).
        /// </summary>
        /// <typeparam name="T">Тип коллекции.</typeparam>
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
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static ICollection<T> CloneCollectionAbsolute<T>(this ICollection<T> collection) where T : class, ICloneable
        {
            var result = (ICollection<T>)Activator.CreateInstance(collection.GetType());
            foreach (var item in collection)
            {
                result.Add(item.Clone() as T);
            }
            return result;
        }
    }
}
