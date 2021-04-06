using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SpeedTests
{
    public class FloatChecks
    {
        public static void FloatMinTest()
        {
            Console.WriteLine("Running FloatMinTest");
            Stopwatch sw = new Stopwatch();
            List<float> floatList = new List<float>(Constants.k_IterationCount);

            Random random = new Random(Constants.k_Seed);

            sw.Start();
            for (int i = 0; i < Constants.k_IterationCount; i++)
            {
                floatList.Add((float)random.NextDouble());
            }
            sw.Stop();
            Console.WriteLine($"Setup time:{sw.ElapsedMilliseconds} ms");
            
            sw.Reset();
            
            float minVal = 0;
            sw.Start();
            for (int i = 0; i < Constants.k_IterationCount - 2; i++)
            {
                minVal += Min1(floatList[i], floatList[i + 1], floatList[i + 2]);
            }
            sw.Stop();
            Console.WriteLine($"Max1: minVal:{minVal} - test duration:{sw.ElapsedMilliseconds} ms");
            
            minVal = 0;
            sw.Reset();
            
            sw.Start();
            for (int i = 0; i < Constants.k_IterationCount - 2; i++)
            {
                minVal += Min2(floatList[i], floatList[i + 1], floatList[i + 2]);
            }
            sw.Stop();
            Console.WriteLine($"Max2: minVal:{minVal} - test duration:{sw.ElapsedMilliseconds} ms");
            sw.Reset();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinInline(float v1, float v2)
        {
            return v1 < v2 ? v1 : v2;
        }
        public static float Min(float v1, float v2)
        {
            return v1 < v2 ? v1 : v2;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Min1(float v1, float v2, float v3)
        {
            return Math.Min(Math.Min(v1, v2), v3);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Min2(float v1, float v2, float v3)
        {
            return v1 > v2 ? (v2 > v3 ? v3 : v2) : (v1 > v3 ? v3 : v1);
        }
        public static void FloatMaxTest()
        {
            Console.WriteLine("Running FloatMaxTest");
            Stopwatch sw = new Stopwatch();
            List<float> floatList = new List<float>(Constants.k_IterationCount);

            Random random = new Random(Constants.k_Seed);

            sw.Start();
            for (int i = 0; i < Constants.k_IterationCount; i++)
            {
                floatList.Add((float)random.NextDouble());
            }
            sw.Stop();
            Console.WriteLine($"Setup time:{sw.ElapsedMilliseconds} ms");
            
            sw.Reset();
            
            float maxTotal = 0;
            sw.Start();
            for (int i = 0; i < Constants.k_IterationCount - 2; i++)
            {
                maxTotal += Max1(floatList[i], floatList[i + 1], floatList[i + 2]);
            }
            sw.Stop();
            Console.WriteLine($"Max1: maxTotal:{maxTotal} - test duration:{sw.ElapsedMilliseconds} ms");
            
            maxTotal = 0;
            sw.Reset();
            
            sw.Start();
            for (int i = 0; i < Constants.k_IterationCount - 2; i++)
            {
                maxTotal += Max2(floatList[i], floatList[i + 1], floatList[i + 2]);
            }
            sw.Stop();
            Console.WriteLine($"Max2: maxTotal:{maxTotal} - test duration:{sw.ElapsedMilliseconds} ms");
            sw.Reset();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Max1(float v1, float v2, float v3)
        {
            return Math.Max(Math.Max(v1, v2), v3);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Max2(float v1, float v2, float v3)
        {
            return v1 < v2 ? (v2 < v3 ? v3 : v2) : (v1 < v3 ? v3 : v1);
        }
        public static void FloatCompareTest()
        {
            Console.WriteLine("Running FloatCompareTest");
            Stopwatch sw = new Stopwatch();
            List<float> floatList = new List<float>(Constants.k_IterationCount);

            Random random = new Random(Constants.k_Seed);

            sw.Start();
            for (int i = 0; i < Constants.k_IterationCount; i++)
            {
                floatList.Add((float)random.NextDouble());
            }
            sw.Stop();
            Console.WriteLine($"Setup time:{sw.ElapsedMilliseconds} ms");
            
            sw.Reset();
            
            int equal = 0;
            sw.Start();
            for (int i = 0; i < Constants.k_IterationCount - 1; i++)
            {
                if (FloatIsEqual_Type1(floatList[i], floatList[i + 1]))
                {
                    equal++;
                }
            }
            sw.Stop();
            System.Console.WriteLine($"FloatIsEqual_Type1: Equal:{equal} - test duration:{sw.ElapsedMilliseconds} ms");
            
            equal = 0;
            sw.Reset();
            
            sw.Start();
            for (int i = 0; i < Constants.k_IterationCount - 1; i++)
            {
                if (FloatIsEqual_Type2(floatList[i], floatList[i + 1]))
                {
                    equal++;
                }
            }
            sw.Stop();
            System.Console.WriteLine($"FloatIsEqual_Type2: Equal:{equal} - test duration:{sw.ElapsedMilliseconds} ms");
            sw.Reset();
            equal = 0;
            
            sw.Start();
            for (int i = 0; i < Constants.k_IterationCount - 1; i++)
            {
                if (FloatIsEqual_Type3(floatList[i], floatList[i + 1]))
                {
                    equal++;
                }
            }
            sw.Stop();
            System.Console.WriteLine($"FloatIsEqual_Type3: Equal:{equal} - test duration:{sw.ElapsedMilliseconds} ms");
            sw.Reset();

            equal = 0;
            
            sw.Start();
            for (int i = 0; i < Constants.k_IterationCount - 1; i++)
            {
                if (FloatComparison_Type1(floatList[i], floatList[i + 1]) == 0)
                {
                    equal++;
                }
            }
            sw.Stop();
            System.Console.WriteLine($"FloatComparison_Type1: equal:{equal} - test duration:{sw.ElapsedMilliseconds} ms");
            sw.Reset();

            equal = 0;
            
            sw.Start();
            for (int i = 0; i < Constants.k_IterationCount - 1; i++)
            {
                if (FloatComparison_Type2(floatList[i], floatList[i + 1]) == 0)
                {
                    equal++;
                }
            }
            sw.Stop();
            System.Console.WriteLine($"FloatComparison_Type2: equal:{equal} - test duration:{sw.ElapsedMilliseconds} ms");
            sw.Reset();

            equal = 0;
            
            sw.Start();
            for (int i = 0; i < Constants.k_IterationCount - 1; i++)
            {
                if (FloatComparison_Type3(floatList[i], floatList[i + 1]) == 0)
                {
                    equal++;
                }
            }
            sw.Stop();
            System.Console.WriteLine($"FloatComparison_Type3: equal:{equal} - test duration:{sw.ElapsedMilliseconds} ms");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool FloatIsEqual_Type1(float f1, float f2)
        {
            return Math.Abs(f1 - f2) < Constants.k_Epsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool FloatIsEqual_Type2(float f1, float f2)
        {
            float val = f1 - f2;
            return val * (double)val < Constants.k_SquaredEpsilon;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool FloatIsEqual_Type3(float a, float b)
        {
            return ((a - b) < 0 ? ((a - b) * -1) : (a - b)) <= Constants.k_Epsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int FloatComparison_Type1(float a, float b)
        {
            return (((a - b) < 0 ? ((a - b) * -1) : (a - b)) <= Constants.k_Epsilon) ? 0 : ((a - b) < 0 ? -1 : 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int FloatComparison_Type2(float v1, float v2)
        {
            float diff = v1 - v2;
            return (diff < 0 ? diff * -1 : diff) <= Constants.k_Epsilon ? 0 : diff < 0 ? -1 : 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int FloatComparison_Type3(float v1, float v2, float precision = Constants.k_Epsilon)
        {
            float diff = v1 - v2;
            // int clamping a float will often give us a value of 0. 
            // This i only safe if the k_offset is the inverse of the precision 
            return diff * (double) diff <= precision * (double)precision ? 0 : (int) (diff * (1.0f / precision));
        }
    }
}