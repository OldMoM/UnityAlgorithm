using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TreeTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void Insert()
        {
            var tree = new BinaryTree(15);
            tree.Insert(10);
            tree.Insert(20);
            tree.Insert(25);
            tree.Insert(19);
            //10
            //    20
            //   19 25 
            Assert.AreEqual(tree.Root.right.left.value, 19);
        }
        [Test]
        public void Max()
        {
            var array = new int[8]
            {
                1,22,20,7,15,10,18,6,
            };
            var tree = new BinaryTree(array);
            Assert.AreEqual(tree.Max(), 22);

 
        }
    }
}
