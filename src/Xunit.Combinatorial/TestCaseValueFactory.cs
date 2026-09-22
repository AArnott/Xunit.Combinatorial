// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

using System.Reflection;

namespace Xunit;

/// <summary>
/// Creates the arguments for each generated test case, recreating member data as necessary
/// so that mutable values are never shared between test cases.
/// </summary>
/// <remarks>
/// Each evaluation of a member data source produces one fresh value for <em>every</em> candidate index,
/// so the fresh values are pooled and handed out as test cases require them.
/// This keeps the number of evaluations of the data source proportional to the number of test cases
/// divided by the number of values the source produces, instead of one evaluation per test case.
/// </remarks>
internal class TestCaseValueFactory
{
    private readonly ParameterInfo[] parameters;
    private readonly object?[][] candidateValues;

    /// <summary>
    /// Pools of fresh, unused values for each parameter that gets its values from member data, indexed by parameter
    /// and then by candidate index. A <see langword="null"/> entry indicates the parameter's values may be shared between test cases.
    /// </summary>
    private readonly Queue<object?>[]?[] freshValuePools;

    private readonly CombinatorialMemberDataAttribute?[] memberDataAttributes;

    /// <summary>
    /// Initializes a new instance of the <see cref="TestCaseValueFactory"/> class.
    /// </summary>
    /// <param name="parameters">The parameters taken by the test method.</param>
    /// <param name="candidateValues">An array of each parameter's list of candidate values, as used to determine the test case combinations.</param>
    internal TestCaseValueFactory(ParameterInfo[] parameters, object?[][] candidateValues)
    {
        Requires.NotNull(parameters, nameof(parameters));
        Requires.NotNull(candidateValues, nameof(candidateValues));
        Requires.Argument(parameters.Length == candidateValues.Length, nameof(candidateValues), $"Expected to have same array length as {nameof(parameters)}");

        this.parameters = parameters;
        this.candidateValues = candidateValues;
        this.memberDataAttributes = new CombinatorialMemberDataAttribute?[parameters.Length];
        this.freshValuePools = new Queue<object?>[]?[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            CombinatorialMemberDataAttribute? memberData = parameters[i].GetCustomAttributes().OfType<CombinatorialMemberDataAttribute>().SingleOrDefault();
            if (memberData is null)
            {
                continue;
            }

            this.memberDataAttributes[i] = memberData;

            // The candidate values themselves were freshly created, so they seed the pools.
            Queue<object?>[] pools = new Queue<object?>[candidateValues[i].Length];
            for (int j = 0; j < pools.Length; j++)
            {
                pools[j] = new Queue<object?>();
                pools[j].Enqueue(candidateValues[i][j]);
            }

            this.freshValuePools[i] = pools;
        }
    }

    /// <summary>
    /// Creates the arguments for one test case.
    /// </summary>
    /// <param name="candidateIndexes">The index of the candidate value selected for each parameter.</param>
    /// <returns>The arguments for the test case.</returns>
    internal object?[] CreateTestCaseArguments(int[] candidateIndexes)
    {
        Requires.NotNull(candidateIndexes, nameof(candidateIndexes));
        Requires.Argument(candidateIndexes.Length == this.parameters.Length, nameof(candidateIndexes), $"Expected to have same array length as the test method's parameters.");

        object?[] arguments = new object?[candidateIndexes.Length];
        for (int i = 0; i < candidateIndexes.Length; i++)
        {
            arguments[i] = this.GetValueForTestCase(i, candidateIndexes[i]);
        }

        return arguments;
    }

    private object? GetValueForTestCase(int parameterIndex, int candidateIndex)
    {
        Queue<object?>[]? pools = this.freshValuePools[parameterIndex];
        if (pools is null)
        {
            return this.candidateValues[parameterIndex][candidateIndex];
        }

        if (pools[candidateIndex].Count == 0)
        {
            this.RefillPools(parameterIndex, pools);
        }

        return pools[candidateIndex].Dequeue();
    }

    private void RefillPools(int parameterIndex, Queue<object?>[] pools)
    {
        ParameterInfo parameter = this.parameters[parameterIndex];
        CombinatorialMemberDataAttribute memberData = this.memberDataAttributes[parameterIndex]!;
        object?[] freshValues = memberData.GetValues(parameter);
        if (freshValues.Length != pools.Length)
        {
            throw new InvalidOperationException(
                $"Member data for parameter '{parameter.Name}' returned {pools.Length} values when determining combinations, but {freshValues.Length} values when creating a test case.");
        }

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i].Enqueue(freshValues[i]);
        }
    }
}
