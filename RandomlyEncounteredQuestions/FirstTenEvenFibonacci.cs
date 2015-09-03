using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace RandomlyEncounteredQuestions
{
    public class FirstTenEvenFibonacci
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

        [Test]
        public void FirstTenEvenFibonacciTest()
        {
            CollectionAssert.AreEqual(FibonacciIterative().Where(x => x % 2 == 0).Take(10), new[] { 0, 2, 8, 34, 144, 610, 2584, 10946, 46368, 196418 });
            CollectionAssert.AreEqual(FibonacciRecursive().Where(x => x % 2 == 0).Take(10), new[] { 0, 2, 8, 34, 144, 610, 2584, 10946, 46368, 196418 });
        }
    }
}