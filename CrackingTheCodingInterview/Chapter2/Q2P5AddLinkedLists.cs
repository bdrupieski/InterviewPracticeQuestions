using System;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Chapter2
{
    /// <summary>
    /// You have two numbers represented by a linked list, where each node contains a
    /// single digit. The digits are stored in reverse order, such that the 1s digit is at the
    /// head of the list. Write a function that adds the two numbers and returns the sum
    /// as a linked list.
    /// 
    /// EXAMPLE
    /// Input: (7-> 1 -> 6) + (5 -> 9 -> 2).That is, 617 + 295.
    /// Output: 2 -> 1 -> 9. That is, 912.
    /// 
    /// FOLLOW UP
    /// Suppose the digits are stored in forward order. Repeat the above problem.
    /// 
    /// EXAMPLE
    /// Input: (6 -> 1 -> 7) + (2 -> 9 -> 5).That is, 617 + 295.
    /// Output: 9 -> 1 -> 2. That is, 912.
    /// </summary>
    public class Q2P5AddLinkedLists
    {
        public static int AddLinkedListsReverseOrder(Node<int> a, Node<int> b)
        {
            var currentANode = a;
            var currentBNode = b;

            int sum = 0;
            int carry = 0;

            while (currentANode != null || currentBNode != null)
            {
                int currentAValue = 0;
                int currentBValue = 0;

                if (currentANode != null)
                {
                    currentAValue = currentANode.Data;
                    currentANode = currentANode.Next;
                }

                if (currentBNode != null)
                {
                    currentBValue = currentBNode.Data;
                    currentBNode = currentBNode.Next;
                }

                int addedVals = currentAValue + currentBValue + carry;
                int onesPlace = addedVals % 10;

                sum *= 10;
                sum += onesPlace;

                if (addedVals >= 10)
                {
                    carry = addedVals / 10;
                }
            }

            return sum;
        }

        public static int AddLinkedListsInOrder(Node<int> a, Node<int> b)
        {
            int lengthOfA = GetLength(a);
            int lengthOfB = GetLength(b);

            if (lengthOfA < lengthOfB)
            {
                a = PrependZeroes(a, lengthOfB - lengthOfA);
            }

            if (lengthOfB < lengthOfA)
            {
                b = PrependZeroes(b, lengthOfA - lengthOfB);
            }

            var currentANode = a;
            var currentBNode = b;

            int sum = 0;

            while (currentANode != null || currentBNode != null)
            {
                int currentAValue = 0;
                int currentBValue = 0;

                if (currentANode != null)
                {
                    currentAValue = currentANode.Data;
                    currentANode = currentANode.Next;
                }

                if (currentBNode != null)
                {
                    currentBValue = currentBNode.Data;
                    currentBNode = currentBNode.Next;
                }

                int addedVals = currentAValue + currentBValue;

                sum *= 10;
                sum += addedVals;
            }

            return sum;
        }

        public static int GetLength<T>(Node<T> head) where T : IEquatable<T>
        {
            int count = 0;

            var current = head;
            while (current != null)
            {
                count++;
                current = current.Next;
            }

            return count;
        }

        public static Node<int> PrependZeroes(Node<int> head, int count)
        {
            if (count > 0)
            {
                var zeroList = new Node<int>(0);
                count--;

                while (count > 0)
                {
                    zeroList.Append(0);
                    count--;
                }

                var endOfZeroList = zeroList;
                while (endOfZeroList.Next != null)
                {
                    endOfZeroList = endOfZeroList.Next;
                }

                endOfZeroList.Next = head;
                head = zeroList;
            }

            return head;
        }

        [TestFixture]
        public class Q2P5AddLinkedListsTests
        {
            [Test]
            public void TestAddingLinkedListsReverseOrder()
            {
                var a = new Node<int>(7);
                a.Append(1);
                a.Append(6);

                var b = new Node<int>(5);
                b.Append(9);
                b.Append(2);

                Assert.AreEqual(219, AddLinkedListsReverseOrder(a, b));
            }

            [Test]
            public void TestAddingLinkedListsReverseOrder2()
            {
                var a = new Node<int>(1);
                a.Append(2);
                a.Append(3);

                var b = new Node<int>(4);
                b.Append(5);
                b.Append(6);

                Assert.AreEqual(579, AddLinkedListsReverseOrder(a, b));
            }

            [Test]
            public void TestAddingLinkedListsReverseOrder3()
            {
                var a = new Node<int>(1);
                a.Append(2);
                a.Append(3);
                a.Append(4);

                var b = new Node<int>(4);
                b.Append(5);

                Assert.AreEqual(5734, AddLinkedListsReverseOrder(a, b));
            }

            [Test]
            public void TestAddingLinkedListsInOrder()
            {
                var a = new Node<int>(6);
                a.Append(1);
                a.Append(7);

                var b = new Node<int>(2);
                b.Append(9);
                b.Append(5);

                Assert.AreEqual(912, AddLinkedListsInOrder(a, b));
            }

            [Test]
            public void TestAddingLinkedListsInOrder2()
            {
                var a = new Node<int>(1);
                a.Append(2);
                a.Append(3);

                var b = new Node<int>(4);
                b.Append(5);
                b.Append(6);

                Assert.AreEqual(579, AddLinkedListsInOrder(a, b));
            }

            [Test]
            public void TestAddingLinkedListsInOrder3()
            {
                var a = new Node<int>(7);
                a.Append(8);
                a.Append(9);

                var b = new Node<int>(5);
                b.Append(5);
                b.Append(5);
                b.Append(5);

                Assert.AreEqual(6344, AddLinkedListsInOrder(a, b));
            }

            [Test]
            public void TestAddingLinkedListsInOrder4()
            {
                var a = new Node<int>(5);
                a.Append(5);
                a.Append(5);
                a.Append(5);

                var b = new Node<int>(7);
                b.Append(8);
                b.Append(9);

                Assert.AreEqual(6344, AddLinkedListsInOrder(a, b));
            }

            [Test]
            public void TestAddingLinkedListsInOrder5()
            {
                var a = new Node<int>(9);
                a.Append(9);
                a.Append(9);
                a.Append(9);

                var b = new Node<int>(9);
                b.Append(9);

                Assert.AreEqual(10098, AddLinkedListsInOrder(a, b));
            }

            [Test]
            public void TestAddingLinkedListsInOrder6()
            {
                var a = new Node<int>(9);

                var b = new Node<int>(9);
                b.Append(9);
                b.Append(9);
                b.Append(9);
                b.Append(9);

                Assert.AreEqual(100008, AddLinkedListsInOrder(a, b));
            }
        }
    }
}