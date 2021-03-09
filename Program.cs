using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace FloatComparison
{
    class Program
    {
        private const int k_Count = 100000;
        private const int k_Seed = 11515;
        private const float k_Epsilon = 1E-05f;
        private const float k_SquaredEpsilon = k_Epsilon * k_Epsilon;

        static void Main(string[] args)
        {
            //FloatCompareTest();
            //VectorTest();
            //DictionaryTest();
            StringConcatTesting();
        }

        static void StringConcatTesting()
        {
            Console.WriteLine("Running StringConcatTesting");
            Stopwatch sw = new Stopwatch();
            List<int> intList = new List<int>();

            Random random = new Random(k_Seed);

            sw.Start();
            for (int i = 0; i < k_Count; i++)
            {
                intList.Add(random.Next());
            }
            sw.Stop();
            Console.WriteLine($"Setup time:{sw.ElapsedMilliseconds} ms");
            sw.Reset();

            StringBuilder sb = new StringBuilder();
            
            sw.Start();
            for (int i = 0; i < k_Count; i++)
            {
                sb.Append($"{intList[i]} - {intList[i]}");
            }
            sw.Stop();
            Console.WriteLine($"Boxing int:{sw.ElapsedMilliseconds} ms - sb.Size{sb.Length}");
            sw.Reset();
            
            sb = new StringBuilder();
            
            sw.Start();
            for (int i = 0; i < k_Count; i++)
            {
                sb.Append($"{intList[i].ToString()} - {intList[i].ToString()}");
            }
            sw.Stop();
            Console.WriteLine($"Int toString:{sw.ElapsedMilliseconds} ms - sb.Size{sb.Length}");
            sw.Reset();
            
            
            sw.Start();
            for (int i = 0; i < k_Count; i++)
            {
                sb.Append(string.Format("{0} - {1}", intList[i], intList[i]));
            }
            sw.Stop();
            Console.WriteLine($"Int Format:{sw.ElapsedMilliseconds} ms - sb.Size{sb.Length}");
            sw.Reset();
        }


        static void DictionaryTest()
        {
            
            Console.WriteLine("Running Dictionary Iteration Test");
            Stopwatch sw = new Stopwatch();
            List<Vec4> vecList = new List<Vec4>();
            Dictionary<Vec4, Vec4> vecDictionary = new Dictionary<Vec4, Vec4>(k_Count);

            Random random = new Random(k_Seed);

            sw.Start();
            for (int i = 0; i < k_Count; i++)
            {
                float val = (float)random.NextDouble();
                Vec4 vec = new Vec4
                {
                    x = val,
                    y = val,
                    z = val,
                    w = val
                };
                if (!vecDictionary.ContainsKey(vec))
                {
                    vecDictionary.Add(vec, vec);
                    vecList.Add(vec);
                }
            }
            sw.Stop();
            Console.WriteLine($"Setup time:{sw.ElapsedMilliseconds} ms - dictionaryCount:{vecDictionary.Count} - listCount:{vecList.Count}");
            sw.Reset();
            
            
            //////////////
            double xCumulative = 0;
            sw.Start();
            foreach(KeyValuePair<Vec4, Vec4> pair in vecDictionary)
            {
                xCumulative += pair.Value.x;
            }
            sw.Stop();
            Console.WriteLine($"KVP iteration:{sw.ElapsedMilliseconds} ms - xCumulative:{xCumulative}");
            sw.Reset();
            
            
            //////////////
            xCumulative = 0;
            sw.Start();
            Dictionary<Vec4, Vec4>.ValueCollection values = vecDictionary.Values;
            foreach(Vec4 valuesVec in values)
            {
                xCumulative += valuesVec.x;
            }
            sw.Stop();
            Console.WriteLine($"Values iteration:{sw.ElapsedMilliseconds} ms - xCumulative:{xCumulative}");
            sw.Reset();
            
            
            //////////////
            xCumulative = 0;
            sw.Start();
            foreach(Vec4 valuesVec in vecList)
            {
                xCumulative += valuesVec.x;
            }
            sw.Stop();
            Console.WriteLine($"Foreach List control:{sw.ElapsedMilliseconds} ms - xCumulative:{xCumulative}");
            sw.Reset();
            
            //////////////
            xCumulative = 0;
            sw.Start();
            for(int i = 0; i < vecList.Count; i++)
            {
                xCumulative += vecList[i].x;
            }
            sw.Stop();
            Console.WriteLine($"For int List control:{sw.ElapsedMilliseconds} ms - xCumulative:{xCumulative}");
            sw.Reset();

        }

        static void FloatCompareTest()
        {
            Console.WriteLine("Running FloatCompareTest");
            Stopwatch sw = new Stopwatch();
            List<float> floatList = new List<float>(k_Count);

            Random random = new Random(k_Seed);

            sw.Start();
            for (int i = 0; i < k_Count; i++)
            {
                floatList.Add((float)random.NextDouble());
            }
            sw.Stop();
            Console.WriteLine($"Setup time:{sw.ElapsedMilliseconds} ms");
            
            sw.Reset();
            
            int equal = 0;
            sw.Start();
            for (int i = 0; i < k_Count - 1; i++)
            {
                if (FloatComparison_Type1(floatList[i], floatList[i + 1]))
                {
                    equal++;
                }
            }
            sw.Stop();
            System.Console.WriteLine($"FloatComparison_Type1: Equal:{equal} - test duration:{sw.ElapsedMilliseconds} ms");
            
            equal = 0;
            sw.Reset();
            
            sw.Start();
            for (int i = 0; i < k_Count - 1; i++)
            {
                if (FloatComparison_Type2(floatList[i], floatList[i + 1]))
                {
                    equal++;
                }
            }
            sw.Stop();
            System.Console.WriteLine($"FloatComparison_Type2: Equal:{equal} - test duration:{sw.ElapsedMilliseconds} ms");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool FloatComparison_Type1(float f1, float f2)
        {
            return Math.Abs(f1 - f2) < k_Epsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool FloatComparison_Type2(float f1, float f2)
        {
            float val = f1 - f2;
            return val * val < k_SquaredEpsilon;
        }


        static void VectorTest()
        {
            
            Console.WriteLine("Running VectorCompareTest");
            Stopwatch sw = new Stopwatch();
            List<Vec4> vecList = new List<Vec4>(k_Count);

            Random random = new Random(k_Seed);

            sw.Start();
            float val = 0;
            for (int i = 0; i < k_Count; i++)
            {
                vecList.Add(new Vec4
                    /*
                    {
                        x = (float)random.NextDouble(),
                        y = (float)random.NextDouble(),
                        z = (float)random.NextDouble(),
                        w = (float)random.NextDouble()
                    }
                    */
                    {
                        x = val, 
                        y = val,
                        z = val,
                        w = val
                    }
                );
                if (i % 3 == 0)
                {
                    val = (val + 1) % 100;
                }
            }
            sw.Stop();
            Console.WriteLine($"Setup time:{sw.ElapsedMilliseconds} ms");
            
            sw.Reset();
            
            int equal = 0;
            sw.Start();
            for (int i = 0; i < k_Count - 1; i++)
            {
                if (Vec4.IsEqualAbs(vecList[i], vecList[i + 1]))
                {
                    equal++;
                }
            }
            sw.Stop();
            System.Console.WriteLine($"VectorComparison_Abs: Equal:{equal} - test duration:{sw.ElapsedMilliseconds} ms");
            
            equal = 0;
            sw.Reset();
            
            sw.Start();
            for (int i = 0; i < k_Count - 1; i++)
            {
                if (Vec4.IsEqual(vecList[i], vecList[i + 1]))
                {
                    equal++;
                }
            }
            sw.Stop();
            System.Console.WriteLine($"VectorComparison_Squared: Equal:{equal} - test duration:{sw.ElapsedMilliseconds} ms");
        }
        

        public struct Vec4
        {   
            /// <summary>
            ///   <para>X component of the vector.</para>
            /// </summary>
            public float x;
            /// <summary>
            ///   <para>Y component of the vector.</para>
            /// </summary>
            public float y;
            /// <summary>
            ///   <para>Z component of the vector.</para>
            /// </summary>
            public float z;
            /// <summary>
            ///   <para>W component of the vector.</para>
            /// </summary>
            public float w;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool IsEqualAbs(Vec4 lhs, Vec4 rhs)
            {
                return Math.Abs(lhs.x - rhs.x) + Math.Abs(lhs.y - rhs.y) + Math.Abs(lhs.z - rhs.z) +
                    Math.Abs(lhs.w - rhs.w) < k_Epsilon;
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool IsEqual(Vec4 lhs, Vec4 rhs)
            {
                float num1 = lhs.x - rhs.x;
                float num2 = lhs.y - rhs.y;
                float num3 = lhs.z - rhs.z;
                float num4 = lhs.w - rhs.w;
                return num1 * (double) num1 + num2 * (double) num2 + num3 * (double) num3 + num4 * (double) num4 < k_SquaredEpsilon;
            }
        }
    }
}