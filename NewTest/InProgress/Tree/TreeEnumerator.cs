using System.Collections;
using System.Collections.Generic;

namespace ZVSlibs.InProgress.Tree
{
    internal class TreeEnumerator<T> : IEnumerator<T>
    {
        private Tree<T> tree;

        public TreeEnumerator(Tree<T> tree)
        {
            this.tree = tree;
        }

        public T Current => tree.root.value;

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public bool MoveNext()
        {
            throw new System.NotImplementedException();
        }

        public void Reset()
        {
            tree.current = tree.root;
        }
    }
}