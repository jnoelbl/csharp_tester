using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace SpeedTests
{
    public class ListChecks
    {
        public class ListTestClass
        {
            public int IntValue;
        }

        private List<ListTestClass> _list = new List<ListTestClass>();
        private int _count;

        public IReadOnlyList<ListTestClass> ReadOnlyTestList => _list;
        public List<ListTestClass> TestList => _list;

        [GlobalSetup]
        public void Setup()
        {
            for (int i = 0; i < Constants.k_IterationCount; i++)
            {
                _list.Add(new ListTestClass{IntValue = i});
            }
        }

        [IterationSetup]
        public void IterSetup() => _count = 0;

        [IterationCleanup]
        public void IterCleanup() => Console.WriteLine($"Count:{_count.ToString()}");

        [Benchmark]
        public void ReadOnlyForeachIteration()
        {
            foreach (var test in ReadOnlyTestList)
            {
                _count += test.IntValue;
            }
        }

        [Benchmark]
        public void DirectForeachIteration()
        {
            foreach (var test in TestList)
            {
                _count += test.IntValue;
            }
        }
        [Benchmark]
        public void ReadOnlyForPropertyIteration()
        {
            for (var index = 0; index < ReadOnlyTestList.Count; index++)
            {
                var test = ReadOnlyTestList[index];
                _count += test.IntValue;
            }
        }

        [Benchmark]
        public void DirectForPropertyIteration()
        {
            for (var index = 0; index < TestList.Count; index++)
            {
                var test = TestList[index];
                _count += test.IntValue;
            }
        }
        [Benchmark]
        public void ReadOnlyForCachedIteration()
        {
            IReadOnlyList<ListTestClass> list = ReadOnlyTestList;
            for (var index = 0; index < list.Count; index++)
            {
                var test = list[index];
                _count += test.IntValue;
            }
        }

        [Benchmark]
        public void DirectForCachedIteration()
        {
            List<ListTestClass> list = TestList;
            for (var index = 0; index < list.Count; index++)
            {
                var test = list[index];
                _count += test.IntValue;
            }
        }
    }
}