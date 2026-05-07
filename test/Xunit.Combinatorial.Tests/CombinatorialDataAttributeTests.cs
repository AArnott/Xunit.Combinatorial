// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

using System.Reflection;
using System.Runtime.CompilerServices;
using Validation;
using Xunit;
using Xunit.Sdk;
using Xunit.v3;

public class CombinatorialDataAttributeTests
{
    [Fact]
    public async Task GetData_NoArguments()
    {
        await AssertData([]);
    }

    [Fact]
    public async Task GetData_Bool()
    {
        await AssertData(
         [
             [true],
            [false],
        ]);
    }

    [Fact]
    public async Task GetData_BoolBool()
    {
        await AssertData(
        [
            [true, true],
            [true, false],
            [false, true],
            [false, false],
        ]);
    }

    [Fact]
    public async Task GetData_BoolBool_ExcludeTrueFalse()
    {
        await AssertData(
        [
            [true, true],
            [false, true],
            [false, false],
        ]);
    }

    [Fact]
    public async Task GetData_Pairwise_ExcludeTrueFalse()
    {
        IReadOnlyCollection<ITheoryDataRow> actualPairwise = await CombinatorialDataAttributeTests.GetData(new PairwiseDataAttribute(), nameof(this.GetData_BoolBool_ExcludeTrueFalse));
        object?[] excludedTestCase = [true, false];

        Assert.DoesNotContain(actualPairwise, row => excludedTestCase.SequenceEqual(row.GetData()));
    }

    [Fact]
    public async Task GetData_BoolBool_ExcludeFirstParameterTrue()
    {
        await AssertData(
        [
            [false, true],
            [false, false],
        ]);
    }

    [Fact]
    public async Task GetData_BoolBool_ExcludeAnyFalse()
    {
        await AssertData(
        [
            [true, true],
            [false, true],
        ]);
    }

    [Fact]
    public async Task GetData_BoolBoolBool_ExcludeTrueTrueTrue()
    {
        await AssertData(
        [
            [true, true, false],
            [true, false, true],
            [true, false, false],
            [false, true, true],
            [false, true, false],
            [false, false, true],
            [false, false, false],
        ]);
    }

    [Fact]
    public async Task GetData_Int()
    {
        await AssertData(
        [
            [0],
            [1],
        ]);
    }

    [Fact]
    public async Task GetData_NullableInt()
    {
        await AssertData(
        [
            [null],
            [0],
            [1],
        ]);
    }

    [Fact]
    public async Task GetData_Int_35()
    {
        await AssertData(
        [
            [3],
            [5],
        ]);
    }

    [Fact]
    public async Task GetData_string_int_bool_Values()
    {
        await AssertData(
        [
            ["a", 2, true],
            ["a", 2, false],
            ["a", 4, true],
            ["a", 4, false],
            ["a", 6, true],
            ["a", 6, false],
            ["b", 2, true],
            ["b", 2, false],
            ["b", 4, true],
            ["b", 4, false],
            ["b", 6, true],
            ["b", 6, false],
        ]);
    }

    [Fact]
    public async Task GetData_DateTimeKind()
    {
        await AssertData(
        [
            [DateTimeKind.Unspecified],
            [DateTimeKind.Utc],
            [DateTimeKind.Local],
        ]);
    }

    [Fact]
    public async Task GetData_NullableDateTimeKind()
    {
        await AssertData(
        [
            [null],
            [DateTimeKind.Unspecified],
            [DateTimeKind.Utc],
            [DateTimeKind.Local],
        ]);
    }

    [Fact]
    public void GetData_UnsupportedType()
    {
        Assert.Throws<NotSupportedException>(() => GetData(new CombinatorialDataAttribute()));
        Assert.Throws<NotSupportedException>(() => GetData(new PairwiseDataAttribute()));
    }

    [Fact]
    public void GetData_UnsupportedNullableType()
    {
        Assert.Throws<NotSupportedException>(() => GetData(new CombinatorialDataAttribute()));
        Assert.Throws<NotSupportedException>(() => GetData(new PairwiseDataAttribute()));
    }

    [Fact]
    public async Task GetData_CustomDataFromDerivedAttriute()
    {
        var att = new CombinatorialDataAttribute();
        MethodInfo testhelperMethodInfo = this.GetType().GetMethod(nameof(this.SomeTestWithCustomValues), BindingFlags.Instance | BindingFlags.NonPublic)!;
        IReadOnlyCollection<ITheoryDataRow> actual = await att.GetData(testhelperMethodInfo, new DisposalTracker());
        Assert.Equal(
            [
                [5],
                [10],
                [15],
            ],
            actual.Select(r => r.GetData()));
    }

    private static void Suppose_NoArguments()
    {
    }

    private static void Suppose_Bool(bool p1)
    {
    }

    private static void Suppose_BoolBool(bool p1, bool p2)
    {
    }

