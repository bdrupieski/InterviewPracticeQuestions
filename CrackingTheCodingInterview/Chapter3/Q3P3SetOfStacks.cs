using System.Collections.Generic;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Chapter3
{
    /// <summary>
    /// Imagine a (literal) stack of plates. If the stack gets too high, it might topple.
    /// Therefore, in real life, we would likely start a new stack when the previous stack
    /// exceeds some threshold. Implement a data structure SetOfStacks that mimics
    /// this. SetOfStacks should be composed of several stacks and should create a
    /// new stack once the previous one exceeds capacity. SetOfStacks.push() and
    /// SetOfStacks.pop() should behave identically to a single stack (that is, pop()
    /// should return the same values as it would if there were just a single stack)
    /// </summary>
    public class Q3P3SetOfStacks
    {
        public class SetOfStacks<T>
        {
            private readonly Stack<Stack<T>> _stacks = new Stack<Stack<T>>();
            private readonly int _toppleHeight;

            public SetOfStacks(int toppleHeight)
            {
                _toppleHeight = toppleHeight;
            }

            public void Push(T data)
            {
                if (_stacks.IsEmpty() || _stacks.Peek().Count == _toppleHeight)
                {
                    _stacks.Push(new Stack<T>());
                }

                _stacks.Peek().Push(data);
            }

            public T Pop()
            {
                var item = _stacks.Peek().Pop();

                if (_stacks.Peek().IsEmpty())
                {
                    _stacks.Pop();
                }

                return item;
            }

            public T Peek()
            {
                return _stacks.Peek().Peek();
            }
        }

        [TestFixture]
        public class Q3P3SetOfStacksTests
        {
            [Test]
            public void SetOfStacksTest()
            {
                var stack = new SetOfStacks<int>(3);

                stack.Push(1);
                stack.Push(2);
                stack.Push(3);
                stack.Push(4);
                stack.Push(5);
                stack.Push(6);
                stack.Push(7);

                Assert.AreEqual(7, stack.Pop());
                Assert.AreEqual(6, stack.Pop());
                Assert.AreEqual(5, stack.Pop());
                Assert.AreEqual(4, stack.Pop());
                Assert.AreEqual(3, stack.Pop());
                Assert.AreEqual(2, stack.Pop());
                Assert.AreEqual(1, stack.Pop());
            }

            [Test]
            public void SetOfStacksTest2()
            {
                var stack = new SetOfStacks<int>(3);

                stack.Push(1);
                stack.Push(2);
                stack.Push(3);
                stack.Push(4);
                stack.Push(5);
                stack.Push(6);
                stack.Push(7);

                Assert.AreEqual(7, stack.Pop());
                Assert.AreEqual(6, stack.Pop());
                Assert.AreEqual(5, stack.Pop());
                Assert.AreEqual(4, stack.Pop());

                stack.Push(4);
                stack.Push(5);
                stack.Push(6);
                stack.Push(7);

                Assert.AreEqual(7, stack.Pop());
                Assert.AreEqual(6, stack.Pop());
                Assert.AreEqual(5, stack.Pop());
                Assert.AreEqual(4, stack.Pop());
            }
        }
    }
}