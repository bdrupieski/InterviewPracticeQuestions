using System;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Chapter2
{
    public class Node<T> where T : IEquatable<T>
    {
        public Node(T data)
        {
            Data = data;
        }

        public T Data { get; internal set; }
        public Node<T> Next { get; internal set; }

        public void Append(T data)
        {
            Node<T> current = this;
            while (current.Next != null)
            {
                current = current.Next;
            }

            current.Next = new Node<T>(data);
        }

        public static Node<T> DeleteNode(Node<T> head, T data)
        {
            if (head.Data.Equals(data))
            {
                return head.Next;
            }

            var current = head;
            while (current.Next != null)
            {
                if (current.Next.Data.Equals(data))
                {
                    current.Next = current.Next.Next;
                    return head;
                }

                current = current.Next;
            }

            return head;
        }

        public override string ToString()
        {
            if (Next == null)
            {
                return Data != null ? Data.ToString() : string.Empty;
            }
            else
            {
                return string.Format("{0} {1}", Data, Next);
            }
        }
    }

    [TestFixture]
    public class NodeTests
    {
        [Test]
        public void AppendAndDeleteTest()
        {
            var head = new Node<int>(1);
            Assert.AreEqual("1", head.ToString());

            head.Append(2);
            Assert.AreEqual("1 2", head.ToString());

            head = Node<int>.DeleteNode(head, 2);
            Assert.AreEqual("1", head.ToString());

            head.Append(5);
            Assert.AreEqual("1 5", head.ToString());

            head.Append(4);
            Assert.AreEqual("1 5 4", head.ToString());

            head = Node<int>.DeleteNode(head, 1);
            Assert.AreEqual("5 4", head.ToString());

            head.Append(1);
            head.Append(2);
            head.Append(3);
            head.Append(4);
            Assert.AreEqual("5 4 1 2 3 4", head.ToString());

            head = Node<int>.DeleteNode(head, 3);
            head = Node<int>.DeleteNode(head, 5);
            head = Node<int>.DeleteNode(head, 4);
            Assert.AreEqual("1 2 4", head.ToString());

            var headBeforeDelete = head;
            var headAfterDelete = Node<int>.DeleteNode(head, 5);

            Assert.True(headBeforeDelete == headAfterDelete);
        }
    }
}