using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SpeedTests
{
    public class StringChecks
    {
        public static void StringConcatFormatTesting()
        {
            int count = Constants.k_IterationCount * 6;
            Console.WriteLine("Running StringConcatFormatTesting");
            Stopwatch sw = new Stopwatch();
            List<float> floatList = new List<float>();

            string initialString = "0x2020202";

            Random random = new Random(Constants.k_Seed);

            sw.Start();
            for (int i = 0; i < count; i++)
            {
                floatList.Add(random.Next());
            }
            sw.Stop();
            Console.WriteLine($"Setup time:{sw.ElapsedMilliseconds} ms");
            sw.Reset();

            StringBuilder sb = new StringBuilder();
            
            sw.Start();
            for (int i = 0; i < count / 6; i += 6)
            {
                sb.Append($"2 {initialString} {floatList[i]:f3} {floatList[i + 1]:f3} {-floatList[i + 2]:f3} {floatList[i + 3]:f3} {-floatList[i + 4]:f3} {floatList[i + 5]:f3}");
            }
            sw.Stop();
            Console.WriteLine($"Boxing Float:{sw.ElapsedMilliseconds} ms - sb.Size:{sb.Length}");
            sw.Reset();
            sb.Clear();
            
            sb = new StringBuilder();
            
            sw.Start();
            for (int i = 0; i < count / 6; i += 6)
            {
                sb.Append(
                    $"2 {initialString} {floatList[i].ToString("F3")} {floatList[i + 1].ToString("F3")} {(-floatList[i + 2]).ToString("F3")} {floatList[i + 3].ToString("F3")} {(-floatList[i + 4]).ToString("F3")} {floatList[i + 5].ToString("F3")}");
            }
            sw.Stop();
            Console.WriteLine($"String Concat Float ToString:{sw.ElapsedMilliseconds} ms - sb.Size:{sb.Length}");
            sw.Reset();
            sb.Clear();
            
            
            sw.Start();
            for (int i = 0; i < count / 6; i += 6)
            {
                sb.AppendFormat("2 {0} {1:f3} {2:f3} {3:f3} {4:f3} {5:f3} {6:f3}", initialString, floatList[i], floatList[i + 1], -floatList[i + 2], floatList[i + 3], -floatList[i + 4], floatList[i + 5]);
            }
            sw.Stop();
            Console.WriteLine($"Append Format:{sw.ElapsedMilliseconds} ms - sb.Size:{sb.Length}");
            sw.Reset();
            sb.Clear();
            
            sw.Start();
            for (int i = 0; i < count / 6; i += 6)
            {
                sb.AppendFormat("2 {0} {1} {2} {3} {4} {5} {6}", initialString, floatList[i].ToString("F3"), floatList[i + 1].ToString("F3"), (-floatList[i + 2]).ToString("F3"), floatList[i + 3].ToString("F3"), (-floatList[i + 4]).ToString("F3"), floatList[i + 5].ToString("F3"));
            }
            sw.Stop();
            Console.WriteLine($"Append Format ToString:{sw.ElapsedMilliseconds} ms - sb.Size:{sb.Length}");
            sw.Reset();
            sb.Clear();
        }

        public static void StringConcatTesting()
        {
            Console.WriteLine("Running StringConcatTesting");
            Stopwatch sw = new Stopwatch();
            List<int> intList = new List<int>();

            Random random = new Random(Constants.k_Seed);

            sw.Start();
            for (int i = 0; i < Constants.k_IterationCount; i++)
            {
                intList.Add(random.Next());
            }
            sw.Stop();
            Console.WriteLine($"Setup time:{sw.ElapsedMilliseconds} ms");
            sw.Reset();

            StringBuilder sb = new StringBuilder();
            
            sw.Start();
            for (int i = 0; i < Constants.k_IterationCount; i++)
            {
                sb.Append($"{intList[i]} - {intList[i]}");
            }
            sw.Stop();
            Console.WriteLine($"Boxing int:{sw.ElapsedMilliseconds} ms - sb.Size{sb.Length}");
            sw.Reset();
            
            sb = new StringBuilder();
            
            sw.Start();
            for (int i = 0; i < Constants.k_IterationCount; i++)
            {
                sb.Append($"{intList[i].ToString()} - {intList[i].ToString()}");
            }
            sw.Stop();
            Console.WriteLine($"Int toString:{sw.ElapsedMilliseconds} ms - sb.Size{sb.Length}");
            sw.Reset();
            
            
            sw.Start();
            for (int i = 0; i < Constants.k_IterationCount; i++)
            {
                sb.Append(string.Format("{0} - {1}", intList[i], intList[i]));
            }
            sw.Stop();
            Console.WriteLine($"Int Format:{sw.ElapsedMilliseconds} ms - sb.Size{sb.Length}");
            sw.Reset();
        }
    }
}