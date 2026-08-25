// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

using Xunit;
using Xunit.Sdk;

public class CombinatorialTheoryDataBuilderTests
{
    [Fact]
    public void BuildCombinations_ExpandsGeneratedColumnsAcrossBaseRows()
    {
        IReadOnlyCollection<ITheoryDataRow> results = new CombinatorialTheoryDataBuilder()
            .AddRows([10, 0], [5, 2])
            .AddValues(true, false)
            .AddTestCase(6, 2, false)
            .BuildCombinations();

        Assert.Equal(
            [
                [10, 0, true],
                [10, 0, false],
                [5, 2, true],
                [5, 2, false],
                [6, 2, false],
            ],
            results.Select(row => row.GetData()));
    }

    [Fact]
    public void BuildCombinations_CanGenerateColumnsWithoutBaseRows()
    {
        IReadOnlyCollection<ITheoryDataRow> results = new CombinatorialTheoryDataBuilder()
            .AddValues(1, 2)
            .AddValues("a", "b")
            .BuildCombinations();

        Assert.Equal(
            [
                [1, "a"],
                [1, "b"],
                [2, "a"],
                [2, "b"],
            ],
            results.Select(row => row.GetData()));
    }

    [Fact]
    public void BuildCombinations_MultipleAddRowsCallsAppendBaseRows()
    {
        IReadOnlyCollection<ITheoryDataRow> results = new CombinatorialTheoryDataBuilder()
            .AddRows([1, 2])
            .AddRows([3, 4])
            .AddValues(false, true)
            .BuildCombinations();

        Assert.Equal(4, results.Count);
        Assert.Contains(results, row => row.GetData().SequenceEqual([3, 4, true]));
    }

    [Fact]
    public void BuildCombinations_AppliesAllConstraintsOnlyToGeneratedRows()
    {
        IReadOnlyCollection<ITheoryDataRow> results = new CombinatorialTheoryDataBuilder()
            .AddRows([10, 0], [5, 2])
            .AddValues(true, false)
            .Where(row => (int)row[0]! > 5)
            .Where(row => (bool)row[2]!)
            .AddTestCase(1, 2, false)
            .BuildCombinations();

        Assert.Equal([[10, 0, true], [1, 2, false]], results.Select(row => row.GetData()));
    }

    [Fact]
    public void BuildPairwiseCombinations_CoversPairsAcrossBaseRowsAndColumns()
    {
        IReadOnlyCollection<ITheoryDataRow> results = new CombinatorialTheoryDataBuilder()
            .AddRows([10, 0], [5, 2], [8, 3])
            .AddValues("a", "b", "c")
            .AddValues(false, true)
            .BuildPairwiseCombinations(seed: 42);

        object?[][] rows = results.Select(row => row.GetData()).ToArray();
        foreach (int firstBaseValue in new[] { 10, 5, 8 })
        {
            foreach (string text in new[] { "a", "b", "c" })
            {
                Assert.Contains(rows, row => Equals(row[0], firstBaseValue) && Equals(row[2], text));
            }

            foreach (bool flag in new[] { false, true })
            {
                Assert.Contains(rows, row => Equals(row[0], firstBaseValue) && Equals(row[3], flag));
            }
        }
    }

    [Fact]
    public void BuildPairwiseCombinations_AppliesConstraint()
    {
        IReadOnlyCollection<ITheoryDataRow> results = new CombinatorialTheoryDataBuilder()
            .AddValues(0, 1, 2)
            .AddValues("small", "large")
            .AddValues(false, true)
            .Where(row => !Equals(row[0], 0) || !Equals(row[1], "large"))
            .BuildPairwiseCombinations(seed: 42);

        Assert.DoesNotContain(results, row => Equals(row.GetData()[0], 0) && Equals(row.GetData()[1], "large"));
        Assert.Contains(results, row => Equals(row.GetData()[0], 0));
        Assert.Contains(results, row => Equals(row.GetData()[1], "large"));
    }

    [Fact]
    public void EnumerableOverloadsBuildRows()
    {
        IEnumerable<IReadOnlyList<object?>> baseRows =
        [
            new object?[] { 1, 2 },
            new object?[] { 3, 4 },
        ];
        IEnumerable<int> values = new List<int> { 5, 6 };

        IReadOnlyCollection<ITheoryDataRow> results = new CombinatorialTheoryDataBuilder()
            .AddRows(baseRows)
            .AddValues(values)
            .BuildCombinations();

        Assert.Equal(4, results.Count);
        Assert.Contains(results, row => row.GetData().SequenceEqual([3, 4, 6]));
    }

    [Fact]
    public void InputsAreSnapshotted()
    {
        object?[] baseRow = [1, 2];
        int[] values = [3, 4];
        CombinatorialTheoryDataBuilder builder = new CombinatorialTheoryDataBuilder()
            .AddRows(baseRow)
            .AddValues(values);

        baseRow[0] = 99;
        values[0] = 99;

        Assert.Equal(
            [[1, 2, 3], [1, 2, 4]],
            builder.BuildCombinations().Select(row => row.GetData()));
    }

    [Fact]
    public void AddRows_AfterAddValuesThrows()
    {
        CombinatorialTheoryDataBuilder builder = new CombinatorialTheoryDataBuilder().AddValues(1, 2);

        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => builder.AddRows([3, 4]));
        Assert.Contains(nameof(CombinatorialTheoryDataBuilder.AddRows), exception.Message);
    }

    [Fact]
    public void AddRows_RequiresConsistentWidth()
    {
        Assert.Throws<ArgumentException>(
            () => new CombinatorialTheoryDataBuilder().AddRows([1, 2], [3]));
    }

    [Fact]
    public void AddRows_RequiresAtLeastOneRow()
    {
        Assert.Throws<ArgumentException>(
            () => new CombinatorialTheoryDataBuilder().AddRows(ReadOnlySpan<object?[]>.Empty));
    }

    [Fact]
    public void AddRows_RejectsNullRows()
    {
        object?[] nullRow = null!;

        Assert.Throws<ArgumentException>(
            () => new CombinatorialTheoryDataBuilder().AddRows(nullRow));
    }

    [Fact]
    public void EnumerableAddRows_RequiresAtLeastOneRow()
    {
        Assert.Throws<ArgumentException>(
            () => new CombinatorialTheoryDataBuilder().AddRows((IEnumerable<IReadOnlyList<object?>>)Array.Empty<IReadOnlyList<object?>>()));
    }

    [Fact]
    public void EnumerableAddValues_RequiresAtLeastOneValue()
    {
        Assert.Throws<ArgumentException>(
            () => new CombinatorialTheoryDataBuilder().AddValues(Enumerable.Empty<int>()));
    }

    [Fact]
    public void AddValues_RequiresAtLeastOneValue()
    {
        Assert.Throws<ArgumentException>(
            () => new CombinatorialTheoryDataBuilder().AddValues(ReadOnlySpan<int>.Empty));
    }

    [Fact]
    public void BuildCombinations_RequiresExplicitRowsToMatchFinalWidth()
    {
        CombinatorialTheoryDataBuilder builder = new CombinatorialTheoryDataBuilder()
            .AddValues(1, 2)
            .AddTestCase(1, 2);

        Assert.Throws<InvalidOperationException>(() => builder.BuildCombinations());
    }

    [Fact]
    public void BuildCombinations_WithOnlyExplicitRows()
    {
        IReadOnlyCollection<ITheoryDataRow> results = new CombinatorialTheoryDataBuilder()
            .AddTestCase()
            .BuildCombinations();

        Assert.Single(results);
        Assert.Empty(results.Single().GetData());
    }
}
