Xunit.Combinatorial
======================

This project allows for parameterizing your Xunit test methods such that
they run multiple times, once for each combination of possible arguments
for your test method.

## Example

Suppose you have this test method:

    [Fact]
    public void CheckFileSystem(bool recursive)
    {
        // verifications here
    }

To arrange for your test method to be invoked twice, once for each value
of its bool parameter, change the attributes to 

    [Theory, CombinatorialData]
    public void CheckFileSystem(bool recursive)
    {
        // verifications here
    }

The `CombinatorialDataAttribute` will supply Xunit with both `true` and `false`
arguments to run the test method with, resulting in two invocations of your 
test method with individual results reported for each invocation.
