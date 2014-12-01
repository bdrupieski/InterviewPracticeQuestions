using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CodingForInterviews
{
    /// <summary>
    /// Given a binary tree of integers, print it in level order. 
    /// The output will contain space between the numbers in the same level, and new line between different levels.
    /// </summary>
    public class LevelOrderTreePrint
    {
        public static void PrintTreeInLevelOrder<T>(BinaryTree<T> tree) where T : IComparable<T>
        {
            var welp = GetLevelOrderTreeLayers(tree);

            foreach (var nodes in welp)
            {
                Console.WriteLine(string.Join(" ", nodes.Select(x => x.Data)));
            }
        }

        private static List<List<BinaryTree<T>.Node<T>>> GetLevelOrderTreeLayers<T>(BinaryTree<T> tree) where T : IComparable<T>
        {
            var layers = new List<List<BinaryTree<T>.Node<T>>>();

            if (tree.Root != null)
            {
                var layer = new List<BinaryTree<T>.Node<T>> { tree.Root };
                layers.Add(layer);

                while (layer.Any())
                {
                    layer = GetNextLayer(layer);
                    layers.Add(layer);
                }
            }

            return layers;
        }

        private static List<BinaryTree<T>.Node<T>> GetNextLayer<T>(IEnumerable<BinaryTree<T>.Node<T>> topLayer)
            where T : IComparable<T>
        {
            var layer = new List<BinaryTree<T>.Node<T>>();

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
        public class Tests
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

                PrintTreeInLevelOrder(tree);
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
                
                PrintTreeInLevelOrder(tree);
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

                PrintTreeInLevelOrder(tree);
            }
        }
    }

    public class BinaryTree<T> where T : IComparable<T>
    {
        public Node<T> Root { get; private set; }

        public void Insert(T data)
        {
            if (Root == null)
            {
                Root = new Node<T>(data);
            }
            else
            {
                Insert(Root, data);
            }
        }

        private static void Insert(Node<T> subtreeRoot, T data)
        {
            if (data.CompareTo(subtreeRoot.Data) < 0)
            {
                if (subtreeRoot.Left == null)
                {
                    subtreeRoot.Left = new Node<T>(data);
                }
                else
                {
                    Insert(subtreeRoot.Left, data);
                }
            }
            else if (data.CompareTo(subtreeRoot.Data) > 0)
            {
                if (subtreeRoot.Right == null)
                {
                    subtreeRoot.Right = new Node<T>(data);
                }
                else
                {
                    Insert(subtreeRoot.Right, data);
                }
            }
        }

        public class Node<TData>
        {
            public Node(TData data)
            {
                Data = data;
            }

            public TData Data { get; private set; }

            public Node<TData> Left { get; set; }
            public Node<TData> Right { get; set; }
        }
    }
}