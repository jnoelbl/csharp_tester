using System;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace SpeedTests
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            //SizeOfChecks.Check();
            
            //FloatChecks.FloatCompareTest();
            //FloatChecks.FloatMaxTest();
            //FloatChecks.FloatMinTest();
            
            //DictionaryChecks.DictionaryTest();
            
            //StringChecks.StringConcatTesting();
            //StringChecks.StringConcatFormatTesting();
            
            //VectorChecks.VectorEqualTest();
            //VectorChecks.VectorAccessTest();
            //VectorChecks.VectorMinTest();

            //Summary summary = BenchmarkRunner.Run<StringConcatTesting>();
            Summary summary = BenchmarkRunner.Run<StringSplitVsStartsWithTesting>();
        }
    }
}