using NUnit.Framework;

namespace CrackingTheCodingInterview.Chapter1
{
    /// <summary>
    /// Assume you have a method isSubstring which checks if one word is a substring of 
    /// another. Given two strings, s1 and s2, write code to check if s2 is a rotation 
    /// of s1 using only one call to isSubstring.
    /// (i.e., “waterbottle” is a rotation of “erbottlewat”)
    /// </summary>
    public class Q1P8
    {
        public static bool IsRotated(string maybeRotated, string s)
        {
            if (maybeRotated.Length != s.Length || s.Length == 0)
                return false;

            var monsterString = maybeRotated + maybeRotated;
            return IsSubstring(s, monsterString);
        }

        public static bool IsSubstring(string sub, string super)
        {
            return super.Contains(sub);
        }

        [TestFixture]
        public class Question1P8Tests
        {
            [Test]
            public void IsRotatedTest()
            {
                Assert.True(IsRotated("erbottlewat", "waterbottle"));
                Assert.True(IsRotated("waterbottle", "waterbottle")); // should this really pass?

                Assert.False(IsRotated("", ""));
                Assert.False(IsRotated("ermelon", "watermelon"));
                Assert.False(IsRotated("ermelonwater", "watermelon"));
            }

            [Test]
            public void IsSubstringTest()
            {
                Assert.True(IsSubstring("", ""));
                Assert.True(IsSubstring("he", "hello"));
                Assert.True(IsSubstring("yes", "asdhfkjsd_yes_adsjkfh"));
                Assert.False(IsSubstring("no", "asdhfkjsd_yes_adsjkfh"));
            }
        }
    }
}