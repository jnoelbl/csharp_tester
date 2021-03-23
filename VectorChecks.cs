using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SpeedTests
{
    public class VectorChecks
    {
        public static void VectorEqualTest()
        {
            
            Console.WriteLine("Running VectorCompareTest");
            Stopwatch sw = new Stopwatch();
            List<Vec4> vecList = new List<Vec4>(Constants.k_IterationCount);

            Random random = new Random(Constants.k_Seed);

            sw.Start();
            float val = 0;
            for (int i = 0; i < Constants.k_IterationCount; i++)
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
            for (int i = 0; i < Constants.k_IterationCount - 1; i++)
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
            for (int i = 0; i < Constants.k_IterationCount - 1; i++)
            {
                if (Vec4.IsEqual(vecList[i], vecList[i + 1]))
                {
                    equal++;
                }
            }
            sw.Stop();
            System.Console.WriteLine($"VectorComparison_Squared: Equal:{equal} - test duration:{sw.ElapsedMilliseconds} ms");
            sw.Reset();

            equal = 0;
            sw.Start();
            for (int i = 0; i < Constants.k_IterationCount - 1; i++)
            {
                if (Vec4.IsEqualShortCircuit(vecList[i], vecList[i + 1]))
                {
                    equal++;
                }
            }
            sw.Stop();
            System.Console.WriteLine($"VectorComparison_ShortCircuit: Equal:{equal} - test duration:{sw.ElapsedMilliseconds} ms");
        }
        public static void VectorAccessTest()
        {
            
            Console.WriteLine("Running VectorAccessTest");
            Stopwatch sw = new Stopwatch();
            List<Vec4> vecList = new List<Vec4>(Constants.k_IterationCount);

            Random random = new Random(Constants.k_Seed);

            sw.Start();
            float val = 0;
            for (int i = 0; i < Constants.k_IterationCount; i++)
            {
                vecList.Add(new Vec4
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

            StringBuilder sb = new StringBuilder();
            
            sw.Reset();
            
            sw.Start();
            for (int i = 0; i < Constants.k_IterationCount - 1; i++)
            {
                sb.AppendFormat("{0:f3} {1:f3} {2:f3} {3:f3}", vecList[i].x, vecList[i].y, vecList[i].z, vecList[i].w);
            }
            sw.Stop();
            System.Console.WriteLine($"Vector array access - test duration:{sw.ElapsedMilliseconds} ms");
            sw.Reset();
            
            sb.Clear();
            
            sw.Start();
            for (int i = 0; i < Constants.k_IterationCount - 1; i++)
            {
                Vec4 vec = vecList[i];
                sb.AppendFormat("{0:f3} {1:f3} {2:f3} {3:f3}", vec.x, vec.y, vec.z, vec.w);
            }
            sw.Stop();
            System.Console.WriteLine($"Vector assignment - test duration:{sw.ElapsedMilliseconds} ms");
        }
    }
}