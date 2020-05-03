using System;
using FluentAssertions;
using Xunit;

namespace Leetcode.PalindromeNumber
{
    public class PalindromeNumberTests
    {
        [Theory]
        [InlineData(121, true)]
        [InlineData(11, true)]
        [InlineData(1111, true)]
        [InlineData(2225222, true)]
        [InlineData(12345, false)]
        [InlineData(-121, false)]
        [InlineData(10, false)]
        public void Test_Naive(int input, bool expectedResult)
        {
            IPalindromeNumberSolution solution = new NaivePalindromeNumberSolution();

            bool output = solution.IsPalindrome(input);

            output.Should().Be(expectedResult);

        }

        [Theory]
        [InlineData(121, true)]
        [InlineData(11, true)]
        [InlineData(1111, true)]
        [InlineData(2225222, true)]
        [InlineData(12345, false)]
        [InlineData(-121, false)]
        [InlineData(10, false)]
        public void Test_Revert(int input, bool expectedResult)
        {
            IPalindromeNumberSolution solution = new RevertNumberPalindromeNumberSolution();

            bool output = solution.IsPalindrome(input);

            output.Should().Be(expectedResult);

        }
    }

    interface IPalindromeNumberSolution
    {
        bool IsPalindrome(int x);
    }

    class RevertNumberPalindromeNumberSolution : IPalindromeNumberSolution
    {
        public bool IsPalindrome(int x)
        {
            if (x < 0 || (x % 10 == 0 && x != 0))
                return false;
            else if (x < 9)
                return true;

            int revNumber = 0;
            while (x >= revNumber)
            {
                if (x == revNumber || x / 10 == revNumber)
                    return true;

                revNumber = revNumber * 10 + x % 10;
                x /= 10;
            }

            return false;
        }
    }

    class NaivePalindromeNumberSolution : IPalindromeNumberSolution
    {
        public bool IsPalindrome(int x)
        {
            if (x < 0 || (x % 10 == 0 && x != 0))
                return false;
            else if (x > 0 && x < 9)
                return true;

            int[] digits = new int[10];
            int len = 0;
            do
            {
                digits[len] = x % 10;
                x = x / 10;
                len++;
            } while (x != 0);

            for (int i = 0; i < len / 2; i++)
            {
                if (digits[i] != digits[len - i - 1])
                    return false;
            }

            return true;
        }
    }
}
