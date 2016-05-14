// Copyright (c) Andrew Arnott. All rights reserved. Licensed under the Ms-PL.

namespace Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;
    using Xunit.Sdk;

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

            var currentValues = new object[parameters.Length];
            return this.FillCombinations(parameters, values, currentValues, 0);
        }

        /// <summary>
        /// Produces a sequence of argument arrays that capture every possible
        /// combination of values.
        /// </summary>
        /// <param name="parameters">The parameters taken by the test method.</param>
        /// <param name="candidateValues">An array of each argument's list of possible values.</param>
        /// <param name="currentValues">An array that is being recursively initialized with a set of arguments to pass to the test method.</param>
        /// <param name="index">The index into <paramref name="currentValues"/> that this particular invocation should rotate through <paramref name="candidateValues"/> for.</param>
        /// <returns>A sequence of all combinations of arguments from <paramref name="candidateValues"/>, starting at <paramref name="index"/>.</returns>
        private IEnumerable<object[]> FillCombinations(ParameterInfo[] parameters, List<object>[] candidateValues, object[] currentValues, int index)
        {
            Requires.NotNull(parameters, nameof(parameters));
            Requires.NotNull(candidateValues, nameof(candidateValues));
            Requires.NotNull(currentValues, nameof(currentValues));
            Requires.Argument(parameters.Length == candidateValues.Length, nameof(candidateValues), $"Expected to have same array length as {nameof(parameters)}");
            Requires.Argument(parameters.Length == currentValues.Length, nameof(currentValues), $"Expected to have same array length as {nameof(parameters)}");
            Requires.Range(index >= 0 && index < parameters.Length, nameof(index));

            foreach (object value in candidateValues[index])
            {
                currentValues[index] = value;

                if (index + 1 < parameters.Length)
                {
                    foreach (object[] result in this.FillCombinations(parameters, candidateValues, currentValues, index + 1))
                    {
                        yield return result;
                    }
                }
                else
                {
                    // We're the tail end, so just produce the value.
                    // Copy the array before returning since we're about to mutate currentValues
                    object[] finalSet = new object[currentValues.Length];
                    Array.Copy(currentValues, finalSet, currentValues.Length);
                    yield return finalSet;
                }
            }
        }
    }
}
