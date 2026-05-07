// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

using System.Reflection;

namespace Xunit;

/// <summary>
/// Suppresses generation of a specific test case from a combinatorial or pairwise theory.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class ExcludeTestCaseAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExcludeTestCaseAttribute"/> class.
    /// </summary>
    /// <param name="arguments">The values that match the test case to exclude.</param>
    public ExcludeTestCaseAttribute(params object?[] arguments)
    {
        this.Arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));
    }

    /// <summary>
    /// Gets the values that match the test case to exclude.
    /// </summary>
    public object?[] Arguments { get; }

    /// <summary>
    /// Gets and validates the test case exclusions on a test method.
    /// </summary>
    /// <param name="testMethod">The test method to inspect for exclusions.</param>
    /// <returns>The exclusions applied to <paramref name="testMethod"/>.</returns>
    internal static ExcludeTestCaseAttribute[] GetExclusions(MethodInfo testMethod)
    {
        Requires.NotNull(testMethod, nameof(testMethod));

        int parameterCount = testMethod.GetParameters().Length;
        ExcludeTestCaseAttribute[] exclusions = testMethod.GetCustomAttributes<ExcludeTestCaseAttribute>(true).ToArray();
        foreach (ExcludeTestCaseAttribute exclusion in exclusions)
        {
            if (exclusion.Arguments.Length != parameterCount)
            {
                throw new ArgumentException($"The number of arguments in {nameof(ExcludeTestCaseAttribute)} must match the number of test method parameters.", nameof(testMethod));
            }
        }

        return exclusions;
    }

    /// <summary>
    /// Creates a predicate that checks whether an indexed test case is allowed.
    /// </summary>
    /// <param name="candidateValues">The possible values for each parameter position.</param>
    /// <param name="exclusions">The exclusions to evaluate against indexed test cases.</param>
    /// <returns>A predicate that returns <see langword="true"/> when a test case is allowed.</returns>
    internal static Predicate<int[]>? CreateIndexMatcher(List<object?>[] candidateValues, ExcludeTestCaseAttribute[] exclusions)
    {
        Requires.NotNull(candidateValues, nameof(candidateValues));
        Requires.NotNull(exclusions, nameof(exclusions));

        if (exclusions.Length == 0)
        {
            return null;
        }

        List<IndexedExclusion> indexedExclusions = [];
        foreach (ExcludeTestCaseAttribute exclusion in exclusions)
        {
            IndexedExclusion? indexedExclusion = IndexedExclusion.Create(candidateValues, exclusion);
            if (indexedExclusion is not null)
            {
                indexedExclusions.Add(indexedExclusion);
            }
        }

        if (indexedExclusions.Count == 0)
        {
            return null;
        }

        return testCase =>
        {
            Requires.NotNull(testCase, nameof(testCase));

            foreach (IndexedExclusion exclusion in indexedExclusions)
            {
                if (exclusion.Matches(testCase))
                {
                    return false;
                }
            }

            return true;
        };
    }

    /// <summary>
    /// Gets a value indicating whether this attribute matches a test case.
    /// </summary>
    /// <param name="testCase">The test case to compare against this exclusion.</param>
    /// <returns>A value indicating whether this attribute matches <paramref name="testCase"/>.</returns>
    internal bool Matches(object?[] testCase)
    {
        Requires.NotNull(testCase, nameof(testCase));
        Requires.Argument(this.Arguments.Length == testCase.Length, nameof(testCase), $"Expected to have same array length as {nameof(this.Arguments)}");

        for (int i = 0; i < this.Arguments.Length; i++)
        {
            if (!IsAny(this.Arguments[i]) && !EqualityComparer<object?>.Default.Equals(this.Arguments[i], testCase[i]))
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsAny(object? argument) => argument is Type type && type == typeof(AnyDataValue);

    private sealed class IndexedExclusion
    {
        private readonly bool[]?[] matchingValueIndices;

        private IndexedExclusion(bool[]?[] matchingValueIndices)
        {
            this.matchingValueIndices = matchingValueIndices;
        }

        internal static IndexedExclusion? Create(List<object?>[] candidateValues, ExcludeTestCaseAttribute exclusion)
        {
            Requires.NotNull(candidateValues, nameof(candidateValues));
            Requires.NotNull(exclusion, nameof(exclusion));
            Requires.Argument(candidateValues.Length == exclusion.Arguments.Length, nameof(candidateValues), $"Expected to have same array length as {nameof(exclusion.Arguments)}");

            bool[]?[] matchingValueIndices = new bool[]?[candidateValues.Length];
            for (int parameterIndex = 0; parameterIndex < candidateValues.Length; parameterIndex++)
            {
                object? excludedValue = exclusion.Arguments[parameterIndex];
                if (IsAny(excludedValue))
                {
                    continue;
                }

                bool[] matches = new bool[candidateValues[parameterIndex].Count];
                bool anyMatches = false;
                for (int valueIndex = 0; valueIndex < candidateValues[parameterIndex].Count; valueIndex++)
                {
                    if (EqualityComparer<object?>.Default.Equals(candidateValues[parameterIndex][valueIndex], excludedValue))
                    {
                        matches[valueIndex] = true;
                        anyMatches = true;
                    }
                }

                if (!anyMatches)
                {
                    return null;
                }

                matchingValueIndices[parameterIndex] = matches;
            }

            return new IndexedExclusion(matchingValueIndices);
        }

        internal bool Matches(int[] testCase)
        {
            Requires.NotNull(testCase, nameof(testCase));
            Requires.Argument(this.matchingValueIndices.Length == testCase.Length, nameof(testCase), $"Expected to have same array length as {nameof(this.matchingValueIndices)}");

            for (int parameterIndex = 0; parameterIndex < testCase.Length; parameterIndex++)
            {
                bool[]? matches = this.matchingValueIndices[parameterIndex];
                if (matches is not null && !matches[testCase[parameterIndex]])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
