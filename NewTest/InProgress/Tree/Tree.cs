using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZVSlibs.Extensions;

namespace ZVSlibs.InProgress.Tree
{
    public class Tree<T> : IEnumerable<T?> where T : struct
    {
        internal Node<T> root;
        internal Node<T> current;

        public string ExceptionMessage { get; private set; }

        public int Count { get; private set; }

        public Tree()
        {
            this.root = new Node<T>()
            {
                childs = new List<Node<T>>()
            };
            this.current = root;
            this.Count = 0;
            this.ExceptionMessage = "";
        }

        internal class Node<T> where T : struct
        {
            public Node<T> root;
            public T? value;
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

        public T? Get()
        {
            return current.value;
        }

        public T? GetAt(string indexes)
        {
            if (MoveTo(indexes))
                return current.value;
            else throw new ArgumentOutOfRangeException("элемент по заданному индексу не найден");
        }

        public T? GetAt(params int[] indexes)
        {
            if (MoveTo(indexes))
                return current.value;
            else throw new ArgumentOutOfRangeException("элемент по заданному индексу не найден");
        }

        public bool MoveTo(string indexes)
        {
            int[] indxs = indexes.Split('.').ParseToInt();
            return MoveTo(indxs);
        }

        public bool MoveTo(params int[] indexes)
        {
            foreach (var i in indexes)
            {
                if (!MoveDown(i))
                    return false;
            }
            return true;
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

        public IEnumerator<T?> GetEnumerator()
        {
            return new TreeEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
