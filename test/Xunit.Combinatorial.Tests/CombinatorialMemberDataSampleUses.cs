// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

using Xunit;

public class CombinatorialMemberDataSampleUses
{
    private static readonly Random Random = new Random();

#pragma warning disable SA1202 // Elements should be ordered by access
    public static readonly IEnumerable<int> IntFieldValues = Enumerable.Range(0, 5).Select(_ => Random.Next());
    public static readonly IEnumerable<Guid> GuidFieldValues = Enumerable.Range(0, 5).Select(_ => Guid.NewGuid());
#pragma warning restore SA1202 // Elements should be ordered by access

    public static readonly TheoryData<MyTestCase> MyTestCases = new(
        new MyTestCase(1, "Foo"),
        new MyTestCase(2, "Bar"));

    public static IEnumerable<int> IntPropertyValues => GetIntMethodValues();

    public static IEnumerable<Guid> GuidPropertyValues => GetGuidMethodValues();

    public static IEnumerable<int> GetIntMethodValues()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return Random.Next();
        }
    }

    public static IEnumerable<Guid> GetGuidMethodValues()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return Guid.NewGuid();
        }
    }

    public static IEnumerable<int> GetRange(int start, int count)
    {
        return Enumerable.Range(start, count);
    }

    public static IEnumerable<float> GetRange(float start, float count)
    {
        return Enumerable.Range((int)start, (int)count).Select(i => (float)i);
    }

    public static IEnumerable<Guid> GetGuidRange(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return Guid.NewGuid();
        }
    }

#pragma warning disable xUnit1026 // Theory methods should use all of their parameters

    [Theory, CombinatorialData]
    public void CombinatorialMemberDataFromParameterizedMethods(
        [CombinatorialMemberData(nameof(GetRange), 0, 5)] int p1,
        [CombinatorialMemberData(nameof(GetRange), 0f, 5f)] float p2,
        [CombinatorialMemberData(nameof(GetGuidRange), 5)] Guid p3)
    {
        Assert.True(true);
    }

    [Theory, CombinatorialData]
    public void CombinatorialMemberDataFromProperties(
        [CombinatorialMemberData(nameof(GuidPropertyValues))] Guid p1,
        [CombinatorialMemberData(nameof(IntPropertyValues))] int p2)
    {
        Assert.True(true);
    }

    [Theory, CombinatorialData]
    public void CombinatorialMemberDataFromMethods(
        [CombinatorialMemberData(nameof(GetGuidMethodValues))] Guid p1,
        [CombinatorialMemberData(nameof(GetIntMethodValues))] int p2)
    {
        Assert.True(true);
    }

    [Theory, CombinatorialData]
    public void CombinatorialMemberDataFromFields(
        [CombinatorialMemberData(nameof(GuidFieldValues))] Guid p1,
        [CombinatorialMemberData(nameof(IntFieldValues))] int p2)
    {
        Assert.True(true);
    }

    [Theory, CombinatorialData]
    public void TheoryDataOfT([CombinatorialMemberData(nameof(MyTestCases))] MyTestCase testCase, bool flag)
    {
        /*
            This will give you:
            testCase(1, "Foo"), true
            testCase(1, "Foo"), false
            testCase(2, "Bar"), true
            testCase(2, "Bar"), false
        */
    }

    public record MyTestCase(int Number, string Text);
}
