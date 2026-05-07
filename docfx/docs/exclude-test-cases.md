# Excluding generated test cases

Use @Xunit.ExcludeTestCaseAttribute on a combinatorial or pairwise theory to suppress generated cases you do not want to run.
The attribute arguments must line up with the theory parameter order.

## Exclude one exact case

You can exclude a single exact combination by specifying the full argument list:

[!code-csharp[](../../samples/ExcludeTestCases.cs#ExcludeSpecificCase)]

In this example, the generated case where `isAdmin` is `true` and `isActive` is `false` is omitted, while the other combinations are still produced.

## Exclude multiple cases with `AnyDataValue`

Use `typeof(AnyDataValue)` in a parameter position when any generated value in that position should match:

[!code-csharp[](../../samples/ExcludeTestCases.cs#ExcludeWildcardCase)]

In this example, every generated case with `isEnabled` equal to `false` is excluded, regardless of the `isAdmin` value.

This is especially useful when one parameter represents a broad precondition and you want to veto an entire slice of the generated matrix without listing each case individually.
