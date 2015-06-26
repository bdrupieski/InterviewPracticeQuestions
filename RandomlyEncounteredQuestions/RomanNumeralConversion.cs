using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NUnit.Framework;


namespace RandomlyEncounteredQuestions
{
    /// <summary>
    /// 1. Write a method to convert a standard integer to a roman numeral and a method to convert a roman numeral to a standard integer.
    /// 2. Using polymorphism, make the following functional changes: 1000 = P (not M); 50 = B (not L)
    /// 3. Unit tests
    /// </summary>
    public class FunkyRomanNumerals : RomanNumerals
    {
        protected override string Fifty => "B";
        protected override string Thousand => "P";
    }

    public class RomanNumerals
    {
        protected virtual string One => "I";
        protected virtual string Five => "V";
        protected virtual string Ten => "X";
        protected virtual string Fifty => "L";
        protected virtual string Hundred => "C";
        protected virtual string FiveHundred => "D";
        protected virtual string Thousand => "M";

        private readonly Dictionary<int, Dictionary<int, string>> _numberToNumeralMaps;
        private readonly Dictionary<string, int> _numeralToNumberMap;

        public RomanNumerals()
        {
            _numberToNumeralMaps = new Dictionary<int, Dictionary<int, string>>
                {
                    {
                        10, new Dictionary<int, string>
                        {
                            { 0, string.Empty },
                            { 1, One },
                            { 2, One + One },
                            { 3, One + One + One },
                            { 4, One + Five },
                            { 5, Five },
                            { 6, Five + One },
                            { 7, Five + One + One },
                            { 8, Five + One + One + One },
                            { 9, One + Ten },
                        }
                    },
                    {
                        100, new Dictionary<int, string>
                        {
                            { 0, string.Empty },
                            { 10, Ten },
                            { 20, Ten + Ten },
                            { 30, Ten + Ten + Ten },
                            { 40, Ten + Fifty },
                            { 50, Fifty },
                            { 60, Fifty + Ten },
                            { 70, Fifty + Ten + Ten },
                            { 80, Fifty + Ten + Ten + Ten },
                            { 90, Ten + Hundred },
                        }
                    },
                    {
                        1000, new Dictionary<int, string>
                        {
                            { 0, string.Empty },
                            { 100, Hundred },
                            { 200, Hundred + Hundred },
                            { 300, Hundred + Hundred + Hundred },
                            { 400, Hundred + FiveHundred },
                            { 500, FiveHundred },
                            { 600, FiveHundred + Hundred },
                            { 700, FiveHundred + Hundred + Hundred },
                            { 800, FiveHundred + Hundred + Hundred + Hundred },
                            { 900, Hundred + Thousand },
                        }
                    },
                    {
                        10000, new Dictionary<int, string>
                        {
                            { 0, string.Empty },
                            { 1000, Thousand },
                            { 2000, Thousand + Thousand },
                            { 3000, Thousand + Thousand + Thousand },
                        }
                    },
                };

            _numeralToNumberMap = new Dictionary<string, int>
                {
                    { One, 1 },
                    { Five, 5 },
                    { Ten, 10 },
                    { Fifty, 50 },
                    { Hundred, 100 },
                    { FiveHundred, 500 },
                    { Thousand, 1000 },
                };
        }

        /// <summary>
        /// Only converts numbers up to and including 3999.
        /// </summary>
        public string ConvertBaseTenNumberToRomanNumeral(int n)
        {
            var romanNumerals = new List<string>();
            int digitPlace = 10;
            while (n > 0)
            {
                var digit = n % digitPlace;
                var romanNumeral = _numberToNumeralMaps[digitPlace][digit];
                romanNumerals.Add(romanNumeral);

                digitPlace *= 10;
                n -= digit;
            }

            romanNumerals.Reverse();
            return string.Join("", romanNumerals);
        }

        /// <summary>
        /// Only converts roman numerals up to and including MMMCMXCIX (3999).
        /// </summary>
        public int ConvertRomanNumeralToBaseTenNumber(string n)
        {
            int sum = 0;
            int lastBaseTenNumberFound = int.MinValue;

            foreach (var character in n.Reverse())
            {
                int num = _numeralToNumberMap[character.ToString(CultureInfo.InvariantCulture)];

                if (num < lastBaseTenNumberFound)
                {
                    sum -= num;
                }
                else
                {
                    sum += num;
                }

                lastBaseTenNumberFound = num;
            }

            return sum;
        }
    }

