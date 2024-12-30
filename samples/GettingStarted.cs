// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

public class Facts
{
    #region CheckFileSystemFact
    [Fact]
    public void CheckFileSystem()
    {
        // verifications here
    }
    #endregion
}

public class CombinatorialSimple
{
    #region CombinatorialBool
    [Theory, CombinatorialData]
    public void CheckFileSystem(bool recursive)
    {
        // verifications here
    }
    #endregion
}
