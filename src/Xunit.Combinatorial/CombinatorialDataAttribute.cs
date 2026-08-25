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
        CombinatorialIndexPredicate? isTestCaseAllowed = ExcludeTestCaseAttribute.CreateIndexMatcher(values, exclusions);
        int[][] testCases = CombinatorialTestCaseGenerator.GenerateCombinations([.. values.Select(v => v.Length)], isTestCaseAllowed);
        return new(
            [..
                testCases
                    .Select(indices => new TheoryDataRow(indices.Select((valueIndex, parameterIndex) =>
                        ValuesUtilities.GetValueForTestCase(parameters[parameterIndex], values[parameterIndex], valueIndex)).ToArray()))
            ]);
    }
}
