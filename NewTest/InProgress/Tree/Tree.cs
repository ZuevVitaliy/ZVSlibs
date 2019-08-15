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
        internal Node<T> mRoot;
        internal Node<T> mCurrent;
        internal List<int> mPosition = new List<int>();

        public int Count { get; private set; }
        public string Position
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var i in mPosition)
                {
                    sb.Append($"{i}.");
                }
                if (sb.Length > 0)
                    sb.Remove(sb.Length - 1, 1);
                return sb.ToString();
            }
        }

        public Tree()
        {
            this.mRoot = new Node<T>()
            {
                childs = new List<Node<T>>()
            };
            this.mCurrent = mRoot;
            this.Count = 0;
        }

        internal class Node<T>
        {
            public Node<T> root;
            public T value;
            public List<Node<T>> childs;
        }

        public void Add(T value)
        {
            mCurrent.childs.Add(new Node<T>
            {
                root = mCurrent,
                value = value,
                childs = new List<Node<T>>()
            });
            Count++;
        }

        public T Get()
        {
            if (mCurrent.root == null)
                throw new NullReferenceException("Значение в корневом элементе отсутствует.");
            return mCurrent.value;
        }

        public T GetAt(string indexes)
        {
            Node<T> saveNode = mCurrent;
            if (MoveTo(indexes))
            {
                T result = mCurrent.value;
                mCurrent = saveNode;
                return result;
            }
            else throw new ArgumentOutOfRangeException("Элемент по заданному индексу не найден.");
        }

        public T GetAt(params int[] indexes)
        {
            Node<T> saveNode = mCurrent;
            if (MoveTo(indexes))
            {
                T result = mCurrent.value;
                mCurrent = saveNode;
                return result;
            }
            else throw new ArgumentOutOfRangeException("Элемент по заданному индексу не найден.");
        }

        public bool MoveTo(string indexes)
        {
            int[] indxs = indexes.Split('.').ParseToInt();
            return MoveTo(indxs);
        }


        public bool MoveTo(params int[] indexes)
        {
            Node<T> buf = mCurrent;
            mCurrent = mRoot;
            foreach (var i in indexes)
            {
                if (!MoveDown(i))
                {
                    mCurrent = buf;
                    return false;
                }
            }
            mPosition = indexes.ToList();
            return true;
        }

        public bool MoveUp()
        {
            if (mCurrent.root != null)
            {
                mCurrent = mCurrent.root;
                mPosition.RemoveAt(mPosition.Count - 1);
                return true;
            }
            else return false;
        }

        public bool MoveDown(int index)
        {
            try
            {
                mCurrent = mCurrent.childs[index];
                mPosition.Add(index);
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
