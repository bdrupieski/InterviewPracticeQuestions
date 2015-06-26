using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Chapter2
{
    /// <summary>
    /// Implement a function to check if a linked list is a palindrome.
    /// </summary>
    public class Q2P7IsLinkedListAPalindrome
    {
        public static bool IsPalindrome<T>(Node<T> head) where T : IEquatable<T>
        {
            if (head == null)
                return false;
            if (head.Next == null)
                return true;

            var items = new Stack<T>();
            items.Push(head.Data);

            Node<T> current = head;
            while (current.Next != null)
            {
                current = current.Next;
                items.Push(current.Data);
            }

            current = head;
            for (int i = 0; i < items.Count / 2; i++)
            {
                var itemFromEnd = items.Pop();
                var itemFromBeginning = current.Data;

                if (!itemFromEnd.Equals(itemFromBeginning))
                {
                    return false;
                }

                current = current.Next;
            }

            return true;
        }

        public static bool IsPalindrome_WithRunner<T>(Node<T> head) where T : IEquatable<T>
        {
            var fast = head;
            var slow = head;

            var firstHalfItems = new Stack<T>();

            while (fast?.Next != null)
            {
                firstHalfItems.Push(slow.Data);
                slow = slow.Next;
                fast = fast.Next.Next;
            }

            // odd number of elements, so slow skips middle element
            if (fast != null)
            {
                slow = slow.Next;
            }

            while (slow != null)
            {
                var itemFromFirstHalf = firstHalfItems.Pop();
                var itemFromSecondHalf = slow.Data;

                if (!itemFromFirstHalf.Equals(itemFromSecondHalf))
                {
                    return false;
                }

                slow = slow.Next;
            }

            return true;
        }

        [TestFixture]
        public class Q2P7IsLinkedListAPalindromeTests
        {
            [Test]
            public void IsLinkedListPalindromeTestOdd()
            {
                foreach (var m in typeof(Q2P7IsLinkedListAPalindrome).PublicStaticMethods())
                {
                    var head = new Node<int>(1);
                    head.Append(2);
                    head.Append(3);
                    head.Append(2);
                    head.Append(1);

                    Assert.IsTrue(m.IsPalindrome(head));

                    head.Append(1);

                    Assert.IsFalse(m.IsPalindrome(head));
                }
            }

            [Test]
            public void IsLinkedListPalindromeTestEven()
            {
                foreach (var m in typeof(Q2P7IsLinkedListAPalindrome).PublicStaticMethods())
                {
                    var head = new Node<int>(1);
                    head.Append(2);
                    head.Append(3);
                    head.Append(3);
                    head.Append(2);
                    head.Append(1);

                    Assert.IsTrue(m.IsPalindrome(head));

                    head.Append(1);

                    Assert.IsFalse(m.IsPalindrome(head));
                }
            }

            [Test]
            public void IsLinkedListPalindromeTest2()
            {
                foreach (var m in typeof(Q2P7IsLinkedListAPalindrome).PublicStaticMethods())
                {
                    var head = new Node<string>("a");
                    head.Append("b");
                    head.Append("c");
                    head.Append("d");
                    head.Append("c");
                    head.Append("b");
                    head.Append("a");

                    Assert.IsTrue(m.IsPalindrome(head));
                }
            }

            [Test]
            public void IsLinkedListPalindromeTest3()
            {
                foreach (var m in typeof(Q2P7IsLinkedListAPalindrome).PublicStaticMethods())
                {
                    var head = new Node<int>(1);

                    Assert.IsTrue(m.IsPalindrome(head));

                    head.Append(1);

                    Assert.IsTrue(m.IsPalindrome(head));

                    head.Append(1);

                    Assert.IsTrue(m.IsPalindrome(head));
                }
            }
        }
    }

    public static class Q2P7IsLinkedListAPalindromeMethodInfoExtensions
    {
        public static bool IsPalindrome<T>(this MethodInfo m, Node<T> head) where T : IEquatable<T>
        {
            return (bool)m.MakeGenericMethod(typeof(T)).Invoke(null, new object[] { head });
        }
    }
}