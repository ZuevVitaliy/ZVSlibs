using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZVSlibs.Extensions;

namespace ZVSlibs.InProgress.Tree
{
    public class Tree<T> : IEnumerable<T>
    {
        internal Node<T> root;
        internal Node<T> current;

        public string ExceptionMessage { get; private set; } = "";

        public int Count { get; private set; }

        internal class Node<T>
        {
            public Node<T> root;
            public T value;
            public List<Node<T>> childs;
        }

        public void Add(T value)
        {
            current.childs.Add(new Node<T>
            {
                root = current,
                value = value,
                childs = new List<Node<T>>()
            });
            Count++;
        }

        public bool Select(string indexes)
        {
            string[] ixs = indexes.Split('.');
            return Select(ixs.ParseToInt());
        }

        public bool Select(params int[] indexes)
        {
            current = root;
            Node<T> memory = current;
            try
            {
                foreach (int i in indexes)
                {
                    current = current.childs[i];
                }
                return true;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                ExceptionMessage = ex.Message;
                current = memory;
                return false;
            }
        }

        public bool MoveUp()
        {
            if (current.root != null)
            {
                current = current.root;
                return true;
            }
            else return false;
        }

        public bool MoveDown(int index)
        {
            try
            {
                current = current.childs[index];
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new TreeEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
