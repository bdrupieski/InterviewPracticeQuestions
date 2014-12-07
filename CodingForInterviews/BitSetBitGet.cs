using NUnit.Framework;

namespace CodingForInterviews
{
    /// <summary>
    /// This week's problem: Bit Set, Bit Get!
    /// 
    /// This week's exercise is to write two bit manipulation helper functions using only bitwise + bit shift operators:
    /// 
    /// public static boolean getBit(int number, int index)
    /// public static int setBit(int number, int index, boolean set)
    /// 
    /// Note: indices in binary representations start from the right hand side.
    /// 
    /// Get Bit returns whether the bit at a given index is set (a 1 in binary representation).
    /// 
    /// Set Bit modifies the integer number by making the bit at index set (1) or clear (0).
    /// </summary>
    public class BitSetBitGet
    {
        public static bool GetBit(int number, int index)
        {
            number >>= index;

            int endBit = number & 1;

            return endBit == 1;
        }

        public static int SetBit(int number, int index, bool set)
        {
            int mask = 1 << index;

            int result;

            if (set)
            {
                result = number | mask;
            }
            else
            {
                mask = ~mask;
                result = number & mask;
            }

            return result;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void TestGetBit()
            {
                Assert.AreEqual(false, GetBit(12, 0));
                Assert.AreEqual(false, GetBit(12, 1));
                Assert.AreEqual(true, GetBit(12, 2));
                Assert.AreEqual(true, GetBit(12, 3));
            }

            [Test]
            public void TestGetBit2()
            {
                Assert.AreEqual(true, GetBit(341, 0));
                Assert.AreEqual(false, GetBit(341, 1));
                Assert.AreEqual(true, GetBit(341, 2));
                Assert.AreEqual(false, GetBit(341, 3));
                Assert.AreEqual(true, GetBit(341, 4));
                Assert.AreEqual(false, GetBit(341, 5));
                Assert.AreEqual(true, GetBit(341, 6));
                Assert.AreEqual(false, GetBit(341, 7));
            }

            [Test]
            public void TestGetBitAllTrue()
            {
                for (int i = 0; i < 8; i++)
                {
                    Assert.AreEqual(true, GetBit(255, i));
                }
            }

            [Test]
            public void TestSetBit()
            {
                Assert.AreEqual(128, SetBit(0, 7, true));
                Assert.AreEqual(192, SetBit(128, 6, true));
                Assert.AreEqual(224, SetBit(192, 5, true));
                Assert.AreEqual(240, SetBit(224, 4, true));
                Assert.AreEqual(248, SetBit(240, 3, true));
                Assert.AreEqual(252, SetBit(248, 2, true));
                Assert.AreEqual(254, SetBit(252, 1, true));
                Assert.AreEqual(255, SetBit(254, 0, true));
            }

            [Test]
            public void TestSetBitClear()
            {
                Assert.AreEqual(127, SetBit(255, 7, false));
                Assert.AreEqual(63, SetBit(127, 6, false));
                Assert.AreEqual(31, SetBit(63, 5, false));
            }

            [Test]
            public void TestSetBitClear2()
            {
                Assert.AreEqual(247, SetBit(255, 3, false));
                Assert.AreEqual(243, SetBit(247, 2, false));
                Assert.AreEqual(241, SetBit(243, 1, false));
            }
        }
    }
}