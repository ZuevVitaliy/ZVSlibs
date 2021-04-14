using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ZVS.FilterSort
{
    public class SorterDescriptor<T, TKey> : IFilterSorterDescriptor<T> where TKey : struct
    {
        private readonly Func<T, TKey> mSorterFunction;
        private readonly SortOrder mSortOrder;

        public SorterDescriptor(Func<T, TKey> sorterFunction, SortOrder sortOrder = SortOrder.Unspecified)
        {
            mSorterFunction = sorterFunction;
            mSortOrder = sortOrder == SortOrder.Unspecified ? SortOrder.Ascending : sortOrder;
        }

        public string Expression => mSorterFunction.ToString();

        public IEnumerable<T> Apply(IEnumerable<T> sourceData)
        {
            if (sourceData is IOrderedEnumerable<T> orderedSource)
                return mSortOrder == SortOrder.Ascending 
                    ? orderedSource.ThenBy(mSorterFunction) 
                    : orderedSource.ThenByDescending(mSorterFunction);

            return mSortOrder == SortOrder.Ascending 
                ? sourceData.OrderBy(mSorterFunction) 
                : sourceData.OrderByDescending(mSorterFunction);
        }
    }
}