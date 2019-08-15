using NewTest.InProgress.Tree;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ZVSlibs.InProgress.Tree
{
    internal class TreeEnumerator<T> : IEnumerator<T>
    {
        private readonly Tree<T>.Node<T> cRoot;
        private Tree<T>.Node<T> current;
        private StringBuilder positionBuilder = new StringBuilder();
        public string Position { get => positionBuilder.ToString(); }

        public TreeEnumerator(Tree<T>.Node<T> rootNode)
        {
            this.current = this.cRoot = rootNode;
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
                    positionBuilder.Append("0.");
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
                    positionBuilder.Append("0.");
                    return true;
                }
                else
                {
                    int i;
                    do
                    {
                        if (!MoveUp())
                            return false;

                        positionBuilder.RemoveLastIndex();
                        i = path.Pop();
                        i++;
                    } while (current.childs.Count <= i);

                    path.Push(i);
                    current = current.childs[i];
                    positionBuilder.Append($"{i}.");
                    return true;
                }
            }
        }

        private bool MoveUp()
        {
            if (current.root == null)
                return false;

            current = current.root;
            return true;
        }

        public void Reset()
        {
            current = cRoot;
            positionBuilder = new StringBuilder();
        }
    }
}