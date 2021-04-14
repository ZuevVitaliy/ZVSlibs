using System.Collections;
using System.Collections.Generic;

namespace ZVS.FilterSort
{
    public interface IFilterSorterManager<T> : IEnumerable<IFilterSorterDescriptor<T>>
    {
        void Add(IFilterSorterDescriptor<T> filterSorterDescriptor);

        void AddRange(IEnumerable<IFilterSorterDescriptor<T>> filterSorters);

        void Clear();

        bool Contains(IFilterSorterDescriptor<T> filterSorterDescriptor);

        bool Remove(IFilterSorterDescriptor<T> filterSorterDescriptor);

        bool RemoveRange(IEnumerable<IFilterSorterDescriptor<T>> filterSorters);

        IEnumerable<T> Apply(IEnumerable<T> sourceData);
    }
}