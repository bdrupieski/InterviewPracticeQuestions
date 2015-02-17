using System;
using NUnit.Framework;

namespace RandomlyEncounteredQuestions
{
    public class CallTwiceReturnsNegative
    {
        /// <summary>
        /// Design a function f, such that:
        /// 
        /// f(f(n)) == -n
        /// 
        /// Where n is a 32 bit signed integer; you can't use complex numbers arithmetic.
        /// 
        /// If you can't design such a function for the whole range of numbers, design it for the largest range possible.
        /// 
        /// <see cref="http://stackoverflow.com/questions/731832/interview-question-ffn-n"/>
        /// </summary>
        public static dynamic F(dynamic n)
        {
            if (n is int)
            {
                return new Func<int>(() => -n);
            }
            else if (n is Func<int>)
            {
                return n();
            }
            else
            {
                throw new ArgumentException("n is invalid type");
            }
        }

        [Test]
        public void FTest()
        {
            Assert.AreEqual(-3, F(F(3)));
            Assert.AreEqual(3, F(F(-3)));

            Assert.AreEqual(int.MinValue + 1, F(F(int.MaxValue)));
            Assert.AreEqual(int.MaxValue, F(F(int.MinValue + 1)));
        }
    }
}