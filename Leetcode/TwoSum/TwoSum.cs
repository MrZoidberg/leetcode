using System;
using Xunit;
using FluentAssertions;
using System.Collections;
using C5;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Attributes;

namespace Leetcode.TwoSum
{
    /// https://leetcode.com/problems/two-sum/
    /// Given an array of integers, return indices of the two numbers such that they add up to a specific target.
    ///
    ///    You may assume that each input would have exactly one solution, and you may not use the same element twice.
    ///
    ///    Example:
    ///    Given nums = [2, 7, 11, 15], target = 9,
    ///
    ///   Because nums[0] + nums[1] = 2 + 7 = 9,
    ///    return [0, 1].

    public class TwoSumTests
    {
        [Theory]
        [InlineData(new[] { 3, 3 }, 6, new[] { 0, 1 })]
        [InlineData(new[] { 2, 7, 11, 15 }, 9, new[] { 0, 1 })]
        [InlineData(new[] { 2, 1, 0, 7, 11, 15 }, 2, new[] { 0, 2 })]
        [InlineData(new[] { 0, 1, 4, 7, 11, 15 }, 16, new[] { 1, 5 })]
        [InlineData(new[] { -1, -2, -3, -4, -5 }, -8, new[] { 2, 4 })]
        [InlineData(new[] { -3, 4, 3, 90 }, 0, new[] { 0, 2 })]
        public void Test(int[] nums, int target, int[] expectedResult)
        {
            ITwoSumSolution solution = new TwoSumSolution();
            int[] result = solution.TwoSum(nums, target);

            result.Should().Equal(expectedResult);
        }

        [Benchmark]
        public void Perf_Test()
        {
            ITwoSumSolution solution = new TwoSumSolution();
            _ = solution.TwoSum(new[] { 0, 1, 4, 7, 11, 15 }, 16);
        }

        [Benchmark]
        public void Perf_Test_C5()
        {
            ITwoSumSolution solution = new TwoSumSolution_C5();
            _ = solution.TwoSum(new[] { 0, 1, 4, 7, 11, 15 }, 16);
        }


        [Theory]
        [InlineData(new[] { 3, 3 }, 6, new[] { 0, 1 })]
        [InlineData(new[] { 2, 7, 11, 15 }, 9, new[] { 0, 1 })]
        [InlineData(new[] { 2, 1, 0, 7, 11, 15 }, 2, new[] { 0, 2 })]
        [InlineData(new[] { 0, 1, 4, 7, 11, 15 }, 16, new[] { 1, 5 })]
        [InlineData(new[] { -1, -2, -3, -4, -5 }, -8, new[] { 2, 4 })]
        [InlineData(new[] { -3, 4, 3, 90 }, 0, new[] { 0, 2 })]
        public void TestC5(int[] nums, int target, int[] expectedResult)
        {
            ITwoSumSolution solution = new TwoSumSolution_C5();
            int[] result = solution.TwoSum(nums, target);

            result.Should().Equal(expectedResult);
        }

    }

    internal interface ITwoSumSolution
    {
        int[] TwoSum(int[] nums, int target);
    }

    internal class TwoSumSolution : ITwoSumSolution
    {
        // Time Complexity - O(n)
        // Space Complexity - O(n)


        public int[] TwoSum(int[] nums, int target)
        {
            Hashtable hashtable = new Hashtable(nums.Length);

            int length = nums.Length;
            for (int i = 0; i < length; i++)
            {
                int delta = target - nums[i];
                if (hashtable.ContainsKey(delta))
                {
                    return new[] { (int)hashtable[delta], i };
                }

                if (!hashtable.ContainsKey(nums[i]))
                    hashtable.Add(nums[i], i);
            }

            return Array.Empty<int>();
        }
    }

    internal class TwoSumSolution_C5 : ITwoSumSolution
    {
        public int[] TwoSum(int[] nums, int target)
        {
            int length = nums.Length;
            HashDictionary<int, int> hashtable = new HashDictionary<int, int>(nums.Length, 0.1, EqualityComparer<int>.Default, MemoryType.Strict);

            for (int i = 0; i < length; i++)
            {
                int delta = target - nums[i];
                if (hashtable.Contains(delta))
                {
                    return new[] { (int)hashtable[delta], i };
                }

                if (!hashtable.Contains(nums[i]))
                    hashtable.Add(nums[i], i);
            }

            return Array.Empty<int>();
        }
    }
}