    [TestFixture]
    public class RomanNumeralTests
    {
        [Test]
        public void ConvertBaseTenNumberToRomanNumeralAndRomanNumeralToBaseTenNumber()
        {
            var r = new RomanNumerals();

            AssertAreInterconvertible(r, "I", 1);
            AssertAreInterconvertible(r, "II", 2);
            AssertAreInterconvertible(r, "III", 3);
            AssertAreInterconvertible(r, "IV", 4);
            AssertAreInterconvertible(r, "V", 5);
            AssertAreInterconvertible(r, "VI", 6);
            AssertAreInterconvertible(r, "VII", 7);
            AssertAreInterconvertible(r, "VIII", 8);
            AssertAreInterconvertible(r, "IX", 9);
            AssertAreInterconvertible(r, "X", 10);
            AssertAreInterconvertible(r, "XI", 11);
            AssertAreInterconvertible(r, "XII", 12);
            AssertAreInterconvertible(r, "XIII", 13);
            AssertAreInterconvertible(r, "XIV", 14);
            AssertAreInterconvertible(r, "XV", 15);

            AssertAreInterconvertible(r, "XIX", 19);
            AssertAreInterconvertible(r, "XX", 20);
            AssertAreInterconvertible(r, "XXI", 21);

            AssertAreInterconvertible(r, "XCIX", 99);
            AssertAreInterconvertible(r, "C", 100);
            AssertAreInterconvertible(r, "CI", 101);

            AssertAreInterconvertible(r, "CMXCIX", 999);
            AssertAreInterconvertible(r, "M", 1000);
            AssertAreInterconvertible(r, "MI", 1001);

            AssertAreInterconvertible(r, "LXXIV", 74);
            AssertAreInterconvertible(r, "MCMIV", 1904);
            AssertAreInterconvertible(r, "MDCCCLXXIV", 1874);
            AssertAreInterconvertible(r, "CXVII", 117);
            AssertAreInterconvertible(r, "DCLXXXII", 682);

            AssertAreInterconvertible(r, "MDCCCXLI", 1841);
            AssertAreInterconvertible(r, "CDXCVI", 496);
            AssertAreInterconvertible(r, "DI", 501);
            AssertAreInterconvertible(r, "CXLIX", 149);
            AssertAreInterconvertible(r, "CCCLXX", 370);

            AssertAreInterconvertible(r, "MMMCMXCIX", 3999);
            Assert.Throws<KeyNotFoundException>(() => r.ConvertBaseTenNumberToRomanNumeral(4000));
        }

        [Test]
        public void ConvertNumbersWithDifferentRomanNumerals()
        {
            var r = new FunkyRomanNumerals();

            AssertAreInterconvertible(r, "I", 1);
            AssertAreInterconvertible(r, "II", 2);
            AssertAreInterconvertible(r, "III", 3);
            AssertAreInterconvertible(r, "IV", 4);
            AssertAreInterconvertible(r, "V", 5);
            AssertAreInterconvertible(r, "VI", 6);
            AssertAreInterconvertible(r, "VII", 7);
            AssertAreInterconvertible(r, "VIII", 8);
            AssertAreInterconvertible(r, "IX", 9);
            AssertAreInterconvertible(r, "X", 10);
            AssertAreInterconvertible(r, "XI", 11);
            AssertAreInterconvertible(r, "XII", 12);
            AssertAreInterconvertible(r, "XIII", 13);
            AssertAreInterconvertible(r, "XIV", 14);
            AssertAreInterconvertible(r, "XV", 15);

            AssertAreInterconvertible(r, "XIX", 19);
            AssertAreInterconvertible(r, "XX", 20);
            AssertAreInterconvertible(r, "XXI", 21);

            AssertAreInterconvertible(r, "XCIX", 99);
            AssertAreInterconvertible(r, "C", 100);
            AssertAreInterconvertible(r, "CI", 101);

            AssertAreInterconvertible(r, "CPXCIX", 999);
            AssertAreInterconvertible(r, "P", 1000);
            AssertAreInterconvertible(r, "PI", 1001);

            AssertAreInterconvertible(r, "BXXIV", 74);
            AssertAreInterconvertible(r, "PCPIV", 1904);
            AssertAreInterconvertible(r, "PDCCCBXXIV", 1874);
            AssertAreInterconvertible(r, "CXVII", 117);
            AssertAreInterconvertible(r, "DCBXXXII", 682);

            AssertAreInterconvertible(r, "PDCCCXBI", 1841);
            AssertAreInterconvertible(r, "CDXCVI", 496);
            AssertAreInterconvertible(r, "DI", 501);
            AssertAreInterconvertible(r, "CXBIX", 149);
            AssertAreInterconvertible(r, "CCCBXX", 370);

            AssertAreInterconvertible(r, "PPPCPXCIX", 3999);
            Assert.Throws<KeyNotFoundException>(() => r.ConvertBaseTenNumberToRomanNumeral(4000));
        }

        private static void AssertAreInterconvertible(RomanNumerals r, string romanNumeral, int baseTenNumber)
        {
            Assert.AreEqual(romanNumeral, r.ConvertBaseTenNumberToRomanNumeral(baseTenNumber));
            Assert.AreEqual(baseTenNumber, r.ConvertRomanNumeralToBaseTenNumber(romanNumeral));
        }
    }
}