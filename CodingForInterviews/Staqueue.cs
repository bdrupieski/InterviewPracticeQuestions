using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CodingForInterviews
{
    /// <summary>
    /// Implement a queue using two stacks.
    /// </summary>
    public class Staqueue<T>
    {
        private readonly Stack<T> _queueStack = new Stack<T>();
        private readonly Stack<T> _dequeueStack = new Stack<T>();

        public void Queue(T data)
        {
            if (IsEverythingInDequeueStack())
            {
                ShiftContentsToQueueStack();
            }

            _queueStack.Push(data);
        }

        public T Dequeue()
        {
            if (IsEverythingInQueueStack())
            {
                ShiftContentsToDequeueStack();
            }

            return _dequeueStack.Pop();
        }

        private void ShiftContentsToQueueStack()
        {
            ShiftContentsOfStack(_dequeueStack, _queueStack);
        }

        private void ShiftContentsToDequeueStack()
        {
            ShiftContentsOfStack(_queueStack, _dequeueStack);
        }

        private void ShiftContentsOfStack(Stack<T> from, Stack<T> to)
        {
            while (from.Any())
            {
                to.Push(from.Pop());
            }
        }

        private bool IsEverythingInQueueStack()
        {
            return _queueStack.Any() && _dequeueStack.IsEmpty();
        }

        private bool IsEverythingInDequeueStack()
        {
            return _queueStack.IsEmpty() && _dequeueStack.Any();
        }
    }

    [TestFixture]
    public class StaqueueTests
    {
        [Test]
        public void SimpleQueueAndDequeue()
        {
            var staqueue = new Staqueue<int>();

            staqueue.Queue(1);
            staqueue.Queue(2);
            staqueue.Queue(3);
            staqueue.Queue(4);

            Assert.AreEqual(1, staqueue.Dequeue());
            Assert.AreEqual(2, staqueue.Dequeue());
            Assert.AreEqual(3, staqueue.Dequeue());
            Assert.AreEqual(4, staqueue.Dequeue());

            staqueue.Queue(5);
            staqueue.Queue(6);
            Assert.AreEqual(5, staqueue.Dequeue());
            Assert.AreEqual(6, staqueue.Dequeue());
        }

        [Test]
        public void MixQueueAndDequeue()
        {
            var staqueue = new Staqueue<int>();

            staqueue.Queue(1);
            staqueue.Queue(2);
            Assert.AreEqual(1, staqueue.Dequeue());

            staqueue.Queue(3);
            staqueue.Queue(4);
            Assert.AreEqual(2, staqueue.Dequeue());

            staqueue.Queue(5);
            Assert.AreEqual(3, staqueue.Dequeue());
            Assert.AreEqual(4, staqueue.Dequeue());
            Assert.AreEqual(5, staqueue.Dequeue());
        }
    }
}