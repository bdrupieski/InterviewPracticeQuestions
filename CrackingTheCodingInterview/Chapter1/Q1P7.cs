using NUnit.Framework;

namespace CrackingTheCodingInterview.Chapter1
{
    /// <summary>
    /// Write an algorithm such that if an element in an MxN matrix is 0, its entire row and column is set to 0.
    /// </summary>
    public class Q1P7
    {
        public static void SetRowsAndColsToZero(int[][] matrix)
        {
            if (matrix == null || matrix.Length < 1)
                return;

            var rowsWithZero = new bool[matrix.Length];
            var colsWithZero = new bool[matrix[0].Length];

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] == 0)
                    {
                        rowsWithZero[i] = true;
                        colsWithZero[j] = true;
                    }
                }
            }

            for (int i = 0; i < rowsWithZero.Length; i++)
            {
                if (rowsWithZero[i])
                {
                    for (int j = 0; j < matrix[i].Length; j++)
                    {
                        matrix[i][j] = 0;
                    }
                }
            }

            for (int j = 0; j < colsWithZero.Length; j++)
            {
                if (colsWithZero[j])
                {
                    for (int i = 0; i < matrix.Length; i++)
                    {
                        matrix[i][j] = 0;
                    }
                }
            }
        }

        [TestFixture]
        public class Question1P7Tests
        {
            [Test]
            public void ZeroOutRowsAndColumnsWithZeroTest()
            {
                var originalMatrix = new int[][]
                {
                    new int[] {  1,  2,  3,  4,  5 },
                    new int[] {  6,  0,  8,  9, 10 },
                    new int[] { 11, 12, 13, 14, 15 },
                    new int[] { 16, 17, 18,  0, 20 },
                    new int[] { 21, 22, 23, 24, 25 }
                };

                var zeroedMatrix = new int[][]
                {
                    new int[] {  1,  0,  3,  0,  5 },
                    new int[] {  0,  0,  0,  0,  0 },
                    new int[] { 11,  0, 13,  0, 15 },
                    new int[] {  0,  0,  0,  0,  0 },
                    new int[] { 21,  0, 23,  0, 25 }
                };

                SetRowsAndColsToZero(originalMatrix);

                Assert.AreEqual(originalMatrix, zeroedMatrix);
            }

            [Test]
            public void ZeroOutRowsAndColumnsWithZeroTest2()
            {
                var originalMatrix = new int[][]
                {
                    new int[] {  1,  2,  3,  4,  5 },
                    new int[] {  6,  0,  8,  9, 10 },
                    new int[] { 11,  0, 13, 14, 15 },
                    new int[] { 16, 17, 18,  0,  0 },
                    new int[] { 21, 22,  0, 24,  0 }
                };

                var zeroedMatrix = new int[][]
                {
                    new int[] {  1,  0,  0,  0,  0 },
                    new int[] {  0,  0,  0,  0,  0 },
                    new int[] {  0,  0,  0,  0,  0 },
                    new int[] {  0,  0,  0,  0,  0 },
                    new int[] {  0,  0,  0,  0,  0 }
                };

                SetRowsAndColsToZero(originalMatrix);

                Assert.AreEqual(originalMatrix, zeroedMatrix);
            }
        }
    }
}