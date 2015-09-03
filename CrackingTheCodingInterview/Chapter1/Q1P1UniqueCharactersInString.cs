using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Chapter1
{
    /// <summary>
    /// Implement an algorithm to determine if a string has all unique characters. What if you cannot use additional data structures?
    /// </summary>
    public class Q1P1UniqueCharactersInString
    {
        public static bool HasAllUniqueCharacters_Linq(string s)
        {
            var strippedOfDuplicates = new string(s.Distinct().ToArray());
            return s == strippedOfDuplicates;
        }

        public static bool HasAllUniqueCharacters_Linq2(string s)
        {
            return s.Length == s.Distinct().Count();
        }

        public static bool HasAllUniqueCharacters_Hash(string s)
        {
            var foundLetters = new HashSet<char>();
            foreach (var character in s)
            {
                if (foundLetters.Contains(character))
                {
                    return false;
                }
                foundLetters.Add(character);
            }
            return true;
        }

        public static bool HasAllUniqueCharacters_Hash2(string s)
        {
            var foundLetters = new HashSet<char>(s);
            return foundLetters.Count == s.Count();
        }

        public static bool HasAllUniqueCharacters_NoAdditionalDataStructures(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                char p = s[i];
                for (int j = i + 1; j < s.Length; j++)
                {
                    char q = s[j];

                    if (p == q)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        [TestFixture]
        public class Q1P1UniqueCharactersInStringTests
        {
            [Test]
            public void UniqueStringTest()
            {
                foreach (var m in typeof(Q1P1UniqueCharactersInString).PublicStaticMethods())
                {
                    Assert.True(m.HasAllUniqueCharacters(""));
                    Assert.True(m.HasAllUniqueCharacters("a"));
                    Assert.True(m.HasAllUniqueCharacters("abcdefghijklmnopqrstuvwxyz"));
                    Assert.True(m.HasAllUniqueCharacters("poiuytrewq"));

                    Assert.False(m.HasAllUniqueCharacters("aa"));
                    Assert.False(m.HasAllUniqueCharacters("iabcdefghi"));
                    Assert.False(m.HasAllUniqueCharacters("asdfasdfasdf"));
                }
            }

            [Test]
            [Ignore]
            public void TestQ1P1UniqueCharactersInStringPerformance1()
            {
                PerformanceHelper.PerformanceTestPublicStaticMethods<Q1P1UniqueCharactersInString>("abcdefghijklmnopqrstuvwxyz");
            }

            [Test]
            [Ignore]
            public void TestQ1P1UniqueCharactersInStringPerformance2()
            {
                PerformanceHelper.PerformanceTestPublicStaticMethods<Q1P1UniqueCharactersInString>("asdflkjhalsdkjfhsdlfkgjdfkljghklj5iw5g78054hg8o7h45gh8754h");
            }

            [Test]
            [Ignore]
            public void TestQ1P1UniqueCharactersInStringPerformance3()
            {
                PerformanceHelper.PerformanceTestPublicStaticMethods<Q1P1UniqueCharactersInString>("12345");
            }
        }
    }

    public static class Q1P1UniqueCharactersInStringMethodInfoExtensions
    {
        public static bool HasAllUniqueCharacters(this MethodInfo m, string s)
        {
            return (bool)m.Invoke(null, new object[] { s });
        }
    }
}