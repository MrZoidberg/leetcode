using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Attributes;
using FluentAssertions;
using Xunit;

namespace Leetcode.RotateArray
{
    //Given an array, rotate the array to the right by k steps, where k is non-negative.
    //
    //Follow up:
    //
    // Try to come up as many solutions as you can, there are at least 3 different ways to solve this problem.
    //Could you do it in-place with O(1) extra space?
    //Constraints:
    //
    //1 <= nums.length <= 2 * 10^4
    //It's guaranteed that nums[i] fits in a 32 bit-signed integer.
    //k >= 0

    public class RotateArrayTests
    {
        [Theory]
        [InlineData(new[] { 1, 2, 3, 4, 5, 6, 7 }, 3, new[] { 5, 6, 7, 1, 2, 3, 4 })]
        [InlineData(new[] { -1, -100, 3, 99 }, 2, new[] { 3, 99, -1, -100 })]


        public void Test_ExtraArraySolution(int[] input, int k, int[] expectedOutput)
        {
            ISolution solution = new ExtraArraySolution();

            solution.Rotate(input, k);

            input.Should().Equal(expectedOutput);

        }

        [Theory]
        [InlineData(new[] { 1, 2, 3, 4, 5, 6, 7 }, 3, new[] { 5, 6, 7, 1, 2, 3, 4 })]
        [InlineData(new[] { -1, -100, 3, 99 }, 2, new[] { 3, 99, -1, -100 })]


        public void Test_InPlaceSolution(int[] input, int k, int[] expectedOutput)
        {
            ISolution solution = new InPlaceSolution();

            solution.Rotate(input, k);

            input.Should().Equal(expectedOutput);

        }

        [Benchmark]
        public void Perf_Test_ExtraArray()
        {
            int[] input = new[] { 200, 20, 1130, 1270, 990, 1430, 1600, 1660, 2680, 2080, 1980, 2740, 2700, 2670, 1960, 1790, 2570, 270, 1980, 1020, 2530, 2770, 2250, 2090, 250, 2340, 380, 660, 540, 2520, 1820, 980, 1910, 2270, 980, 10, 2790, 1290, 1040, 1580, 910, 1160, 440, 570, 570, 2520, 480, 420, 1280, 1930 };
            ISolution solution = new ExtraArraySolution();

            solution.Rotate(input, 20);
        }

        [Benchmark]
        public void Perf_Test_InPlace()
        {
            int[] input = new[] { 200, 20, 1130, 1270, 990, 1430, 1600, 1660, 2680, 2080, 1980, 2740, 2700, 2670, 1960, 1790, 2570, 270, 1980, 1020, 2530, 2770, 2250, 2090, 250, 2340, 380, 660, 540, 2520, 1820, 980, 1910, 2270, 980, 10, 2790, 1290, 1040, 1580, 910, 1160, 440, 570, 570, 2520, 480, 420, 1280, 1930 };
            ISolution solution = new InPlaceSolution();

            solution.Rotate(input, 20);
        }
    }

    public interface ISolution
    {
        void Rotate(int[] nums, int k);
    }

    /// <summary>
    /// Time complexity: O(n), but faster then in-place solution
    /// Space complexity: O(n)
    /// </summary>
    public class ExtraArraySolution : ISolution
    {
        public void Rotate(int[] nums, int k)
        {
            int len = nums.Length;
            int[] result = new int[len];

            for (int i = 0; i < len; i++)
            {
                int newIndex = i + k;
                if (newIndex >= len)
                    newIndex = newIndex % len;

                result[newIndex] = nums[i];
            }

            result.CopyTo(nums, 0);
        }
    }

    /// <summary>
    /// Time complexity: O(n).
    /// Space complexity: O(1)
    /// </summary>
    public class InPlaceSolution : ISolution
    {
        public void Rotate(int[] nums, int k)
        {
            int len = nums.Length;
            k = k % len;

            int count = 0;
            for (int i = 0; count < len; i++)
            {
                int cur = i;
                int prev = nums[i];

                do
                {
                    int nextPos = (cur + k) % len;
                    int temp = nums[nextPos];
                    nums[nextPos] = prev;
                    prev = temp;
                    cur = nextPos;
                    count++;
                } while (i != cur);

            }
        }
    }
}
