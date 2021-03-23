namespace SpeedTests
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            FloatChecks.FloatCompareTest();
            DictionaryChecks.DictionaryTest();
            StringChecks.StringConcatTesting();
            StringChecks.StringConcatFormatTesting();
            VectorChecks.VectorEqualTest();
            VectorChecks.VectorAccessTest();
        }
    }
}