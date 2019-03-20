﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZVSlibs.Sorting
{
    public static class Sorter
    {
        private static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        /// <summary>
        /// Сортировка методом "пузырька".
        /// </summary>
        /// <typeparam name="T">Тип массива.</typeparam>
        /// <param name="array">Массив для сортировки.</param>
        /// <param name="ascending">если <c>true</c>, то сортировка по возрастанию.</param>
        public static void BubbleSorter<T>(T[] array, bool ascending) where T : IComparable<T>
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (ascending)
                    {
                        if (array[i].CompareTo(array[j]) > 0)
                            Swap(ref array[i], ref array[j]);
                    }

                    else
                    {
                        if (array[i].CompareTo(array[j]) < 0)
                            Swap(ref array[i], ref array[j]);
                    }
                }
            }
        }

        /// <summary>
        /// Быстрая сортировка.
        /// </summary>
        /// <typeparam name="T">Тип массива.</typeparam>
        /// <param name="array">Массив для сортировки.</param>
        /// <param name="ascending">если <c>true</c>, то сортировка по возрастанию.</param>
        public static void QuickSorter<T>(T[] array, bool ascending) where T : IComparable<T>
        {
            int partition(T[] m, int a, int b)
            {
                int wall = a;
                T pivot = m[b];
                for (int j = a; j <= b; j++)
                {
                    if (ascending)
                    {
                        if (m[j].CompareTo(pivot) <= 0)
                        {
                            Swap(ref m[wall], ref m[j]);
                            wall++;
                        }
                    }
                    else
                    {
                        if (m[j].CompareTo(pivot) >= 0)
                        {
                            Swap(ref m[wall], ref m[j]);
                            wall++;
                        }
                    }
                }
                return wall--;
            }
            void quicksort(T[] m, int a, int b)
            {
                if (a >= b) return;
                int c = partition(m, a, b);
                quicksort(m, a, c - 1);
                quicksort(m, c + 1, b);
            }
            quicksort(array, 0, array.Length - 1);
        }
    }
}
