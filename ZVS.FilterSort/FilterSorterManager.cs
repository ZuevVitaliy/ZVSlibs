using System;
using System.Collections;
using System.Collections.Generic;

namespace ZVS.FilterSort
{
    public class FilterSorterManager<T> : IFilterSorterManager<T>
    {
        private Dictionary<string, IFilterSorterDescriptor<T>> mFilterSorters;

        public int Count => mFilterSorters.Count;

        public bool IsReadOnly => false;

        public void Add(IFilterSorterDescriptor<T> filterSorterDescriptor)
        {
            mFilterSorters.Add(filterSorterDescriptor.Expression, filterSorterDescriptor);
        }

        public void AddRange(IEnumerable<IFilterSorterDescriptor<T>> filterSorters)
        {
            foreach (var filterSorterDescriptor in filterSorters)
            {
                if (!mFilterSorters.ContainsKey(filterSorterDescriptor.Expression))
                    mFilterSorters.Add(filterSorterDescriptor.Expression, filterSorterDescriptor);
            }
        }

        public IEnumerable<T> Apply(IEnumerable<T> sourceData)
        {
            if (sourceData == null)
                throw new NullReferenceException("Source data is null");

            var result = sourceData;
            foreach (var filterSorter in mFilterSorters.Values)
            {
                result = filterSorter.Apply(result);
            }
            return result;
        }

        public IEnumerable Apply(IEnumerable sourceData)
        {
            return Apply(sourceData as IEnumerable<T>);
        }

        public void Clear()
        {
            mFilterSorters.Clear();
        }

        public bool Contains(IFilterSorterDescriptor<T> filterSorterDescriptor)
        {
            return filterSorterDescriptor != null && mFilterSorters.ContainsKey(filterSorterDescriptor.Expression);
        }

        public void CopyTo(IFilterSorterDescriptor<T>[] array, int arrayIndex)
        {
            mFilterSorters.Values.CopyTo(array, arrayIndex);
        }

        public bool Remove(IFilterSorterDescriptor<T> filterSorterDescriptor)
        {
            return filterSorterDescriptor != null && mFilterSorters.Remove(filterSorterDescriptor.Expression);
        }

        public bool RemoveRange(IEnumerable<IFilterSorterDescriptor<T>> filterSorters)
        {
            bool hasAllRemoved = true;
            foreach (var filterSorterDescriptor in filterSorters)
            {
                if (!mFilterSorters.Remove(filterSorterDescriptor.Expression))
                    hasAllRemoved = false;
            }

            return hasAllRemoved;
        }

        public IEnumerator<IFilterSorterDescriptor<T>> GetEnumerator()
        {
            return mFilterSorters.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}