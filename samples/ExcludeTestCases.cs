// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

public class ExcludeSpecificCase
{
    #region ExcludeSpecificCase
    [Theory, CombinatorialData]
    [ExcludeTestCase(true, false)]
    public void CheckOnlyWantedPairs(bool isAdmin, bool isActive)
    {
        // Combinatorial would normally generate these 4 test cases:
        // false false
        // false true
        // true  false
        // true  true
        // The ExcludeTestCase attribute removes: true false
    }
    #endregion
}

public class ExcludeWildcardCase
{
    #region ExcludeWildcardCase
    [Theory, CombinatorialData]
    [ExcludeTestCase(typeof(AnyDataValue), false)]
    public void CheckOnlyEnabledStates(bool isAdmin, bool isEnabled)
    {
        // This excludes every generated test case where isEnabled is false,
        // regardless of the value selected for isAdmin.
    }
    #endregion
}
