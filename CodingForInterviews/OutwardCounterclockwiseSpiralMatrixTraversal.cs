using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace CodingForInterviews
{
    /// <summary>
    /// Write a function that accepts four arguments. 
    /// 
    /// The first two arguments are the size of the grid (h x w), 
    /// filled with ascending integers from left to right, top to bottom, starting from 1. 
    /// 
    /// The next two arguments are is the starting positions, the row (r) and column (c).
    /// 
    /// Return an array of integers obtained by spiraling outward anti-clockwise from the r and c, starting upward.
    /// </summary>
    public class OutwardCounterclockwiseSpiralMatrixTraversal
    {
        public static int[] Spiral(int gridHeight, int gridWidth, int startingRow, int startingCol)
        {
            var visitedNumbers = new List<int>();

            int currentRow = startingRow - 1;
            int currentCol = startingCol - 1;

            var currentDirection = Up;

            int runLength = 1;
            int runLengthUntilNextDirectionChange = 1;

            int totalCellsVisited = 0;

            while (totalCellsVisited != (gridWidth * gridHeight))
            {
                if (currentRow.IsBetween(0, gridHeight - 1) && currentCol.IsBetween(0, gridWidth - 1))
                {
                    visitedNumbers.Add((currentRow * gridWidth) + currentCol + 1);
                    totalCellsVisited++;
                }

                if (runLengthUntilNextDirectionChange == 0)
                {
                    currentDirection = GetNextDirection(currentDirection);
                    if (currentDirection == Up || currentDirection == Down)
                    {
                        runLength++;
                    }
                    runLengthUntilNextDirectionChange = runLength;
                }

                currentCol += currentDirection.X;
                currentRow += currentDirection.Y;

                runLengthUntilNextDirectionChange--;
            }

            return visitedNumbers.ToArray();
        }

        private static Vector2D GetNextDirection(Vector2D currentDirection)
        {
            if (currentDirection == Up)
            {
                return Left;
            }
            else if (currentDirection == Left)
            {
                return Down;
            }
            else if (currentDirection == Down)
            {
                return Right;
            }
            else if (currentDirection == Right)
            {
                return Up;
            }
            else
            {
                throw new ArgumentException("currentDirection not a valid direction");
            }
        }

        private static readonly Vector2D Up = new Vector2D(0, -1);
        private static readonly Vector2D Down = new Vector2D(0, +1);
        private static readonly Vector2D Left = new Vector2D(-1, 0);
        private static readonly Vector2D Right = new Vector2D(+1, 0);

        public class Vector2D : IEquatable<Vector2D>
        {
            public Vector2D(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; private set; }
            public int Y { get; private set; }

            public bool Equals(Vector2D other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return X == other.X && Y == other.Y;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != GetType()) return false;
                return Equals((Vector2D)obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (X * 397) ^ Y;
                }
            }

            public static bool operator ==(Vector2D left, Vector2D right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(Vector2D left, Vector2D right)
            {
                return !Equals(left, right);
            }
        }

        [TestFixture]
        public class OutwardCounterclockwiseSpiralMatrixTraversalTests
        {
            [Test]
            public void Small()
            {
                var small = Spiral(2, 4, 1, 2);
                Assert.AreEqual(small, new[] { 2, 1, 5, 6, 7, 3, 8, 4 });
            }

            [Test]
            public void Big()
            {
                var bigger = Spiral(5, 5, 3, 3);
                Assert.AreEqual(bigger, new[] { 13, 8, 7, 12, 17, 18, 19, 14, 9, 4, 3, 2, 1, 6, 11, 16, 21, 22, 23, 24, 25, 20, 15, 10, 5 });
            }
        }
    }
}