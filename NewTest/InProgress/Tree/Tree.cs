using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private int[] PositionArray { get => mPosition.ToArray(); }
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
            Node<T> savedPosition = mCurrent;
            if (MoveTo(indexes))
            {
                T result = mCurrent.value;
                mCurrent = savedPosition;
                return result;
            }
            else throw new ArgumentOutOfRangeException("Элемент по заданному индексу не найден.");
        }

        public T GetAt(params int[] indexes)
        {
            Node<T> savedPosition = mCurrent;
            if (MoveTo(indexes))
            {
                T result = mCurrent.value;
                mCurrent = savedPosition;
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

        public bool MoveDown(int index)
        {
            if (index > mCurrent.childs.Count - 1)
                return false;

            mCurrent = mCurrent.childs[index];
            mPosition.Add(index);
            return true;
        }

        public bool MoveUp()
        {
            if (mCurrent.root == null)
                return false;

            mCurrent = mCurrent.root;
            mPosition.RemoveAt(mPosition.Count - 1);
            return true;
        }

        public bool Remove()
        {
            Node<T> savedPosition = mCurrent;

            if (!MoveUp())
                return false;

            for (int i = 0; i < mCurrent.childs.Count(); i++)
            {
                if (savedPosition == mCurrent.childs[i])
                {
                    mCurrent.childs.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public bool RemoveAt(string indexes)
        {
            int[] indxs = indexes.Split('.').ParseToInt();
            return RemoveAt(indxs);
        }

        public bool RemoveAt(int[] indexes)
        {
            int[] position = PositionArray;
            Node<T> savedPosition = mCurrent;
            bool done;

            if (OnOnePath(position, indexes))
            {
                if (MoveTo(indexes))
                {
                    done = Remove();
                    return done;
                }
                else return false;
            }
            else
            {
                if (MoveTo(indexes))
                {
                    done = Remove();
                    mCurrent = savedPosition;
                    return done;
                }
                else return false;
            }
        }

        public string[] GetIndexesMap()
        {
            List<string> indexes = new List<string>();
            TreeEnumerator<T> enumerator = this.GetEnumerator() as TreeEnumerator<T>;
            while (enumerator.MoveNext())
            {
                indexes.Add(enumerator.Position);
            }
            return indexes.ToArray();
        }

        private bool OnOnePath(int[] path1, int[] path2)
        {
            if (path1.Length > path2.Length)
            {
                for (int i = 0; i < path2.Length; i++)
                {
                    if (path1[i] != path2[i])
                        return false;
                }
            }
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new TreeEnumerator<T>(mRoot);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
