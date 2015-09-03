using System;
using System.Reflection;
using Common;
using NUnit.Framework;

namespace RandomlyEncounteredQuestions
{
    public class AddWithoutUsingAdditionOperator
    {
        public static int AddUsingMinus(int a, int b)
        {
            return a - -b;
        }

        public static int AddUsingLoopAndIncrement(int a, int b)
        {
            while (b > 0)
            {
                b--;
                a++;
            }

            return a;
        }

        public static int AddUsingBitFiddling(int a, int b)
        {
            while (b != 0)
            {
                Console.WriteLine($"A: {Convert.ToString(a, 2).PadLeft(8, '0')}");
                Console.WriteLine($"B: {Convert.ToString(b, 2).PadLeft(8, '0')}");
                Console.WriteLine("-----------");
                int carry = (a & b) << 1;
                int sum = a ^ b;
                a = sum;
                b = carry;
            }

            Console.WriteLine($"A: {Convert.ToString(a, 2).PadLeft(8, '0')}");
            Console.WriteLine($"B: {Convert.ToString(b, 2).PadLeft(8, '0')}");
            return a;
        }

        [Test]
        public void AdditionTests()
        {
            foreach (var m in typeof(AddWithoutUsingAdditionOperator).PublicStaticMethods())
            {
                Assert.AreEqual(m.Add(14, 3), 17);
            }
        }
    }

    public static class AddWithoutUsingAdditionOperatorMethodInfoExtensions
    {
        public static int Add(this MethodInfo m, int a, int b)
        {
            return (int)m.Invoke(null, new object[] { a, b });
        }
    }
}