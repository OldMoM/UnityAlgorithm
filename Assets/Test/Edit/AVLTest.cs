using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class AVLTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void AVL__LeftRotate()
        {
            var tree = new AVLTree(1);
            tree.Insert(2);
            tree.Insert(3);
            tree.Insert(4);
            // 1->2->3

            Debug.Log("左旋前-----------------");
            tree.DFS();

            Debug.Log("左旋后-----------------");
            var unbance_node = tree.Root;
            tree.LL_Rotate(unbance_node);
            //   2
            // 1   3
            tree.DFS();
            Assert.Pass();
        }
        [Test]
        public void AVL_RRRotate()
        {
            var tree = new AVLTree(3);
            tree.Insert(2);
            tree.Insert(1);
            //1<-2<-3
            var unbance_node = tree.Root;
            Debug.Log("右旋前-----------------");
            tree.DFS();
            tree.RR_Rotate(unbance_node);
            Debug.Log("右旋后-----------------");
            tree.DFS();
            Assert.Pass();
        }
        [Test]
        public void AVL_RL_Rotate()
        {
            var tree = new AVLTree(8);
            tree.Insert(4);
            tree.Insert(15);
            tree.Insert(3);
            tree.Insert(6);
            tree.Insert(5);
          
            var unbance_node = tree.Root;

            Debug.Log("LR旋前-----------------");
            tree.DFS();
            Debug.Log("LR旋后-----------------");
            tree.LR_Rotate(unbance_node);
            tree.DFS();
            Assert.Pass();
        }
        [Test]
        public void AVL_LR_Rotate()
        {
            var tree = new AVLTree(8);
            tree.Insert(4);
            tree.Insert(12);
            tree.Insert(10);
            tree.Insert(13);
            tree.Insert(11);

            tree.RL_Rotate(tree.Root);
            tree.DFS();
            Assert.Pass();
        }

        [Test]
        public void AVL_Inset()
        {
            var tree = new AVLTree(8);
            tree.Insert(4);
            tree.Insert(15);
            tree.Insert(3);
            tree.Insert(6);
            tree.Insert(5);
            tree.DFS();
            Assert.Fail();
        }
    }
}
