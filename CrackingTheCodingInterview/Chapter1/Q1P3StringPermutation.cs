using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Chapter1
{
    /// <summary>
    /// Given two strings, write a method to determine if one is a permutation of the other.
    /// </summary>
    public class Q1P3StringPermutation
    {
        public static bool ArePermutations_Sorting(string a, string b)
        {
            if (a.Length != b.Length)
                return false;

            return a.OrderBy(x => x).Zip(b.OrderBy(x => x), (x, y) => x == y).All(x => x);
        }

        public static bool ArePermutations_SortingFaster(string a, string b)
        {
            if (a.Length != b.Length)
                return false;

            var aCharArray = a.ToArray();
            var bCharArray = b.ToArray();

            Array.Sort(aCharArray);
            Array.Sort(bCharArray);

            for (int i = 0; i < aCharArray.Length; i++)
            {
                if (aCharArray[i] != bCharArray[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool ArePermutations_CountingFrequencies(string a, string b)
        {
            if (a.Length != b.Length)
                return false;

            var aCharCounts = GetCharacterFrequencies(a);
            var bCharCounts = GetCharacterFrequencies(b);

            var dictEqualityComparer = new DictionaryEqualityComparer<char, int>();
            return dictEqualityComparer.Equals(aCharCounts, bCharCounts);
        }

        private static Dictionary<char, int> GetCharacterFrequencies(string s)
        {
            var frequencies = new Dictionary<char, int>();
            foreach (var c in s)
            {
                if (frequencies.ContainsKey(c))
                {
                    frequencies[c]++;
                }
                else
                {
                    frequencies[c] = 1;
                }
            }
            return frequencies;
        }

        [TestFixture]
        public class Q1P3StringPermutationTests
        {
            [Test]
            public void PermutationsTest()
            {
                foreach (var m in typeof(Q1P3StringPermutation).PublicStaticMethods())
                {
                    Assert.True(m.ArePermutations("", ""));
                    Assert.True(m.ArePermutations("a", "a"));
                    Assert.True(m.ArePermutations("123456789", "918273645"));
                    Assert.True(m.ArePermutations("qwerty", "qwerty"));
                    Assert.True(m.ArePermutations("qwerty", "ytrewq"));
                    Assert.True(m.ArePermutations("abcccc", "ccccba"));
                    Assert.True(m.ArePermutations("great scott", "scott great"));

                    Assert.False(m.ArePermutations("asdf", "asdfasdf"));
                    Assert.False(m.ArePermutations("asdfasdf", "asdf"));
                    Assert.False(m.ArePermutations("a", "b"));
                    Assert.False(m.ArePermutations("b", "a"));
                    Assert.False(m.ArePermutations("aa", "a"));
                    Assert.False(m.ArePermutations("a", "aa"));
                    Assert.False(m.ArePermutations("something", ""));
                    Assert.False(m.ArePermutations("", "something"));

                    Assert.False(m.ArePermutations("abunchofdifferentcharacters", "asdfasdfasdfasdfasdfasdfasd"));
                }
            }

            [Test]
            [Ignore]
            public void TestQ1P3StringPermutationPerformance1()
            {
                PerformanceHelper.PerformanceTestPublicStaticMethods<Q1P3StringPermutation>("abcccc", "ccccba");
            }

            [Test]
            [Ignore]
            public void TestQ1P3StringPermutationPerformance2()
            {
                PerformanceHelper.PerformanceTestPublicStaticMethods<Q1P3StringPermutation>(
                    "abccccabccccabccccabccccabccccabccccabccccabcccc", 
                    "ccccbaccccbaccccbaccccbaccccbaccccbaccccbaccccba");
            }
        }
    }

    public static class Q1P3StringPermutationMethodInfoExtensions
    {
        public static bool ArePermutations(this MethodInfo m, string a, string b)
        {
            return (bool)m.Invoke(null, new object[] { a, b });
        }
    }
}