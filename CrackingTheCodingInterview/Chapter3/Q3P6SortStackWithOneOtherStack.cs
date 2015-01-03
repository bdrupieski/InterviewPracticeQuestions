using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Chapter3
{
    /// <summary>
    /// Write a program to sort a stack in ascending order (with biggest items on top).
    /// You may use at most one additional stack to hold items, but you may not copy
    /// the elements into any other data structure (such as an array). The stack supports
    /// the following operations: push, pop, peek, and isEmpty.
    /// </summary>
    public class Q3P6SortStackWithOneOtherStack
    {
        public static Stack<T> SortStack<T>(Stack<T> s) where T : IComparable<T>
        {
            var sorted = new Stack<T>();
            while (!s.IsEmpty())
            {
                T temp = s.Pop();
                while (!sorted.IsEmpty() && sorted.Peek().CompareTo(temp) > 0)
                {
                    s.Push(sorted.Pop());
                }
                sorted.Push(temp);
            }

            return sorted;
        }
            
        [TestFixture]
        public class Q3P6SortStackWithOneOtherStackTests 
        {
            [Test]
            public void SortStackTest()
            {
                var s = new Stack<int>();
                s.Push(5);
                s.Push(4);
                s.Push(3);
                s.Push(2);
                s.Push(1);

                var sorted = SortStack(s);

                Assert.AreEqual(5, sorted.Pop());
                Assert.AreEqual(4, sorted.Pop());
                Assert.AreEqual(3, sorted.Pop());
                Assert.AreEqual(2, sorted.Pop());
                Assert.AreEqual(1, sorted.Pop());
            }

            [Test]
            public void SortStackTest2()
            {
                var s = new Stack<int>();
                s.Push(5);
                s.Push(3);
                s.Push(2);
                s.Push(4);
                s.Push(1);

                var sorted = SortStack(s);

                Assert.AreEqual(5, sorted.Pop());
                Assert.AreEqual(4, sorted.Pop());
                Assert.AreEqual(3, sorted.Pop());
                Assert.AreEqual(2, sorted.Pop());
                Assert.AreEqual(1, sorted.Pop());
            }

            [Test]
            public void SortStackTest3()
            {
                var s = new Stack<int>();
                s.Push(1);
                s.Push(3);
                s.Push(2);
                s.Push(4);
                s.Push(5);

                var sorted = SortStack(s);

                Assert.AreEqual(5, sorted.Pop());
                Assert.AreEqual(4, sorted.Pop());
                Assert.AreEqual(3, sorted.Pop());
                Assert.AreEqual(2, sorted.Pop());
                Assert.AreEqual(1, sorted.Pop());
            }
        }
    }
}