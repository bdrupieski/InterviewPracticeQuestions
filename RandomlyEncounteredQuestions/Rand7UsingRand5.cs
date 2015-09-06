using System;
using System.Linq;
using NUnit.Framework;

namespace RandomlyEncounteredQuestions
{
    public class Rand7UsingRand5
    {
        private static readonly Random Random = new Random(42);

        public static int Rand5()
        {
            return Random.Next(1, 6);
        }

        public static int Rand7()
        {
            int r;

            do
            {
                r = ((Rand5() * 5) + Rand5()) - 5;
            } while (r > 21);

            return (r % 7) + 1;
        }

        [Test]
        public void DistributionOfRand5()
        {
            PrintAndVerifyDistributionIsNotSignificantlyDifferentFrom(Rand5, 1.0 / 5.0);
        }

        [Test]
        public void DistributionOfRand7()
        {
            PrintAndVerifyDistributionIsNotSignificantlyDifferentFrom(Rand7, 1.0 / 7.0);
        }

        private static void PrintAndVerifyDistributionIsNotSignificantlyDifferentFrom(Func<int> randFunc, double expectedFrequency)
        {
            int n = 1000000;
            var numberFrequencies = Enumerable.Range(0, n)
                .Select(x => randFunc())
                .GroupBy(x => x)
                .Select(x => new { Number = x.Key, Frequency = (double)x.Count() / n })
                .OrderBy(x => x.Number);

            foreach (var numberFrequency in numberFrequencies)
            {
                Assert.AreEqual(expectedFrequency, numberFrequency.Frequency, 0.001);
            }

            Console.WriteLine(string.Join(Environment.NewLine, numberFrequencies.Select(x => $"{x.Number}: {x.Frequency}")));
        }
    }
}