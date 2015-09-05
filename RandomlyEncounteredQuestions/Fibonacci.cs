using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace RandomlyEncounteredQuestions
{
    public class Fibonacci
    {
        static IEnumerable<int> FibonacciIterative()
        {
            int a = 0;
            int b = 1;
            while (true)
            {
                yield return a;
                var temp = b;
                b = a + b;
                a = temp;
            }
        }

        static IEnumerable<int> FibonacciRecursive(int a = 0, int b = 1)
        {
            yield return a;
            foreach (var elem in FibonacciRecursive(b, a + b))
            {
                yield return elem;
            }
        }

        static int FibonacciN(int n)
        {
            if (n <= 1)
            {
                return n;
            }
            else
            {
                return FibonacciN(n - 1) + FibonacciN(n - 2);
            }
        }

        [Test]
        public void FirstTenEvenFibonacciTest()
        {
            CollectionAssert.AreEqual(FibonacciIterative().Where(x => x % 2 == 0).Take(10), new[] { 0, 2, 8, 34, 144, 610, 2584, 10946, 46368, 196418 });
            CollectionAssert.AreEqual(FibonacciRecursive().Where(x => x % 2 == 0).Take(10), new[] { 0, 2, 8, 34, 144, 610, 2584, 10946, 46368, 196418 });
            CollectionAssert.AreEqual(Enumerable.Range(0, 10).Select(FibonacciN), new[] { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34 });
        }
    }
}