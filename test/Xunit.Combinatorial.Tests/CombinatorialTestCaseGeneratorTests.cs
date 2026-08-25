// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

using System.Reflection;
using Xunit;
using Xunit.Sdk;

public class CombinatorialTestCaseGeneratorTests
{
    [Fact]
    public void GenerateCombinations_ReturnsCartesianProductInStableOrder()
    {
        int[][] results = CombinatorialTestCaseGenerator.GenerateCombinations([2, 3]);

        Assert.Equal(
            [
                [0, 0],
                [0, 1],
                [0, 2],
                [1, 0],
                [1, 1],
                [1, 2],
            ],
            results);
    }

    [Fact]
    public void GenerateCombinations_AppliesConstraint()
    {
        int[][] results = CombinatorialTestCaseGenerator.GenerateCombinations(
            [3, 3],
            indices => indices[0] < indices[1]);

        Assert.Equal([[0, 1], [0, 2], [1, 2]], results);
    }

    [Theory]
    [InlineData(new int[0])]
    [InlineData(new[] { 2, 0, 3 })]
    public void GenerateCombinations_EmptyDimensionProducesNoRows(int[] dimensions)
    {
        Assert.Empty(CombinatorialTestCaseGenerator.GenerateCombinations(dimensions));
        Assert.Empty(CombinatorialTestCaseGenerator.GeneratePairwiseCombinations(dimensions));
    }

