using System;
using Common.BinaryTree;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Chapter4
{
    /// <summary>
    /// Implement a function to check if a binary tree is balanced. For the purposes of
    /// this question, a balanced tree is defined to be a tree such that the heights of the
    /// two subtrees of any node never differ by more than one.
    /// </summary>
    public class Q4P1IsBinaryTreeBalanced
    {
        public static bool IsBalanced<T>(BinaryTreeNode<T> root)
        {
            return GetHeightOfTree(root) != -1;
        }

        public static int GetHeightOfTree<T>(BinaryTreeNode<T> root)
        {
            if (root == null)
            {
                return 0;
            }

            int leftHeight = GetHeightOfTree(root.Left);
            int rightHeight = GetHeightOfTree(root.Right);

            if (leftHeight == -1 || rightHeight == -1 || Math.Abs(leftHeight - rightHeight) > 1)
            {
                return -1;
            }

            return Math.Max(leftHeight, rightHeight) + 1;
        }

        [TestFixture]
        public class Q4P1IsBinaryTreeBalancedTests
        {
            [Test]
            public void IsTreeBalancedTest()
            {
                var root = new BinaryTreeNode<int>();
                root.Left = new BinaryTreeNode<int>();
                root.Right = new BinaryTreeNode<int>();
                root.Left.Left = new BinaryTreeNode<int>();
                root.Left.Right = new BinaryTreeNode<int>();
                root.Right.Left = new BinaryTreeNode<int>();
                root.Right.Right = new BinaryTreeNode<int>();

                root.Print();
                Assert.True(IsBalanced(root));
            }

            [Test]
            public void IsTreeBalancedTest2()
            {
                var root = new BinaryTreeNode<int>();
                root.Left = new BinaryTreeNode<int>();
                root.Right = new BinaryTreeNode<int>();
                root.Left.Left = new BinaryTreeNode<int>();
                root.Left.Right = new BinaryTreeNode<int>();
                root.Right.Left = new BinaryTreeNode<int>();
                root.Right.Left.Left = new BinaryTreeNode<int>();
                Assert.False(IsBalanced(root));
                root.Right.Right = new BinaryTreeNode<int>();
                Assert.True(IsBalanced(root));
            }
        }
    }
}