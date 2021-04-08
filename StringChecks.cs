using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace SpeedTests
{
    public class StringConcatTesting
    {
        private readonly List<float> floatList = new List<float>();
        private readonly Random _random = new Random(Constants.k_Seed);
        private const string k_InitialString = "0x2020202";
        private readonly StringBuilder _sb = new StringBuilder();
        private int _count;
        
        [GlobalSetup(Targets = new[]
        {
            nameof(StringInterpolationFloatBoxing), nameof(StringInterpolationFloatToString), 
            nameof(StringAppendFormatFloat), nameof(StringAppendFormatFloatToString)
        })]
        public void FloatListSetup()
        {
            _count = Constants.k_IterationCount * 6;
            for (int i = 0; i < _count; i++)
            {
                floatList.Add(_random.Next());
            }
        }

        [Benchmark]
        public void StringInterpolationFloatBoxing()
        {
            for (int i = 0; i < _count / 6; i += 6)
            {
                _sb.Append($"2 {k_InitialString} {floatList[i]:f3} {floatList[i + 1]:f3} {-floatList[i + 2]:f3} {floatList[i + 3]:f3} {-floatList[i + 4]:f3} {floatList[i + 5]:f3}");
            }
        }

        [Benchmark]
        public void StringInterpolationFloatToString()
        {
            for (int i = 0; i < _count / 6; i += 6)
            {
                _sb.Append(
                    $"2 {k_InitialString} {floatList[i].ToString("F3")} {floatList[i + 1].ToString("F3")} {(-floatList[i + 2]).ToString("F3")} {floatList[i + 3].ToString("F3")} {(-floatList[i + 4]).ToString("F3")} {floatList[i + 5].ToString("F3")}");
            }
        }

        [Benchmark]
        public void StringAppendFormatFloat()
        {
            for (int i = 0; i < _count / 6; i += 6)
            {
                _sb.AppendFormat("2 {0} {1:f3} {2:f3} {3:f3} {4:f3} {5:f3} {6:f3}", k_InitialString, floatList[i], floatList[i + 1], -floatList[i + 2], floatList[i + 3], -floatList[i + 4], floatList[i + 5]);
            }
        }

        [Benchmark]
        public void StringAppendFormatFloatToString()
        {
            
            for (int i = 0; i < _count / 6; i += 6)
            {
                _sb.AppendFormat("2 {0} {1} {2} {3} {4} {5} {6}", k_InitialString, floatList[i].ToString("F3"), floatList[i + 1].ToString("F3"), (-floatList[i + 2]).ToString("F3"), floatList[i + 3].ToString("F3"), (-floatList[i + 4]).ToString("F3"), floatList[i + 5].ToString("F3"));
            }
        }
    }

    public class StringSplitVsStartsWithTesting
    {
        private const string k_DirectoryPath = "/Users/jnoel/Development/_build_studio/ldraw";

        private string[] _filePaths;

        private int _count = 0;

        [GlobalSetup]
        public void Setup()
        {
            if (Directory.Exists(k_DirectoryPath))
            {
                _filePaths = Directory.GetFiles(k_DirectoryPath, "*", SearchOption.AllDirectories);
                string[] limitedPaths = new string[500];
                Array.Copy(_filePaths, limitedPaths, 500);
                _filePaths = limitedPaths;
            }
        }

        [IterationSetup] 
        public void IterationStart() => _count = 0;

        [IterationCleanup]
        public void IterationEnd() => Console.WriteLine($"Count:{_count.ToString()}");

        //[Benchmark]
        public void SplitRemoveEntries()
        {
            foreach (string filePath in _filePaths)
            {
                if (!File.Exists(filePath))
                    continue;
                
                string[] lines = File.ReadAllLines(filePath);
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] tokens = lines[i].Split((char[]) null, StringSplitOptions.RemoveEmptyEntries);
                    if (tokens[0] == "0")
                    {
                        _count++;
                    }
                }
            }
        }
        //[Benchmark]
        public void SplitEntries()
        {
            foreach (string filePath in _filePaths)
            {
                if (!File.Exists(filePath))
                    continue;
                string[] lines = File.ReadAllLines(filePath);
                
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] tokens = lines[i].Split(null);
                    if (tokens[0] == "0")
                    {
                        _count++;
                    }
                }
            }
        }
        //[Benchmark]
        public void StartsWith()
        {
            foreach (string filePath in _filePaths)
            {
                if (!File.Exists(filePath))
                    continue;
                string[] lines = File.ReadAllLines(filePath);
                
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("0"))
                    {
                        _count++;
                    }
                }
            }
        }

        [Benchmark]
        public void SplitAndJoin()
        {
            foreach (string filePath in _filePaths)
            {
                if (!File.Exists(filePath))
                    continue;
                
                string[] lines = File.ReadAllLines(filePath);
                
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    string[] tokens = line.Split((char[]) null, StringSplitOptions.RemoveEmptyEntries);
                    if (tokens.Length <= 0)
                        continue;

                    StringBuilder sb = new StringBuilder(line.Length);
                    sb.Append(tokens[0]);
                    for (int j = 1; j < tokens.Length; j++)
                    {
                        sb.Append(' ');
                        sb.Append(tokens[j]);
                    }

                    line = sb.ToString().Trim();

                    _count += line.Length;
                }
            }
        }
        [Benchmark]
        public void ContainsAndReplace()
        {
            foreach (string filePath in _filePaths)
            {
                if (!File.Exists(filePath))
                    continue;

                string[] lines = File.ReadAllLines(filePath);
                
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i].Replace("\t", " ").Trim();
                    line = line.Replace("  ", " ");
                    while (line.Contains("  "))
                        line = line.Replace("  ", " ");

                    _count += line.Length;
                }
            }
        }

        [Benchmark]
        public void StringBuilderCleanup()
        {
            foreach (string filePath in _filePaths)
            {
                if (!File.Exists(filePath))
                    continue;
                
                string[] lines = File.ReadAllLines(filePath);
                
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = RemoveExtraWhiteSpace(lines[i]);
                    _count += line.Length;
                }
            }
        }
        [Benchmark]
        public void CharArrayCleanup()
        {
            foreach (string filePath in _filePaths)
            {
                if (!File.Exists(filePath))
                    continue;
                
                string[] lines = File.ReadAllLines(filePath);
                
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = RemoveExtraWhiteSpaceCharArray(lines[i]);
                    _count += line.Length;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string RemoveExtraWhiteSpaceCharArray(string text)
        {
            bool inSpaceGroup = false;
            char[] charArray = text.ToCharArray();
            char[] newCharArray = new char[charArray.Length];
            int insertPoint = 0;
            for (int i = 0; i < charArray.Length; i++)
            {
                char c = charArray[i];
                if (inSpaceGroup)
                {
                    if (c == ' ' || c == '\t') 
                        continue;
                    
                    inSpaceGroup = false;
                    newCharArray[insertPoint++] = c;
                }
                else if (c == ' ' || c == '\t')
                {
                    inSpaceGroup = true;
                    newCharArray[insertPoint++] = ' ';
                }
                else
                {
                    newCharArray[insertPoint++] = c;
                }
            }

            return new string(newCharArray, 0, insertPoint);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string RemoveExtraWhiteSpace(string text)
        {
            bool inSpaceGroup = false;
            StringBuilder sb = new StringBuilder(text.Length);
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (inSpaceGroup)
                {
                    if (c == ' ' || c == '\t') 
                        continue;
                    
                    inSpaceGroup = false;
                    sb.Append(c);
                }
                else if (c == ' ' || c == '\t')
                {
                    inSpaceGroup = true;
                    sb.Append(' ');
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString().Trim();
        }

    }
    public class StringChecks
    {
        public static void StringLiteralCheck()
        {
            string literal = @"Testing item
                More text 
                some more text
                each might be a new line, which is what I'm afraid of.";

            string nonLiteral = "Testin item " +
                                "More Text " +
                                "some more text " +
                                "each should be a new line, which is what I'm afraid of.";
            
            Console.WriteLine($"Literal:\n{literal}");
            Console.WriteLine($"NonLiteral:\n{nonLiteral}");
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