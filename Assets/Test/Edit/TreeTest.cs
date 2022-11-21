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
        [Test]
        public void DFS(){
            var tree = new BinaryTree(15);
            tree.Insert(10);
            tree.Insert(5);
            tree.Insert(20);
            tree.Insert(25);
            tree.Insert(19);
            tree.DFS();
            Assert.Pass();
        }
        [Test]
        public void GetHeight()
        {
             //       10 
             //   5       15
             //4     8
             //    7   9
             //   6
            var tree = new BinaryTree(10);
            tree.Insert(5);
            tree.Insert(15);
            tree.Insert(4);
            tree.Insert(8);
            tree.Insert(7);
            tree.Insert(9);
            tree.Insert(6);

            Assert.AreEqual(4, tree.Height);
        }
    }
}
