using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Chapter18
{
    /// <summary>
    /// Write a method to shuffle a deck of cards. It must be a perfect shuffle -- in other words,
    /// each of the 52! permutations of the deck has to be equally likely. Assume that you are
    /// given a random number generator which is perfect.
    /// </summary>
    public class Q18P2PerfectCardShuffle
    {
        public class Deck
        {
            private readonly List<Card> _cards;
            public IReadOnlyCollection<Card> Cards => _cards.AsReadOnly();
            private readonly Random _random;

            public Deck()
            {
                _random = new Random(42);
                _cards = new List<Card>();
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    foreach (Value value in Enum.GetValues(typeof(Value)))
                    {
                        _cards.Add(new Card(value, suit));
                    }
                }
            }

            public void ShuffleRecursively()
            {
                ShuffleRecursively(_cards.Count - 1);
            }

            private void ShuffleRecursively(int index)
            {
                if (index == 0)
                    return;

                ShuffleRecursively(index - 1);
                int randomIndexToSwap = _random.Next(0, index);
                _cards.Swap(index, randomIndexToSwap);
            }

            public void ShuffleIteratively()
            {
                for (int i = 1; i < _cards.Count; i++)
                {
                    int randomIndexToSwap = _random.Next(0, i);
                    _cards.Swap(i, randomIndexToSwap);
                }
            }
        }

        public class Card
        {
            public Card(Value value, Suit suit)
            {
                Value = value;
                Suit = suit;
            }

            public Value Value { get; }
            public Suit Suit { get; }

            public override string ToString()
            {
                return $"Value: {Value}, Suit: {Suit}";
            }
        }

        public enum Value
        {
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King,
            Ace
        }

        public enum Suit
        {
            Hearts,
            Spades,
            Clubs,
            Diamonds
        }

        [Test]
        public void RecursivelyShuffle()
        {
            var d = new Deck();
            PrintOutCards(d.Cards);
            d.ShuffleRecursively();
            PrintOutCards(d.Cards);
        }

        [Test]
        public void IterativelyShuffle()
        {
            var d = new Deck();
            PrintOutCards(d.Cards);
            d.ShuffleIteratively();
            PrintOutCards(d.Cards);
        }

        private static void PrintOutCards(IEnumerable<Card> cards)
        {
            Console.WriteLine(string.Join(Environment.NewLine, cards.Select(x => x.ToString())));
            Console.WriteLine();
        }
    }
}