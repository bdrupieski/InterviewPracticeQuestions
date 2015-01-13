using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Chapter3
{
    /// <summary>
    /// Implement a MyQueue class which implements a queue using two stacks.
    /// </summary>
    public class Q3P5QueueFromTwoStacks
    {
        public class MyQueue<T>
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
        public class MyQueueTests
        {
            [Test]
            public void SimpleQueueAndDequeue()
            {
                var myQueue = new MyQueue<int>();

                myQueue.Queue(1);
                myQueue.Queue(2);
                myQueue.Queue(3);
                myQueue.Queue(4);

                Assert.AreEqual(1, myQueue.Dequeue());
                Assert.AreEqual(2, myQueue.Dequeue());
                Assert.AreEqual(3, myQueue.Dequeue());
                Assert.AreEqual(4, myQueue.Dequeue());

                myQueue.Queue(5);
                myQueue.Queue(6);
                Assert.AreEqual(5, myQueue.Dequeue());
                Assert.AreEqual(6, myQueue.Dequeue());
            }

            [Test]
            public void MixQueueAndDequeue()
            {
                var myQueue = new MyQueue<int>();

                myQueue.Queue(1);
                myQueue.Queue(2);
                Assert.AreEqual(1, myQueue.Dequeue());

                myQueue.Queue(3);
                myQueue.Queue(4);
                Assert.AreEqual(2, myQueue.Dequeue());

                myQueue.Queue(5);
                Assert.AreEqual(3, myQueue.Dequeue());
                Assert.AreEqual(4, myQueue.Dequeue());
                Assert.AreEqual(5, myQueue.Dequeue());
            }
        } 
    }
}