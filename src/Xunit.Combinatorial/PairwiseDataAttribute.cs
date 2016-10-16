// Copyright (c) Andrew Arnott. All rights reserved. Licensed under the Ms-PL.

namespace Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Sdk;
    using Combinatorial.Internal;

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

            var parameters = testMethod.GetParameters();
            if (parameters.Length == 0)
            {
                return Enumerable.Empty<object[]>();
            }

            var values = new List<object>[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                values[i] = ValuesUtilities.GetValuesFor(parameters[i]).ToList();
            }

            var testCaseInfo = PairwiseStrategy.GetTestCases(values.Select(v => v.Count).ToArray());
            return from testCase in testCaseInfo
                   select testCase.Select((j, i) => values[i][j]).ToArray();
        }
    }
}
