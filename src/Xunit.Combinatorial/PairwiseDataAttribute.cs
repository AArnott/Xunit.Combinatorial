// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

using System.Reflection;
using Xunit.Sdk;
using Xunit.v3;

namespace Xunit;

/// <summary>
/// Provides a test method decorated with a <see cref="TheoryAttribute"/>
/// with arguments to run various combination of values for the
/// parameters taken by the test method using a pairwise strategy.
/// </summary>
public class PairwiseDataAttribute : DataAttribute
{
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

        object?[][] values = new object?[parameters.Length][];
        for (int i = 0; i < parameters.Length; i++)
        {
            values[i] = ValuesUtilities.GetValuesFor(parameters[i]).ToArray();
        }

        int[][] testCaseInfo = PairwiseStrategy.GetTestCases([.. values.Select(v => v.Length)]);
        IEnumerable<TheoryDataRow> intermediate =
            from testCase in testCaseInfo
            select new TheoryDataRow(testCase.Select((j, i) => values[i][j]).ToArray());
        return new ValueTask<IReadOnlyCollection<ITheoryDataRow>>([.. intermediate]);
    }
}
