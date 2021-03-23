using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SpeedTests
{
    public class DictionaryChecks
    {
        public static void DictionaryTest()
        {
            
            Console.WriteLine("Running Dictionary Iteration Test");
            Stopwatch sw = new Stopwatch();
            List<Vec4> vecList = new List<Vec4>();
            Dictionary<Vec4, Vec4> vecDictionary = new Dictionary<Vec4, Vec4>(Constants.k_IterationCount);

            Random random = new Random(Constants.k_Seed);

            sw.Start();
            for (int i = 0; i < Constants.k_IterationCount; i++)
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
    }
}