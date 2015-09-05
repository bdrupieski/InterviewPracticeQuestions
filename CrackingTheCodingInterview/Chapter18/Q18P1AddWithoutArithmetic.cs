using NUnit.Framework;

namespace CrackingTheCodingInterview.Chapter18
{
    /// <summary>
    /// Write a function that adds two numbers. You should not use + or any arithmetic operators.
    /// </summary>
    public class Q18P1AddWithoutArithmetic
    {
        public static int Add(int a, int b)
        {
            while (b != 0)
            {
                int sum = a ^ b;
                int carry = (a & b) << 1;
                a = sum;
                b = carry;
            }
            return a;
        }

        [Test]
        public void AddWithoutArithmeticTest()
        {
            Assert.AreEqual(Add(0, 0), 0);
            Assert.AreEqual(Add(0, 1), 1);
            Assert.AreEqual(Add(1, 0), 1);
            Assert.AreEqual(Add(1, 1), 2);
            Assert.AreEqual(Add(1, 3), 4);
            Assert.AreEqual(Add(3, 1), 4);
            Assert.AreEqual(Add(17, 3), 20);
            Assert.AreEqual(Add(3, 17), 20);
            Assert.AreEqual(Add(245, 1), 246);
            Assert.AreEqual(Add(1, 245), 246);
            Assert.AreEqual(Add(1024, 1024), 2048);
            Assert.AreEqual(Add(123456789, 123456789), 246913578);
            Assert.AreEqual(Add(int.MaxValue - 1, 1), int.MaxValue);
            Assert.AreEqual(Add(1, int.MaxValue - 1), int.MaxValue);
            Assert.AreEqual(Add(int.MaxValue / 2, int.MaxValue / 2), int.MaxValue - 1);
        }
    }
}