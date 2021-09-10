using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZVS.Global.Extensions
{
    public static class GroupingExtensions
    {
        /// <summary>
        /// Вложенная группировка по нескольким ключам.
        /// </summary>
        public static IEnumerable<IGrouping<TKey1, IGrouping<TKey2, TElement>>>
            GroupByMany<TKey1, TKey2, TElement>(
            this IEnumerable<TElement> elements,
            Func<TElement, TKey1> key1Selector,
            Func<TElement, TKey2> key2Selector
            )
        {
            return elements.GroupBy(key1Selector).SelectMany(gr1 =>
                gr1.GroupBy(key2Selector)
                    .GroupBy(gr2 => gr1.Key, gr2 => gr2));
        }

        /// <summary>
        /// Вложенная группировка по нескольким ключам.
        /// </summary>
        public static IEnumerable<IGrouping<TKey1, IGrouping<TKey2, IGrouping<TKey3, TElement>>>>
            GroupByMany<TKey1, TKey2, TKey3, TElement>(
            this IEnumerable<TElement> elements,
            Func<TElement, TKey1> key1Selector,
            Func<TElement, TKey2> key2Selector,
            Func<TElement, TKey3> key3Selector
            )
        {
            return elements.GroupBy(key1Selector).SelectMany(gr1 =>
                gr1.GroupBy(key2Selector).SelectMany(gr2 =>
                        gr2.GroupBy(key3Selector)
                            .GroupBy(gr3 => gr2.Key, gr3 => gr3))
                    .GroupBy(gr2 => gr1.Key, gr2 => gr2));
        }

        /// <summary>
        /// Вложенная группировка по нескольким ключам.
        /// </summary>
        public static IEnumerable<IGrouping<TKey1, IGrouping<TKey2, IGrouping<TKey3, IGrouping<TKey4, TElement>>>>>
            GroupByMany<TKey1, TKey2, TKey3, TKey4, TElement>(
            this IEnumerable<TElement> elements,
            Func<TElement, TKey1> key1Selector,
            Func<TElement, TKey2> key2Selector,
            Func<TElement, TKey3> key3Selector,
            Func<TElement, TKey4> key4Selector
            )
        {
            return elements.GroupBy(key1Selector).SelectMany(gr1 =>
                gr1.GroupBy(key2Selector).SelectMany(gr2 =>
                        gr2.GroupBy(key3Selector).SelectMany(gr3 =>
                                gr3.GroupBy(key4Selector)
                                    .GroupBy(gr4 => gr3.Key, gr4 => gr4))
                            .GroupBy(gr3 => gr2.Key, gr3 => gr3))
                    .GroupBy(gr2 => gr1.Key, gr2 => gr2));
        }

        /// <summary>
        /// Вложенная группировка по нескольким ключам.
        /// </summary>
        public static IEnumerable<IGrouping<TKey1, IGrouping<TKey2, IGrouping<TKey3, IGrouping<TKey4, IGrouping<TKey5, TElement>>>>>>
            GroupByMany<TKey1, TKey2, TKey3, TKey4, TKey5, TElement>(
            this IEnumerable<TElement> elements,
            Func<TElement, TKey1> key1Selector,
            Func<TElement, TKey2> key2Selector,
            Func<TElement, TKey3> key3Selector,
            Func<TElement, TKey4> key4Selector,
            Func<TElement, TKey5> key5Selector
            )
        {
            return elements.GroupBy(key1Selector).SelectMany(gr1 =>
                gr1.GroupBy(key2Selector).SelectMany(gr2 =>
                        gr2.GroupBy(key3Selector).SelectMany(gr3 =>
                                gr3.GroupBy(key4Selector).SelectMany(gr4 =>
                                        gr4.GroupBy(key5Selector)
                                            .GroupBy(gr5 => gr4.Key, gr5 => gr5))
                                    .GroupBy(gr4 => gr3.Key, gr4 => gr4))
                            .GroupBy(gr3 => gr2.Key, gr3 => gr3))
                    .GroupBy(gr2 => gr1.Key, gr2 => gr2));
        }

        /// <summary>
        /// Вложенная группировка по нескольким ключам.
        /// </summary>
        public static IEnumerable<IGrouping<TKey1, IGrouping<TKey2, IGrouping<TKey3, IGrouping<TKey4, IGrouping<TKey5, IGrouping<TKey6, TElement>>>>>>>
            GroupByMany<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TElement>(
            this IEnumerable<TElement> elements,
            Func<TElement, TKey1> key1Selector,
            Func<TElement, TKey2> key2Selector,
            Func<TElement, TKey3> key3Selector,
            Func<TElement, TKey4> key4Selector,
            Func<TElement, TKey5> key5Selector,
            Func<TElement, TKey6> key6Selector
        )
        {
            return elements.GroupBy(key1Selector).SelectMany(gr1 =>
                gr1.GroupBy(key2Selector).SelectMany(gr2 =>
                        gr2.GroupBy(key3Selector).SelectMany(gr3 =>
                                gr3.GroupBy(key4Selector).SelectMany(gr4 =>
                                        gr4.GroupBy(key5Selector).SelectMany(gr5 =>
                                                gr5.GroupBy(key6Selector)
                                                    .GroupBy(gr6 => gr5.Key, gr6 => gr6))
                                            .GroupBy(gr5 => gr4.Key, gr5 => gr5))
                                    .GroupBy(gr4 => gr3.Key, gr4 => gr4))
                            .GroupBy(gr3 => gr2.Key, gr3 => gr3))
                    .GroupBy(gr2 => gr1.Key, gr2 => gr2));
        }

        /// <summary>
        /// Вложенная группировка по нескольким ключам.
        /// </summary>
        public static IEnumerable<IGrouping<TKey1, IGrouping<TKey2, IGrouping<TKey3, IGrouping<TKey4, IGrouping<TKey5, IGrouping<TKey6, IGrouping<TKey7, TElement>>>>>>>>
            GroupByMany<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TElement>(
                this IEnumerable<TElement> elements,
                Func<TElement, TKey1> key1Selector,
                Func<TElement, TKey2> key2Selector,
                Func<TElement, TKey3> key3Selector,
                Func<TElement, TKey4> key4Selector,
                Func<TElement, TKey5> key5Selector,
                Func<TElement, TKey6> key6Selector,
                Func<TElement, TKey7> key7Selector
            )
        {
            return elements.GroupBy(key1Selector).SelectMany(gr1 =>
                gr1.GroupBy(key2Selector).SelectMany(gr2 =>
                        gr2.GroupBy(key3Selector).SelectMany(gr3 =>
                                gr3.GroupBy(key4Selector).SelectMany(gr4 =>
                                        gr4.GroupBy(key5Selector).SelectMany(gr5 =>
                                                gr5.GroupBy(key6Selector).SelectMany(gr6 =>
                                                        gr6.GroupBy(key7Selector)
                                                            .GroupBy(gr7 => gr6.Key, gr7 => gr7))
                                                    .GroupBy(gr6 => gr5.Key, gr6 => gr6))
                                            .GroupBy(gr5 => gr4.Key, gr5 => gr5))
                                    .GroupBy(gr4 => gr3.Key, gr4 => gr4))
                            .GroupBy(gr3 => gr2.Key, gr3 => gr3))
                    .GroupBy(gr2 => gr1.Key, gr2 => gr2));
        }

        /// <summary>
        /// Вложенная группировка по нескольким ключам.
        /// </summary>
        public static IEnumerable<IGrouping<TKey1, IGrouping<TKey2, IGrouping<TKey3, IGrouping<TKey4, IGrouping<TKey5, IGrouping<TKey6, IGrouping<TKey7, IGrouping<TKey8, TElement>>>>>>>>>
            GroupByMany<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8, TElement>(
                this IEnumerable<TElement> elements,
                Func<TElement, TKey1> key1Selector,
                Func<TElement, TKey2> key2Selector,
                Func<TElement, TKey3> key3Selector,
                Func<TElement, TKey4> key4Selector,
                Func<TElement, TKey5> key5Selector,
                Func<TElement, TKey6> key6Selector,
                Func<TElement, TKey7> key7Selector,
                Func<TElement, TKey8> key8Selector
            )
        {
            return elements.GroupBy(key1Selector).SelectMany(gr1 =>
                gr1.GroupBy(key2Selector).SelectMany(gr2 =>
                        gr2.GroupBy(key3Selector).SelectMany(gr3 =>
                                gr3.GroupBy(key4Selector).SelectMany(gr4 =>
                                        gr4.GroupBy(key5Selector).SelectMany(gr5 =>
                                                gr5.GroupBy(key6Selector).SelectMany(gr6 =>
                                                        gr6.GroupBy(key7Selector).SelectMany(gr7 =>
                                                                gr7.GroupBy(key8Selector)
                                                                    .GroupBy(gr8 => gr7.Key, gr8 => gr8))
                                                            .GroupBy(gr7 => gr6.Key, gr7 => gr7))
                                                    .GroupBy(gr6 => gr5.Key, gr6 => gr6))
                                            .GroupBy(gr5 => gr4.Key, gr5 => gr5))
                                    .GroupBy(gr4 => gr3.Key, gr4 => gr4))
                            .GroupBy(gr3 => gr2.Key, gr3 => gr3))
                    .GroupBy(gr2 => gr1.Key, gr2 => gr2));
        }

        /// <summary>
        /// Вложенная группировка по нескольким ключам.
        /// </summary>
        public static IEnumerable<IGrouping<TKey1, IGrouping<TKey2, IGrouping<TKey3, IGrouping<TKey4, IGrouping<TKey5, IGrouping<TKey6, IGrouping<TKey7, IGrouping<TKey8, IGrouping<TKey9, TElement>>>>>>>>>>
            GroupByMany<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8, TKey9, TElement>(
               this IEnumerable<TElement> elements,
               Func<TElement, TKey1> key1Selector,
               Func<TElement, TKey2> key2Selector,
               Func<TElement, TKey3> key3Selector,
               Func<TElement, TKey4> key4Selector,
               Func<TElement, TKey5> key5Selector,
               Func<TElement, TKey6> key6Selector,
               Func<TElement, TKey7> key7Selector,
               Func<TElement, TKey8> key8Selector,
               Func<TElement, TKey9> key9Selector
           )
        {
            return elements.GroupBy(key1Selector).SelectMany(gr1 =>
                gr1.GroupBy(key2Selector).SelectMany(gr2 =>
                        gr2.GroupBy(key3Selector).SelectMany(gr3 =>
                                gr3.GroupBy(key4Selector).SelectMany(gr4 =>
                                        gr4.GroupBy(key5Selector).SelectMany(gr5 =>
                                                gr5.GroupBy(key6Selector).SelectMany(gr6 =>
                                                        gr6.GroupBy(key7Selector).SelectMany(gr7 =>
                                                                gr7.GroupBy(key8Selector).SelectMany(gr8 =>
                                                                        gr8.GroupBy(key9Selector)
                                                                            .GroupBy(gr9 => gr8.Key, gr9 => gr9))
                                                                    .GroupBy(gr8 => gr7.Key, gr8 => gr8))
                                                            .GroupBy(gr7 => gr6.Key, gr7 => gr7))
                                                    .GroupBy(gr6 => gr5.Key, gr6 => gr6))
                                            .GroupBy(gr5 => gr4.Key, gr5 => gr5))
                                    .GroupBy(gr4 => gr3.Key, gr4 => gr4))
                            .GroupBy(gr3 => gr2.Key, gr3 => gr3))
                    .GroupBy(gr2 => gr1.Key, gr2 => gr2));
        }

        /// <summary>
        /// Вложенная группировка по нескольким ключам.
        /// </summary>
        public static IEnumerable<IGrouping<TKey1, IGrouping<TKey2, IGrouping<TKey3, IGrouping<TKey4, IGrouping<TKey5, IGrouping<TKey6, IGrouping<TKey7, IGrouping<TKey8, IGrouping<TKey9, IGrouping<TKey10, TElement>>>>>>>>>>> 
            GroupByMany<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8, TKey9, TKey10, TElement>(
              this IEnumerable<TElement> elements,
              Func<TElement, TKey1> key1Selector,
              Func<TElement, TKey2> key2Selector,
              Func<TElement, TKey3> key3Selector,
              Func<TElement, TKey4> key4Selector,
              Func<TElement, TKey5> key5Selector,
              Func<TElement, TKey6> key6Selector,
              Func<TElement, TKey7> key7Selector,
              Func<TElement, TKey8> key8Selector,
              Func<TElement, TKey9> key9Selector,
              Func<TElement, TKey10> key10Selector
          )
        {
            return elements.GroupBy(key1Selector).SelectMany(gr1 =>
                gr1.GroupBy(key2Selector).SelectMany(gr2 =>
                        gr2.GroupBy(key3Selector).SelectMany(gr3 =>
                                gr3.GroupBy(key4Selector).SelectMany(gr4 =>
                                        gr4.GroupBy(key5Selector).SelectMany(gr5 =>
                                                gr5.GroupBy(key6Selector).SelectMany(gr6 =>
                                                        gr6.GroupBy(key7Selector).SelectMany(gr7 =>
                                                                gr7.GroupBy(key8Selector).SelectMany(gr8 =>
                                                                        gr8.GroupBy(key9Selector).SelectMany(gr9 =>
                                                                                gr9.GroupBy(key10Selector)
                                                                                    .GroupBy(gr10 => gr9.Key, gr10 => gr10))
                                                                            .GroupBy(gr9 => gr8.Key, gr9 => gr9))
                                                                    .GroupBy(gr8 => gr7.Key, gr8 => gr8))
                                                            .GroupBy(gr7 => gr6.Key, gr7 => gr7))
                                                    .GroupBy(gr6 => gr5.Key, gr6 => gr6))
                                            .GroupBy(gr5 => gr4.Key, gr5 => gr5))
                                    .GroupBy(gr4 => gr3.Key, gr4 => gr4))
                            .GroupBy(gr3 => gr2.Key, gr3 => gr3))
                    .GroupBy(gr2 => gr1.Key, gr2 => gr2));
        }
    }
}
