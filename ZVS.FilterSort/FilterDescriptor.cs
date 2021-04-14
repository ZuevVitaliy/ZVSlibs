using System;
using System.Collections.Generic;
using System.Linq;

namespace ZVS.FilterSort
{
    public class FilterDescriptor<T> : IFilterSorterDescriptor<T>
    {
        private readonly Func<T, bool> mFilterFunction;

        public FilterDescriptor(Func<T, bool> filterFunction)
        {
            mFilterFunction = filterFunction;
        }

        public string Expression => mFilterFunction.ToString();

        public IEnumerable<T> Apply(IEnumerable<T> sourceData)
        {
            return sourceData.Where(mFilterFunction);
        }
    }
}