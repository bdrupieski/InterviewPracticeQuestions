using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common;
using NUnit.Framework;

namespace RandomlyEncounteredQuestions
{
    /// <summary>
    /// Consider an array. The value at each index of the array represents the height of a wall.
    /// Now imagine it rains. How much water will accumulate between the walls?
    /// The volume of the water is measured in 1x1 blocks in the array.
    /// For example, for the array [1, 0, 4, 3] one block of water accumulates between the 1 and 4,
    /// but rolls off to the right of the 4 because there is no wall at the end of the array.
    /// 
    /// Write a program to compute the volume of water in the puddle.
    /// 
    /// https://medium.com/@bearsandsharks/i-failed-a-twitter-interview-52062fbb534b
    /// </summary>
    public class WaterFillingValleysInAnArray
    {
        // My initial O(n^2) solution
        public static int GetVolumeInefficiently(int[] arr)
        {
            int maxSoFar = 0;
            int volume = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                maxSoFar = Math.Max(maxSoFar, arr[i]);
                int maxAfterCurrent = arr.Skip(i).Max();

                int highestCanGoWithoutSpillage = Math.Min(maxSoFar, maxAfterCurrent);

                if (highestCanGoWithoutSpillage > arr[i])
                {
                    volume += highestCanGoWithoutSpillage - arr[i];
                }
            }

            return volume;
        }

        // My O(nlogn)ish confusing solution
        public static int GetVolumeInefficiently2(int[] arr)
        {
            int maxHeight = int.MinValue;
            int maxIndex = int.MinValue;

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > maxHeight)
                {
                    maxHeight = arr[i];
                    maxIndex = i;
                }
            }

            int volume = 0;

            var heightsRightOfMax = new Stack<int>(arr.Skip(maxIndex + 1).OrderBy(x => x));
            if (heightsRightOfMax.Any())
            {
                int nextHeightRight = heightsRightOfMax.Pop();
                for (int i = maxIndex + 1; i < arr.Length; i++)
                {
                    if (arr[i] < nextHeightRight)
                    {
                        volume += nextHeightRight - arr[i];
                    }

                    if (arr[i] == nextHeightRight && heightsRightOfMax.Any())
                    {
                        nextHeightRight = heightsRightOfMax.Pop();
                    }
                }
            }

            var heightsLeftOfMax = new Stack<int>(arr.Take(maxIndex).OrderBy(x => x));
            if (heightsLeftOfMax.Any())
            {
                int nextHeightLeft = heightsLeftOfMax.Pop();
                for (int i = maxIndex; i > 0; i--)
                {
                    if (arr[i] < nextHeightLeft)
                    {
                        volume += nextHeightLeft - arr[i];
                    }

                    if (arr[i] == nextHeightLeft && heightsLeftOfMax.Any())
                    {
                        nextHeightLeft = heightsLeftOfMax.Pop();
                    }
                }
            }
           
            return volume;
        }

        // My implementation after reading a description of the O(n) solution
        public static int GetVolume(int[] arr)
        {
            int leftIndex = 0;
            int rightIndex = arr.Length - 1;

            int leftMaxSoFar = 0;
            int rightMaxSoFar = 0;

            int volume = 0;

            while (leftIndex < rightIndex)
            {
                int leftHeight = arr[leftIndex];
                int rightHeight = arr[rightIndex];

                int shortestMax = Math.Min(leftMaxSoFar, rightMaxSoFar);

                if (leftHeight < shortestMax)
                {
                    volume += shortestMax - leftHeight;
                    leftIndex++;
                }
                else if (rightHeight < shortestMax)
                {
                    volume += shortestMax - rightHeight;
                    rightIndex--;
                }
                else
                {
                    if (leftHeight < rightHeight)
                    {
                        leftIndex++;
                    }
                    else
                    {
                        rightIndex--;
                    }
                }

                leftMaxSoFar = Math.Max(leftMaxSoFar, leftHeight);
                rightMaxSoFar = Math.Max(rightMaxSoFar, rightHeight);
            }

            return volume;
        }

        [Test]
        public void FillTest()
        {
            foreach (var m in typeof(WaterFillingValleysInAnArray).PublicStaticMethods())
            {
                Assert.AreEqual(17, m.GetVolume(new[] { 2, 5, 1, 3, 1, 2, 1, 7, 7, 6 }));
                Assert.AreEqual(0, m.GetVolume(new[] { 1, 2, 1 }));

                Assert.AreEqual(1, m.GetVolume(new[] { 2, 1, 2 }));
               
                Assert.AreEqual(2, m.GetVolume(new[] { 5, 4, 4, 5 }));
                Assert.AreEqual(3, m.GetVolume(new[] { 1, 0, 0, 0, 1 }));

                Assert.AreEqual(2, m.GetVolume(new[] { 1, 0, 1, 0, 1 }));
                Assert.AreEqual(5, m.GetVolume(new[] { 2, 0, 1, 0, 2 }));
                Assert.AreEqual(4, m.GetVolume(new[] { 2, 0, 2, 0, 2 }));

                Assert.AreEqual(0, m.GetVolume(new[] { 6, 5, 4, 3, 2, 1 }));
                Assert.AreEqual(0, m.GetVolume(new[] { 1, 2, 3, 4, 5, 6 }));

                Assert.AreEqual(7, m.GetVolume(new[] { 7, 4, 3, 7, 6, 5 }));
                Assert.AreEqual(7, m.GetVolume(new[] { 5, 6, 7, 3, 4, 7 }));

                Assert.AreEqual(16, m.GetVolume(new[] { 5, 4, 3, 2, 1, 2, 3, 4, 5 }));
                Assert.AreEqual(0, m.GetVolume(new[] { 1, 2, 3, 4, 5, 4, 3, 2, 1 }));
                Assert.AreEqual(0, m.GetVolume(new[] { 1, 2, 3, 4, 5, 4, 3, 2, 1 }));

                Assert.AreEqual(5, m.GetVolume(new[] { 3, 5, 2, 7, 8, 4, 6, 4 }));
                Assert.AreEqual(5, m.GetVolume(new[] { 4, 6, 4, 8, 7, 2, 5, 3 }));

                Assert.AreEqual(24, m.GetVolume(new[] { 4, 0, 3, 0, 2, 0, 5, 2, 1, 0, 4 }));
                Assert.AreEqual(24, m.GetVolume(new[] { 4, 0, 1, 2, 5, 0, 2, 0, 3, 0, 4 }));
            }
        }
    }

    public static class WaterFillingValleysInAnArrayMethodInfoExtensions
    {
        public static int GetVolume(this MethodInfo m, int[] arr)
        {
            return (int)m.Invoke(null, new object[] { arr });
        }
    }
}