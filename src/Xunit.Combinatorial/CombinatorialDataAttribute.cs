// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

using System.Reflection;
using Xunit.Sdk;
using Xunit.v3;

namespace Xunit;

/// <summary>
/// Provides a test method decorated with a <see cref="TheoryAttribute"/>
/// with arguments to run every possible combination of values for the
/// parameters taken by the test method.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class CombinatorialDataAttribute : DataAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CombinatorialDataAttribute"/> class.
    /// </summary>
    public CombinatorialDataAttribute()
    {
    }

    /// <inheritdoc />
    public override bool SupportsDiscoveryEnumeration() => true;

    /// <inheritdoc />
    public override ValueTask<IReadOnlyCollection<ITheoryDataRow>> GetData(MethodInfo testMethod, DisposalTracker disposalTracker)
    {
        Requires.NotNull(testMethod, nameof(testMethod));

        ParameterInfo[]? parameters = testMethod.GetParameters();
        if (parameters.Length == 0)
        {
            return new([]);
        }

        var values = new object?[parameters.Length][];
        for (int i = 0; i < parameters.Length; i++)
        {
            values[i] = ValuesUtilities.GetValuesFor(parameters[i]).ToArray();
        }

        ExcludeTestCaseAttribute[] exclusions = ExcludeTestCaseAttribute.GetExclusions(testMethod);
        int[] currentValueIndices = new int[parameters.Length];
        return new(
            [..
                this.FillCombinations(parameters, values, currentValueIndices, exclusions, 0)
                    .Select(indices => new TheoryDataRow(indices.Select((valueIndex, parameterIndex) =>
                        ValuesUtilities.GetValueForTestCase(parameters[parameterIndex], values[parameterIndex], valueIndex)).ToArray()))
            ]);
    }

    /// <summary>
    /// Produces a sequence of argument arrays that capture every possible
    /// combination of values.
    /// </summary>
    /// <param name="parameters">The parameters taken by the test method.</param>
    /// <param name="candidateValues">An array of each argument's list of possible values.</param>
    /// <param name="currentValueIndices">An array that is being recursively initialized with the candidate value index for each argument.</param>
    /// <param name="exclusions">Test cases that should not be generated.</param>
    /// <param name="index">The index into <paramref name="currentValueIndices"/> that this particular invocation should rotate through <paramref name="candidateValues"/> for.</param>
    /// <returns>A sequence of all combinations of candidate value indices, starting at <paramref name="index"/>.</returns>
    private IEnumerable<int[]> FillCombinations(ParameterInfo[] parameters, object?[][] candidateValues, int[] currentValueIndices, ExcludeTestCaseAttribute[] exclusions, int index)
    {
        Requires.NotNull(parameters, nameof(parameters));
        Requires.NotNull(candidateValues, nameof(candidateValues));
        Requires.NotNull(currentValueIndices, nameof(currentValueIndices));
        Requires.NotNull(exclusions, nameof(exclusions));
        Requires.Argument(parameters.Length == candidateValues.Length, nameof(candidateValues), $"Expected to have same array length as {nameof(parameters)}");
        Requires.Argument(parameters.Length == currentValueIndices.Length, nameof(currentValueIndices), $"Expected to have same array length as {nameof(parameters)}");
        Requires.Range(index >= 0 && index < parameters.Length, nameof(index));

        for (int valueIndex = 0; valueIndex < candidateValues[index].Length; valueIndex++)
        {
            currentValueIndices[index] = valueIndex;

            if (index + 1 < parameters.Length)
            {
                foreach (int[] result in this.FillCombinations(parameters, candidateValues, currentValueIndices, exclusions, index + 1))
                {
                    yield return result;
                }
            }
            else
            {
                object?[] finalSet = currentValueIndices.Select((candidateIndex, parameterIndex) => candidateValues[parameterIndex][candidateIndex]).ToArray();
                if (!exclusions.Any(exclusion => exclusion.Matches(finalSet)))
                {
                    yield return [.. currentValueIndices];
                }
            }
        }
    }
}