    [ExcludeTestCase(true, false)]
    private static void Suppose_BoolBool_ExcludeTrueFalse(bool p1, bool p2)
    {
    }

    [ExcludeFirstParameterTrue]
    private static void Suppose_BoolBool_ExcludeFirstParameterTrue(bool p1, bool p2)
    {
    }

    [ExcludeTestCase(typeof(AnyDataValue), false)]
    private static void Suppose_BoolBool_ExcludeAnyFalse(bool p1, bool p2)
    {
    }

    [ExcludeTestCase(true, true, true)]
    private static void Suppose_BoolBoolBool_ExcludeTrueTrueTrue(bool p1, bool p2, bool p3)
    {
    }

    private static void Suppose_Int(int p1)
    {
    }

    private static void Suppose_NullableInt(int? p1)
    {
    }

    private static void Suppose_Int_35([CombinatorialValues(3, 5)] int p1)
    {
    }

    private static void Suppose_string_int_bool_Values([CombinatorialValues("a", "b")] string p1, [CombinatorialValues(2, 4, 6)] int p2, bool p3)
    {
    }

    private static void Suppose_type([CombinatorialValues(typeof(CombinatorialDataAttributeTests))] Type p1, [CombinatorialValues(2, 4, 6)] int p2, bool p3)
    {
    }

    private static void Suppose_DateTimeKind(DateTimeKind p1)
    {
    }

    private static void Suppose_NullableDateTimeKind(DateTimeKind? p1)
    {
    }

    private static void Suppose_UnsupportedType(System.AggregateException p1)
    {
    }

    private static void Suppose_UnsupportedNullableType(Guid? p1)
    {
    }

    private static async Task AssertData(IReadOnlyCollection<object?[]> expectedCombinatorial, [CallerMemberName] string? testMethodName = null)
    {
        IReadOnlyCollection<ITheoryDataRow> actualCombinatorial = await GetData(new CombinatorialDataAttribute(), testMethodName);
        IReadOnlyCollection<ITheoryDataRow> actualPairwise = await GetData(new PairwiseDataAttribute(), testMethodName);

        // Verify that the combinatorial result is as expected.
        Assert.Equal(expectedCombinatorial, actualCombinatorial.Select(row => row.GetData()).ToArray());
        Assert.All(
            actualPairwise,
            row => Assert.Contains(
                expectedCombinatorial,
                expected => expected.SequenceEqual(row.GetData())));

        if (expectedCombinatorial.Any())
        {
            // Verify that the pairwise result covers every pair.
            int parameterCount = expectedCombinatorial.First().Length;
            for (int i = 0; i < parameterCount - 1; i++)
            {
                for (int j = i + 1; j < parameterCount; j++)
                {
                    foreach ((object? first, object? second) in ExtractPossiblePairs(expectedCombinatorial, i, j))
                    {
                        Assert.Contains(
                            actualPairwise,
                            testCase =>
                                EqualityComparer<object?>.Default.Equals(testCase.GetData()[i], first) &&
                                EqualityComparer<object?>.Default.Equals(testCase.GetData()[j], second));
                    }
                }
            }
        }
    }

    private static HashSet<(object? First, object? Second)> ExtractPossiblePairs(IEnumerable<object?[]> combinatorialTestCases, int firstParameterIndex, int secondParameterIndex)
    {
        Requires.NotNull(combinatorialTestCases, nameof(combinatorialTestCases));

        HashSet<(object? First, object? Second)> possiblePairs = [];

        foreach (object?[] combination in combinatorialTestCases)
        {
            possiblePairs.Add((combination[firstParameterIndex], combination[secondParameterIndex]));
        }

        return possiblePairs;
    }

    private static ValueTask<IReadOnlyCollection<ITheoryDataRow>> GetData(DataAttribute dataAttribute, [CallerMemberName] string? testMethodName = null)
    {
        Requires.NotNull(dataAttribute, nameof(dataAttribute));
        Requires.NotNullOrEmpty(testMethodName!, nameof(testMethodName));

        string supposeMethodName = testMethodName.Replace("GetData_", "Suppose_");
        MethodInfo? methodInfo = typeof(CombinatorialDataAttributeTests).GetTypeInfo()
            .DeclaredMethods.First(m => m.Name == supposeMethodName);
        return dataAttribute.GetData(methodInfo, new DisposalTracker());
    }

    private void SomeTestWithCustomValues([CustomValues] int a)
    {
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    private class ExcludeFirstParameterTrueAttribute : ExcludeTestCaseAttribute
    {
        public ExcludeFirstParameterTrueAttribute()
            : base(true, typeof(AnyDataValue))
        {
        }
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    private class CustomValuesAttribute : CombinatorialValuesAttribute
    {
        public CustomValuesAttribute()
            : base([5, 10, 15])
        {
        }
    }
}
