// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

using Xunit.Sdk;
using Xunit.v3;

namespace Xunit;

/// <summary>
/// Determines whether a generated theory data row is allowed.
/// </summary>
/// <param name="values">The values in the completed theory data row.</param>
/// <returns><see langword="true"/> if the row is allowed; otherwise, <see langword="false"/>.</returns>
public delegate bool CombinatorialTheoryDataPredicate(ReadOnlySpan<object?> values);

/// <summary>
/// Builds xUnit theory data from hand-authored base rows and generated columns.
/// </summary>
public sealed class CombinatorialTheoryDataBuilder
{
    private readonly List<object?[]> baseRows = [];
    private readonly List<object?[]> generatedColumns = [];
    private readonly List<object?[]> explicitTestCases = [];
    private readonly List<CombinatorialTheoryDataPredicate> predicates = [];
    private int? baseColumnCount;

    /// <summary>
    /// Adds complete hand-authored rows for the base columns.
    /// </summary>
    /// <param name="rows">The rows to add. Every row must have the same width.</param>
    /// <returns>This builder.</returns>
    public CombinatorialTheoryDataBuilder AddRows(params ReadOnlySpan<object?[]> rows)
    {
        if (rows.Length == 0)
        {
            throw new ArgumentException("At least one row is required.", nameof(rows));
        }

        this.EnsureBaseRowsMayBeAdded();
        for (int i = 0; i < rows.Length; i++)
        {
            if (rows[i] is null)
            {
                throw new ArgumentException("Base rows cannot be null.", nameof(rows));
            }

            this.AddBaseRow(rows[i]);
        }

        return this;
    }

    /// <summary>
    /// Adds complete hand-authored rows for the base columns.
    /// </summary>
    /// <param name="rows">The rows to add. Every row must have the same width.</param>
    /// <returns>This builder.</returns>
    public CombinatorialTheoryDataBuilder AddRows(IEnumerable<IReadOnlyList<object?>> rows)
    {
        Requires.NotNull(rows, nameof(rows));
        this.EnsureBaseRowsMayBeAdded();

        bool any = false;
        foreach (IReadOnlyList<object?> row in rows)
        {
            if (row is null)
            {
                throw new ArgumentException("Base rows cannot be null.", nameof(rows));
            }

            any = true;
            object?[] rowCopy = new object?[row.Count];
            for (int i = 0; i < row.Count; i++)
            {
                rowCopy[i] = row[i];
            }

            this.AddBaseRow(rowCopy);
        }

        if (!any)
        {
            throw new ArgumentException("At least one row is required.", nameof(rows));
        }

        return this;
    }

    /// <summary>
    /// Adds a generated column with the specified candidate values.
    /// </summary>
    /// <typeparam name="T">The type of values in the column.</typeparam>
    /// <param name="values">The candidate values for the column.</param>
    /// <returns>This builder.</returns>
    public CombinatorialTheoryDataBuilder AddValues<T>(params ReadOnlySpan<T> values)
    {
        if (values.Length == 0)
        {
            throw new ArgumentException("At least one value is required.", nameof(values));
        }

        object?[] valuesCopy = new object?[values.Length];
        for (int i = 0; i < values.Length; i++)
        {
            valuesCopy[i] = values[i];
        }

        this.generatedColumns.Add(valuesCopy);
        return this;
    }

    /// <summary>
    /// Adds a generated column with the specified candidate values.
    /// </summary>
    /// <typeparam name="T">The type of values in the column.</typeparam>
    /// <param name="values">The candidate values for the column.</param>
    /// <returns>This builder.</returns>
    public CombinatorialTheoryDataBuilder AddValues<T>(IEnumerable<T> values)
    {
        Requires.NotNull(values, nameof(values));
        object?[] valuesCopy = values.Cast<object?>().ToArray();
        if (valuesCopy.Length == 0)
        {
            throw new ArgumentException("At least one value is required.", nameof(values));
        }

        this.generatedColumns.Add(valuesCopy);
        return this;
    }

    /// <summary>
    /// Adds a complete hand-authored test case after the generated rows.
    /// </summary>
    /// <param name="values">The values in the test case.</param>
    /// <returns>This builder.</returns>
    public CombinatorialTheoryDataBuilder AddTestCase(params ReadOnlySpan<object?> values)
    {
        this.explicitTestCases.Add(values.ToArray());
        return this;
    }

