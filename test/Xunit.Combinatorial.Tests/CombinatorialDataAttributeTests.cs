// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

using System.Reflection;
using System.Runtime.CompilerServices;
using Validation;
using Xunit;
using Xunit.Sdk;

public class CombinatorialDataAttributeTests
{
    [Fact]
    public void GetData_NoArguments()
    {
        AssertData(new object[][]
        {
        });
    }

    [Fact]
    public void GetData_Bool()
    {
        AssertData(new object[][]
        {
                new object[] { true },
                new object[] { false },
        });
    }

    [Fact]
    public void GetData_BoolBool()
    {
        AssertData(new object[][]
        {
                new object[] { true, true },
                new object[] { true, false },
                new object[] { false, true },
                new object[] { false, false },
        });
    }

    [Fact]
    public void GetData_Int()
    {
        AssertData(new object[][]
        {
                new object[] { 0 },
                new object[] { 1 },
        });
    }

    [Fact]
    public void GetData_NullableInt()
    {
        AssertData(new object?[][]
        {
                new object?[] { null },
                new object?[] { 0 },
                new object?[] { 1 },
        });
    }

    [Fact]
    public void GetData_Int_35()
    {
        AssertData(new object[][]
        {
                new object[] { 3 },
                new object[] { 5 },
        });
    }

    [Fact]
    public void GetData_string_int_bool_Values()
    {
        AssertData(new object[][]
        {
                new object[] { "a", 2, true },
                new object[] { "a", 2, false },
                new object[] { "a", 4, true },
                new object[] { "a", 4, false },
                new object[] { "a", 6, true },
                new object[] { "a", 6, false },
                new object[] { "b", 2, true },
                new object[] { "b", 2, false },
                new object[] { "b", 4, true },
                new object[] { "b", 4, false },
                new object[] { "b", 6, true },
                new object[] { "b", 6, false },
        });
    }

    [Fact]
    public void GetData_DateTimeKind()
    {
        AssertData(new object[][]
        {
                new object[] { DateTimeKind.Unspecified },
                new object[] { DateTimeKind.Utc },
                new object[] { DateTimeKind.Local },
        });
    }

    [Fact]
    public void GetData_NullableDateTimeKind()
    {
        AssertData(new object?[][]
        {
                new object?[] { null },
                new object?[] { DateTimeKind.Unspecified },
                new object?[] { DateTimeKind.Utc },
                new object?[] { DateTimeKind.Local },
        });
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
    public void GetData_CustomDataFromDerivedAttriute()
    {
        var att = new CombinatorialDataAttribute();
        MethodInfo testhelperMethodInfo = this.GetType().GetMethod(nameof(this.SomeTestWithCustomValues), BindingFlags.Instance | BindingFlags.NonPublic)!;
        IEnumerable<object?[]> actual = att.GetData(testhelperMethodInfo);
        Assert.Equal(
            new[]
            {
                new object[] { 5 },
                new object[] { 10 },
                new object[] { 15 },
            },
            actual);
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

    private static void AssertData(IEnumerable<object?[]> expectedCombinatorial, [CallerMemberName] string? testMethodName = null)
    {
        IEnumerable<object[]> actualCombinatorial = GetData(new CombinatorialDataAttribute(), testMethodName).ToArray();
        IEnumerable<object[]> actualPairwise = GetData(new PairwiseDataAttribute(), testMethodName).ToArray();

        // Verify that the combinatorial result is as expected.
        Assert.Equal(expectedCombinatorial, actualCombinatorial);

        if (expectedCombinatorial.Any())
        {
            // Verify that the pairwise result covers every pair.
            HashSet<object?>[] possibleValues = ExtractPossibleValues(expectedCombinatorial);

            for (int i = 0; i < possibleValues.Length - 1; i++)
            {
                for (int j = i + 1; j < possibleValues.Length; j++)
                {
                    foreach (object? iValue in possibleValues[i])
                    {
                        foreach (object? jValue in possibleValues[j])
                        {
                            Assert.NotEmpty(actualPairwise.Where(
                                testCase =>
                                    EqualityComparer<object?>.Default.Equals(testCase[i], iValue) &&
                                    EqualityComparer<object?>.Default.Equals(testCase[j], jValue)));
                        }
                    }
                }
            }
        }
    }

    private static HashSet<object?>[] ExtractPossibleValues(IEnumerable<object?[]> combinatorialTestCases)
    {
        Requires.NotNull(combinatorialTestCases, nameof(combinatorialTestCases));

        HashSet<object?>[] possibleValues = new HashSet<object?>[combinatorialTestCases.First().Length];
        for (int i = 0; i < possibleValues.Length; i++)
        {
            possibleValues[i] = new HashSet<object?>();
        }

        foreach (object?[] combination in combinatorialTestCases)
        {
            for (int i = 0; i < combination.Length; i++)
            {
                possibleValues[i].Add(combination[i]);
            }
        }

        return possibleValues;
    }

    private static IEnumerable<object[]> GetData(DataAttribute dataAttribute, [CallerMemberName] string? testMethodName = null)
    {
        Requires.NotNull(dataAttribute, nameof(dataAttribute));
        Requires.NotNullOrEmpty(testMethodName!, nameof(testMethodName));

        string supposeMethodName = testMethodName.Replace("GetData_", "Suppose_");
        MethodInfo? methodInfo = typeof(CombinatorialDataAttributeTests).GetTypeInfo()
            .DeclaredMethods.First(m => m.Name == supposeMethodName);
        return dataAttribute.GetData(methodInfo);
    }

    private void SomeTestWithCustomValues([CustomValues] int a)
    {
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    private class CustomValuesAttribute : CombinatorialValuesAttribute
    {
        public CustomValuesAttribute()
            : base(new object[] { 5, 10, 15 })
        {
        }
    }
}
