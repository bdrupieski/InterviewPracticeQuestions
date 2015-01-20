using System.Linq;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Chapter4
{
    public class Q4P3MinimalHeightBinarySearchTree
    {
        public static BinaryTreeNode<int> CreateMinimalHeightBinarySearchTree(int[] values)
        {
            if (values.Length == 0)
            {
                return null;
            }
            else if (values.Length == 1)
            {
                return new BinaryTreeNode<int> { Data = values[0] };
            }
            else
            {
                int middleIndex = values.Length / 2;
                int[] leftSide = values.Take(middleIndex).ToArray();
                int[] rightSide = values.Skip(middleIndex + 1).Take(values.Length - middleIndex + 1).ToArray();
                var node = new BinaryTreeNode<int>
                {
                    Data = values[middleIndex],
                    Left = CreateMinimalHeightBinarySearchTree(leftSide),
                    Right = CreateMinimalHeightBinarySearchTree(rightSide)
                };

                return node;
            }
        }

        [TestFixture]
        public class Q4P3MinimalHeightBinarySearchTreeTests
        {
            [Test]
            public void MinimalHeightBinarySearchTreeTest()
            {
                var nums = new[] { 1, 4, 6, 9, 10, 15, 19, 20, 54, 55 };

                var tree = CreateMinimalHeightBinarySearchTree(nums);

                tree.Print();

                Assert.True(Q4P1IsBinaryTreeBalanced.IsBalanced(tree));
                Assert.AreEqual(4, Q4P1IsBinaryTreeBalanced.GetHeightOfTree(tree));
            }

            [Test]
            public void MinimalHeightBinarySearchTreeTest2()
            {
                var nums = new[] { 1 };

                var tree = CreateMinimalHeightBinarySearchTree(nums);

                tree.Print();

                Assert.True(Q4P1IsBinaryTreeBalanced.IsBalanced(tree));
                Assert.AreEqual(1, Q4P1IsBinaryTreeBalanced.GetHeightOfTree(tree));
            }

            [Test]
            public void MinimalHeightBinarySearchTreeTest3()
            {
                var nums = new[] { 1, 4, 8 };

                var tree = CreateMinimalHeightBinarySearchTree(nums);

                tree.Print();

                Assert.True(Q4P1IsBinaryTreeBalanced.IsBalanced(tree));
                Assert.AreEqual(2, Q4P1IsBinaryTreeBalanced.GetHeightOfTree(tree));
            }

            [Test]
            public void MinimalHeightBinarySearchTreeTest4()
            {
                var nums = new[] { 1, 4, 8 };

                var tree = CreateMinimalHeightBinarySearchTree(nums);

                tree.Print();

                Assert.True(Q4P1IsBinaryTreeBalanced.IsBalanced(tree));
                Assert.AreEqual(2, Q4P1IsBinaryTreeBalanced.GetHeightOfTree(tree));
            }

            [Test]
            public void MinimalHeightBinarySearchTreeTest5()
            {
                for (int i = 1; i < 20; i++)
                {
                    int max = 2 << i - 1;
                    var nums = Enumerable.Range(0, max).ToArray();

                    var tree = CreateMinimalHeightBinarySearchTree(nums);

                    Assert.True(Q4P1IsBinaryTreeBalanced.IsBalanced(tree));
                    Assert.AreEqual(i + 1, Q4P1IsBinaryTreeBalanced.GetHeightOfTree(tree));
                }
            }
        }
    }
}