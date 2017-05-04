// Copyright (c) parts Andrew Arnott, parts Matthew Wilkinson. All rights reserved. Licensed under the Ms-PL.

namespace Xunit.Combinatorial.Tests
{
    using System;
    using Xunit.Combinatorial.Tests.Utils;

    public sealed class AllFlagsTests
    {
        [Flags]
        private enum AllFlagTestEnum
        {
            A = 1, B = 2, C = 4
        }

        [Fact]
        public void GeneratesAllFlagCombinations()
        {
            var data = Data.Generate<AllFlagTestEnum>(new CombinatorialDataAttribute(), AllFlags);
            AssertSets.Equal(new []
            {
                new object[] { (AllFlagTestEnum) 0 },
                new object[] { AllFlagTestEnum.A },
                new object[] { AllFlagTestEnum.B },
                new object[] { AllFlagTestEnum.C },
                new object[] { AllFlagTestEnum.A | AllFlagTestEnum.B },
                new object[] { AllFlagTestEnum.A | AllFlagTestEnum.C },
                new object[] { AllFlagTestEnum.B | AllFlagTestEnum.C },
                new object[] { AllFlagTestEnum.A | AllFlagTestEnum.B | AllFlagTestEnum.C }
            }, data, Test.SetsEqual);
        }

        private static void AllFlags([AllFlags] AllFlagTestEnum afte) { }

        [Fact]
        public void RejectsNonEnumTypes()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                Data.Generate<int>(new CombinatorialDataAttribute(), RejectsNonEnumTypes);
            });
        }

        private static void RejectsNonEnumTypes([AllFlags] int i) { }

        [Fact]
        public void RejectsNonFlagsEnums()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                Data.Generate<DateTimeKind>(new CombinatorialDataAttribute(), RejectsNonFlagsEnums);
            });
        }

        private static void RejectsNonFlagsEnums([AllFlags] DateTimeKind dtk) { }
    }
}
