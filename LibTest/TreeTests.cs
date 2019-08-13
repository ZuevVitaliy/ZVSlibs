using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using ZVSlibs.Extensions;


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
    }
}
