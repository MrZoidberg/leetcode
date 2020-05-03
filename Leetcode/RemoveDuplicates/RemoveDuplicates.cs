using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Leetcode.RemoveDuplicates
{
    //Given a sorted array nums, remove the duplicates in-place such that each element appear only once and return the new length.
    //Do not allocate extra space for another array, you must do this by modifying the input array in-place with O(1) extra memory.

    public class RemoveDuplicatesTests
    {
        [Theory]
        [InlineData(new[] { 1, 1, 2 }, 2)]
        [InlineData(new[] { 1, 2 }, 2)]
        [InlineData(new[] { 1 }, 1)]
        [InlineData(new int[0], 0)]
        [InlineData(new[] { 0, 0, 1, 1, 1, 2, 2, 3, 3, 4 }, 5)]

        public void Test_Naive(int[] input, int expectedLength)
        {
            IRemoveDuplicatesSolution solution = new RemoveDuplicatesSolution();

            var result = solution.RemoveDuplicates(input);

            result.Should().Be(expectedLength);

        }
    }

    interface IRemoveDuplicatesSolution
    {
        int RemoveDuplicates(int[] nums);
    }


    /// <summary>
    /// Time complextiy : O(n), Space complexity : O(1)
    /// </summary>
    class RemoveDuplicatesSolution : IRemoveDuplicatesSolution
    {
        public int RemoveDuplicates(int[] nums)
        {
            if (nums.Length == 0)
                return 0;

            int i = 0;
            int len = nums.Length;
            for (int j = 1; j < len; j++)
            {
                if (nums[i] != nums[j])
                {
                    i++;
                    nums[i] = nums[j];
                }
            }

            return i + 1;
        }
    }
}
