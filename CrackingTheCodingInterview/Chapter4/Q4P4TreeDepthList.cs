using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.BinaryTree;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Chapter4
{
    /// <summary>
    /// Given a binary tree, design an algorithm which creates a list of all the
    /// nodes at each depth (e.g., if you have a tree with depth D, you'll have D lists).
    /// </summary>
    public class Q4P4TreeDepthList
    {
        public static string BuildTreeLayerRepresentation<T>(BinaryTree<T> tree) where T : IComparable<T>
        {
            var sb = new StringBuilder();

            var nodeLayers = GetTreeLayers(tree);
            foreach (var nodeLayer in nodeLayers)
            {
                sb.AppendLine(string.Join(" ", nodeLayer.Select(x => x.Data)));
            }

            return sb.ToString();
        }

        private static List<List<BinaryTreeNode<T>>> GetTreeLayers<T>(BinaryTree<T> tree) where T : IComparable<T>
        {
            var layers = new List<List<BinaryTreeNode<T>>>();

            if (tree.Root != null)
            {
                var layer = new List<BinaryTreeNode<T>> { tree.Root };
                layers.Add(layer);

                while (layer.Any())
                {
                    layer = GetNextLayer(layer);
                    layers.Add(layer);
                }
            }

            return layers;
        }

        private static List<BinaryTreeNode<T>> GetNextLayer<T>(IEnumerable<BinaryTreeNode<T>> topLayer)
            where T : IComparable<T>
        {
            var layer = new List<BinaryTreeNode<T>>();

            foreach (var node in topLayer)
            {
                if (node.Left != null)
                {
                    layer.Add(node.Left);
                }
                if (node.Right != null)
                {
                    layer.Add(node.Right);
                }
            }

            return layer;
        }

        [TestFixture]
        public class Q4P4TreeDepthListTests
        {
            [Test]
            public void SmallTree()
            {
                var tree = new BinaryTree<int>();

                tree.Insert(5);
                tree.Insert(2);
                tree.Insert(3);
                tree.Insert(7);
                tree.Insert(6);

                string actualTree = BuildTreeLayerRepresentation(tree);
                Console.WriteLine(actualTree);

                string expectedTree =
                    "5" + Environment.NewLine +
                    "2 7" + Environment.NewLine +
                    "3 6" + Environment.NewLine + Environment.NewLine;
                Assert.AreEqual(expectedTree, actualTree);
            }

            [Test]
            public void TwoLongListsOnBothSides()
            {
                var tree = new BinaryTree<int>();

                tree.Insert(5);

                tree.Insert(6);
                tree.Insert(7);
                tree.Insert(8);
                tree.Insert(9);

                tree.Insert(4);
                tree.Insert(3);
                tree.Insert(2);
                tree.Insert(1);

                string actualTree = BuildTreeLayerRepresentation(tree);
                Console.WriteLine(actualTree);

                string expectedTree =
                    "5" + Environment.NewLine +
                    "4 6" + Environment.NewLine +
                    "3 7" + Environment.NewLine +
                    "2 8" + Environment.NewLine +
                    "1 9" + Environment.NewLine + Environment.NewLine;
                Assert.AreEqual(expectedTree, actualTree);
            }

            [Test]
            public void Uneven()
            {
                var tree = new BinaryTree<int>();

                tree.Insert(10);

                tree.Insert(8);

                tree.Insert(18);
                tree.Insert(16);
                tree.Insert(19);
                tree.Insert(29);

                string actualTree = BuildTreeLayerRepresentation(tree);
                Console.WriteLine(actualTree);

                string expectedTree =
                    "10" + Environment.NewLine +
                    "8 18" + Environment.NewLine +
                    "16 19" + Environment.NewLine +
                    "29" + Environment.NewLine + Environment.NewLine;
                Assert.AreEqual(expectedTree, actualTree);
            }
        }
    }
}