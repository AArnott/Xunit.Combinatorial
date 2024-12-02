// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

using Xunit;

#pragma warning disable xUnit1026 // Theory methods should use all of their parameters

public class SampleUses
{
    public static TheoryData<CustomCombinatorialDataSourceTestCase> GetCustomCombinatorialMemberData(
        int seed /* Optional parameters are currently not supported on member data accessors */)
    {
        return CustomCombinatorialDataSource.GetMemberData(seed);
    }

    [Theory, CombinatorialData]
    public void CombinatorialBoolean(bool v1, bool v2, bool v3)
    {
        // Combinatorial generates these 8 test cases:
        // false false false
        // false false true
        // false true  false
        // false true  true
        // true  false false
        // true  false true
        // true  true  false
        // true  true  true
    }

    [Theory, PairwiseData]
    public void PairwiseBoolean(bool v1, bool v2, bool v3)
    {
        // Pairwise generates these 4 test cases:
        // false false false
        // false true  true
        // true  false true
        // true  true  false
    }

    [Theory, CombinatorialData]
    public void CombinatorialCustomData([CustomValues] int v1, [CustomValues] int v2)
    {
        // Combinatorial generates these test cases:
        // 5 5
        // 5 10
        // 5 15
        // 10 5
        // 10 10
        // 10 15
        // 15 5
        // 15 10
        // 15 15
    }

    [Theory, CombinatorialData]
    public void CombinatorialCustomRange([CombinatorialRange(0, 5)] int p1, [CombinatorialRange(0, 3, 2)] int p2)
    {
        // Combinatorial generates these test cases:
        // 0 0
        // 1 0
        // 2 0
        // 3 0
        // 4 0
        // 0 2
        // 1 2
        // 2 2
        // 3 2
        // 4 2
        Assert.True(p1 == 0 || p1 == 1 || p1 == 2 || p1 == 3 || p1 == 4);
        Assert.True(p2 == 0 || p2 == 2);
    }

    [Theory, CombinatorialData]
    public void CombinatorialRandomValuesDefault([CombinatorialRandomData] int p1)
    {
        Assert.InRange(p1, 0, int.MaxValue);
    }

    [Theory, CombinatorialData]
    public void CombinatorialRandomValuesCount([CombinatorialRandomData(Count = 10)] int p1)
    {
        Assert.InRange(p1, 0, int.MaxValue);
    }

    [Theory, CombinatorialData]
    public void CombinatorialRandomValuesCountMaxValue([CombinatorialRandomData(Count = 10, Maximum = 35)] int p1)
    {
        Assert.InRange(p1, 0, 35);
    }

    [Theory, CombinatorialData]
    public void CombinatorialRandomValuesCountMinMaxValues(
        [CombinatorialRandomData(Count = 10, Minimum = -20, Maximum = -5)]
        int p1)
    {
        Assert.InRange(p1, -20, -5);
    }

    [Theory, CombinatorialData]
    public void CombinatorialRandomValuesCountMinMaxValuesSeed(
        [CombinatorialRandomData(Count = 10, Minimum = -5, Maximum = 6, Seed = 567)]
        int p1)
    {
        Assert.InRange(p1, -5, 6);
    }

    [Theory, CombinatorialData]
    public void CombinatorialMemberDataAttributeTest(
        [CombinatorialMemberData(
            nameof(GetCustomCombinatorialMemberData),
            /* seed */ 0)]
        CustomCombinatorialDataSourceTestCase testCase,
        bool flag)
    {
        Assert.InRange(testCase.Value, 1, 3);
    }

#if NETSTANDARD2_0_OR_GREATER
    [Theory, CombinatorialData]
    public void GenericCombinatorialMemberDataAttributeTest(
        [CombinatorialMemberData<CustomCombinatorialDataSource>(
            nameof(CustomCombinatorialDataSource.GetMemberData),
            /* seed */ 0)]
        CustomCombinatorialDataSourceTestCase testCase,
        bool flag)
    {
        Assert.InRange(testCase.Value, 1, 3);
    }
#endif

    [Theory, CombinatorialData]
    public void CombinatorialClassDataAttributeTest(
        [CombinatorialClassData(typeof(CustomCombinatorialDataSource))]
        CustomCombinatorialDataSourceTestCase testCase,
        bool flag)
    {
        Assert.InRange(testCase.Value, 1, 3);
    }

#if NETSTANDARD2_0_OR_GREATER
    [Theory, CombinatorialData]
    public void GenericCombinatorialClassDataAttributeTest(
        [CombinatorialClassData<CustomCombinatorialDataSource>]
        CustomCombinatorialDataSourceTestCase testCase,
        bool flag)
    {
        Assert.InRange(testCase.Value, 1, 3);
    }
#endif

    [Theory, CombinatorialData]
    public void CombinatorialClassDataAttributeTestWithArgument(
        [CombinatorialClassData(typeof(CustomCombinatorialDataSource), 42)]
        CustomCombinatorialDataSourceTestCase testCase,
        bool flag)
    {
        var argumentValue = 42;
        Assert.InRange(testCase.Value, argumentValue + 1, argumentValue + 3);
    }

#if NETSTANDARD2_0_OR_GREATER
    [Theory, CombinatorialData]
    public void GenericCombinatorialClassDataAttributeTestWithArgument(
        [CombinatorialClassData<CustomCombinatorialDataSource>(42)]
        CustomCombinatorialDataSourceTestCase testCase,
        bool flag)
    {
        var argumentValue = 42;
        Assert.InRange(testCase.Value, argumentValue + 1, argumentValue + 3);
    }
#endif

    public class CustomCombinatorialDataSourceTestCase
    {
        public CustomCombinatorialDataSourceTestCase(int value)
        {
            this.Value = value;
        }

        public int Value { get; }
    }

    private class CustomCombinatorialDataSource : TheoryData<CustomCombinatorialDataSourceTestCase>
    {
        public CustomCombinatorialDataSource(int seed = 0)
        {
            this.Add(new CustomCombinatorialDataSourceTestCase(seed + 1));
            this.Add(new CustomCombinatorialDataSourceTestCase(seed + 2));
            this.Add(new CustomCombinatorialDataSourceTestCase(seed + 3));
        }

        public static TheoryData<CustomCombinatorialDataSourceTestCase> GetMemberData(int seed = 0)
        {
            return new CustomCombinatorialDataSource(seed);
        }
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    private class CustomValuesAttribute : CombinatorialValuesAttribute
    {
        public CustomValuesAttribute()
            : base([5, 10, 15])
        {
        }
    }
}
