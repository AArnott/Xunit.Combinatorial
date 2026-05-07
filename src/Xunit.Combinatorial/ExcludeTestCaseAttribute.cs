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
    /// A sentinel type that matches any value in the same test method parameter position.
    /// </summary>
    /// <remarks>
    /// Use this with <see langword="typeof"/> when specifying exclusions, for example:
    /// <c>[ExcludeTestCase(typeof(ExcludeTestCaseAttribute.Any), false)]</c>.
    /// </remarks>
    public static class Any
    {
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

    private static bool IsAny(object? argument) => argument is Type type && type == typeof(Any);
}
