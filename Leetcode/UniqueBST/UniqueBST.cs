using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Leetcode.UniqueBST
{
    public class UniqueBSTSolutionTests
    {
        [Theory]
        [InlineData(3, 5)]
        [InlineData(2, 2)]
        public void Catalan_Test(int n, int answer)
        {
            UniqueBSTSolution solution = new UniqueBSTSolution();
            solution.NumTrees(n).Should().Be(answer);
        }
    }

    public class UniqueBSTSolution
    {
        public int NumTrees(int n)
        {
            int[] catalanValues = new int[n+1];
            catalanValues[0] = catalanValues[1] = 1;

            for (int i = 2; i <= n; i++)
            {
                catalanValues[i] = 0;
                for (int j = 0; j < i; j++)
                {
                    catalanValues[i] += catalanValues[j] * catalanValues[i - j - 1];
                }
            }

            return catalanValues[n];
        }
    }
}
