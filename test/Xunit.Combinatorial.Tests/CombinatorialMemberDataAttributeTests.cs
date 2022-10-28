// Copyright (c) Andrew Arnott. All rights reserved. Licensed under the Ms-PL.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

public class CombinatorialMemberDataAttributeTests
{
    private static readonly MethodInfo StubIntMethodInfo = typeof(CombinatorialMemberDataAttributeTests).GetMethod(nameof(StubIntMethod), BindingFlags.Instance | BindingFlags.NonPublic)!;
    private static readonly MethodInfo StubGuidMethodInfo = typeof(CombinatorialMemberDataAttributeTests).GetMethod(nameof(StubGuidMethod), BindingFlags.Instance | BindingFlags.NonPublic)!;
    private static readonly MethodInfo StubStringMethodInfo = typeof(CombinatorialMemberDataAttributeTests).GetMethod(nameof(StubStringMethod), BindingFlags.Instance | BindingFlags.NonPublic)!;

    [Fact]
    public void EnumerableOfIntReturnsValues()
    {
        var attribute = new CombinatorialMemberDataAttribute(nameof(GetValuesAsEnumerableOfInt));
        ParameterInfo parameter = StubIntMethodInfo.GetParameters()[0];
        object?[]? values = attribute.GetValues(parameter);
        Assert.Equal(new object[] { 1, 2, 3, 4 }, values);
    }

    [Fact]
    public void ConcreteClassImplementingEnumerableOfIntReturnsValues()
    {
        var attribute = new CombinatorialMemberDataAttribute(nameof(GetValuesAsConcreteClassImplementingEnumerableOfInt));
        ParameterInfo parameter = StubIntMethodInfo.GetParameters()[0];
        object?[]? values = attribute.GetValues(parameter);
        Assert.Equal(new object[] { 1, 2, 3, 4 }, values);
    }

    [Fact]
    public void ConcreteNonGenericClassImplementingEnumerableOfIntReturnsValues()
    {
        var attribute = new CombinatorialMemberDataAttribute(nameof(GetValuesAsConcreteNonGenericClassImplementingEnumerableOfInt));
        ParameterInfo parameter = StubIntMethodInfo.GetParameters()[0];
        object?[]? values = attribute.GetValues(parameter);
        Assert.Equal(new object[] { 1, 2, 3, 4 }, values);
    }

    [Fact]
    public void EnumerableOfArrayThrows()
    {
        var attribute = new CombinatorialMemberDataAttribute(nameof(GetValuesAsEnumerableOfIntArray));
        ParameterInfo parameter = StubIntMethodInfo.GetParameters()[0];

        ArgumentException? exception = Assert.Throws<ArgumentException>(() => attribute.GetValues(parameter));
        Assert.Equal($"Member {nameof(GetValuesAsEnumerableOfIntArray)} on {nameof(CombinatorialMemberDataAttributeTests)} returned IEnumerable<Int32[]>, which is not supported.", exception.Message);
    }

    [Fact]
    public void EnumerableOfEnumerableThrows()
    {
        var attribute = new CombinatorialMemberDataAttribute(nameof(GetValuesAsEnumerableOfIntEnumerable));
        ParameterInfo parameter = StubIntMethodInfo.GetParameters()[0];

        ArgumentException? exception = Assert.Throws<ArgumentException>(() => attribute.GetValues(parameter));
        Assert.Equal($"Member {nameof(GetValuesAsEnumerableOfIntEnumerable)} on {nameof(CombinatorialMemberDataAttributeTests)} returned IEnumerable<IEnumerable<Int32>>, which is not supported.", exception.Message);
    }

    [Fact]
    public void NonGenericEnumerableThrows()
    {
        var attribute = new CombinatorialMemberDataAttribute(nameof(GetValuesAsTypeThatImplementsNonGenericIEnumerable));
        MethodInfo testMethod = this.GetType().GetMethod(nameof(this.StubIntMethod), BindingFlags.Instance | BindingFlags.NonPublic)!;
        ParameterInfo parameter = testMethod.GetParameters()[0];

        ArgumentException? exception = Assert.Throws<ArgumentException>(() => attribute.GetValues(parameter));
        Assert.Equal($"Member {nameof(GetValuesAsTypeThatImplementsNonGenericIEnumerable)} on {typeof(CombinatorialMemberDataAttributeTests)} must return a type that implements IEnumerable<T>.", exception.Message);
    }

