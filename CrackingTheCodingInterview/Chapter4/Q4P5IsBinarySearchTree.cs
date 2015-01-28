using System;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Chapter4
{
    /// <summary>
    /// Implement a function to check if a binary tree is a binary search tree.
    /// </summary>
    public class Q4P5IsBinarySearchTree
    {
        public static bool IsBinarySearchTree(BinaryTreeNode<int> root, ref int? lastValue) 
        {
            if (root == null)
            {
                return true;
            }

            if (!IsBinarySearchTree(root.Left, ref lastValue))
            {
                return false;
            }

            if (root.Data <= lastValue)
            {
                return false;
            }
            lastValue = root.Data;

            if (!IsBinarySearchTree(root.Right, ref lastValue))
            {
                return false;
            }

            return true;
        }

        [TestFixture]
        public class Q4P5IsBinarySearchTreeTests
        {
            [Test]
            public void IsBinarySearchTreeTest()
            {
                var tree = new BinaryTree<int>();
                tree.Insert(5);
                tree.Insert(3);
                tree.Insert(7);
                tree.Insert(4);
                tree.Insert(2);

                tree.Root.Print();
                int? val = null;
                Assert.IsTrue(IsBinarySearchTree(tree.Root, ref val));
            }
        }

        [Test]
        public void IsBinarySearchTreeTest()
        {
            var tree = new BinaryTree<int>();
            tree.Insert(5);
            tree.Insert(10);
            tree.Insert(7);
            tree.Insert(13);
            tree.Insert(20);
            tree.Insert(3);
            tree.Insert(2);
            tree.Insert(6);

            tree.Root.Print();
            int? val = null;
            Assert.IsTrue(IsBinarySearchTree(tree.Root, ref val));

            tree.Root.Left.Left.Left = new BinaryTreeNode<int> { Data = 5 };
            tree.Root.Print();

            val = null;
            Assert.IsFalse(IsBinarySearchTree(tree.Root, ref val));
        }
    }
}