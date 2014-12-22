using System;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Chapter2
{
    /// <summary>
    /// Implement an algorithm to delete a node in the middle of a singly linked list, 
    /// given only access to that node.
    /// </summary>
    public class Q2P3DeleteNodeInMiddleOfLinkedList
    {
        // Won't work for the last node. Try to just clear the node's data when it's the last one.
        public static void Delete<T>(Node<T> node) where T : IEquatable<T>
        {
            if (node.Next != null)
            {
                var next = node.Next;
                // copy data from next node to current
                node.Data = next.Data;
                // delete next node
                node.Next = next.Next;
            }
            else
            {
                node.Data = default(T);
            }
        }

        [TestFixture]
        public class Q2P3DeleteNodeInMiddleOfLinkedListTests
        {
            [Test]
            public void TestDeleteNode()
            {
                var head = new Node<int>(1);
                head.Append(2);
                head.Append(3);
                head.Append(4);
                head.Append(5);

                var nodeToDelete = head.Next.Next;

                Delete(nodeToDelete);

                Assert.AreEqual("1 2 4 5", head.ToString());
            }

            [Test]
            public void TestDeleteLastNode()
            {
                var head = new Node<string>("1");
                head.Append("2");
                head.Append("3");
                head.Append("4");
                head.Append("5");

                var nodeToDelete = head.Next.Next.Next.Next;

                Delete(nodeToDelete);

                Assert.AreEqual("1 2 3 4 ", head.ToString());
            }
        }
    }
}