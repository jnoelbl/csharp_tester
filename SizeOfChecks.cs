using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SpeedTests
{
    public static class SizeOfChecks
    {
        public static void Check()
        {
            try
            {
                WriteSizeOfType<int>();
                WriteSizeOfType<float>();
                WriteSizeOfType<double>();
                WriteSizeOfType<char>();
                // not fixed can't be printed.
                //WriteSizeOfType<string>();
                //WriteSizeOfType<Dictionary<string,string>>();
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"Exception occured:{e}");
            }
        }

        private static void WriteSizeOfType<T>()
        {
            Console.WriteLine($"Size of {typeof(T)}: {Marshal.SizeOf<T>().ToString()}");
        }
    }
}