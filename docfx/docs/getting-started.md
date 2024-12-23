# Getting Started

## Installation

Consume this library via its NuGet Package.
Click on the badge to find its latest version and the instructions for consuming it that best apply to your project.

[![NuGet package](https://img.shields.io/nuget/v/Xunit.Combinatorial.svg)](https://nuget.org/packages/Xunit.Combinatorial)

> [!NOTE]
> Xunit.Combinatorial v1.x supports Xunit 2.
>
> Xunit.Combinatorial v2.x supports Xunit 3.

## Introductory example

Suppose you have this test method:

[!code-csharp[](../../samples/GettingStarted.cs#CheckFileSystemFact)]

To arrange for your test method to be invoked twice, once for each of two modes, add a `bool` parameter, make it a theory, and add @Xunit.CombinatorialDataAttribute or @Xunit.PairwiseDataAttribute.

[!code-csharp[](../../samples/GettingStarted.cs#CombinatorialBool)]

The @Xunit.CombinatorialDataAttribute or @Xunit.CombinatorialDataAttribute will supply Xunit with both `true` and `false` arguments to run the test method with, resulting in two invocations of your test method with individual results reported for each invocation.

[Learn more about the difference between @Xunit.CombinatorialDataAttribute and @Xunit.CombinatorialDataAttribute](combinatorial-vs-pairwise.md).

[Learn more about supported parameter types and where values come from](value-sources.md).
