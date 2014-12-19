using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MoreLinq;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Chapter1
{
    /// <summary>
    /// Implement a method to perform basic string compression using the counts of repeated characters. 
    /// For example, the string aabcccccaaa would become a2b1c5a3. If the "compressed" string 
    /// would not become smaller than the original string, your method should return the original string.
    /// </summary>
    public class Q1P5RunLengthEncoding
    {
        public static string RunLengthEncoding_MoreLinqGroupAdjacent(string s)
        {
            var characterGroups = s.GroupAdjacent(x => x);

            var sb = new StringBuilder();
            foreach (var characterGroup in characterGroups)
            {
                sb.Append(string.Format("{0}{1}", characterGroup.Key, characterGroup.Count()));
            }
            string encodedString = sb.ToString();

            if (s.Length < encodedString.Length)
                return s;
            else
                return encodedString;
        }

        public static string RunLengthEncoding(string s)
        {
            var characterGroups = GroupAdjacentCharacters(s);

            var sb = new StringBuilder();
            foreach (var characterGroup in characterGroups)
            {
                sb.Append(string.Format("{0}{1}", characterGroup.Character, characterGroup.Count));
            }
            string encodedString = sb.ToString();

            if (s.Length < encodedString.Length)
                return s;
            else
                return encodedString;
        }

        private static IEnumerable<MyGrouping> GroupAdjacentCharacters(string s)
        {
            var groups = new List<MyGrouping>();
            if (s.Length == 0)
                return groups;

            char lastCharacter = s[0];
            int count = 1;
            for (int i = 1; i < s.Length; i++)
            {
                char currentCharacter = s[i];

                if (currentCharacter == lastCharacter)
                {
                    count++;
                }
                else
                {
                    groups.Add(new MyGrouping { Character = lastCharacter, Count = count });
                    count = 1;
                }

                lastCharacter = currentCharacter;
            }

            groups.Add(new MyGrouping { Character = lastCharacter, Count = count });

            return groups;
        }

        class MyGrouping
        {
            public char Character { get; set; }
            public int Count { get; set; }
        }

        [TestFixture]
        public class Q1P5RunLengthEncodingTests
        {
            [Test]
            public void RunLengthEncodingTest()
            {
                foreach (var m in typeof(Q1P5RunLengthEncoding).PublicStaticMethods())
                {
                    Console.WriteLine("Testing {0}", m);
                    Assert.That(m.RunLengthEncoding("") == "");
                    Assert.That(m.RunLengthEncoding("a") == "a");
                    Assert.That(m.RunLengthEncoding("abc") == "abc");
                    Assert.That(m.RunLengthEncoding("aaaaaaaaaa") == "a10");
                    Assert.That(m.RunLengthEncoding("aabcccccaaa") == "a2b1c5a3");
                    Assert.That(m.RunLengthEncoding("aabbccddeeff") == "a2b2c2d2e2f2");
                    Assert.That(m.RunLengthEncoding("aaaaabaaaaacdefg") == "a5b1a5c1d1e1f1g1");
                    Assert.That(m.RunLengthEncoding("baaaaaaab") == "b1a7b1");
                }
            }

            [Test]
            [Ignore]
            public void TestQ1P5RunLengthEncodingPerformance()
            {
                PerformanceHelper.PerformanceTestPublicStaticMethods<Q1P5RunLengthEncoding>("aabbccddeeff");
            }
        }
    }

    public static class Q1P5RunLengthEncodingMethodInfoExtensions
    {
        public static string RunLengthEncoding(this MethodInfo m, string s)
        {
            return (string)m.Invoke(null, new object[] { s });
        }
    }
}