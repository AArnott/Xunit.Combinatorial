// Copyright (c) parts Andrew Arnott, parts Matthew Wilkinson. All rights reserved. Licensed under the Ms-PL.

namespace Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Xunit.Sdk;

    /// <summary>
    /// Provides a test method decorated with a <see cref="TheoryAttribute"/>
    /// with arguments to run every possible combination of values for the
    /// parameters taken by the test method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CombinatorialDataAttribute : DataAttribute
    {
        /// <inheritdoc />
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            Requires.NotNull(testMethod, nameof(testMethod));

            var parameters = testMethod.GetParameters();
            switch (parameters.Length)
            {
                case 0: return Enumerable.Empty<object[]>();
                case 1: return CombinationGenerator.Generate(Values(parameters[0]));
                case 2: return CombinationGenerator.Generate(Values(parameters[0]), Values(parameters[1]));
                case 3: return CombinationGenerator.Generate(Values(parameters[0]), Values(parameters[1]), Values(parameters[2]));
                case 4: return CombinationGenerator.Generate(Values(parameters[0]), Values(parameters[1]), Values(parameters[2]), Values(parameters[3]));
                case 5: return CombinationGenerator.Generate(Values(parameters[0]), Values(parameters[1]), Values(parameters[2]), Values(parameters[3]), Values(parameters[4]));
                case 6: return CombinationGenerator.Generate(Values(parameters[0]), Values(parameters[1]), Values(parameters[2]), Values(parameters[3]), Values(parameters[4]), Values(parameters[5]));
                case 7: return CombinationGenerator.Generate(Values(parameters[0]), Values(parameters[1]), Values(parameters[2]), Values(parameters[3]), Values(parameters[4]), Values(parameters[5]), Values(parameters[6]));
                case 8: return CombinationGenerator.Generate(Values(parameters[0]), Values(parameters[1]), Values(parameters[2]), Values(parameters[3]), Values(parameters[4]), Values(parameters[5]), Values(parameters[6]), Values(parameters[7]));
                case 9: return CombinationGenerator.Generate(Values(parameters[0]), Values(parameters[1]), Values(parameters[2]), Values(parameters[3]), Values(parameters[4]), Values(parameters[5]), Values(parameters[6]), Values(parameters[7]), Values(parameters[8]));
                case 10: return CombinationGenerator.Generate(Values(parameters[0]), Values(parameters[1]), Values(parameters[2]), Values(parameters[3]), Values(parameters[4]), Values(parameters[5]), Values(parameters[6]), Values(parameters[7]), Values(parameters[8]), Values(parameters[9]));
                default: return GenerateCombinations(parameters);
            }
        }

        private static object[] Values(ParameterInfo param) => ValuesUtilities.GetValuesFor(param);

        private static IEnumerable<object[]> GenerateCombinations(ParameterInfo[] parameters)
        {
            var values = new object[parameters.Length][];
            for (int i = 0; i < parameters.Length; i++)
            {
                values[i] = Values(parameters[i]);
            }

            return CombinationGenerator.Generate(values);
        }
    }
}
