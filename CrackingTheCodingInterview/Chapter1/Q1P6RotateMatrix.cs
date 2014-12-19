using NUnit.Framework;

namespace CrackingTheCodingInterview.Chapter1
{
    /// <summary>
    /// Given an image represented by an NxN matrix, where each pixel in the image is 4 bytes, 
    /// write a method to rotate the image by 90 degrees Can you do this in place?
    /// </summary>
    public class Q1P6RotateMatrix
    {
        public static int[][] RotateMatrix_NotInPlace_Clockwise(int[][] matrix)
        {
            var newMatrix = new int[matrix.Length][];
            for (int i = 0; i < matrix.Length; i++)
            {
                newMatrix[i] = new int[matrix[i].Length];
            }

            for (int x = 0; x < matrix.Length; x++)
            {
                for (int y = 0; y < matrix[x].Length; y++)
                {
                    int newX = y;
                    int newY = matrix[x].Length - 1 - x;

                    newMatrix[newX][newY] = matrix[x][y];
                }
            }
            return newMatrix;
        }

        // rotates each concentric ring 4 elements at a time
        public static int[][] RotateMatrix_InPlace_Clockwise(int[][] matrix)
        {
            int halfMatrixLength = matrix.Length / 2;
            for (int i = 0; i < halfMatrixLength; i++)
            {
                for (int j = i; j < matrix.Length - 1 - i; j++)
                {
                    Rotate4Elements(matrix, i, j);
                }
            }
            return matrix;
        }

        private static void Rotate4Elements(int[][] matrix, int x, int y)
        {
            int topLeft = matrix[x][y];
            int topRight = matrix[y][matrix.Length - 1 - x];
            int bottomRight = matrix[matrix.Length - 1 - x][matrix.Length - 1 - y];
            int bottomLeft = matrix[matrix.Length - 1 - y][x];

            matrix[x][y] = bottomLeft;
            matrix[y][matrix.Length - 1 - x] = topLeft;
            matrix[matrix.Length - 1 - x][matrix.Length - 1 - y] = topRight;
            matrix[matrix.Length - 1 - y][x] = bottomRight;
        }

        [TestFixture]
        public class Q1P6RotateMatrixTests
        {
            [Test]
            public void Rotate3X3MatrixTest()
            {
                var originalMatrix = new int[][]
                {
                    new int[] { 1, 2, 3 },
                    new int[] { 4, 5, 6 },
                    new int[] { 7, 8, 9 }
                };

                var rotatedMatrix = new int[][]
                {
                    new int[] { 7, 4, 1 },
                    new int[] { 8, 5, 2 },
                    new int[] { 9, 6, 3 }
                };

                Assert.AreEqual(RotateMatrix_NotInPlace_Clockwise(originalMatrix), rotatedMatrix);
                Assert.AreEqual(RotateMatrix_InPlace_Clockwise(originalMatrix), rotatedMatrix);
            }

            [Test]
            public void Rotate4X4MatrixTest()
            {
                var originalMatrix = new int[][]
                {
                    new int[] {  1,  2,  3,  4 },
                    new int[] {  5,  6,  7,  8 },
                    new int[] {  9, 10, 11, 12 },
                    new int[] { 13, 14, 15, 16 },
                };

                var rotatedMatrix = new int[][]
                {
                    new int[] { 13,  9,  5,  1 },
                    new int[] { 14, 10,  6,  2 },
                    new int[] { 15, 11,  7,  3 },
                    new int[] { 16, 12,  8,  4 },
                };

                Assert.AreEqual(RotateMatrix_NotInPlace_Clockwise(originalMatrix), rotatedMatrix);
                Assert.AreEqual(RotateMatrix_InPlace_Clockwise(originalMatrix), rotatedMatrix);
            }

            [Test]
            public void Rotate5X5MatrixTest()
            {
                var originalMatrix = new int[][]
                {
                    new int[] {  1,  2,  3,  4,  5 },
                    new int[] {  6,  7,  8,  9, 10 },
                    new int[] { 11, 12, 13, 14, 15 },
                    new int[] { 16, 17, 18, 19, 20 },
                    new int[] { 21, 22, 23, 24, 25 },
                };

                var rotatedMatrix = new int[][]
                {
                    new int[] { 21, 16, 11,  6,  1 },
                    new int[] { 22, 17, 12,  7,  2 },
                    new int[] { 23, 18, 13,  8,  3 },
                    new int[] { 24, 19, 14,  9,  4 },
                    new int[] { 25, 20, 15, 10,  5 },
                };

                Assert.AreEqual(RotateMatrix_NotInPlace_Clockwise(originalMatrix), rotatedMatrix);
                Assert.AreEqual(RotateMatrix_InPlace_Clockwise(originalMatrix), rotatedMatrix);
            }
        }
    }
}