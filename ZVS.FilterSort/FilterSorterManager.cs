using System.Collections;
using System.Collections.Generic;
using ZVS.Global.Extensions;

namespace ZVS.FilterSort
{
    public class FilterSorterManager<T> : IFilterSorterManager<T>
    {
        private HashSet<IFilterSorter<T>> mFilterSorters;

        public int Count => mFilterSorters.Count;

        public bool IsReadOnly => false;

        public void Add(IFilterSorter<T> filterSorter)
        {
            mFilterSorters.Add(filterSorter);
        }

        public void AddRange(IEnumerable<IFilterSorter<T>> filterSorters)
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

        public bool Contains(IFilterSorter<T> filterSorter)
        {
            return mFilterSorters.Contains(filterSorter);
        }

        public bool Remove(IFilterSorter<T> filterSorter)
        {
            return mFilterSorters.Remove(filterSorter);
        }

        public bool RemoveRange(IEnumerable<IFilterSorter<T>> filterSorters)
        {
            return mFilterSorters.RemoveRange(filterSorters);
        }
    }
}
