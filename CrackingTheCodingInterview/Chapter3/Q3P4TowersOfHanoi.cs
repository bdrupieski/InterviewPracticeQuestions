using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Chapter3
{
    public class Q3P4TowersOfHanoi
    {
        public static void MoveDiscs<T>(int number, Stack<T> source, Stack<T> destination, Stack<T> buffer)
        {
            if (number > 0)
            {
                // move items from source to buffer using destination as temp
                MoveDiscs(number - 1, source, buffer, destination);

                destination.Push(source.Pop());

                // move items from buffer to destination using source as temp
                MoveDiscs(number - 1, buffer, destination, source);
            }
        }

        [TestFixture]
        public class Q3P4TowersOfHanoiTests
        {
            [Test]
            public void TowersOfHanoiTest()
            {
                TestWithNDiscs(1);
                TestWithNDiscs(2);
                TestWithNDiscs(3);
                TestWithNDiscs(4);
                TestWithNDiscs(5);
                TestWithNDiscs(6);
                TestWithNDiscs(7);
            }

            private static void TestWithNDiscs(int n)
            {
                var a = new Stack<int>();
                var b = new Stack<int>();
                var c = new Stack<int>();

                var nums = Enumerable.Range(1, n).ToArray();

                foreach (var num in nums)
                {
                    a.Push(num);
                }

                Assert.That(a.ToArray(), Is.EquivalentTo(nums));
                Assert.That(b.IsEmpty());
                Assert.That(c.IsEmpty());

                MoveDiscs(nums.Length, a, c, b);

                Assert.That(a.IsEmpty());
                Assert.That(b.IsEmpty());
                Assert.That(c.ToArray(), Is.EquivalentTo(nums));
            }

            [Test]
            public void TowersOfHanoiTest2()
            {
                var a = new Stack<int>();
                var b = new Stack<int>();
                var c = new Stack<int>();

                a.Push(7);
                a.Push(6);
                a.Push(5);
                a.Push(4);
                a.Push(3);
                a.Push(2);
                a.Push(1);

                Assert.That(a.ToArray(), Is.EquivalentTo(new[] { 7, 6, 5, 4, 3, 2, 1 }));
                Assert.That(b.IsEmpty());
                Assert.That(c.IsEmpty());

                MoveDiscs(4, a, c, b);

                Assert.That(a.ToArray(), Is.EquivalentTo(new[] { 7, 6, 5 }));
                Assert.That(b.IsEmpty());
                Assert.That(c.ToArray(), Is.EquivalentTo(new[] { 4, 3, 2, 1 }));
            }
        }
    }
}