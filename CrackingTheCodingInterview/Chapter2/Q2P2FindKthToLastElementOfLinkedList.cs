using System;
using System.Reflection;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Chapter2
{
    /// <summary>
    /// Implement an algorithm to find the kth to last element of a singly linked list
    /// </summary>
    public class Q2P2FindKthToLastElementOfLinkedList
    {
        public static Node<T> FindKthToLastElement_Iterative<T>(Node<T> head, int k) where T : IEquatable<T>
        {
            Node<T> last = head;
            Node<T> kthToLast = head;

            for (int i = 0; i < k; i++)
            {
                if (last.Next == null)
                {
                    return null;
                }
                else
                {
                    last = last.Next;
                }
            }

            while (last.Next != null)
            {
                last = last.Next;
                kthToLast = kthToLast.Next;
            }


            return kthToLast;
        }

        public static Node<T> FindKthToLastElement_Recursive<T>(Node<T> head, int k) where T : IEquatable<T>
        {
            int currentElement = 0;
            return FindKthToLastElement_Recursive_Impl(head, k, ref currentElement);
        }

        private static Node<T> FindKthToLastElement_Recursive_Impl<T>(Node<T> head, int k, ref int currentElement) where T : IEquatable<T>
        {
            if (head == null)
            {
                return null;
            }

            var node = FindKthToLastElement_Recursive_Impl(head.Next, k, ref currentElement);
            currentElement++;
            if (currentElement == k + 1)
            {
                return head;
            }
            return node;
        }

        [TestFixture]
        public class Q2P2FindKthToLastElementOfLinkedListTests
        {
            [Test]
            public void FindKthToLastElementsTest()
            {
                foreach (var m in typeof(Q2P2FindKthToLastElementOfLinkedList).PublicStaticMethods())
                {
                    var head = new Node<int>(1);
                    head.Append(2);
                    head.Append(3);
                    head.Append(4);
                    head.Append(5);

                    Assert.AreEqual(5, m.FindKthToLastElement(head, 0).Data);
                    Assert.AreEqual(4, m.FindKthToLastElement(head, 1).Data);
                    Assert.AreEqual(3, m.FindKthToLastElement(head, 2).Data);
                    Assert.AreEqual(2, m.FindKthToLastElement(head, 3).Data);
                    Assert.AreEqual(1, m.FindKthToLastElement(head, 4).Data);

                    Assert.AreEqual(null, m.FindKthToLastElement(head, 5));
                    Assert.AreEqual(null, m.FindKthToLastElement(head, 6));
                }
            }

            [Test]
            [Ignore]
            public void TestQ1P5RunLengthEncodingPerformance()
            {
                var head = new Node<int>(1);
                head.Append(2);
                head.Append(3);
                head.Append(4);
                head.Append(5);

                PerformanceHelper.PerformanceTestPublicStaticMethods<Q2P2FindKthToLastElementOfLinkedList, int>(head, 4);
            }
        }
    }

    public static class Q2P2FindKthToLastElementOfLinkedListMethodExtensions
    {
        public static Node<T> FindKthToLastElement<T>(this MethodInfo m, Node<T> head, int k) where T : IEquatable<T>
        {
            return (Node<T>)m.MakeGenericMethod(typeof(T)).Invoke(null, new object[] { head, k });
        }
    }
}