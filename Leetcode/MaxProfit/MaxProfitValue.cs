using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Leetcode.MaxProfit
{
    //Say you have an array prices for which the ith element is the price of a given stock on day i.
    //Design an algorithm to find the maximum profit.You may complete as many transactions as you like (i.e., buy one and sell one share of the stock multiple times).
    //Note: You may not engage in multiple transactions at the same time(i.e., you must sell the stock before you buy again).

    //Constraints
    //1 <= prices.length <= 3 * 10 ^ 4
    //0 <= prices[i] <= 10 ^ 4

    public class MaxProfitTests
    {
        [Theory]
        [InlineData(new[] { 7, 1, 5, 3, 6, 4 }, 7)]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, 4)]
        [InlineData(new[] { 1, 2 }, 1)]
        [InlineData(new[] { 1 }, 0)]
        public void Test_PeakValley(int[] prices, int expectedMaxProfit)
        {
            IMaxProfitNaiveSolution solution = new MaxProfitPeakValleySolution();

            var output = solution.MaxProfit(prices);

            output.Should().Be(expectedMaxProfit);

        }
    }

    public interface IMaxProfitNaiveSolution
    {
        int MaxProfit(int[] prices);
    }


    /// <summary>
    /// Complexity Analysis
    /// Time complexity : O(n)O(n). Single pass.
    /// Space complexity : O(1)O(1). Constant space required.
    /// </summary>
    public class MaxProfitPeakValleySolution : IMaxProfitNaiveSolution
    {
        public int MaxProfit(int[] prices)
        {
            if (prices.Length == 0)
            {
                return 0;
            }

            int i = 0, buy = 0, sell = 0, profit = 0;
            int len = prices.Length - 1;
            while (i < len)
            {
                while (i < len && prices[i] >= prices[i + 1])
                {
                    i++;
                }
                buy = prices[i];

                while (i < len && prices[i] < prices[i + 1])
                {
                    i++;
                }
                sell = prices[i];

                profit += sell - buy;
            }

            return profit;

        }
    }
}
