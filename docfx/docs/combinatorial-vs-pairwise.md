# Combinatorial vs. pairwise

Combinatorial and pairwise are equivalent while using only 1 or 2 parameters in your theory.
For 1-2 parameters, each of these schemes would produce a test case of every combination of every allowed value for each parameter.

For example, consider this two parameter theory:

[!code-csharp[](../../samples/CombinatorialVsPairwise.cs#CombinatorialTwoParameters)]

## Combinatorial testing

Once you have more than two parameters, the number of test cases grow exponentially in order to cover every possible combination when using @Xunit.CombinatorialDataAttribute.
Consider this test with 3 parameters, each taking just two values:

[!code-csharp[](../../samples/CombinatorialVsPairwise.cs#CombinatorialThreeParameters)]

We already have 8 test cases for just 3 @System.Boolean parameters.
With more parameters or more values per parameter the test cases can quickly grow to a very large number.
In general, the exponential function is:

$$a^p$$

Where `a` is the number of allowed possible values for an argument and `p` is the number of parameters.
Or if parameters each have a unique number of possible values, the combinatorial explosion is modeled as:

$$p_1 \times p_2 \times p_3 \times ...$$

Where p<sub>n</sub> is the number of possibles values of the parameter at index `n`.

## Pairwise testing

An exponential explosion of test cases can cause your test runs to take too long.
This level of exhaustive testing is often not necessary as many bugs will show up given a combination of just two parameters with particular values.
Pairwise testing focuses on this idea and it generates far fewer test cases than combinatorial testing because it only ensures there is a test case covering every combination of two parameters.
It does this in a clever way that can "compress" the test cases by making each test case significantly test more than one pair.

To use pairwise testing, use the @Xunit.PairwiseDataAttribute instead of the
@Xunit.CombinatorialDataAttribute:

[!code-csharp[](../../samples/CombinatorialVsPairwise.cs#PairwiseThreeParameters)]

We have cut the number of test cases in half by using pairwise instead of combinatorial.
As parameter count rises or allowed values per parameter are more than 2, the test case reduction by switching from combinatorial to pairwise can be much greater.

Notice that although the test cases are fewer, you can still find a test case that covers any *two* parameter values (thus *pair*wise).
