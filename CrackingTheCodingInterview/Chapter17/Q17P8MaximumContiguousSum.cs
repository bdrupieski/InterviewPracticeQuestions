using System;
using System.Linq;
using System.Reflection;
using Common;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Chapter17
{
    /// <summary>
    /// You are given an array of integers (both positive and negative). 
    /// Find the contiguous sequence with the largest sum. 
    /// Return the sum.
    /// 
    /// EXAMPLE
    /// 
    /// Input: [2, -8, 3, -2, 4, -10 ]
    /// Output: 5 (i.e. {3, -2, 4})
    /// </summary>
    public class Q17P8MaximumContiguousSum
    {
        public static int MaximumSumBruteForce(int[] arr)
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

            return Math.Max(maxSubRun, 0);
        }

        public static int MaximumSum(int[] arr)
        {
            int maxSum = 0;
            int sum = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                sum += arr[i];

                if (sum > maxSum)
                {
                    maxSum = sum;
                }
                else if (sum < 0)
                {
                    sum = 0;
                }
            }
            
            return maxSum;
        }
    }

    [TestFixture]
    public class Q17P8MaximumContiguousSumTests
    {
        [Test]
        public void Simple()
        {
            foreach (var m in typeof(Q17P8MaximumContiguousSum).PublicStaticMethods())
            {
                Assert.AreEqual(29, m.MaximumSum(new[] { -1, 5, 6, -2, 20, -50, 4 }));
                Assert.AreEqual(0, m.MaximumSum(new[] { -1, -1, -1, 0, -1, -1 }));
                Assert.AreEqual(0, m.MaximumSum(new[] { 0, 0, 0 }));
                Assert.AreEqual(0, m.MaximumSum(new[] { -3, -5, -10, -2, -7 }));
                Assert.AreEqual(102, m.MaximumSum(new[] { -50, -50, 100, 2 }));
                Assert.AreEqual(102, m.MaximumSum(new[] { 1, 100, -50, -50, 100, 1 }));
                Assert.AreEqual(29, m.MaximumSum(new[] { -1, 5, 6, -2, 20, -50, 4 }));
                Assert.AreEqual(5, m.MaximumSum(new[] { 2, -8, 3, -2, 4, -10 }));
            }
        }
    }

    public static class Q17P8MaximumContiguousSumMethodInfoExtensions
    {
        public static int MaximumSum(this MethodInfo m, int[] arr)
        {
            return (int)m.Invoke(null, new object[] { arr });
        }
    }
}