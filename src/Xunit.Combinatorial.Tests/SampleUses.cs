namespace Xunit.Combinatorial.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

#pragma warning disable xUnit1026 // Theory methods should use all of their parameters

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

        [Theory, CombinatorialData]
        public void CombinatorialCustomData([CustomValues] int v1, [CustomValues] int v2)
        {
            // Combinatorial generates these test cases:
            // 5 5
            // 5 10
            // 5 15
            // 10 5
            // 10 10
            // 10 15
            // 15 5
            // 15 10
            // 15 15
        }

        [Theory, CombinatorialData]
        public void CombinatorialCustomRange([CustomCountRange] int p1, [CustomStepRange] int p2)
        {
            // Combinatorial generates these test cases:
            // 0 0
            // 1 0
            // 2 0
            // 3 0
            // 4 0
            // 0 2
            // 1 2
            // 2 2
            // 3 2
            // 4 2
        }

        [AttributeUsage(AttributeTargets.Parameter)]
        private class CustomValuesAttribute : CombinatorialValuesAttribute
        {
            public CustomValuesAttribute()
                : base(new object[] { 5, 10, 15 })
            {
            }
        }

        [AttributeUsage(AttributeTargets.Parameter)]
        private class CustomCountRangeAttribute : CombinatorialRangeAttribute
        {
            public CustomCountRangeAttribute()
                : base(0, 5)
            {
            }
        }

        [AttributeUsage(AttributeTargets.Parameter)]
        private class CustomStepRangeAttribute : CombinatorialRangeAttribute
        {
            public CustomStepRangeAttribute()
                : base(0, 3, 2)
            {
            }
        }
    }
}
