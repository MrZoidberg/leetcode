using System;
using BenchmarkDotNet.Running;
using Leetcode.ReverseInteger;
using Leetcode.RotateArray;
using Leetcode.TwoSum;

namespace Leetcode
{
    class Program
    {
        public static void Main(string[] args)
        {
            //Console.Write(BenchmarkRunner.Run(typeof(TwoSumTests)));
            //Console.ReadKey();

            //Console.Write(BenchmarkRunner.Run(typeof(ReverseIntegerTests)));

            Console.Write(BenchmarkRunner.Run(typeof(RotateArrayTests)));
            
            Console.ReadKey();

        }
        
    }
}