    [Fact]
    public void EnumerableOfGuidReturnsValue()
    {
        var attribute = new CombinatorialMemberDataAttribute(nameof(GetValuesAsEnumerableOfGuid));
        ParameterInfo parameter = StubGuidMethodInfo.GetParameters()[0];
        object?[]? values = attribute.GetValues(parameter);

        Assert.Contains(values, obj => obj is Guid guid && guid != Guid.Empty);
    }

    [Fact]
    public void EnumerableOfStringReturnsValue()
    {
        var attribute = new CombinatorialMemberDataAttribute(nameof(GetValuesAsEnumerableOfString));
        ParameterInfo parameter = StubStringMethodInfo.GetParameters()[0];
        object?[]? values = attribute.GetValues(parameter);

        Assert.Contains(values, obj => obj is string str && !string.IsNullOrEmpty(str));
    }

    [Fact]
    public void IncompatibleMemberDataTypeThrows()
    {
        var attribute = new CombinatorialMemberDataAttribute(nameof(GetValuesAsEnumerableOfGuid));
        ParameterInfo parameter = StubIntMethodInfo.GetParameters()[0];

        ArgumentException? exception = Assert.Throws<ArgumentException>(() => attribute.GetValues(parameter));
        Assert.Equal("Parameter type System.Int32 is not compatible with returned member type System.Guid.", exception.Message);
    }

    private static IEnumerable<int> GetValuesAsEnumerableOfInt()
    {
        yield return 1;
        yield return 2;
        yield return 3;
        yield return 4;
    }

    private static ListOfInt<string> GetValuesAsConcreteClassImplementingEnumerableOfInt() => new ListOfInt<string> { 1, 2, 3, 4 };

    private static ListOfInt GetValuesAsConcreteNonGenericClassImplementingEnumerableOfInt() => new ListOfInt { 1, 2, 3, 4 };

    private static ImplementsOnlyNonGenericIEnumerable GetValuesAsTypeThatImplementsNonGenericIEnumerable() => new();

    private static IEnumerable<int[]> GetValuesAsEnumerableOfIntArray()
    {
        yield return new[] { 1 };
        yield return new[] { 2 };
        yield return new[] { 3 };
        yield return new[] { 4 };
        yield return new[] { 5 };
    }

    private static IEnumerable<IEnumerable<int>> GetValuesAsEnumerableOfIntEnumerable()
    {
        yield return new[] { 1 };
        yield return new[] { 2 };
        yield return new[] { 3 };
        yield return new[] { 4 };
        yield return new[] { 5 };
    }

    private static IEnumerable<Guid> GetValuesAsEnumerableOfGuid()
    {
        yield return Guid.NewGuid();
        yield return Guid.NewGuid();
        yield return Guid.NewGuid();
        yield return Guid.NewGuid();
        yield return Guid.NewGuid();
    }

    private static IEnumerable<string> GetValuesAsEnumerableOfString()
    {
        yield return Guid.NewGuid().ToString();
        yield return Guid.NewGuid().ToString();
        yield return Guid.NewGuid().ToString();
        yield return Guid.NewGuid().ToString();
        yield return Guid.NewGuid().ToString();
    }

    private void StubIntMethod(int p1)
    {
    }

    private void StubGuidMethod(Guid p1)
    {
    }

    private void StubStringMethod(string p1)
    {
    }

    private class ListOfInt : List<int>
    {
    }

    private class ListOfInt<T> : List<int>
    {
    }

    private class DoesNotImplementIEnumerable
    {
    }

    private class ImplementsOnlyNonGenericIEnumerable : IEnumerable
    {
        public IEnumerator GetEnumerator() => throw new NotImplementedException();
    }
}
