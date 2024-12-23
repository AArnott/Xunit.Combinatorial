// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

public class CombinatorialTwoParameters
{
    #region CombinatorialTwoParameters
    [Theory, CombinatorialData]
    public void CheckValidAge(
        [CombinatorialValues(5, 18, 21, 25)] int age,
        bool friendlyOfficer)
    {
        // This will run with all combinations:
        // 5  true
        // 18 true
        // 21 true
        // 25 true
        // 5  false
        // 18 false
        // 21 false
        // 25 false
    }
    #endregion
}

public class CombinatorialThreeParameters
{
    #region CombinatorialThreeParameters
    [Theory, CombinatorialData]
    public void CheckValidAge(bool p1, bool p2, bool p3)
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
    #endregion
}

public class PairwiseThreeParameters
{
    #region PairwiseThreeParameters
    [Theory, PairwiseData]
    public void CheckValidAge(bool p1, bool p2, bool p3)
    {
        // Pairwise generates these 4 test cases:
        // false false false
        // false true  true
        // true  false true
        // true  true  false
    }
    #endregion
}
