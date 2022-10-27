using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace RandomlyEncounteredQuestions
{
    /// <summary>
    /// Given an array that represents stock prices over time, write a
    /// function that calculates the amount for the best possible trade
    /// from buying at a given time and selling at a later time.
    /// No shorting (selling first then buying back later).
    ///
    /// For example, using these prices:
    /// { 50, 20, 40, 70, 100, 40 }
    /// The best possible single trade would be 100 - 20 = 80.
    /// </summary>
    public class StockBestTradeAmount
    {
        public static int BestTradeAmountNSquared(int[] prices)
        {
            int bestProfit = 0;

            for (int i = 0; i < prices.Length - 1; i++)
            {
                for (int j = i + 1; j < prices.Length; j++)
                {
                    var buyPrice = prices[i];
                    var sellPrice = prices[j];

                    var tradeAmount = sellPrice - buyPrice;

                    bestProfit = Math.Max(bestProfit, tradeAmount);
                }
            }

            return bestProfit;
        }

        /// <summary>
        /// This works by using two additional arrays to track
        /// 1. the lowest possible price we've seen in the past at a given index (inclusive of the present), and
        /// 2. the highest possible price we'll see in the future from a given index (inclusive of the present).
        ///
        /// This is what the lowest and highest prices would look
        /// like for a sample set of prices:
        /// 
        /// prices { 50,  20,  40,  70,  100, 40 }
        ///
        /// low    { 50,  20,  20,  20,  20,  20 }
        ///        ---------→
        ///
        /// high   { 100, 100, 100, 100, 100, 40 }
        ///                             ←---------
        ///
        /// For the "low" array, it changes to 20 at index 1 because 20 is
        /// less than 50 so that makes it the lowest value we've seen so far.
        /// It stays at 20 for the rest of the array because that remains the lowest
        /// value we encounter when iterating from the left.
        ///
        /// For the "high" array, it's 40 at the end because at that last
        /// index that's the highest price we'll see from then on into the future.
        /// Then working backward, it changes to 100 at index 4 and stays 100
        /// back to the start of the array, because at each of those positions
        /// 100 is the highest price we'll encounter in the future.
        ///
        /// This iterates over the array 3 times, but it could be cut down
        /// to 2 since we don't actually need to build the lowestFromLeft array.
        /// When using the two arrays in the last loop, we can get the
        /// information the lowestFromLeft array is giving us directly
        /// from the prices array.
        /// </summary>
        public static int BestTradeAmountLinear(int[] prices)
        {
            var lowestFromLeft = new int[prices.Length];
            var highestFromRight = new int[prices.Length];

            // record the lowest price we've seen before a given index (in other words, in the "past")
            lowestFromLeft[0] = prices[0];
            for (int i = 1; i < prices.Length; i++)
            {
                lowestFromLeft[i] = Math.Min(prices[i], lowestFromLeft[i - 1]);
            }

            // record the highest price we'll see after a given index (the "future")
            highestFromRight[prices.Length - 1] = prices[prices.Length - 1];
            for (int i = prices.Length - 2; i >= 0; i--)
            {
                highestFromRight[i] = Math.Max(prices[i], highestFromRight[i + 1]);
            }

            int bestProfit = 0;

            // Traverse the two arrays we've built that at each index will
            // tell us the lowest price in the past and highest price in the
            // future we'll see at that index. 
            for (int i = 0; i < prices.Length - 1; i++)
            {
                int bestProfitAtIndex = highestFromRight[i] - lowestFromLeft[i];

                bestProfit = Math.Max(bestProfit, bestProfitAtIndex);
            }

            return bestProfit;
        }

        /// <summary>
        /// Same solution as <see cref="BestTradeAmountLinear"/> but with 2 passes
        /// through the array instead of 3.
        /// </summary>
        public static int BestTradeAmountLinear2(int[] prices)
        {
            var highestFromRight = new int[prices.Length];

            highestFromRight[prices.Length - 1] = prices[prices.Length - 1];
            for (int i = prices.Length - 2; i >= 0; i--)
            {
                highestFromRight[i] = Math.Max(prices[i], highestFromRight[i + 1]);
            }

            int bestProfit = 0;

            for (int i = 0; i < prices.Length - 1; i++)
            {
                int bestProfitAtIndex = highestFromRight[i] - prices[i];

                bestProfit = Math.Max(bestProfit, bestProfitAtIndex);
            }

            return bestProfit;
        }

        [Test]
        public void StockPriceBestProfitTest()
        {
            var bestAmountAndPrices = new List<(int bestAmount, int[] prices)>
            {
                (1100, new[] { 1201, 101, 1198, 299, 298, 400, 100, 500, 1200, 1, 50 }),
                (4, new[] { 1, 2, 3, 4, 5 }),
                (0, new[] { 5, 4, 3, 2, 1 }),
                (0, new[] { 0, 0, 0, 0, 0 }),
                (0, new[] { 5, 5, 5, 5, 5 }),
                (100, new[] { 10, 5, 30, 0, 50, 100, 10, 5, 30 }),
                (100, new[] { 0, 90, 10, 80, 20, 100 }),
                (80, new[] { 50, 20, 40, 70, 100, 40 }),
                (10, new[] { 0, 0, 0, 10 }),
                (10, new[] { 0, 10, 0, 0 }),
            };

            foreach (var (bestAmount, prices) in bestAmountAndPrices)
            {
                Assert.AreEqual(bestAmount, BestTradeAmountNSquared(prices));
                Assert.AreEqual(bestAmount, BestTradeAmountLinear(prices));
                Assert.AreEqual(bestAmount, BestTradeAmountLinear2(prices));
            }
        }
    }
}