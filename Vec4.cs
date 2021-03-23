using System;
using System.Runtime.CompilerServices;

namespace SpeedTests
{
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
            public static bool IsEqualShortCircuit(Vec4 lhs, Vec4 rhs, float precision = Constants.k_Epsilon)
            {
                if (FastApproximately(rhs.x, lhs.x, precision) == false)
                    return false;
                if (FastApproximately(rhs.y, lhs.y, precision) == false)
                    return false;
                if (FastApproximately(rhs.z, lhs.z, precision) == false)
                    return false;
                return FastApproximately(rhs.w, lhs.w, precision);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool FastApproximately(float a, float b, float precision = Constants.k_Epsilon)
            {
                return ((a - b) < 0 ? ((a - b) * -1) : (a - b)) <= precision;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool IsEqualAbs(Vec4 lhs, Vec4 rhs)
            {
                return Math.Abs(lhs.x - rhs.x) + Math.Abs(lhs.y - rhs.y) + Math.Abs(lhs.z - rhs.z) +
                    Math.Abs(lhs.w - rhs.w) < Constants.k_Epsilon;
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool IsEqual(Vec4 lhs, Vec4 rhs)
            {
                float num1 = lhs.x - rhs.x;
                float num2 = lhs.y - rhs.y;
                float num3 = lhs.z - rhs.z;
                float num4 = lhs.w - rhs.w;
                return num1 * (double) num1 + num2 * (double) num2 + num3 * (double) num3 + num4 * (double) num4 < Constants.k_SquaredEpsilon;
            }
        }
}