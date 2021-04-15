using BenchmarkDotNet.Attributes;

namespace SpeedTests
{
    public class NullChecks
    {
        private class TestClass
        {
#pragma warning disable 649
            public bool IsValid;
#pragma warning restore 649
        }

        private class ComplexTestClass
        {
            public bool IsValid;
            public int IntVal = -1;
            public float FloatVal;
        }

        private int _modCount = 15;
        private int _count = 0;

        [IterationSetup]
        public void IterSetup() => _count = 0;

        [IterationCleanup]
        public void IterCleanup() => System.Console.WriteLine($"Count: {_count.ToString()}");

        [Benchmark]
        public void IsNullCheck()
        {
            TestClass obj = null;
            for (int i = 0; i < Constants.k_IterationCount; i++)
            {
                if (i % _modCount == 0)
                {
                    if (obj is null)
                    {
                        _count++;
                        obj = new TestClass();
                    }
                    else
                    {
                        obj = null;
                    }
                }
            }
        }
        
        [Benchmark]
        public void EqualityNullCheck()
        {
            TestClass obj = null;
            for (int i = 0; i < Constants.k_IterationCount; i++)
            {
                if (i % _modCount == 0)
                {
                    if (obj == null)
                    {
                        _count++;
                        obj = new TestClass();
                    }
                    else
                    {
                        obj = null;
                    }
                }
                
            }
        }
        
        [Benchmark]
        public void IsNotNullCheck()
        {
            TestClass obj = new TestClass();
            for (int i = 0; i < Constants.k_IterationCount; i++)
            {
                if (i % _modCount == 0)
                {
                    if (obj is not null)
                    {
                        _count++;
                        obj = null;
                    }
                    else
                    {
                        obj = new TestClass();
                    }
                }
            }
        }
        [Benchmark]
        public void EqualityNotNullCheck()
        {
            TestClass obj = new TestClass();
            for (int i = 0; i < Constants.k_IterationCount; i++)
            {
                if (i % _modCount == 0)
                {
                    if (obj != null)
                    {
                        _count++;
                        obj = null;
                    }
                    else
                    {
                        obj = new TestClass();
                    }
                }
            }
        }
        
        [Benchmark]
        public void IsNotNullIsValidCheck()
        {
            TestClass obj = new TestClass();
            for (int i = 0; i < Constants.k_IterationCount; i++)
            {
                if (i % _modCount == 0)
                {
                    if (obj is { IsValid:false })
                    {
                        _count++;
                        obj = new TestClass();
                    }
                    else
                    {
                        obj = null;
                    }
                }
            }
        }
        
        [Benchmark]
        public void EqualityNotNullIsValidCheck()
        {
            TestClass obj = new TestClass();
            for (int i = 0; i < Constants.k_IterationCount; i++)
            {
                if (i % _modCount == 0)
                {
                    if (obj != null && obj.IsValid == false)
                    {
                        _count++;
                        obj = new TestClass();
                    }
                    else
                    {
                        obj = null;
                    }
                }
            }
        }
        
        
        [Benchmark]
        public void ComplexIsNotNullIsValidCheck()
        {
            ComplexTestClass obj = new ComplexTestClass();
            for (int i = 0; i < Constants.k_IterationCount; i++)
            {
                if (i % _modCount == 0)
                {
                    if (obj is { IsValid:false, FloatVal:0, IntVal:-1 })
                    {
                        _count++;
                        obj = new ComplexTestClass();
                    }
                    else
                    {
                        obj = null;
                    }
                }
            }
        }
        
        
        [Benchmark]
        public void ComplexEqualityNotNullIsValidCheck()
        {
            ComplexTestClass obj = new ComplexTestClass();
            for (int i = 0; i < Constants.k_IterationCount; i++)
            {
                if (i % _modCount == 0)
                {
                    if (obj != null && obj.IsValid == false && obj.FloatVal == 0 && obj.IntVal == -1)
                    {
                        _count++;
                        obj = new ComplexTestClass();
                    }
                    else
                    {
                        obj = null;
                    }
                }
            }
        }
    }
}