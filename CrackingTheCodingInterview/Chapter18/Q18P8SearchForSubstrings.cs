using System.Collections.Generic;
using System.Linq;
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

        [Test]
        public void ContainsSuffixes()
        {
            string s = "thequickredfoxjumpedoverthelazybrowndog";
            var suffixTree = new SuffixTree(s);

            Assert.True(suffixTree.AllExistAsSubstrings(new []{ "azy", "own", "dog", "la", "br", "fox", "thequick" }));
            Assert.True(suffixTree.AllExistAsSubstrings(new []{ "lazybrowndog", "azybrowndog", "zybrowndog", "ybrowndog" }));
            Assert.True(suffixTree.AllExistAsSubstrings(new []{ "l", "g", "a", "o" }));
        }

        [Test]
        public void DoesNotContainSuffixes()
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
    }
}