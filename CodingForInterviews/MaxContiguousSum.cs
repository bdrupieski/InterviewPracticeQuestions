using System;
using System.Linq;
using NUnit.Framework;

namespace CodingForInterviews
{
    /// <summary>
    /// Find the maximum sum possible from picking a contiguous subsequence of an array.
    /// </summary>
    public class MaxContiguousSum
    {
        public static int MaxSum(int[] arr)
        {
            int maxSubRun = arr.Min();

            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    int subRun = arr.Skip(j).Take(i - j + 1).Sum();

                    maxSubRun = Math.Max(maxSubRun, subRun);
                }
            }

            return maxSubRun;
        }

        [TestFixture]
        public class MaxContiguousSumTests
        {
            [Test]
            public void Simple()
            {
                Assert.AreEqual(29, MaxSum(new[] { -1, 5, 6, -2, 20, -50, 4 }));
                Assert.AreEqual(0, MaxSum(new[] { -1, -1, -1, 0, -1, -1 }));
                Assert.AreEqual(0, MaxSum(new[] { 0, 0, 0 }));
                Assert.AreEqual(-2, MaxSum(new[] { -3, -5, -10, -2, -7 }));
                Assert.AreEqual(102, MaxSum(new[] { -50, -50, 100, 2 }));
                Assert.AreEqual(102, MaxSum(new[] { 1, 100, -50, -50, 100, 1 }));
                Assert.AreEqual(29, MaxSum(new[] { -1, 5, 6, -2, 20, -50, 4 }));
            }
        }
    }
}