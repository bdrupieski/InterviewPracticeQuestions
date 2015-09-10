using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace RandomlyEncounteredQuestions
{
    public class Sorting
    {
        public static void SelectionSort(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                int min = arr[i];
                int nextIndex = i;
                for (int j = i; j < arr.Length; j++)
                {
                    if (arr[j] < min)
                    {
                        min = arr[j];
                        nextIndex = j;
                    }
                }

                if (nextIndex != i)
                {
                    arr.Swap(i, nextIndex);
                }
            }
        }

        public static void InsertionSort(int[] arr)
        {
            for (int i = 0; i <= arr.Length - 1; i++)
            {
                for (int j = i; j > 0; j--)
                {
                    if (arr[j] < arr[j - 1])
                    {
                        arr.Swap(j, j - 1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public static void QuickSort(int[] arr)
        {
            QuickSort(arr, 0, arr.Length - 1);
        }

        private static void QuickSort(int[] arr, int lowIndex, int highIndex)
        {
            if (lowIndex < highIndex)
            {
                int pivot = Partition(arr, lowIndex, highIndex);
                QuickSort(arr, lowIndex, pivot - 1);
                QuickSort(arr, pivot + 1, highIndex);
            }
        }

        private static int Partition(int[] arr, int low, int high)
        {
            int pivotVal = arr[high];

            while (low < high)
            {
                while (arr[low] < pivotVal) low++;
                while (arr[high] > pivotVal) high--;
                if (arr[low] == pivotVal && arr[high] == pivotVal) low++;

                if (low < high)
                {
                    arr.Swap(low, high);
                }
            }
            return high;
        }

        [Test]
        public void SelectionSortTest()
        {
            SortTest(SelectionSort);
        }

        [Test]
        public void InsertionSortTest()
        {
            SortTest(InsertionSort);
        }

        [Test]
        public void QuickSortTest()
        {
            SortTest(QuickSort);
        }

        private static void SortTest(Action<int[]> sort)
        {
            int[] unordered = { 9, 1, 5, 3, 4, 6, 7, 8, 4, 4, 3, 7 };
            sort(unordered);

            CollectionAssert.AreEqual(new[] { 1, 3, 3, 4, 4, 4, 5, 6, 7, 7, 8, 9 }, unordered);
        }
    }

    public static class SortingListExtensions
    {
        public static void Swap<T>(this IList<T> list, int a, int b)
        {
            T temp = list[a];
            list[a] = list[b];
            list[b] = temp;
        }
    }
}