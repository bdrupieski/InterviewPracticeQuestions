using System.Collections.Generic;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Chapter3
{
    /// <summary>
    /// How would you design a stack which, in addition to push and pop, also has a
    /// function min which returns the minimum element? Push, pop and min should
    /// all operate in O(1) time.
    /// </summary>
    public class Q3P2StackWithConstantTimeMin
    {
        private readonly Stack<int> _data;
        private readonly Stack<int> _mins;

        public Q3P2StackWithConstantTimeMin()
        {
            _data = new Stack<int>();
            _mins = new Stack<int>();
        }

        public void Push(int data)
        {
            _data.Push(data);

            if (data <= Min())
            {
                _mins.Push(data);
            }
        }

        public int Pop()
        {
            var item = _data.Pop();

            if (item == Min())
            {
                _mins.Pop();
            }

            return item;
        }

        public int Min()
        {
            if (_mins.IsEmpty())
            {
                return int.MaxValue;
            }
            else
            {
                return _mins.Peek();
            }
        }
    }

    [TestFixture]
    public class Q3P2StackWithConstantTimeMinTests
    {
        [Test]
        public void TestStackWithMin()
        {
            var stack = new Q3P2StackWithConstantTimeMin();

            stack.Push(2);
            Assert.AreEqual(2, stack.Min());

            stack.Push(5);
            Assert.AreEqual(2, stack.Min());

            Assert.AreEqual(5, stack.Pop());
            Assert.AreEqual(2, stack.Min());

            stack.Push(1);
            Assert.AreEqual(1, stack.Min());

            stack.Push(7);
            Assert.AreEqual(1, stack.Min());

            stack.Push(1);
            Assert.AreEqual(1, stack.Min());

            Assert.AreEqual(1, stack.Pop());
            Assert.AreEqual(1, stack.Min());

            Assert.AreEqual(7, stack.Pop());
            Assert.AreEqual(1, stack.Min());

            Assert.AreEqual(1, stack.Pop());
            Assert.AreEqual(2, stack.Min());
        }
    }
}