using System.Collections;
using System.Collections.Generic;
using static ZVSlibs.InProgress.Tree.Tree<object>;

namespace ZVSlibs.InProgress.Tree
{
    internal class TreeEnumerator<T> : IEnumerator<T>
    {
        private Tree<T> tree;
        private Tree<T>.Node<T> current;

        public TreeEnumerator(Tree<T> tree)
        {
            this.tree = tree;
            this.current = tree.root;
        }

        public T Current => current.value;

        object IEnumerator.Current => Current;

        public void Dispose()
        { }

        private Stack<int> path = new Stack<int>();
        public bool MoveNext()
        {
            if (current.root == null)
            {
                if (current.childs.Count > 0)
                {
                    path.Push(0);
                    current = current.childs[0];
                    return true;
                }
                else return false;
            }
            else
            {
                if (current.childs.Count > 0)
                {
                    path.Push(0);
                    current = current.childs[0];
                    return true;
                }
                else
                {
                    int i;
                    do
                    {
                        if (current.root == null)
                            return false;

                        current = current.root;
                        i = path.Pop();
                        i++;
                    } while (current.childs.Count <= i);
                    path.Push(i);
                    current = current.childs[i];
                    return true;
                }
            }
        }

        public void Reset()
        {
            current = tree.root;
        }
    }
}