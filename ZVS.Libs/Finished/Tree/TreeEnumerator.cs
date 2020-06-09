using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ZVSlibs.Tree
{
    internal class TreeEnumerator<T> : IEnumerator<T>
    {
        private readonly Tree<T>.TreeNode<T> cRoot;
        private Tree<T>.TreeNode<T> mCurrent;
        private Stack<string> mPosition = new Stack<string>();

        public TreeEnumerator(Tree<T>.TreeNode<T> rootNode)
        {
            this.mCurrent = this.cRoot = rootNode;
        }

        public T Current => mCurrent.value;
        object IEnumerator.Current => Current;
        public string[] Position { get => mPosition.Reverse().ToArray(); }

        public void Dispose()
        { }

        public bool MoveNext()
        {
            if (mCurrent.root == null)
            {
                if (mCurrent.childs.Count > 0)
                {
                    mPosition.Push(mCurrent.childs.First().Key);
                    mCurrent = mCurrent.childs.First().Value;
                    return true;
                }
                else return false;
            }
            else
            {
                if (mCurrent.childs.Count > 0)
                {
                    mPosition.Push(mCurrent.childs.First().Key);
                    mCurrent = mCurrent.childs.First().Value;
                    return true;
                }
                else
                {
                    int i;
                    string key;
                    do
                    {
                        if (!MoveUp())
                        {
                            return false;
                        }
                        else
                        {
                            i = 0;
                            key = mPosition.Pop();
                            foreach (var child in mCurrent.childs)
                            {
                                i++;
                                if (child.Key == key)
                                {
                                    break;
                                }
                            }
                        }
                    } while (mCurrent.childs.Count <= i);

                    foreach (var child in mCurrent.childs)
                    {
                        if (i == 0)
                        {
                            mPosition.Push(child.Key);
                            mCurrent = mCurrent.childs[child.Key];
                            return true;
                        }
                        i--;
                    }
                    return false;
                }
            }
        }

        public void Reset()
        {
            mCurrent = cRoot;
            mPosition.Clear();
        }

        private bool MoveUp()
        {
            if (mCurrent.root == null)
                return false;

            mCurrent = mCurrent.root;
            return true;
        }
    }
}