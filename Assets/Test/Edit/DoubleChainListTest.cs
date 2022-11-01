using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class DoubleChainListTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void DoubleChainList_ADD()
        {
            var double_list = new DoubleChainList<int>();
            for (int i = 5; i < 10; i++)
            {
                double_list.Add(i);
            }
            Assert.AreEqual(5, double_list.Count);
        }
        [Test]
        public void DoubleChainList_Index_Start()
        {
            var double_list = new DoubleChainList<int>();
            for (int i = 1; i < 20; i++)
            {
                double_list.Add(i);
            }
            Assert.AreEqual(0, double_list[0]);
        }
        [Test]
        public void DoubleChainList_Index_End()
        {
            var double_list = new DoubleChainList<int>();
            for (int i = 1; i < 20; i++)
            {
                double_list.Add(i);
            }
            Assert.AreEqual(19, double_list[19]);
        }
        [Test]
        public void DoubleChainList_ForwardSearch()
        {
            var double_list = new DoubleChainList<int>();
            for (int i = 1; i < 20; i++)
            {
                double_list.Add(i);
            }
            Assert.AreEqual(2, double_list[2]);
        }
        [Test]
        public void DoubleChainList_BackwardSearch()
        {
            var double_list = new DoubleChainList<int>();
            for (int i = 0; i < 20; i++)
            {
                double_list.Add(i);
            }
            Assert.AreEqual(17, double_list[18]);
        }
    }
}
