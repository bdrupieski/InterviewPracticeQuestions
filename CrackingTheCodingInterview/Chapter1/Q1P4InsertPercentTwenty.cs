using System;
using System.Reflection;
using Common;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Chapter1
{
    /// <summary>
    /// Write a method to replace all spaces in a string with "%20". You may assume that the string has 
    /// sufficient space at the end of the string to hold the additional characters, and that you are given the "true" 
    /// length of the string. (Note: If implementing in Java, please use a character array so that you can 
    /// perform this operation in place.)
    /// 
    /// EXAMPLE
    /// Input: "Mr John Smith    " 
    /// Output: "Mr%20John%20Smith"
    /// </summary>
    public class Q1P4InsertPercentTwenty
    {
        static readonly char[] SpaceSeparator = { ' ' };
        const string Percent20 = "%20";

        public static string InsertPercent20(string s, int length)
        {
            return s.Substring(0, length).Replace(" ", "%20");
        }

        public static string InsertPercent20_Splitting(string s, int length)
        {
            string contentWithoutTrailingSpaces = s.Substring(0, length);
            var tokens = contentWithoutTrailingSpaces.Split(SpaceSeparator);

            return string.Join("%20", tokens);
        }

        // just as fast as InsertPercent20 above
        public static string Insert20Percent_MaybeFaster(string s, int length)
        {
            var resultStringArray = new char[s.Length];

            int j = s.Length - 1;
            for (int i = length - 1; i >= 0; i--)
            {
                if (s[i] != ' ')
                {
                    resultStringArray[j] = s[i];
                }
                else
                {
                    j = j - 2;
                    Percent20.CopyTo(0, resultStringArray, j, Percent20.Length);
                }
                j--;
            }

            return new string(resultStringArray);
        }

        [TestFixture]
        public class Q1P4InsertPercentTwentyTests
        {
            [Test]
            public void InsertPercent20Test()
            {
                foreach (var m in typeof(Q1P4InsertPercentTwenty).PublicStaticMethods())
                {
                    Console.WriteLine("Testing {0}", m);
                    Assert.That(m.Insert20Percent("Mr John Smith    ", 13) == "Mr%20John%20Smith");
                    Assert.That(m.Insert20Percent("", 0) == "");
                    Assert.That(m.Insert20Percent("   ", 1) == "%20");
                    Assert.That(m.Insert20Percent("hello how are you      ", 17) == "hello%20how%20are%20you");
                }
            }

            [Test]
            [Ignore]
            public void TestQ1P4InsertPercentTwentyPerformance1()
            {
                PerformanceHelper.PerformanceTestPublicStaticMethods<Q1P4InsertPercentTwenty>("hello how are you      ", 13);
            }
        }
    }

    public static class Q1P4InsertPercentTwentyMethodInfoExtensions
    {
        public static string Insert20Percent(this MethodInfo m, string s, int length)
        {
            return (string)m.Invoke(null, new object[] { s, length });
        }
    }
}