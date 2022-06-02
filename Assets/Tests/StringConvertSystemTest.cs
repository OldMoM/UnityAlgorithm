using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class StringConvertSystemTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void TestConvertToIntArray()
        {
            var input = "5+4+6";
            var output = StringConvertSystem.ConvertToIntArray(input);

            Assert.AreEqual(output[0], 5);
            Assert.AreEqual(output[1], 4);
            Assert.AreEqual(output[2], 6);
        }
    }
}
