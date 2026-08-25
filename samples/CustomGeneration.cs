// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

using Xunit.Sdk;

public class MixedGeneratedData
{
    #region MixedGeneratedData
    public static IReadOnlyCollection<ITheoryDataRow> Cases =>
        new CombinatorialTheoryDataBuilder()
            .AddRows([10, 0], [5, 2])
            .AddValues(true, false)
            .AddTestCase(6, 2, false)
            .BuildCombinations();

    [Theory, MemberData(nameof(Cases))]
    public void Example(int a, int b, bool c)
    {
        Assert.True(a > b);
    }
    #endregion
}

public class GeneratedPermutations
{
    #region GeneratedPermutations
    public static IEnumerable<object?[]> Permutations =>
        CombinatorialTestCaseGenerator.GeneratePermutations([1, 2, 3])
            .Select(permutation => new object?[] { permutation });

    [Theory, MemberData(nameof(Permutations))]
    public void Example(int[] values)
    {
        Assert.Equal(3, values.Length);
    }
    #endregion
}

public class ConstrainedPairwiseData
{
    #region ConstrainedPairwiseData
    public static IReadOnlyCollection<ITheoryDataRow> CreateCases(int? seed = null)
    {
        return new CombinatorialTheoryDataBuilder()
            .AddValues(0, 1, 2)
            .AddValues("small", "large")
            .AddValues(false, true)
            .Where(row => !Equals(row[0], 0) || !Equals(row[1], "large"))
            .BuildPairwiseCombinations(seed);
    }
    #endregion
}

public class LowLevelGeneration
{
    #region LowLevelGeneration
    public static IEnumerable<object?[]> Cases
    {
        get
        {
            string[] operatingSystems = ["Windows", "Linux"];
            int[] runtimes = [8, 9, 10];
            foreach (int[] selection in CombinatorialTestCaseGenerator.GeneratePairwiseCombinations(
                [operatingSystems.Length, runtimes.Length]))
            {
                yield return [operatingSystems[selection[0]], runtimes[selection[1]]];
            }
        }
    }
    #endregion
}