    /// <summary>
    /// Adds a constraint that generated test cases must satisfy.
    /// </summary>
    /// <param name="isTestCaseAllowed">The predicate to evaluate against completed generated rows.</param>
    /// <returns>This builder.</returns>
    public CombinatorialTheoryDataBuilder Where(CombinatorialTheoryDataPredicate isTestCaseAllowed)
    {
        Requires.NotNull(isTestCaseAllowed, nameof(isTestCaseAllowed));
        this.predicates.Add(isTestCaseAllowed);
        return this;
    }

    /// <summary>
    /// Builds every possible combination of the configured base rows and generated columns.
    /// </summary>
    /// <returns>The generated rows followed by explicitly added test cases.</returns>
    public IReadOnlyCollection<ITheoryDataRow> BuildCombinations()
    {
        return this.Build(pairwise: false, seed: null);
    }

    /// <summary>
    /// Builds rows that cover every possible pair across the configured base rows and generated columns.
    /// </summary>
    /// <param name="seed">An optional seed used to vary the generated covering set. Omit for deterministic results.</param>
    /// <returns>The generated rows followed by explicitly added test cases.</returns>
    public IReadOnlyCollection<ITheoryDataRow> BuildPairwiseCombinations(int? seed = null)
    {
        return this.Build(pairwise: true, seed);
    }

    private void AddBaseRow(ReadOnlySpan<object?> row)
    {
        if (this.baseColumnCount is int expectedWidth && row.Length != expectedWidth)
        {
            throw new ArgumentException($"Expected a base row with {expectedWidth} values, but found {row.Length}.", nameof(row));
        }

        this.baseColumnCount ??= row.Length;
        this.baseRows.Add(row.ToArray());
    }

    private IReadOnlyCollection<ITheoryDataRow> Build(bool pairwise, int? seed)
    {
        int totalColumns = (this.baseColumnCount ?? 0) + this.generatedColumns.Count;
        foreach (object?[] testCase in this.explicitTestCases)
        {
            if (testCase.Length != totalColumns)
            {
                throw new InvalidOperationException($"Expected explicit test cases to have {totalColumns} values, but found {testCase.Length}.");
            }
        }

        List<object?[]> dimensions = [];
        if (this.baseRows.Count > 0)
        {
            dimensions.Add(this.baseRows.Cast<object?>().ToArray());
        }

        dimensions.AddRange(this.generatedColumns);
        int[] dimensionSizes = dimensions.Select(dimension => dimension.Length).ToArray();
        CombinatorialIndexPredicate? indexPredicate = this.predicates.Count == 0
            ? null
            : indices => this.IsAllowed(dimensions, indices);
        int[][] selections = pairwise
            ? CombinatorialTestCaseGenerator.GeneratePairwiseCombinations(dimensionSizes, indexPredicate, seed)
            : CombinatorialTestCaseGenerator.GenerateCombinations(dimensionSizes, indexPredicate);

        List<ITheoryDataRow> results = new(selections.Length + this.explicitTestCases.Count);
        foreach (int[] selection in selections)
        {
            results.Add(new TheoryDataRow(this.Flatten(dimensions, selection)));
        }

        foreach (object?[] testCase in this.explicitTestCases)
        {
            results.Add(new TheoryDataRow([.. testCase]));
        }

        return results;
    }

    private void EnsureBaseRowsMayBeAdded()
    {
        if (this.generatedColumns.Count > 0)
        {
            throw new InvalidOperationException($"{nameof(this.AddRows)} must be called before {nameof(this.AddValues)}.");
        }
    }

    private object?[] Flatten(List<object?[]> dimensions, ReadOnlySpan<int> indices)
    {
        int outputLength = (this.baseColumnCount ?? 0) + this.generatedColumns.Count;
        object?[] values = new object?[outputLength];
        int outputIndex = 0;
        int dimensionIndex = 0;

        if (this.baseRows.Count > 0)
        {
            object?[] baseRow = this.baseRows[indices[dimensionIndex]];
            Array.Copy(baseRow, 0, values, 0, baseRow.Length);
            outputIndex += baseRow.Length;
            dimensionIndex++;
        }

        for (; dimensionIndex < dimensions.Count; dimensionIndex++)
        {
            values[outputIndex++] = dimensions[dimensionIndex][indices[dimensionIndex]];
        }

        return values;
    }

    private bool IsAllowed(List<object?[]> dimensions, ReadOnlySpan<int> indices)
    {
        object?[] values = this.Flatten(dimensions, indices);
        foreach (CombinatorialTheoryDataPredicate predicate in this.predicates)
        {
            if (!predicate(values))
            {
                return false;
            }
        }

        return true;
    }
}
