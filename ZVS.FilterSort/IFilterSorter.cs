using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZVS.FilterSort
{
    public interface IFilterSorter<T>
    {
        IEnumerable<T> Apply(IEnumerable<T> sourceData);
    }
}
