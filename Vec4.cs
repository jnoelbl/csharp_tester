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
                return FastApproximately(rhs.x, lhs.x, precision) &&
                       FastApproximately(rhs.y, lhs.y, precision) && 
                       FastApproximately(rhs.z, lhs.z, precision) && 
                       FastApproximately(rhs.w, lhs.w, precision);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static bool FastApproximately(float a, float b, float precision = Constants.k_Epsilon)
            {
                // cache the difference for use
                float diff = a - b;
                // Comparables are supposed to return
                // 0 if equal,
                // > 0 if v1 comes before v2,
                // < 0 if v1 comes after v2
                return diff * (double) diff < precision * (double) precision;
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

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static int Vector4Compare(Vec4 lhs, Vec4 rhs, float precision = Constants.k_Epsilon)
            {
                    int value = Shared.FloatCompare(lhs.x, rhs.x, precision);
                    if (value != 0) 
                        return value;
            
                    value = Shared.FloatCompare(lhs.y, rhs.y, precision);
                    if (value != 0) 
                        return value;
            
                    value = Shared.FloatCompare(lhs.z, rhs.z, precision);
            
                    return value != 0 ? value : Shared.FloatCompare(lhs.w, rhs.w, precision);
            }
        }
}