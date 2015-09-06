using System.Collections.Generic;
using System.Linq;
using Common;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Chapter18
{
    /// <summary>
    /// Given a string s and an array of smaller strings T, design a method to search s for each small string in T.
    /// </summary>
    public class Q18P8SearchForSubstrings
    {
        public class SuffixTree
        {
            private readonly SuffixTreeNode _root;

            public SuffixTree(string s)
            {
                _root = new SuffixTreeNode();
                for (int i = 0; i < s.Length; i++)
                {
                    string suffix = s.Substring(i);
                    _root.Insert(suffix);
                }
            }

            public bool AllExistAsSubstrings(IEnumerable<string> t)
            {
                return t.All(ExistsAsSubstring);
            }

            private bool ExistsAsSubstring(string t)
            {
                SuffixTreeNode currentNodeToSearch = _root;
                foreach (var c in t)
                {
                    if (currentNodeToSearch.Children.ContainsKey(c))
                    {
                        currentNodeToSearch = currentNodeToSearch.Children[c];
                    }
                    else
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public class SuffixTreeNode
        {
            public Dictionary<char, SuffixTreeNode> Children { get; }

            public SuffixTreeNode()
            {
                Children = new Dictionary<char, SuffixTreeNode>();
            } 

            public void Insert(string s)
            {
                if (s.Length == 0)
                    return;

                char c = s[0];

                if (!Children.ContainsKey(c))
                {
                    Children[c] = new SuffixTreeNode();
                }

                Children[c].Insert(s.Substring(1));
            }
        }

        public static bool SearchUsingStringContains(string s, IEnumerable<string> t)
        {
            return t.All(s.Contains);
        }

        public class PrecomputedHashes
        {
            private readonly HashSet<int> _allSubstringHashes;

            public PrecomputedHashes(string s)
            {
                _allSubstringHashes = new HashSet<int>();
                for (int i = 0; i < s.Length; i++)
                {
                    for (int j = i; j <= s.Length; j++)
                    {
                        var substring = s.Substring(i, j - i);
                        _allSubstringHashes.Add(substring.GetHashCode());
                    }
                    
                }
            }

            public bool AllExistAsSubstrings(IEnumerable<string> t)
            {
                return t.All(x => _allSubstringHashes.Contains(x.GetHashCode()));
            }
        }

        [Test]
        public void ContainsForSuffixTree()
        {
            string s = "thequickredfoxjumpedoverthelazybrowndog";
            var suffixTree = new SuffixTree(s);

            Assert.True(suffixTree.AllExistAsSubstrings(new []{ "azy", "own", "dog", "la", "br", "fox", "thequick" }));
            Assert.True(suffixTree.AllExistAsSubstrings(new []{ "lazybrowndog", "azybrowndog", "zybrowndog", "ybrowndog" }));
            Assert.True(suffixTree.AllExistAsSubstrings(new []{ "l", "g", "a", "o" }));
        }

        [Test]
        public void DoesNotContainForSuffixTree()
        {
            string s = "thequickredfoxjumpedoverthelazybrowndog";
            var suffixTree = new SuffixTree(s);

            Assert.False(suffixTree.AllExistAsSubstrings(new[] { "purple" }));
            Assert.False(suffixTree.AllExistAsSubstrings(new[] { "mountain" }));
            Assert.False(suffixTree.AllExistAsSubstrings(new[] { "majesties" }));

            Assert.False(suffixTree.AllExistAsSubstrings(new[] { "thequickrd" }));
            Assert.False(suffixTree.AllExistAsSubstrings(new[] { "dogm" }));
            Assert.False(suffixTree.AllExistAsSubstrings(new[] { "thequickredfoxjumpedoverthelazybrowndog " }));
        }

        [Test]
        public void ContainsForPrecomputedHashes()
        {
            string s = "thequickredfoxjumpedoverthelazybrowndog";
            var precomputedHashes = new PrecomputedHashes(s);

            Assert.True(precomputedHashes.AllExistAsSubstrings(new[] { "azy", "own", "dog", "la", "br", "fox", "thequick" }));
            Assert.True(precomputedHashes.AllExistAsSubstrings(new[] { "lazybrowndog", "azybrowndog", "zybrowndog", "ybrowndog" }));
            Assert.True(precomputedHashes.AllExistAsSubstrings(new[] { "l", "g", "a", "o" }));
        }

        [Test]
        public void DoesNotContainForForPrecomputedHashes()
        {
            string s = "thequickredfoxjumpedoverthelazybrowndog";
            var precomputedHashes = new PrecomputedHashes(s);

            Assert.False(precomputedHashes.AllExistAsSubstrings(new[] { "purple" }));
            Assert.False(precomputedHashes.AllExistAsSubstrings(new[] { "mountain" }));
            Assert.False(precomputedHashes.AllExistAsSubstrings(new[] { "majesties" }));

            Assert.False(precomputedHashes.AllExistAsSubstrings(new[] { "thequickrd" }));
            Assert.False(precomputedHashes.AllExistAsSubstrings(new[] { "dogm" }));
            Assert.False(precomputedHashes.AllExistAsSubstrings(new[] { "thequickredfoxjumpedoverthelazybrowndog " }));
        }

        [Test]
        public void PerformanceComparisonSmallString()
        {
            string s = "smallstring";
            string[] t = { "small", "mall", "string", "str", "lls" };
            var suffixTree = new SuffixTree(s);
            var precomputedHashes = new PrecomputedHashes(s);

            PerformanceHelper.PerformanceTestAction(() => suffixTree.AllExistAsSubstrings(t), "SuffixTree small string");
            PerformanceHelper.PerformanceTestAction(() => SearchUsingStringContains(s, t), "String.Contains small string");
            PerformanceHelper.PerformanceTestAction(() => precomputedHashes.AllExistAsSubstrings(t), "PrecomputedHashes small string");
        }

        [Test]
        public void PerformanceComparisonLongString()
        {
            string s = "thequickredfoxjumpedoverthelazybrowndogandthatishowyoudoitdontyouknowthattheworldisroundyoushouldyouknowifyoucanuseawebbrowsertonavigatetogithubdotcomandseethistext";
            string[] t =
            {
                "redfoxjumpedovert", "azybrow", "ndogandthatishowyoudoi", "quickredfoxjum", "oudoitdontyoukn", "otcoman", "useawebbrows",
                "thequickredfoxjumpedoverthelazybrowndogandthatishowyoudoitdontyouknowthattheworldisroundyoushouldyouknowifyoucanuseawebbrowsertonavigatetogithubdotcomandseethistex",
                "quickredfoxjumpedoverthelazybrowndogandthatishowyoudoitdontyouknowthattheworldisroundyoushouldyouknowifyoucanuseawebbrowsertonavigatetogithubdotcomandseethistext",
                "ogandthatishowyoudoitdontyouknowthattheworldisroundyoushouldyouknowifyoucanuseawebbrowsertonavigatetogithubdotcomandseethist"
            };

            var suffixTree = new SuffixTree(s);
            var precomputedHashes = new PrecomputedHashes(s);
            int numTimes = 100000;

            PerformanceHelper.PerformanceTestAction(() => suffixTree.AllExistAsSubstrings(t), "SuffixTree long string", numTimes);
            PerformanceHelper.PerformanceTestAction(() => SearchUsingStringContains(s, t), "String.Contains long string", numTimes);
            PerformanceHelper.PerformanceTestAction(() => precomputedHashes.AllExistAsSubstrings(t), "String.Contains long string", numTimes);
        }
    }
}