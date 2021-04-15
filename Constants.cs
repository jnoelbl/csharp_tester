using System.Runtime.CompilerServices;

namespace SpeedTests
{
    public static class Constants
    {
        public const int k_IterationCount = 100000000;
        public const int k_Seed = 11515;
        public const float k_Epsilon = 1E-05f;
        public const double k_SquaredEpsilon = k_Epsilon * k_Epsilon;
    }

    public static class Shared
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FloatCompare(float lhs, float rhs, float precision = Constants.k_Epsilon)
        {
            float diff = lhs - rhs;
            return diff * (double) diff < precision * (double) precision ? 0 : (int) (diff * (1.0f / precision));
        }
    }
}