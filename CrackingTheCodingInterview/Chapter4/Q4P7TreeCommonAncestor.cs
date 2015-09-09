using Common.BinaryTree;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Chapter4
{
    /// <summary>
    /// Design an algorithm and write code to find the first common ancestor of two
    /// nodes in a binary tree. Avoid storing additional nodes in a data structure. NOTE:
    /// This is not necessarily a binary search tree.
    /// </summary>
    public class Q4P7TreeCommonAncestor
    {
        public static BinaryTreeNode<T> FindCommonAncestor<T>(BinaryTreeNode<T> root, BinaryTreeNode<T> a, BinaryTreeNode<T> b)
        {
            if (root == null)
            {
                return null;
            }

            if (a == root || b == root)
            {
                return root;
            }

            bool aOnLeft = ContainsNode(root.Left, a);
            bool bOnLeft = ContainsNode(root.Left, b);

            if (aOnLeft != bOnLeft)
            {
                return root;
            }

            var nodeToSearch = aOnLeft && bOnLeft ? root.Left : root.Right;
            return FindCommonAncestor(nodeToSearch, a, b);
        }

        public static bool ContainsNode<T>(BinaryTreeNode<T> root, BinaryTreeNode<T> node)
        {
            if (root == null)
            {
                return false;
            }

            if (node == root)
            {
                return true;
            }

            return ContainsNode(root.Left, node) || ContainsNode(root.Right, node);
        }

        [TestFixture]
        public class Q4P7TreeCommonAncestorTests
        {
            [Test]
            public void CommonAncestorTest()
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

                var a = tree.Root.Right.Left;
                var b = tree.Root.Right.Right;
                var ancestor = FindCommonAncestor(tree.Root, a, b);
                Assert.AreSame(tree.Root.Right, ancestor);
            }
        }
    }
}