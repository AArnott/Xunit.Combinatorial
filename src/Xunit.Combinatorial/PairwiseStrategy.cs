// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

// ***********************************************************************
// Copyright (c) 2008 Charlie Poole
// Copyright (c) 2015 Andrew Arnott (modified from Charlie's original)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************
namespace Xunit;

/// <summary>
/// PairwiseStrategy creates test cases by combining the parameter
/// data so that all possible pairs of data items are used.
/// </summary>
/// <remarks>
/// <para>
/// The number of test cases that cover all possible pairs of test function
/// parameters values is significantly less than the number of test cases
/// that cover all possible combination of test function parameters values.
/// And because different studies show that most of software failures are
/// caused by combination of no more than two parameters, pairwise testing
/// can be an effective ways to test the system when it's impossible to test
/// all combinations of parameters.
/// </para>
/// <para>
/// The PairwiseStrategy code is based on "jenny" tool by Bob Jenkins:
/// <see href="http://burtleburtle.net/bob/math/jenny.html"/>.
/// </para>
/// </remarks>
internal static class PairwiseStrategy
{
    // NOTE: Terminology in this class is based on the literature
    // relating to strategies for combining variable features when
    // creating tests. This terminology is more closely related to
    // higher level testing than it is to unit tests. See
    // comments in the code for further explanations.

    /// <summary>
    /// Creates a set of test cases for specified dimensions.
    /// </summary>
    /// <param name="dimensions">
    /// An array which contains information about dimensions. Each element of
    /// this array represents a number of features in the specific dimension.
    /// </param>
    /// <returns>
    /// A set of test cases.
    /// </returns>
    public static List<int[]> GetTestCases(int[] dimensions)
    {
        return GetTestCases(dimensions, null);
    }

    /// <summary>
    /// Creates a set of test cases for specified dimensions.
    /// </summary>
    /// <param name="dimensions">
    /// An array which contains information about dimensions. Each element of
    /// this array represents a number of features in the specific dimension.
    /// </param>
    /// <param name="isTestCaseAllowed">A predicate that returns <see langword="true"/> when a generated test case is allowed.</param>
    /// <returns>
    /// A set of test cases.
    /// </returns>
    public static List<int[]> GetTestCases(int[] dimensions, Predicate<int[]>? isTestCaseAllowed)
    {
        return (from testCase in new PairwiseTestCaseGenerator().GetTestCases(dimensions, isTestCaseAllowed)
                select testCase.Features).ToList();
    }

