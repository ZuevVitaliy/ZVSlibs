using System;
using System.Collections.Generic;

namespace ZVS.Global.Comparers
{
    /// <summary>
    /// Компаратор на основе ключей.
    /// </summary>
    public class KeyComparer<TElement, TKey> : IEqualityComparer<TElement>
    {
        private readonly Func<TElement, TKey> _keyProvider;
        
        public KeyComparer(Func<TElement, TKey> keyProvider)
        {
            _keyProvider = keyProvider;
        }

        public bool Equals(TElement first, TElement second)
        {
            var firstProp = _keyProvider(first);
            var secondProp = _keyProvider(second);

            return EqualityComparer<TKey>.Default.Equals(firstProp, secondProp);
        }

        public int GetHashCode(TElement obj)
        {
            return _keyProvider(obj).GetHashCode();
        }
    }
}
