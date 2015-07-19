Xunit.Combinatorial
======================

This project allows for parameterizing your Xunit test methods such that
they run multiple times, once for each combination of possible arguments
for your test method.

[![Build status](https://ci.appveyor.com/api/projects/status/7w8sae8tfn0gf4g6?svg=true)](https://ci.appveyor.com/project/AArnott/xunit-combinatorial)

## Installation

This project is available as a [NuGet package][NuPkg]

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

To supply your own values to pass in for each parameter, use the
`CombinatorialValuesAttribute`:

    [Theory, CombinatorialData]
    public void CheckValidAge([CombinatorialValues(5, 18, 21, 25)] int age)
    {
        // verifications here
    }

This will run your test method four times with each of the prescribed values.

 [NuPkg]: https://www.nuget.org/packages/Xunit.Combinatorial