    private static bool IsTupleCovered(this TestCaseInfo testCaseInfo, FeatureTuple tuple)
    {
        for (int i = 0; i < tuple.Length; i++)
        {
            if (testCaseInfo.Features[tuple[i].Dimension] != tuple[i].Feature)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// FleaRand is a pseudo-random number generator developed by Bob Jenkins:
    /// <see href="http://burtleburtle.net/bob/rand/talksmall.html#flea" />.
    /// </summary>
    private class FleaRand
    {
        private uint b;
        private uint c;
        private uint d;
        private uint z;
        private uint[] m;
        private uint[] r;
        private uint q;

        /// <summary>
        /// Initializes a new instance of the <see cref="FleaRand"/> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public FleaRand(uint seed)
        {
            this.b = seed;
            this.c = seed;
            this.d = seed;
            this.z = seed;
            this.m = new uint[256];
            this.r = new uint[256];

            for (int i = 0; i < this.m.Length; i++)
            {
                this.m[i] = seed;
            }

            for (int i = 0; i < 10; i++)
            {
                this.Batch();
            }

            this.q = 0;
        }

        public uint Next()
        {
            if (this.q == 0)
            {
                this.Batch();
                this.q = (uint)this.r.Length - 1;
            }
            else
            {
                this.q--;
            }

            return this.r[this.q];
        }

        private void Batch()
        {
            uint a;
            uint b = this.b;
            uint c = this.c + (++this.z);
            uint d = this.d;

            for (int i = 0; i < this.r.Length; i++)
            {
                a = this.m[b % this.m.Length];
                this.m[b % this.m.Length] = d;
                d = (c << 19) + (c >> 13) + b;
                c = b ^ this.m[i];
                b = a + d;
                this.r[i] = c;
            }

            this.b = b;
            this.c = c;
            this.d = d;
        }
    }

    /// <summary>
    /// FeatureInfo represents coverage of a single value of test function
    /// parameter, represented as a pair of indices, Dimension and Feature. In
    /// terms of unit testing, Dimension is the index of the test parameter and
    /// Feature is the index of the supplied value in that parameter's list of
    /// sources.
    /// </summary>
    private class FeatureInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureInfo"/> class.
        /// </summary>
        /// <param name="dimension">Index of a dimension.</param>
        /// <param name="feature">Index of a feature.</param>
        public FeatureInfo(int dimension, int feature)
        {
            this.Dimension = dimension;
            this.Feature = feature;
        }

        public int Dimension { get; }

        public int Feature { get; }
    }

    /// <summary>
    /// A FeatureTuple represents a combination of features, one per test
    /// parameter, which should be covered by a test case. In the
    /// PairwiseStrategy, we are only trying to cover pairs of features, so the
    /// tuples actually may contain only single feature or pair of features, but
    /// the algorithm itself works with triplets, quadruples and so on.
    /// </summary>
    private class FeatureTuple
    {
        private readonly FeatureInfo[] features;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureTuple"/> class
        /// for a single feature.
        /// </summary>
        /// <param name="feature1">Single feature.</param>
        public FeatureTuple(FeatureInfo feature1)
        {
            this.features = new FeatureInfo[] { feature1 };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureTuple"/> class
        /// for a pair of features.
        /// </summary>
        /// <param name="feature1">First feature.</param>
        /// <param name="feature2">Second feature.</param>
        public FeatureTuple(FeatureInfo feature1, FeatureInfo feature2)
        {
            this.features = new FeatureInfo[] { feature1, feature2 };
        }

        public int Length
        {
            get
            {
                return this.features.Length;
            }
        }

        public FeatureInfo this[int index]
        {
            get
            {
                return this.features[index];
            }
        }
    }

    /// <summary>
    /// TestCase represents a single test case covering a list of features.
    /// </summary>
    private class TestCaseInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseInfo"/> class.
        /// </summary>
        /// <param name="length">A number of features in the test case.</param>
        public TestCaseInfo(int length)
        {
            this.Features = new int[length];
        }

        public int[] Features { get; }
    }

    /// <summary>
    /// PairwiseTestCaseGenerator class implements an algorithm which generates
    /// a set of test cases which covers all pairs of possible values of test
    /// function.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The algorithm starts with creating a set of all feature tuples which we
    /// will try to cover (see <see
    /// cref="PairwiseTestCaseGenerator.CreateAllTuples" /> method). This set
    /// includes every single feature and all possible pairs of features. We
    /// store feature tuples in the 3-D collection (where axes are "dimension",
    /// "feature", and "all combinations which includes this feature"), and for
    /// every two feature (e.g. "A" and "B") we generate both ("A", "B") and
    /// ("B", "A") pairs. This data structure extremely reduces the amount of
    /// time needed to calculate coverage for a single test case (this
    /// calculation is the most time-consuming part of the algorithm).
    /// </para>
    /// <para>
    /// Then the algorithm picks one tuple from the uncovered tuple, creates a
    /// test case that covers this tuple, and then removes this tuple and all
    /// other tuples covered by this test case from the collection of uncovered
    /// tuples.
    /// </para>
    /// <para>
    /// Picking a tuple to cover.
    /// </para>
    /// <para>
    /// There are no any special rules defined for picking tuples to cover. We
    /// just pick them one by one, in the order they were generated.
    /// </para>
    /// <para>
    /// Test generation.
    /// </para>
    /// <para>
    /// Test generation starts from creating a completely random test case which
    /// covers, nevertheless, previously selected tuple. Then the algorithm
    /// tries to maximize number of tuples which this test covers.
    /// </para>
    /// <para>
    /// Test generation and maximization process repeats seven times for every
    /// selected tuple and then the algorithm picks the best test case ("seven"
    /// is a magic number which provides good results in acceptable time).
    /// </para>
    /// <para>Maximizing test coverage.</para>
    /// <para>
    /// To maximize tests coverage, the algorithm walks thru the list of mutable
    /// dimensions (mutable dimension is a dimension that are not included in
    /// the previously selected tuple). Then for every dimension, the algorithm
    /// walks thru the list of features and checks if this feature provides
    /// better coverage than randomly selected feature, and if yes keeps this
    /// feature.
    /// </para>
    /// <para>
    /// This process repeats while it shows progress. If the last iteration
    /// doesn't improve coverage, the process ends.
    /// </para>
    /// <para>
    /// In addition, for better results, before start every iteration, the
    /// algorithm "scrambles" dimensions - so for every iteration dimension
    /// probes in a different order.
    /// </para>
    /// </remarks>
    private class PairwiseTestCaseGenerator
    {
        private FleaRand? prng;

        private int[]? dimensions;

        private List<FeatureTuple>[][]? uncoveredTuples;

        /// <summary>
        /// A predicate that determines whether a generated test case satisfies caller-supplied constraints.
        /// </summary>
        private Predicate<int[]>? isTestCaseAllowed;

        /// <summary>
        /// Creates a set of test cases for specified dimensions.
        /// </summary>
        /// <param name="dimensions">
        /// An array which contains information about dimensions. Each element of
        /// this array represents a number of features in the specific dimension.
        /// </param>
        /// <param name="isTestCaseAllowed">A predicate that returns <see langword="true"/> when a generated test case is allowed.</param>
        /// <returns>
        /// A set of test cases.
        /// </returns>
        public List<TestCaseInfo> GetTestCases(int[] dimensions, Predicate<int[]>? isTestCaseAllowed)
        {
            this.prng = new FleaRand(15485863);
            this.dimensions = dimensions;
            this.isTestCaseAllowed = isTestCaseAllowed;

            this.CreateAllTuples();

            List<TestCaseInfo> testCases = new List<TestCaseInfo>();

            while (true)
            {
                FeatureTuple? tuple = this.GetNextTuple();

                if (tuple is null)
                {
                    break;
                }

                TestCaseInfo? testCase = this.CreateTestCase(tuple);

                if (testCase is null)
                {
                    // No allowed row can cover this tuple, so remove it to avoid repeatedly trying to satisfy an impossible constraint.
                    this.RemoveTuple(tuple);
                    continue;
                }

                this.RemoveTuplesCoveredByTest(testCase);

                testCases.Add(testCase);
            }

            return testCases;
        }

        private int GetNextRandomNumber()
        {
            return (int)(this.prng!.Next() >> 1);
        }

        private void CreateAllTuples()
        {
            this.uncoveredTuples = new List<FeatureTuple>[this.dimensions!.Length][];

            for (int d = 0; d < this.dimensions.Length; d++)
            {
                this.uncoveredTuples[d] = new List<FeatureTuple>[this.dimensions[d]];

                for (int f = 0; f < this.dimensions[d]; f++)
                {
                    this.uncoveredTuples[d][f] = this.CreateTuples(d, f);
                }
            }
        }

        private List<FeatureTuple> CreateTuples(int dimension, int feature)
        {
            List<FeatureTuple> result = new List<FeatureTuple>();

            result.Add(new FeatureTuple(new FeatureInfo(dimension, feature)));

            for (int d = 0; d < this.dimensions!.Length; d++)
            {
                if (d != dimension)
                {
                    for (int f = 0; f < this.dimensions[d]; f++)
                    {
                        result.Add(new FeatureTuple(new FeatureInfo(dimension, feature), new FeatureInfo(d, f)));
                    }
                }
            }

            return result;
        }

        private FeatureTuple? GetNextTuple()
        {
            for (int d = 0; d < this.uncoveredTuples!.Length; d++)
            {
                for (int f = 0; f < this.uncoveredTuples[d].Length; f++)
                {
                    List<FeatureTuple> tuples = this.uncoveredTuples[d][f];

                    if (tuples.Count > 0)
                    {
                        FeatureTuple tuple = tuples[0];
                        tuples.RemoveAt(0);
                        return tuple;
                    }
                }
            }

            return null;
        }

        private TestCaseInfo? CreateTestCase(FeatureTuple tuple)
        {
            TestCaseInfo? bestTestCase = null;
            int bestCoverage = -1;

            for (int i = 0; i < 7; i++)
            {
                TestCaseInfo? testCase = this.CreateRandomTestCase(tuple);
                if (testCase is null)
                {
                    continue;
                }

                int coverage = this.MaximizeCoverage(testCase, tuple);

                if (coverage > bestCoverage)
                {
                    bestTestCase = testCase;
                    bestCoverage = coverage;
                }
            }

            return bestTestCase;
        }

        private TestCaseInfo? CreateRandomTestCase(FeatureTuple tuple)
        {
            TestCaseInfo result = new TestCaseInfo(this.dimensions!.Length);

            for (int d = 0; d < this.dimensions.Length; d++)
            {
                result.Features[d] = this.GetNextRandomNumber() % this.dimensions[d];
            }

            for (int i = 0; i < tuple.Length; i++)
            {
                result.Features[tuple[i].Dimension] = tuple[i].Feature;
            }

            return this.IsTestCaseAllowed(result) ? result : this.FindAllowedTestCase(tuple);
        }

        private int MaximizeCoverage(TestCaseInfo testCase, FeatureTuple tuple)
        {
            // It starts with one because we always have one tuple which is covered by the test.
            int totalCoverage = 1;
            int[] mutableDimensions = this.GetMutableDimensions(tuple);

            while (true)
            {
                bool progress = false;

                this.ScrambleDimensions(mutableDimensions);

                for (int i = 0; i < mutableDimensions.Length; i++)
                {
                    int d = mutableDimensions[i];

                    int bestCoverage = this.CountTuplesCoveredByTest(testCase, d, testCase.Features[d]);

                    int newCoverage = this.MaximizeCoverageForDimension(testCase, d, bestCoverage);

                    totalCoverage += newCoverage;

                    if (newCoverage > bestCoverage)
                    {
                        progress = true;
                    }
                }

                if (!progress)
                {
                    return totalCoverage;
                }
            }
        }

        private int[] GetMutableDimensions(FeatureTuple tuple)
        {
            List<int> result = new List<int>();

            bool[] immutableDimensions = new bool[this.dimensions!.Length];

            for (int i = 0; i < tuple.Length; i++)
            {
                immutableDimensions[tuple[i].Dimension] = true;
            }

            for (int d = 0; d < this.dimensions.Length; d++)
            {
                if (!immutableDimensions[d])
                {
                    result.Add(d);
                }
            }

            return result.ToArray();
        }

        private void ScrambleDimensions(int[] dimensions)
        {
            for (int i = 0; i < dimensions.Length; i++)
            {
                int j = this.GetNextRandomNumber() % dimensions.Length;
                int t = dimensions[i];
                dimensions[i] = dimensions[j];
                dimensions[j] = t;
            }
        }

        private int MaximizeCoverageForDimension(TestCaseInfo testCase, int dimension, int bestCoverage)
        {
            List<int> bestFeatures = new List<int>(this.dimensions![dimension]);

            for (int f = 0; f < this.dimensions[dimension]; f++)
            {
                testCase.Features[dimension] = f;

                if (!this.IsTestCaseAllowed(testCase))
                {
                    continue;
                }

                int coverage = this.CountTuplesCoveredByTest(testCase, dimension, f);

                if (coverage >= bestCoverage)
                {
                    if (coverage > bestCoverage)
                    {
                        bestCoverage = coverage;
                        bestFeatures.Clear();
                    }

                    bestFeatures.Add(f);
                }
            }

            testCase.Features[dimension] = bestFeatures[this.GetNextRandomNumber() % bestFeatures.Count];

            return bestCoverage;
        }

        /// <summary>
        /// Exhaustively searches the feature space to find any allowed test case that covers a tuple when a random test case is vetoed.
        /// </summary>
        /// <param name="tuple">The tuple that must be covered by the returned test case.</param>
        /// <returns>An allowed test case, or <see langword="null"/> when none can cover <paramref name="tuple"/>.</returns>
        private TestCaseInfo? FindAllowedTestCase(FeatureTuple tuple)
        {
            TestCaseInfo result = new TestCaseInfo(this.dimensions!.Length);
            bool[] initializedDimensions = new bool[this.dimensions.Length];

            for (int i = 0; i < tuple.Length; i++)
            {
                result.Features[tuple[i].Dimension] = tuple[i].Feature;
                initializedDimensions[tuple[i].Dimension] = true;
            }

            return this.FindAllowedTestCase(result, initializedDimensions, 0) ? result : null;
        }

        /// <summary>
        /// Recursively assigns features to uninitialized dimensions until it finds an allowed test case.
        /// </summary>
        /// <param name="testCase">The test case being initialized.</param>
        /// <param name="initializedDimensions">A value indicating whether each dimension has already been assigned.</param>
        /// <param name="dimension">The dimension to initialize.</param>
        /// <returns>A value indicating whether <paramref name="testCase"/> was assigned allowed features.</returns>
        private bool FindAllowedTestCase(TestCaseInfo testCase, bool[] initializedDimensions, int dimension)
        {
            if (dimension == this.dimensions!.Length)
            {
                return this.IsTestCaseAllowed(testCase);
            }

            if (initializedDimensions[dimension])
            {
                return this.FindAllowedTestCase(testCase, initializedDimensions, dimension + 1);
            }

            for (int f = 0; f < this.dimensions[dimension]; f++)
            {
                testCase.Features[dimension] = f;
                if (this.FindAllowedTestCase(testCase, initializedDimensions, dimension + 1))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks whether a test case satisfies the caller-supplied constraints.
        /// </summary>
        /// <param name="testCase">The test case to validate.</param>
        /// <returns><see langword="true"/> if the test case is allowed; otherwise, <see langword="false"/>.</returns>
        private bool IsTestCaseAllowed(TestCaseInfo testCase)
        {
            return this.isTestCaseAllowed?.Invoke(testCase.Features) ?? true;
        }

        private int CountTuplesCoveredByTest(TestCaseInfo testCase, int dimension, int feature)
        {
            int result = 0;

            List<FeatureTuple> tuples = this.uncoveredTuples![dimension][feature];

            for (int i = 0; i < tuples.Count; i++)
            {
                if (testCase.IsTupleCovered(tuples[i]))
                {
                    result++;
                }
            }

            return result;
        }

        private void RemoveTuplesCoveredByTest(TestCaseInfo testCase)
        {
            for (int d = 0; d < this.uncoveredTuples!.Length; d++)
            {
                for (int f = 0; f < this.uncoveredTuples[d].Length; f++)
                {
                    List<FeatureTuple> tuples = this.uncoveredTuples[d][f];

                    for (int i = tuples.Count - 1; i >= 0; i--)
                    {
                        if (testCase.IsTupleCovered(tuples[i]))
                        {
                            tuples.RemoveAt(i);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Removes a tuple that cannot be covered by any allowed test case.
        /// </summary>
        /// <param name="tuple">The tuple to remove.</param>
        private void RemoveTuple(FeatureTuple tuple)
        {
            for (int i = 0; i < tuple.Length; i++)
            {
                this.uncoveredTuples![tuple[i].Dimension][tuple[i].Feature].Remove(tuple);
            }
        }
    }
}
