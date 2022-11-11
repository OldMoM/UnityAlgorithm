using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ChainListTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void ChainListTest_Count()
        {
            // SingleList<int> list = new SingleList<int>(79);
            // list.Add(78).Add(77).Add(76).Add(75);
            // Assert.AreEqual(5, list.Count);
        }
        [Test]
        public void ChainListTest_Indexer()
        {
            // SingleList<int> list = new SingleList<int>(79);
            // list.Add(78).Add(77).Add(76).Add(75);
            // Assert.AreEqual(77, list[2]);
        }
        [Test]
        public void ChainListTest_Insert()
        {
            // SingleList<int> list = new SingleList<int>(79);
            // list.Add(78).Add(76).Add(75);

            // list.Insert(2, 77);
            // Assert.AreEqual(77, list[2]);
        }
        [Test]
        public void ChainListTest_RemoveAt()
        {
            // SingleList<int> list = new SingleList<int>(79);
            // list.Add(78).Add(76).Add(75);

            // list.RemoveAt(2);
            // Assert.AreEqual(75, list[2]);
        }
    }
}
