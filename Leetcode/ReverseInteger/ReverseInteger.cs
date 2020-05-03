using System;
using System.Text;
using BenchmarkDotNet.Attributes;
using FluentAssertions;
using Xunit;

namespace Leetcode.ReverseInteger
{
    //Given a 32-bit signed integer, reverse digits of an integer.

    //Example 1:

    //Input: 123
    //Output: 321
    //Example 2:

    //Input: -123
    //Output: -321
    //Example 3:

    //Input: 120
    //Output: 21
    //Note:
    //Assume we are dealing with an environment which could only store integers within the 32-bit signed integer range: [−231,  231 − 1]. For the purpose of this problem, assume that your function returns 0 when the reversed integer overflows.


    public class ReverseIntegerTests
    {
        [Theory]
        [InlineData(123, 321)]
        [InlineData(-123, -321)]
        [InlineData(120, 21)]
        [InlineData(12003, 30021)]
        [InlineData(Int32.MaxValue, 0)]
        [InlineData(Int32.MinValue, 0)]
        [InlineData(1534236469, 0)]

        public void Test_Naive(int input, int expectedResult)
        {
            IReverseIntegerSolution solution = new NaiveReverseIntegerSolution();

            int output = solution.Reverse(input);

            output.Should().Be(expectedResult);

        }

        [Theory]
        [InlineData(123, 321)]
        [InlineData(-123, -321)]
        [InlineData(120, 21)]
        [InlineData(12003, 30021)]
        [InlineData(Int32.MaxValue, 0)]
        [InlineData(Int32.MinValue, 0)]
        [InlineData(1534236469, 0)]

        public void Test_Leetcode(int input, int expectedResult)
        {
            IReverseIntegerSolution solution = new LeedcodeReverseIntegerSolution();

            int output = solution.Reverse(input);

            output.Should().Be(expectedResult);

        }


        [Benchmark]
        public void Perf_Test()
        {
            IReverseIntegerSolution solution = new NaiveReverseIntegerSolution();
            _ = solution.Reverse(Int32.MaxValue / 2);
        }

        [Benchmark]
        public void Perf_Test_Leetcode()
        {
            IReverseIntegerSolution solution = new LeedcodeReverseIntegerSolution();
            _ = solution.Reverse(Int32.MaxValue / 2);
        }
    }

    interface IReverseIntegerSolution
    {
        int Reverse(int x);
    }

    class LeedcodeReverseIntegerSolution : IReverseIntegerSolution
    {
        public int Reverse(int x)
        {
            int rev = 0;
            while (x != 0)
            {
                int pop = x % 10;
                x /= 10;
                if (rev > Int32.MaxValue / 10) return 0; // || (rev == Int32.MaxValue / 10 && pop > 7) these checks from leetcode are not required actually
                if (rev < Int32.MinValue / 10) return 0; // || (rev == Int32.MinValue / 10 && pop < -8)
                rev = rev * 10 + pop;
            }
            return rev;
        }
    }


    class NaiveReverseIntegerSolution : IReverseIntegerSolution
    {

        public int Reverse(int x)
        {
            int len = 0;
            int[] digits = new int[10];

            do
            {
                int lsd = x % 10;
                x = x / 10;
                digits[len] = lsd;
                len++;
            } while (x != 0);

            int reservedX = 0;
            int shift = 1;
            int digit;
            int addendant;
            bool overflow;
            for (int i = len - 1; i >= 0; i--)
            {
                digit = digits[i];
                addendant = digit * shift;
                if (digit != 0 && addendant / digit != shift) //multiplication overflow                
                    return 0;

                //check summation overflow

                if ((addendant ^ reservedX) < 0)
                    overflow = false; /* opposite signs can't overflow */
                else if (addendant > 0)
                    overflow = (reservedX > Int32.MaxValue - addendant);
                else
                    overflow = (reservedX < Int32.MinValue - addendant);
                if (overflow)
                    return 0;

                reservedX += addendant;
                shift *= 10;
            }

            return reservedX;
        }
    }
}
