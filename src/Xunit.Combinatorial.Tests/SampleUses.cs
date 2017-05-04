// Copyright (c) parts Andrew Arnott, parts Matthew Wilkinson. All rights reserved. Licensed under the Ms-PL.

namespace Xunit.Combinatorial.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class SampleUses
    {
        [Theory, CombinatorialData]
        public void CombinatorialBoolean(bool v1, bool v2, bool v3)
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

        [Theory, PairwiseData]
        public void PairwiseBoolean(bool v1, bool v2, bool v3)
        {
            // Pairwise generates these 4 test cases:
            // false false false
            // false true  true
            // true  false true
            // true  true  false
        }
    }
}
