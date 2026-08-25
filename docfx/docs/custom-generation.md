# Generating custom theory data

The parameter attributes in this package cover common cases, but ordinary xUnit member data is more expressive.
<xref:Xunit.CombinatorialTestCaseGenerator> and <xref:Xunit.CombinatorialTheoryDataBuilder> expose the same generation capabilities for use in your own fields, properties, and methods.

## Mix hand-authored rows with generated columns

Use <xref:Xunit.CombinatorialTheoryDataBuilder.AddRows(System.ReadOnlySpan{System.Object[]})> to establish a table of correlated base values.
Each call to <xref:Xunit.CombinatorialTheoryDataBuilder.AddValues``1(System.ReadOnlySpan{``0})> adds one generated column.
Passing an array to `AddValues` treats each array element as a candidate value for that column.

[!code-csharp[](../../samples/CustomGeneration.cs#MixedGeneratedData)]

The base table contains the `(10, 0)` and `(5, 2)` rows.
The Boolean candidates are spread across both rows, producing four combinations.
<xref:Xunit.CombinatorialTheoryDataBuilder.AddTestCase(System.ReadOnlySpan{System.Object})> appends the bespoke `(6, 2, false)` case without expanding it.

Call `AddRows` before the first `AddValues` call.
Additional `AddRows` calls append complete rows with the same width as the original base table.
Explicit test cases must match the final width after all generated columns are added.

## Constrain generated rows

<xref:Xunit.CombinatorialTheoryDataBuilder.Where(Xunit.CombinatorialTheoryDataPredicate)> accepts a predicate over the completed row.
Constraints are applied during generation, so pairwise generation can seek other rows that retain as much pair coverage as possible.
Explicit rows added with `AddTestCase` are not constrained.

[!code-csharp[](../../samples/CustomGeneration.cs#ConstrainedPairwiseData)]

`BuildCombinations` returns the exhaustive Cartesian product.
`BuildPairwiseCombinations` returns a smaller covering set.

## Reproduce or vary pairwise results

Pairwise generation is deterministic when its optional seed is omitted.
Pass a stable integer seed to reproduce another covering set:

```csharp
IReadOnlyCollection<ITheoryDataRow> rows = ConstrainedPairwiseData.CreateCases(seed: 42);
```

To vary coverage between runs, generate a seed and record it with the test output so a failure can be reproduced:

```csharp
int seed = Random.Shared.Next();
IReadOnlyCollection<ITheoryDataRow> rows = ConstrainedPairwiseData.CreateCases(seed);
```

Seeds apply only to pairwise generation.
Exhaustive generation always returns the complete set in stable order.

## Generate permutations

<xref:Xunit.CombinatorialTestCaseGenerator.GeneratePermutations``1(System.ReadOnlySpan{``0})> returns every positional permutation of its input:

[!code-csharp[](../../samples/CustomGeneration.cs#GeneratedPermutations)]

Input positions are distinct.
If the input contains equal values, equal output rows may therefore appear.

## Work directly with dimension indices

For complete control, use <xref:Xunit.CombinatorialTestCaseGenerator.GenerateCombinations(System.ReadOnlySpan{System.Int32},Xunit.CombinatorialIndexPredicate)> or <xref:Xunit.CombinatorialTestCaseGenerator.GeneratePairwiseCombinations(System.ReadOnlySpan{System.Int32},Xunit.CombinatorialIndexPredicate,System.Nullable{System.Int32})>.
Each result contains one selected zero-based candidate index per dimension.

[!code-csharp[](../../samples/CustomGeneration.cs#LowLevelGeneration)]

Both methods accept an optional <xref:Xunit.CombinatorialIndexPredicate> that can reject index selections.
Use the higher-level builder when you want constraints to inspect actual theory argument values instead.
