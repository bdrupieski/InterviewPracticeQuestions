using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Chapter2
{
    public class Q2P1LinkedListRemoveDuplicates
    {
        public static void RemoveDuplicates<T>(Node<T> head) where T : IEquatable<T>
        {
            var visitedElements = new HashSet<T>();
            Node<T> current = head;
            Node<T> previous = null;

            while (current != null)
            {
                if (visitedElements.Contains(current.Data))
                {
                    previous.Next = current.Next;
                }
                else
                {
                    visitedElements.Add(current.Data);
                    previous = current;
                }

                current = current.Next;
            }
        }

        public static void RemoveDuplicates_NoTemporaryBuffer<T>(Node<T> head) where T : IEquatable<T>
        {
            Node<T> current = head;

            while (current != null)
            {
                Node<T> previousRunner = current;
                Node<T> runner = current.Next;
                while (runner != null)
                {
                    if (runner.Data.Equals(current.Data))
                    {
                        previousRunner.Next = runner.Next;
                    }
                    else
                    {
                        previousRunner = runner;
                    }

                    runner = runner.Next;
                }

                current = current.Next;
            }
        }

        [TestFixture]
        public class Q2P1LinkedListRemoveDuplicatesTests
        {
            [Test]
            public void RemoveDuplicatesTest()
            {
                foreach (var m in typeof(Q2P1LinkedListRemoveDuplicates).PublicStaticMethods())
                {
                    var head = new Node<int>(1);
                    head.Append(2);
                    head.Append(3);
                    head.Append(1);
                    head.Append(2);
                    head.Append(6);
                    head.Append(7);
                    head.Append(4);
                    head.Append(4);
                    Assert.AreEqual("1 2 3 1 2 6 7 4 4", head.ToString());

                    m.RemoveDuplicates(head);
                    Assert.AreEqual("1 2 3 6 7 4", head.ToString());
                }
            }

            [Test]
            public void RemoveDuplicatesTest2()
            {
                foreach (var m in typeof(Q2P1LinkedListRemoveDuplicates).PublicStaticMethods())
                {
                    var head = new Node<int>(1);
                    head.Append(1);
                    head.Append(1);
                    head.Append(1);
                    head.Append(1);
                    Assert.AreEqual("1 1 1 1 1", head.ToString());

                    m.RemoveDuplicates(head);
                    Assert.AreEqual("1", head.ToString());
                }
            }

            [Test]
            public void RemoveDuplicatesTest3()
            {
                foreach (var m in typeof(Q2P1LinkedListRemoveDuplicates).PublicStaticMethods())
                {
                    var head = new Node<int>(1);
                    head.Append(2);
                    head.Append(2);
                    head.Append(3);
                    head.Append(3);
                    head.Append(4);
                    head.Append(4);
                    head.Append(4);
                    head.Append(5);
                    head.Append(6);
                    head.Append(7);
                    Assert.AreEqual("1 2 2 3 3 4 4 4 5 6 7", head.ToString());

                    m.RemoveDuplicates(head);
                    Assert.AreEqual("1 2 3 4 5 6 7", head.ToString());
                }
            }
        }
    }

    public static class Q2P1LinkedListRemoveDuplicatesMethodInfoExtensions
    {
        public static void RemoveDuplicates<T>(this MethodInfo m, Node<T> head) where T : IEquatable<T>
        {
            m.MakeGenericMethod(typeof(T)).Invoke(null, new object[] { head });
        }
    }
}