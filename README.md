Xunit.Combinatorial
======================

This project allows for parameterizing your Xunit test methods such that
they run multiple times, once for each combination of possible arguments
for your test method. You can also limit the number of test cases by using
a pairwise strategy, which generally provides good coverage for testing
but significantly reduces the test case explosion you might have when
you have more than two parameters.

[![Build status](https://ci.appveyor.com/api/projects/status/7w8sae8tfn0gf4g6?svg=true)](https://ci.appveyor.com/project/AArnott/xunit-combinatorial)

## Installation

This project is available as a [NuGet package][NuPkg]

## Example

Suppose you have this test method:

```csharp
[Fact]
public void CheckFileSystem(bool recursive)
{
    // verifications here
}
```

To arrange for your test method to be invoked twice, once for each value
of its bool parameter, change the attributes to 

```csharp
[Theory, CombinatorialData]
public void CheckFileSystem(bool recursive)
{
    // verifications here
}
```

The `CombinatorialDataAttribute` will supply Xunit with both `true` and `false`
arguments to run the test method with, resulting in two invocations of your 
test method with individual results reported for each invocation.

To supply your own values to pass in for each parameter, use the
`CombinatorialValuesAttribute`:

```csharp
[Theory, CombinatorialData]
public void CheckValidAge([CombinatorialValues(5, 18, 21, 25)] int age)
{
    // verifications here
}
```

This will run your test method four times with each of the prescribed values.

Of course it wouldn't be combinatorial without multiple parameters:

```csharp
[Theory, CombinatorialData]
public void CheckValidAge([CombinatorialValues(5, 18, 21, 25)] int age, bool friendlyOfficer)
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
```

Once you have more that two parameters, the number of test cases can grow
dramatically in order to cover every possible combination.
Consider this test with 3 parameters, each taking just two values:

```csharp
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
```

We already have 8 test cases. With more parameters or more values per parameter
the test cases can quickly grow to a very large number.
This can cause your test runs to take too long. This level of
exhaustive testing is often not necessary as many bugs will show up whenever
two parameters are specific values. This is called "pairwise testing" and
it generates far fewer test cases than combinatorial testing because
it only ensures there is a test case covering every combination of two
parameters, and thus can "compress" the test cases by making each test case
significantly test more than one pair.

To use pairwise testing, use the `PairwiseDataAttribute` instead of the
`CombinatorialDataAttribute`:

```csharp
[Theory, PairwiseData]
public void CheckValidAge(bool p1, bool p2, bool p3)
{
    // Pairwise generates these 4 test cases:
    // false false false
    // false true  true
    // true  false true
    // true  true  false
}
```

We have cut the number of test cases in half by using pairwise instead of
combinatorial. In many cases the test reduction can be much greater.
Notice that although the test cases are fewer, you can still find a test
case that covers any *two* parameter values (thus *pair*wise).

 [NuPkg]: https://www.nuget.org/packages/Xunit.Combinatorial