    [Fact]
    public void GenerateCombinations_RejectsNegativeDimension()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => CombinatorialTestCaseGenerator.GenerateCombinations([2, -1]));
        Assert.Throws<ArgumentOutOfRangeException>(() => CombinatorialTestCaseGenerator.GeneratePairwiseCombinations([2, -1]));
    }

    [Fact]
    public void GeneratePairwiseCombinations_DefaultSeedIsStable()
    {
        int[][] first = CombinatorialTestCaseGenerator.GeneratePairwiseCombinations([3, 4, 2, 3]);
        int[][] second = CombinatorialTestCaseGenerator.GeneratePairwiseCombinations([3, 4, 2, 3]);

        Assert.Equal(first, second);
        AssertPairwiseCoverage(first, [3, 4, 2, 3]);
    }

    [Fact]
    public void GeneratePairwiseCombinations_SeedIsRepeatableAndCanVaryCoveringSet()
    {
        int[][] first = CombinatorialTestCaseGenerator.GeneratePairwiseCombinations([3, 4, 2, 3], seed: 1);
        int[][] repeated = CombinatorialTestCaseGenerator.GeneratePairwiseCombinations([3, 4, 2, 3], seed: 1);
        int[][] second = CombinatorialTestCaseGenerator.GeneratePairwiseCombinations([3, 4, 2, 3], seed: 2);

        Assert.Equal(first, repeated);
        Assert.NotEqual(first, second);
        AssertPairwiseCoverage(first, [3, 4, 2, 3]);
        AssertPairwiseCoverage(second, [3, 4, 2, 3]);
    }

    [Fact]
    public void GeneratePairwiseCombinations_ImpossibleConstraintProducesNoRows()
    {
        int[][] results = CombinatorialTestCaseGenerator.GeneratePairwiseCombinations([2, 2], _ => false);

        Assert.Empty(results);
    }

    [Fact]
    public void GeneratePairwiseCombinations_ConstraintPreservesPossiblePairCoverage()
    {
        int[][] results = CombinatorialTestCaseGenerator.GeneratePairwiseCombinations(
            [2, 2, 2],
            indices => indices[0] != 1 || indices[1] != 1);

        Assert.All(results, indices => Assert.False(indices[0] == 1 && indices[1] == 1));
        AssertPairwiseCoverage(
            results,
            [2, 2, 2],
            indices => indices[0] != 1 || indices[1] != 1);
    }

    [Fact]
    public void GeneratePermutations_ReturnsEveryPositionalPermutation()
    {
        int[][] results = CombinatorialTestCaseGenerator.GeneratePermutations([1, 2, 3]);

        Assert.Equal(
            [
                [1, 2, 3],
                [1, 3, 2],
                [2, 1, 3],
                [2, 3, 1],
                [3, 1, 2],
                [3, 2, 1],
            ],
            results);
    }

    [Fact]
    public void GeneratePermutations_TreatsDuplicatePositionsAsDistinct()
    {
        int[][] results = CombinatorialTestCaseGenerator.GeneratePermutations([1, 1, 2]);

        Assert.Equal(6, results.Length);
        Assert.Equal(3, results.Distinct(ArrayEqualityComparer<int>.Instance).Count());
    }

    [Fact]
    public void GeneratePermutations_EmptyInputProducesNoRows()
    {
        Assert.Empty(CombinatorialTestCaseGenerator.GeneratePermutations<int>([]));
    }

    [Fact]
    public void PublicParamsOverloadsUseReadOnlySpan()
    {
        MethodInfo addRows = typeof(CombinatorialTheoryDataBuilder).GetMethod(
            nameof(CombinatorialTheoryDataBuilder.AddRows),
            [typeof(ReadOnlySpan<object?[]>)])!;
        MethodInfo addValues = typeof(CombinatorialTheoryDataBuilder).GetMethods()
            .Single(method =>
                method.Name == nameof(CombinatorialTheoryDataBuilder.AddValues) &&
                method.IsGenericMethod &&
                method.GetParameters()[0].ParameterType.IsGenericType &&
                method.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(ReadOnlySpan<>));
        MethodInfo addTestCase = typeof(CombinatorialTheoryDataBuilder).GetMethod(
            nameof(CombinatorialTheoryDataBuilder.AddTestCase),
            [typeof(ReadOnlySpan<object?>)])!;

        Assert.Equal(typeof(ReadOnlySpan<object?[]>), addRows.GetParameters()[0].ParameterType);
        Assert.Equal(typeof(ReadOnlySpan<>), addValues.GetParameters()[0].ParameterType.GetGenericTypeDefinition());
        Assert.Equal(typeof(ReadOnlySpan<object?>), addTestCase.GetParameters()[0].ParameterType);
        AssertParamsCollection(addRows);
        AssertParamsCollection(addValues);
        AssertParamsCollection(addTestCase);
    }

    private static void AssertPairwiseCoverage(
        int[][] results,
        int[] dimensions,
        CombinatorialIndexPredicate? isAllowed = null)
    {
        for (int firstDimension = 0; firstDimension < dimensions.Length - 1; firstDimension++)
        {
            for (int secondDimension = firstDimension + 1; secondDimension < dimensions.Length; secondDimension++)
            {
                for (int firstValue = 0; firstValue < dimensions[firstDimension]; firstValue++)
                {
                    for (int secondValue = 0; secondValue < dimensions[secondDimension]; secondValue++)
                    {
                        bool pairIsPossible = CombinatorialTestCaseGenerator.GenerateCombinations(
                            dimensions,
                            indices =>
                                indices[firstDimension] == firstValue &&
                                indices[secondDimension] == secondValue &&
                                (isAllowed?.Invoke(indices) ?? true)).Length > 0;
                        if (pairIsPossible)
                        {
                            Assert.Contains(
                                results,
                                row => row[firstDimension] == firstValue && row[secondDimension] == secondValue);
                        }
                    }
                }
            }
        }
    }

    private static void AssertParamsCollection(MethodInfo method)
    {
        Assert.Contains(
            method.GetParameters()[0].GetCustomAttributesData(),
            attribute => attribute.AttributeType.FullName == "System.Runtime.CompilerServices.ParamCollectionAttribute");
    }

    private sealed class ArrayEqualityComparer<T> : IEqualityComparer<T[]>
    {
        internal static readonly ArrayEqualityComparer<T> Instance = new();

        public bool Equals(T[]? x, T[]? y) => x is not null && y is not null && x.SequenceEqual(y);

        public int GetHashCode(T[] obj)
        {
            int hashCode = 17;
            foreach (T value in obj)
            {
                hashCode = (hashCode * 31) + EqualityComparer<T>.Default.GetHashCode(value!);
            }

            return hashCode;
        }
    }
}
