using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZVS.FilterSort
{
    public interface IFilterSorterManager
    {
        IEnumerable Apply(IEnumerable sourceData);
    }

    public interface IFilterSorterManager<T> : IFilterSorterManager
    {
        void Add(IFilterSorter<T> filterSorter);
        void AddRange(IEnumerable<IFilterSorter<T>> filterSorters);
        void Clear();
        bool Contains(IFilterSorter<T> filterSorter);
        bool Remove(IFilterSorter<T> filterSorter);
        bool RemoveRange(IEnumerable<IFilterSorter<T>> filterSorters);
        IEnumerable<T> Apply(IEnumerable<T> sourceData);
    }
}
