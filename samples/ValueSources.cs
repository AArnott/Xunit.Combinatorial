// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

public class CombinatorialValues
{
    #region CombinatorialValues
    [Theory, CombinatorialData]
    public void CheckValidAge([CombinatorialValues(5, 18, 21, 25)] int age)
    {
        // verifications here
    }
    #endregion
}

public class ValuesOverRange
{
    #region ValuesOverRange
    [Theory, CombinatorialData]
    public void CombinatorialCustomRange(
        [CombinatorialRange(0, 5)] int p1,
        [CombinatorialRange(0, 3, 2)] int p2)
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
    }
    #endregion
}

public class GeneratedByMethod
{
    #region GeneratedByMethod
    public static IEnumerable<int> GetRange(int start, int count)
    {
        return Enumerable.Range(start, count);
    }

    [Theory, CombinatorialData]
    public void CombinatorialMemberDataFromParameterizedMethods(
        [CombinatorialMemberData(nameof(GetRange), 0, 5)] int p1)
    {
        Assert.True(true);
    }
    #endregion
}

public class GeneratedByProperty
{
    #region GeneratedByProperty
    public static IEnumerable<int> IntPropertyValues
    {
        get
        {
            for (int i = 0; i < 5; i++)
            {
                yield return Random.Shared.Next();
            }
        }
    }

    [Theory, CombinatorialData]
    public void CombinatorialMemberDataFromProperties(
        [CombinatorialMemberData(nameof(IntPropertyValues))] int p1)
    {
        Assert.True(true);
    }
    #endregion
}

public class GeneratedByField
{
    #region GeneratedByField
    public static readonly IEnumerable<int> IntFieldValues =
        Enumerable.Range(0, 5).Select(_ => Random.Shared.Next());

    [Theory, CombinatorialData]
    public void CombinatorialMemberDataFromFields(
        [CombinatorialMemberData(nameof(IntFieldValues))] int p2)
    {
        Assert.True(true);
    }
    #endregion
}

public class CombinatorialRandomData
{
    #region CombinatorialRandomData
    [Theory, CombinatorialData]
    public void CombinatorialRandomValuesCount(
        [CombinatorialRandomData(Count = 10)] int p1)
    {
        Assert.InRange(p1, 0, int.MaxValue);
    }

    [Theory, CombinatorialData]
    public void CombinatorialRandomValuesCountMinMaxValues(
        [CombinatorialRandomData(Count = 10, Minimum = -20, Maximum = -5)] int p1)
    {
        Assert.InRange(p1, -20, -5);
    }
    #endregion
}
