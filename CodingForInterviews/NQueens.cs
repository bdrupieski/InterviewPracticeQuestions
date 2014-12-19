using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace CodingForInterviews
{
    public class NQueens
    {
        private static List<int[]> FindNQueensSolutions(int boardSize)
        {
            var board = new int[boardSize];
            var solutions = new List<int[]>();

            GetPlacements(board, 0, solutions);

            return solutions;
        }

        private static void GetPlacements(int[] board, int currentColumn, List<int[]> solutions)
        {
            if (currentColumn == board.Length)
            {
                var solution = new int[board.Length];
                Array.Copy(board, solution, solution.Length);
                solutions.Add(solution);
            }
            else
            {
                for (int i = 0; i < board.Length; i++)
                {
                    board[currentColumn] = i;
                    if (IsValidPlacement(board, currentColumn))
                    {
                        GetPlacements(board, currentColumn + 1, solutions);
                    }
                }
            }
        }

        private static bool IsValidPlacement(int[] board, int columnToCheckUpTo)
        {
            for (int i = 0; i <= columnToCheckUpTo; i++)
            {
                for (int j = i + 1; j <= columnToCheckUpTo; j++)
                {
                    if (board[i] == board[j])
                    {
                        return false;
                    }
                    if (Math.Abs(i - j) == Math.Abs(board[i] - board[j]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        [TestFixture]
        public class NQueensTests
        {
            [Test]
            public void EightQueens()
            {
                var solutions = FindNQueensSolutions(8);
                Assert.AreEqual(solutions.Count, 92);

                foreach (var solution in solutions)
                {
                    Console.WriteLine(string.Join(", ", solution));
                }
            }

            [Test]
            public void TwelveQueens()
            {
                var solutions = FindNQueensSolutions(12);
                Assert.AreEqual(solutions.Count, 14200);
            }
        }
    }
}