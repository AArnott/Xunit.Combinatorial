# Xunit.Combinatorial

This project allows for parameterizing your Xunit test methods such that
they run multiple times, once for each combination of possible arguments
for your test method. You can also limit the number of test cases by using
a pairwise strategy, which generally provides good coverage for testing
but significantly reduces the test case explosion you might have when
you have more than two parameters.

[![NuGet package](https://img.shields.io/nuget/v/xunit.combinatorial.svg)][NuPkg]
[![🏭 Build](https://github.com/AArnott/Xunit.Combinatorial/actions/workflows/build.yml/badge.svg)](https://github.com/AArnott/Xunit.Combinatorial/actions/workflows/build.yml)

## Installation

This project is available as a [NuGet package][NuPkg].

The v1.x versions of this package support xunit 2.
The v2.x versions of this package support xunit 3.

## Example

Suppose you have this test method:

```cs
[Fact]
public void CheckFileSystem(bool recursive)
{
    // verifications here
}
```

To arrange for your test method to be invoked twice, once for each value
of its bool parameter, change the attributes to:

```cs
[Theory, CombinatorialData]
public void CheckFileSystem(bool recursive)
{
    // verifications here
}
```

The `CombinatorialDataAttribute` will supply Xunit with both `true` and `false`
arguments to run the test method with, resulting in two invocations of your
test method with individual results reported for each invocation.

[Learn much more on our docs site](https://aarnott.github.io/Xunit.Combinatorial/).

[NuPkg]: https://www.nuget.org/packages/Xunit.Combinatorial
