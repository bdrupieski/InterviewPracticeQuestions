using System;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Chapter2
{
    /// <summary>
    /// Write code to partition a linked list around a value x, such that all nodes less than
    /// x come before all nodes greater than or equal to x.
    /// </summary>
    public class Q2P4PartitionLinkedList
    {
        public static Node<T> PartitionLinkedList<T>(Node<T> head, T partitionValue) where T : IEquatable<T>, IComparable<T>
        {
            Node<T> endOfBeforePartition = null;
            Node<T> beginningOfBeforePartition = null;
            Node<T> endOfAfterPartition = null;
            Node<T> beginningOfAfterPartition = null;

            Node<T> current = head;
            while (current != null)
            {
                var next = current.Next;
                current.Next = null;
                if (current.Data.CompareTo(partitionValue) < 0)
                {
                    if (endOfBeforePartition == null)
                    {
                        endOfBeforePartition = current;
                        beginningOfBeforePartition = endOfBeforePartition;
                    }
                    else
                    {
                        endOfBeforePartition.Next = current;
                        endOfBeforePartition = endOfBeforePartition.Next;
                    }
                }
                else
                {
                    if (endOfAfterPartition == null)
                    {
                        endOfAfterPartition = current;
                        beginningOfAfterPartition = endOfAfterPartition;
                    }
                    else
                    {
                        endOfAfterPartition.Next = current;
                        endOfAfterPartition = endOfAfterPartition.Next;
                    }
                }

                current = next;
            }

            Node<T> partitionedList = null;

            if (beginningOfBeforePartition == null)
            {
                partitionedList = beginningOfAfterPartition;
            }
            else
            {
                partitionedList = beginningOfBeforePartition;

                if (endOfBeforePartition != null)
                {
                    endOfBeforePartition.Next = beginningOfAfterPartition;
                }
            }

            return partitionedList;
        }

        [TestFixture]
        public class Q2P4PartitionLinkedListTests
        {
            [Test]
            public void TestPartitioning()
            {
                var head = new Node<int>(8);
                head.Append(7);
                head.Append(6);
                head.Append(5);
                head.Append(4);
                head.Append(3);
                head.Append(2);
                head.Append(1);

                var partitionedHead = PartitionLinkedList(head, 4);
                Assert.AreEqual("3 2 1 8 7 6 5 4", partitionedHead.ToString());
            }

            [Test]
            public void TestPartitioning2()
            {
                var head = new Node<int>(1);
                head.Append(2);
                head.Append(3);
                head.Append(4);
                head.Append(5);

                var partitionedHead = PartitionLinkedList(head, 6);
                Assert.AreEqual("1 2 3 4 5", partitionedHead.ToString());
            }

            [Test]
            public void TestPartitioning3()
            {
                var head = new Node<int>(1);
                head.Append(2);
                head.Append(3);
                head.Append(4);
                head.Append(5);

                var partitionedHead = PartitionLinkedList(head, 2);
                Assert.AreEqual("1 2 3 4 5", partitionedHead.ToString());
            }

            [Test]
            public void TestPartitioning4()
            {
                var head = new Node<int>(1);
                head.Append(2);
                head.Append(3);
                head.Append(4);
                head.Append(5);

                var partitionedHead = PartitionLinkedList(head, 0);
                Assert.AreEqual("1 2 3 4 5", partitionedHead.ToString());
            }

            [Test]
            public void TestPartitioning5()
            {
                var head = new Node<int>(5);
                head.Append(7);
                head.Append(0);
                head.Append(3);
                head.Append(6);
                head.Append(4);
                head.Append(8);

                var partitionedHead = PartitionLinkedList(head, 5);
                Assert.AreEqual("0 3 4 5 7 6 8", partitionedHead.ToString());
            }
        }
    }
}