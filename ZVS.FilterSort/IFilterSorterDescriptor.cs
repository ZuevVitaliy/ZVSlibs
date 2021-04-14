using System.Collections.Generic;

namespace ZVS.FilterSort
{
    public interface IFilterSorterDescriptor<T>
    {
        string Expression { get; }

        IEnumerable<T> Apply(IEnumerable<T> sourceData);
    }
}