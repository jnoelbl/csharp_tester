using System;
using System.Runtime.CompilerServices;

namespace SpeedTests
{
        public struct Vec3
        {   
            //TODO: Vector3 min max comparisons
            
            
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

            public Vec3(float x, float y, float z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vec3 Min(Vec3 lhs, Vec3 rhs)
            {
                return new Vec3(FloatChecks.Min(lhs.x, rhs.x), FloatChecks.Min(lhs.y, rhs.y),
                    FloatChecks.Min(lhs.z, rhs.z));
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vec3 MinInline(Vec3 lhs, Vec3 rhs)
            {
                return new Vec3(FloatChecks.MinInline(lhs.x, rhs.x), FloatChecks.MinInline(lhs.y, rhs.y),
                    FloatChecks.MinInline(lhs.z, rhs.z));
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vec3 MinManualInline(Vec3 lhs, Vec3 rhs)
            {
                return new Vec3(lhs.x < rhs.x ? lhs.x : rhs.x,
                    lhs.y < rhs.y ? lhs.y : rhs.y,
                    lhs.z < rhs.z ? lhs.z : rhs.z);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vec3 MinManualInlineWithInitializer(Vec3 lhs, Vec3 rhs)
            {
                return new Vec3
                {
                    x = lhs.x < rhs.x ? lhs.x : rhs.x,
                    y = lhs.y < rhs.y ? lhs.y : rhs.y,
                    z = lhs.z < rhs.z ? lhs.z : rhs.z
                };
            }
        
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void MinRef(ref Vec3 lhs, Vec3 rhs)
            {
                lhs.x = FloatChecks.MinInline(lhs.x, rhs.x);
                lhs.y = FloatChecks.MinInline(lhs.y, rhs.y);
                lhs.z = FloatChecks.MinInline(lhs.z, rhs.z);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void MinRefManualInline(ref Vec3 lhs, Vec3 rhs)
            {
                lhs.x = lhs.x < rhs.x ? lhs.x : rhs.x;
                lhs.y = lhs.y < rhs.y ? lhs.y : rhs.y;
                lhs.z = lhs.z < rhs.z ? lhs.z : rhs.z;
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool IsEqualShortCircuit(Vec3 lhs, Vec3 rhs, float precision = Constants.k_Epsilon)
            {
                return FastApproximately(rhs.x, lhs.x, precision) && 
                       FastApproximately(rhs.y, lhs.y, precision) && 
                       FastApproximately(rhs.z, lhs.z, precision);
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
            public static bool IsEqualAbs(Vec3 lhs, Vec3 rhs)
            {
                return Math.Abs(lhs.x - rhs.x) + Math.Abs(lhs.y - rhs.y) + Math.Abs(lhs.z - rhs.z) < Constants.k_Epsilon;
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool IsEqual(Vec3 lhs, Vec3 rhs)
            {
                float num1 = lhs.x - rhs.x;
                float num2 = lhs.y - rhs.y;
                float num3 = lhs.z - rhs.z;
                return num1 * (double) num1 + num2 * (double) num2 + num3 * (double) num3 < Constants.k_SquaredEpsilon;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static int Vector3Compare(Vec3 lhs, Vec3 rhs, float precision = Constants.k_Epsilon)
            {
                    int value = Shared.FloatCompare(lhs.x, rhs.x, precision);
                    if (value != 0) 
                        return value;
            
                    value = Shared.FloatCompare(lhs.y, rhs.y, precision);
                    
                    return value != 0 ? value : Shared.FloatCompare(lhs.z, rhs.z, precision);
            }

            public override string ToString()
            {
                return $"({x}, {y}, {z})";
            }
        }
}