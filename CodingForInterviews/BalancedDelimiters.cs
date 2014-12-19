using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CodingForInterviews
{
    /// <summary>
    /// For this question, you will parse a string to determine if it contains only "balanced delimiters."
    /// 
    /// A balanced delimiter starts with an opening character ((, [, {), 
    /// ends with a matching closing character (), ], } respectively), 
    /// and has only other matching delimiters in between. 
    /// 
    /// A balanced delimiter may contain any number of balanced delimiters.
    /// </summary>
    public class BalancedDelimiters
    {
        public static List<Delimiter> Delimiters = new List<Delimiter>
        {
            new Delimiter('{', '}'),
            new Delimiter('(', ')'),
            new Delimiter('[', ']'),
        };

        public static Dictionary<char, Delimiter> DelimiterByOpeningCharacter = Delimiters.ToDictionary(x => x.Open);
        public static HashSet<char> OpeningDelimiters = Delimiters.Select(x => x.Open).ToHashSet();
        public static HashSet<char> ClosingDelimiters = Delimiters.Select(x => x.Close).ToHashSet();

        public static bool IsBalanced(string s)
        {
            var stack = new Stack<char>();

            foreach (var character in s)
            {
                if (stack.IsEmpty() && IsClosingDelimiter(character))
                {
                    return false;
                }
                else if (IsOpeningDelimiter(character))
                {
                    stack.Push(character);
                }
                else if (IsClosingDelimiter(character))
                {
                    char openingDelimiterCharacter = stack.Pop();
                    Delimiter d = DelimiterByOpeningCharacter[openingDelimiterCharacter];

                    // the current closing delimiter is not the closing delimiter for the last opening delimiter
                    if (d.Close != character)
                    {
                        return false;
                    }
                }
            }

            // anything left over? then something wasn't closed
            if (stack.Any())
            {
                return false;
            }

            return true;
        }

        private static bool IsOpeningDelimiter(char s)
        {
            return OpeningDelimiters.Contains(s);
        }

        private static bool IsClosingDelimiter(char s)
        {
            return ClosingDelimiters.Contains(s);
        }

        public class Delimiter
        {
            public Delimiter(char open, char close)
            {
                Open = open;
                Close = close;
            }

            public char Open { get; private set; }
            public char Close { get; private set; }
        }

        [TestFixture]
        public class BalancedDelimitersTests
        {
            [Test]
            public void TestIsBalanced()
            {
                Assert.True(IsBalanced("()"));
                Assert.True(IsBalanced("()()()"));
                Assert.True(IsBalanced("()[]{}"));
                Assert.True(IsBalanced("([{}])"));
                Assert.True(IsBalanced("([]{})"));
                Assert.True(IsBalanced("[[[[][]][][[][]][][][[][[]]]]]"));

                Assert.False(IsBalanced(")"));
                Assert.False(IsBalanced("("));
                Assert.False(IsBalanced("([)]"));
                Assert.False(IsBalanced("([]"));
                Assert.False(IsBalanced("[])"));
                Assert.False(IsBalanced("([})"));
                Assert.False(IsBalanced("{{{{}}}"));
                Assert.False(IsBalanced("[][][]]"));
                Assert.False(IsBalanced("[[][][]"));
            }
        }
    }
}