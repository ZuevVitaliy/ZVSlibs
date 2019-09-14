using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ZVSlibs.Tree
{
    /// <summary>
    /// Структура данных, хранящая значения в виде логического дерева.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Tree<T> : IEnumerable<T>
    {
        internal readonly TreeNode<T> cRoot;
        internal TreeNode<T> mCurrent;
        internal Stack<string> mPosition = new Stack<string>();

        /// <summary>
        /// Ctor
        /// </summary>
        public Tree()
        {
            this.cRoot = new TreeNode<T>()
            {
                childs = new Dictionary<string, TreeNode<T>>()
            };
            this.mCurrent = cRoot;
        }

        /// <summary>
        /// Возвращает значение элемента по цепочке индексов.
        /// </summary>
        /// <param name="indexesChain">Цепочка индексов.</param>
        /// <returns>Значение элемента.</returns>
        public T this[params string[] indexesChain]
        {
            get
            {
                if (indexesChain == null || indexesChain.Length == 0)
                {
                    throw new ArgumentNullException("Значение индекса не может быть пустым.");
                }

                return GetAt(indexesChain);
            }
            set
            {
                if (indexesChain == null || indexesChain.Length == 0)
                {
                    throw new ArgumentNullException("Значение индекса не может быть пустым.");
                }

                SetTo(indexesChain, value);
            }
        }

        /// <summary>
        /// Возвращает экземпляр перечеслителя класса <see cref="TreeEnumerator{T}"/>.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new TreeEnumerator<T>(cRoot);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Возвращает карту цепочек индексов в качестве массива.
        /// </summary>
        /// <returns>Карта цепочек индексов.</returns>
        public KeyValuePair<string[], T>[] GetIndexesMap()
        {
            List<KeyValuePair<string[], T>> indexes = new List<KeyValuePair<string[], T>>();
            TreeEnumerator<T> enumerator = this.GetEnumerator() as TreeEnumerator<T>;
            while (enumerator.MoveNext())
            {
                indexes.Add(new KeyValuePair<string[], T>(enumerator.Position, enumerator.Current));
            }
            return indexes.ToArray();
        }

        /// <summary>
        /// Удалить звено по индексу (нижние индексы также удалятся).
        /// </summary>
        /// <param name="indexesChain">Индекс удаляемого значения.</param>
        /// <returns>Успех операции.</returns>
        public bool RemoveAt(string indexesChain)
        {
            string[] indxs = indexesChain.Split('.');
            return RemoveAt(indxs);
        }

        /// <summary>
        /// Удалить звено по индексу (нижние индексы также удалятся).
        /// </summary>
        /// <param name="indexesChain">Индекс удаляемого значения.</param>
        /// <returns>Успех операции.</returns>
        public bool RemoveAt(params string[] indexesChain)
        {
            Reset();
            if (MoveTo(indexesChain))
            {
                bool done = Remove();
                Reset();
                return done;
            }
            else return false;
        }

        private T GetAt(string[] indexes)
        {
            Reset();
            if (MoveTo(indexes))
            {
                return mCurrent.value;
            }
            else throw new ArgumentOutOfRangeException("Элемент по заданному индексу не найден.");
        }

        private Stack<string> GetPositionFrom(TreeNode<T> current)
        {
            Stack<string> buf = new Stack<string>();
            TreeNode<T> lowerNode;
            while (current.root != null)
            {
                lowerNode = current;
                current = current.root;
                int count = current.childs.Count();
                foreach (var child in current.childs)
                {
                    if (child.Value == lowerNode)
                    {
                        buf.Push(child.Key);
                        break;
                    }
                }
            }
            Stack<string> result = new Stack<string>(buf.Count);
            for (int i = 0; i < buf.Count; i++)
            {
                result.Push(buf.Pop());
            }
            return result;
        }

        private bool MoveDown(string key)
        {
            foreach (var child in mCurrent.childs)
            {
                if (child.Key == key)
                {
                    mCurrent = mCurrent.childs[key];
                    mPosition.Push(key);
                    return true;
                }
            }
            return false;
        }

        private bool MoveTo(string[] indexes)
        {
            Reset();
            foreach (var i in indexes)
            {
                if (!MoveDown(i))
                {
                    return false;
                }
            }
            return true;
        }

        private bool MoveUp()
        {
            if (mCurrent.root == null)
            {
                return false;
            }
            else
            {
                mCurrent = mCurrent.root;
                mPosition.Pop();
                return true;
            }
        }

        private bool Remove()
        {
            TreeNode<T> savedPosition = mCurrent;

            if (!MoveUp())
            {
                return false;
            }

            foreach (var child in mCurrent.childs)
            {
                if (child.Value == savedPosition)
                {
                    mCurrent.childs.Remove(child.Key);
                    return true;
                }
            }
            return false;
        }

        private void Reset()
        {
            mCurrent = cRoot;
            mPosition = new Stack<string>();
        }

        private void SetTo(string[] indexesChain, T value)
        {
            Reset();
            for (int i = 0; i < indexesChain.Length; i++)
            {
                if (!MoveDown(indexesChain[i]))
                {
                    mCurrent.childs.Add(indexesChain[i], new TreeNode<T>
                    {
                        root = mCurrent,
                        childs = new Dictionary<string, TreeNode<T>>()
                    });
                    if (indexesChain.Length - 1 == i)
                    {
                        mCurrent.childs[indexesChain[i]].value = value;
                        break;
                    }
                    MoveDown(indexesChain[i]);
                }
                else if (i == indexesChain.Length - 1)
                {
                    mCurrent.value = value;
                }
            }
        }

        internal class TreeNode<T>
        {
            public Dictionary<string, TreeNode<T>> childs;
            public TreeNode<T> root;
            public T value;
        }
    }
}