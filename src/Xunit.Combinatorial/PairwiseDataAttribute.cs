// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

using System.Reflection;
using Xunit.Sdk;

namespace Xunit
{
    /// <summary>
    /// Provides a test method decorated with a <see cref="TheoryAttribute"/>
    /// with arguments to run various combination of values for the
    /// parameters taken by the test method using a pairwise strategy.
    /// </summary>
    public class PairwiseDataAttribute : DataAttribute
    {
        /// <inheritdoc />
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            Requires.NotNull(testMethod, nameof(testMethod));

            ParameterInfo[]? parameters = testMethod.GetParameters();
            if (parameters.Length == 0)
            {
                return Enumerable.Empty<object[]>();
            }

            var values = new List<object?>[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                values[i] = ValuesUtilities.GetValuesFor(parameters[i]).ToList();
            }

            List<int[]>? testCaseInfo = PairwiseStrategy.GetTestCases(values.Select(v => v.Count).ToArray());
            return from testCase in testCaseInfo
                   select testCase.Select((j, i) => values[i][j]).ToArray();
        }
    }
}
