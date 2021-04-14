using System.Collections;
using System.Collections.Generic;
using ZVS.Global.Extensions;

namespace ZVS.FilterSort
{
    public class FilterSorterManager<T> : IFilterSorterManager<T>
    {
        private HashSet<IFilterSorterDescriptor<T>> mFilterSorters;

        public int Count => mFilterSorters.Count;

        public bool IsReadOnly => false;

        public void Add(IFilterSorterDescriptor<T> filterSorterDescriptor)
        {
            mFilterSorters.Add(filterSorterDescriptor);
        }

        public void AddRange(IEnumerable<IFilterSorterDescriptor<T>> filterSorters)
        {
            mFilterSorters.AddRange(filterSorters);
        }

        public IEnumerable<T> Apply(IEnumerable<T> sourceData)
        {
            if (sourceData == null)
            {
                System.Diagnostics.Debug.WriteLine("Source data is null");
                return null;
            }
            var result = sourceData;
            foreach (var filterSorter in mFilterSorters)
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
            return mFilterSorters.Contains(filterSorterDescriptor);
        }

        public bool Remove(IFilterSorterDescriptor<T> filterSorterDescriptor)
        {
            return mFilterSorters.Remove(filterSorterDescriptor);
        }

        public bool RemoveRange(IEnumerable<IFilterSorterDescriptor<T>> filterSorters)
        {
            return mFilterSorters.RemoveRange(filterSorters);
        }

        public IEnumerator<IFilterSorterDescriptor<T>> GetEnumerator()
        {
            return mFilterSorters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}