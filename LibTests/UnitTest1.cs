using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using ZVS.Global.Extensions;

namespace LibTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var bools1 = new List<bool>
            {
                true,
                false,
                true
            };
            var bools2 = new List<bool>
            {
                true,
                true,
                true
            };

            bool result1 = bools1.All(x => x);
            bool result2 = bools2.All(x => x);
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
    }       
}
