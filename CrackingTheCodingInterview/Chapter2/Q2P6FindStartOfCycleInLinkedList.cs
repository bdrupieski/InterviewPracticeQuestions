using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Chapter2
{
    /// <summary>
    /// Given a circular linked list, implement an algorithm which returns the node at
    /// the beginning of the loop.
    /// 
    /// DEFINITION
    /// Circular linked list: A (corrupt) linked list in which a node's next pointer points
    /// to an earlier node, so as to make a loop in the linked list.
    /// 
    /// EXAMPLE
    /// Input: A - > B - > C - > D - > E - > C [the same C as earlier]
    /// Output: C
    /// </summary>
    public class Q2P6FindStartOfCycleInLinkedList
    {
        public static Node<T> FindStartOfCycle<T>(Node<T> head) where T : IEquatable<T>
        {
            var nodesEncountered = new HashSet<Node<T>>(EqualityComparer<object>.Default);

            var current = head;
            while (current != null)
            {
                if (nodesEncountered.Contains(current))
                {
                    return current;
                }

                nodesEncountered.Add(current);

                current = current.Next;
            }

            return null;
        }

        [TestFixture]
        public class Q2P6FindStartOfCycleInLinkedListTests
        {
            [Test]
            public void FindStartOfCycleTest()
            {
                var head = new Node<int>(0);
                head.Append(1);
                head.Append(2);
                head.Append(3);
                head.Append(4);
                head.Append(5);
                head.Append(6);
                head.Append(7);
                head.Append(8);

                head.Next.Next.Next.Next.Next.Next.Next.Next = head.Next.Next.Next; // 8 to 3

                Assert.AreEqual(3, FindStartOfCycle(head).Data);
            }

            [Test]
            public void FindStartOfCycleTest2()
            {
                var head = new Node<int>(0);
                head.Append(1);

                head.Next = head;

                Assert.AreEqual(0, FindStartOfCycle(head).Data);
            }

            [Test]
            public void FindStartOfCycleTest3()
            {
                var head = new Node<int>(0);
                head.Append(1);
                head.Append(2);
                head.Append(3);
                head.Append(4);

                head.Next.Next.Next.Next = head;

                Assert.AreEqual(0, FindStartOfCycle(head).Data);
            }

            [Test]
            public void FindStartOfCycleTest4()
            {
                var head = new Node<string>("0");
                head.Append("1");
                head.Append("2");
                head.Append("3");
                head.Append("4");

                head.Next.Next.Next.Next = head;

                Assert.AreEqual("0", FindStartOfCycle(head).Data);
            }
        }
    }
}