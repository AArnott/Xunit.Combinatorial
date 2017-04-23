namespace Xunit.Combinatorial.Tests.Utils
{
    using System.Linq;
    using Validation;

    internal static class AssertTuples
    {
        public static void AreCovered(object[][] expectedCombinatorial, object[][] testCases)
        {
            if (expectedCombinatorial.Any())
            {
                // Verify that the pairwise result covers every pair.
                object[][] possibleValues = ExtractPossibleValues(expectedCombinatorial);

                for (int i = 0; i < possibleValues.Length - 1; i++)
                {
                    for (int j = i + 1; j < possibleValues.Length; j++)
                    {
                        foreach (var iValue in possibleValues[i])
                        {
                            foreach (var jValue in possibleValues[j])
                            {
                                Assert.True(testCases.Any(testCase => testCase[i].Equals(iValue) && testCase[j].Equals(jValue)));
                            }
                        }
                    }
                }
            }
        }

        private static object[][] ExtractPossibleValues(object[][] combinatorialTestCases)
        {
            Requires.NotNull(combinatorialTestCases, nameof(combinatorialTestCases));

            var possibleValues = new object[combinatorialTestCases.First().Length][];
            for (int i = 0; i < possibleValues.Length; i++)
            {
                possibleValues[i] = combinatorialTestCases.Select(ctc => ctc[i]).Distinct().ToArray();
            }

            return possibleValues;
        }
    }
}
