using System;
using System.Collections.Generic;
using System.Linq;
using Common.BinaryTree;
using NUnit.Framework;

namespace RandomlyEncounteredQuestions
{
    public class TreeTraversal
    {
        public static IEnumerable<T> PreOrder<T>(BinaryTreeNode<T> root)
        {
            if (root == null)
                yield break;

            yield return root.Data;

            foreach (var l in PreOrder(root.Left))
            {
                yield return l;
            }
            foreach (var r in PreOrder(root.Right))
            {
                yield return r;
            }
        }

        public static IEnumerable<T> InOrder<T>(BinaryTreeNode<T> root)
        {
            if (root == null)
                yield break;

            foreach (var l in InOrder(root.Left))
            {
                yield return l;
            }

            yield return root.Data;

            foreach (var r in InOrder(root.Right))
            {
                yield return r;
            }
        }

        public static IEnumerable<T> PostOrder<T>(BinaryTreeNode<T> root)
        {
            if (root == null)
                yield break;

            foreach (var l in PostOrder(root.Left))
            {
                yield return l;
            }
            foreach (var r in PostOrder(root.Right))
            {
                yield return r;
            }

            yield return root.Data;
        }

        [Test]
        public void PreOrderTest()
        {
            TestTraversal(PreOrder, new[] { 5, 3, 2, 10, 7, 6, 13, 20 });
        }

        [Test]
        public void InOrderTest()
        {
            TestTraversal(InOrder, new[] { 2, 3, 5, 6, 7, 10, 13, 20 });
        }

        [Test]
        public void PostOrderTest()
        {
            TestTraversal(PostOrder, new[] { 2, 3, 6, 7, 20, 13, 10, 5 });
        }

        private void TestTraversal(Func<BinaryTreeNode<int>, IEnumerable<int>> traversal, IEnumerable<int> expectedTraversalSequence)
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

            IEnumerable<int> actualTraversalSeq = traversal(tree.Root).ToList();

            CollectionAssert.AreEqual(expectedTraversalSequence, actualTraversalSeq);
        }
    }
}