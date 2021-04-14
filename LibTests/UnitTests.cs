using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ZVS.Global.Extensions;

namespace LibTests
{
    [TestClass]
    public class UnitTests
    {
        private struct People_TestStruct
        {
            public int Age { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        [TestMethod]
        public void TestMethod()
        {
            string numberString = "1001";
            int number = int.Parse(numberString);
        }

        [TestMethod]
        public void WhereAnyOrAll_ExceptTest()
        {
            var originalList = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var deletingItems = new List<int> { 3, 5, 7 };
            var expectedList = new List<int> { 0, 1, 2, 4, 6, 8, 9 };

            var resultsDictionary = new Dictionary<string, IEnumerable<int>>();
            resultsDictionary.Add("whereAnyEqual", originalList.Where(x => deletingItems.Any(y => x == y)));
            resultsDictionary.Add("whereAnyNotEqual", originalList.Where(x => deletingItems.Any(y => x != y)));
            resultsDictionary.Add("whereAllEqual", originalList.Where(x => deletingItems.All(y => x == y)));
            resultsDictionary.Add("whereAllNotEqual", originalList.Where(x => deletingItems.All(y => x != y)));

            var result = resultsDictionary.Where(x => x.Value.SequenceEqual(expectedList));
            Assert.IsTrue(result.Count() == 1);
            Assert.AreEqual("whereAllNotEqual", result.First().Key);
        }

        [TestMethod]
        public void ExtensionCloneCollection_Test()
        {
            var testCollection = new HashSet<string>
            {
                "string1",
                "string2",
                "string3"
            };
            var clonedCollection = testCollection.CloneCollection();

            Assert.IsTrue(testCollection.GetType() == clonedCollection.GetType());
            Assert.IsFalse(testCollection == clonedCollection);
            Assert.IsTrue(testCollection.SequenceEqual(clonedCollection));
        }

        [TestMethod]
        public void ExtensionRemoveRangeInCollection_Test()
        {
            var testCollection = new HashSet<string>
            {
                "string1",
                "string2",
                "string3"
            };
            var removedCollection = new HashSet<string>
            {
                "string1",
                "string3"
            };
            var result = testCollection.RemoveRange(removedCollection);

            Assert.IsTrue(result);
            Assert.AreEqual(1, testCollection.Count);
            Assert.IsTrue(testCollection.Contains("string2"));
        }

        [TestMethod]
        public void ExtensionCollecionIsNullOrEmpty_Test()
        {
            ArrayList emptyList = null;
            Assert.IsTrue(emptyList.IsNullOrEmpty());
            emptyList = new ArrayList();
            Assert.IsTrue(emptyList.IsNullOrEmpty());
        }
    }
}