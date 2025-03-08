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

## Sponsorships

[GitHub Sponsors](https://github.com/sponsors/AArnott)
[Zcash](zcash:u1vv2ws6xhs72faugmlrasyeq298l05rrj6wfw8hr3r29y3czev5qt4ugp7kylz6suu04363ze92dfg8ftxf3237js0x9p5r82fgy47xkjnw75tqaevhfh0rnua72hurt22v3w3f7h8yt6mxaa0wpeeh9jcm359ww3rl6fj5ylqqv54uuwrs8q4gys9r3cxdm3yslsh3rt6p7wznzhky7)

[NuPkg]: https://www.nuget.org/packages/Xunit.Combinatorial
