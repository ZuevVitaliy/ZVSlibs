using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using ZVSlibs.Extensions;
using ZVSlibs.InProgress.Tree;

namespace LibTest
{
    [TestClass]
    public class TreeTests
    {
        [TestMethod]
        public void CastTest()
        {
            string[] strings = new string[] { "22", "12", "1", "2", "6" };
            int[] expected = new int[] { 22, 12, 1, 2, 6 };

            int[] numbers = strings.ParseToInt();

            Assert.AreEqual(expected.Length, numbers.Length);
            for (int i = 0; i < numbers.Length; i++)
            {
                Assert.AreEqual(expected[i], numbers[i]);
            }
        }

        [TestMethod]
        public void AddTest()
        {
            Tree<int> tree = new Tree<int>();
            tree.Add(2);
            tree.Add(0);

            Assert.AreEqual(2, tree.Count);
        }

        [TestMethod]
        public void ForeachTest()
        {
            Tree<int> tree = new Tree<int>();
            tree.Add(2);
            tree.Add(0);
            tree.MoveDown(1);
            tree.Add(3);
            tree.Add(4);
            tree.MoveDown(0);
            tree.Add(5);

            Tree<int> tree2 = new Tree<int>();
            foreach (var i in tree2)
            {

            }

            List<int> list = new List<int>();

            foreach (int n in tree)
            {
                list.Add(n);
                Console.Write($"{n} ");
            }
            int[] numbers = new int[] { 2, 0, 3, 5, 4 };

            Assert.AreEqual(list.Count, tree.Count);
            for (int i = 0; i < list.Count; i++)
            {
                Assert.AreEqual(numbers[i], list[i]);
            }
        }
    }
}
