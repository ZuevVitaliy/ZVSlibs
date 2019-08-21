using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZVSlibs.Extensions;
using ZVSlibs.InProgress.Tree;
using System.Net.Http;

namespace LibTest
{
    [TestClass]
    public class TreeTests
    {
        [TestMethod]
        public void Test()
        {
            //получить всех наследников от класса
            //var classes = typeof(HttpContent).Assembly.ExportedTypes.Where(t => typeof(HttpContent).IsAssignableFrom(t));

            Tree<int?> tree = new Tree<int?>();
            tree["bla", "bla", "bla"] = null;
        }

        [TestMethod]
        public void SetToGetAt_Test()
        {
            Tree<int> tree = new Tree<int>();
            tree["what", "is", "love"] = 1;
            tree["baby", "don't", "hurt", "me"] = 2;
            tree["no", "more"] = 3;
            Assert.AreEqual(1, tree["what", "is", "love"]);
            Assert.AreEqual(2, tree["baby", "don't", "hurt", "me"]);
            Assert.AreEqual(3, tree["no", "more"]);
        }

        [TestMethod]
        public void RemoveAt_Test()
        {
            Tree<int> tree = new Tree<int>();
            tree["what", "is", "love"] = 1;
            tree["baby", "don't", "hurt", "me"] = 2;
            tree["no", "more"] = 3;
            tree.RemoveAt("baby", "don't", "hurt", "me");
            tree.RemoveAt("what.is.love");
            Assert.AreEqual(7, tree.Count());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                int a = tree["what", "is", "love"];
            });
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                int a = tree["baby", "don't", "hurt", "me"];
            });
        }

        [TestMethod]
        public void ReplaceAt_Test()
        {
            Tree<int> tree = new Tree<int>();
            tree["what", "is", "love"] = 1;
            tree["baby", "don't", "hurt", "me"] = 2;
            tree["no", "more"] = 3;
            tree["what", "is", "love"] = 4;
            tree["baby", "don't", "hurt", "me"] = 5;
            tree["no", "more"] = 6;
            Assert.AreEqual(4, tree["what", "is", "love"]);
            Assert.AreEqual(5, tree["baby", "don't", "hurt", "me"]);
            Assert.AreEqual(6, tree["no", "more"]);
        }

        [TestMethod]
        public void GetIndexesMap_Test()
        {
            Tree<int> tree = new Tree<int>();
            tree["what", "is", "love"] = 1;
            tree["baby", "don't", "hurt", "me"] = 2;
            tree["no", "more"] = 3;
            KeyValuePair<string[], int>[] map = tree.GetIndexesMap();
            Assert.IsTrue(new string[] { "what" }.SequenceEqual(map[0].Key));
            Assert.IsTrue(new string[] { "what", "is" }.SequenceEqual(map[1].Key));
            Assert.IsTrue(new string[] { "what", "is", "love" }.SequenceEqual(map[2].Key));
            Assert.IsTrue(new string[] { "baby" }.SequenceEqual(map[3].Key));
            Assert.IsTrue(new string[] { "baby", "don't" }.SequenceEqual(map[4].Key));
            Assert.IsTrue(new string[] { "baby", "don't", "hurt" }.SequenceEqual(map[5].Key));
            Assert.IsTrue(new string[] { "baby", "don't", "hurt", "me" }.SequenceEqual(map[6].Key));
            Assert.IsTrue(new string[] { "no" }.SequenceEqual(map[7].Key));
            Assert.IsTrue(new string[] { "no", "more" }.SequenceEqual(map[8].Key));
        }

        [TestMethod]
        public void Foreach_Test()
        {
            Tree<int> tree = new Tree<int>();
            tree["what", "is", "love"] = 25;
            Assert.AreEqual(3, tree.Count());
            tree["baby", "don't", "hurt", "me"] = 5;
            Assert.AreEqual(7, tree.Count());
            tree["no", "more"] = 3;
            Assert.AreEqual(9, tree.Count());
        }
    }
}
