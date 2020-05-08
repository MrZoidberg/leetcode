using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenchmarkDotNet.Attributes;
using FluentAssertions;
using Xunit;

namespace Leetcode.ContainsDuplicate
{
    public class ContainsDuplicateTests
    {
        [Theory]
        [InlineData(new[] { 1, 2, 3, 1 }, true)]
        [InlineData(new[] { 1, 2, 3, 4 }, false)]
        [InlineData(new[] { 1, 1, 1, 3, 3, 4, 3, 2, 4, 2 }, true)]
        public void Test_Naive(int[] input, bool expectedResult)
        {
            IContainsDuplicateSolution solution = new NaiveContainsDuplicateSolution();

            var output = solution.ContainsDuplicate(input);

            output.Should().Be(expectedResult);

        }

        [Theory]
        [InlineData(new[] { 1, 2, 3, 1 }, true)]
        [InlineData(new[] { 1, 2, 3, 4 }, false)]
        [InlineData(new[] { 1, 1, 1, 3, 3, 4, 3, 2, 4, 2 }, true)]
        public void Test_Parallel(int[] input, bool expectedResult)
        {
            IContainsDuplicateSolution solution = new ParallelContainsDuplicateSolution();

            var output = solution.ContainsDuplicate(input);

            output.Should().Be(expectedResult);

        }

        [Benchmark]
        [ArgumentsSource(nameof(PerfTestInputLength))]
        public void Perf_Test_Naive(int len)
        {
            int Min = 0;
            int Max = len * 20;
            Random randNum = new Random();
            int[] input = Enumerable
                .Repeat(0, len)
                .Select(i => randNum.Next(Min, Max))
                .ToArray();

            IContainsDuplicateSolution solution = new NaiveContainsDuplicateSolution();
            var _ = solution.ContainsDuplicate(input);
        }

        [Benchmark]
        [ArgumentsSource(nameof(PerfTestInputLength))]
        public void Perf_Test_Parallel(int len)
        {
            int Min = 0;
            int Max = len * 2;
            Random randNum = new Random();
            int[] input = Enumerable
                .Repeat(0, len)
                .Select(i => randNum.Next(Min, Max))
                .ToArray();

            IContainsDuplicateSolution solution = new ParallelContainsDuplicateSolution();
            var _ = solution.ContainsDuplicate(input);
        }

        [Benchmark]
        [ArgumentsSource(nameof(PerfTestInputLength))]
        public void Perf_Test_Parallel2(int len)
        {
            int Min = 0;
            int Max = len * 2;
            Random randNum = new Random();
            int[] input = Enumerable
                .Repeat(0, len)
                .Select(i => randNum.Next(Min, Max))
                .ToArray();

            IContainsDuplicateSolution solution = new ParallelContainsDuplicateSolution2();
            var _ = solution.ContainsDuplicate(input);
        }

        public IEnumerable<int> PerfTestInputLength()
        {
            var b = 100000;
            var a = 10;
            var min = 1;
            var max = 10;

            return Enumerable.Range(1, 10).Select(x => (b - a) * (x - min) / (max - min) + a);
        }

    }

    public interface IContainsDuplicateSolution
    {
        bool ContainsDuplicate(int[] nums);
    }

    public class NaiveContainsDuplicateSolution : IContainsDuplicateSolution
    {
        public bool ContainsDuplicate(int[] nums)
        {
            int length = nums.Length;
            if (length <= 1)
                return false;

            HashSet<int> hashset = new HashSet<int>();
            for (int i = 0; i < length; i++)
            {
                var val = nums[i];
                if (hashset.Contains(val))
                    return true;
                else
                    hashset.Add(val);
            }

            return false;
        }

    }

    public class ParallelContainsDuplicateSolution : IContainsDuplicateSolution
    {
        public bool ContainsDuplicate(int[] nums)
        {
            int length = nums.Length;
            if (length <= 1)
                return false;

            System.Collections.Concurrent.ConcurrentDictionary<int, bool> dict = new System.Collections.Concurrent.ConcurrentDictionary<int, bool>();
            bool foundDuplicate = false;
            System.Threading.Tasks.Parallel.For(0, length, (i, s) =>
             {
                 var val = nums[i];
                 if (!dict.TryAdd(val, true))
                 {
                     foundDuplicate = true;
                     s.Break();
                 }
             });

            return foundDuplicate;
        }
    }

    public class ParallelContainsDuplicateSolution2 : IContainsDuplicateSolution
    {
        public bool ContainsDuplicate(int[] nums)
        {
            int length = nums.Length;
            if (length <= 1)
                return false;

            System.Collections.Concurrent.ConcurrentDictionary<int, bool> dict = new System.Collections.Concurrent.ConcurrentDictionary<int, bool>();
            return nums.AsParallel().WithExecutionMode(ParallelExecutionMode.ForceParallelism).WithDegreeOfParallelism(8).Any(p =>
            {
                if (!dict.TryAdd(p, true))
                    return true;
                return false;
            });
        }
    }
}
