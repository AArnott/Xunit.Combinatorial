// Copyright (c) parts Andrew Arnott, parts Matthew Wilkinson. All rights reserved. Licensed under the Ms-PL.

namespace Xunit.Combinatorial.Tests
{
    using Microsoft.FSharp.Core;
    using FSharpTestTypes;
    using Xunit.Combinatorial.Tests.Utils;
    using Xunit.Sdk;

    public sealed partial class DefaultCombinatorialValuesTests
    {
        [Theory, MemberData(nameof(DataAttributes))]
        public void FSharpEnumCases(DataAttribute dataAttr)
        {
            var data = Data.Generate<EnumCases>(dataAttr, FSharpEnumCases);
            AssertSingleValuesProvided(new object[] { EnumCases.A, EnumCases.B, EnumCases.C }, data);
        }

        private static void FSharpEnumCases(EnumCases ec) { }

        [Theory, MemberData(nameof(DataAttributes))]
        public void FSharpTypedCases(DataAttribute dataAttr)
        {
            var data = Data.Generate<TypeCases>(dataAttr, FSharpTypedCases);
            AssertSingleValuesProvided(new object[]
            {
                TypeCases.NewI(0),
                TypeCases.NewI(1),
                TypeCases.NewB(false),
                TypeCases.NewB(true)
            }, data);
        }

        private static void FSharpTypedCases(TypeCases tc) { }

        [Theory, MemberData(nameof(DataAttributes))]
        public void FSharpMixedCases(DataAttribute dataAttr)
        {
            var data = Data.Generate<MixedCases>(dataAttr, FSharpMixedCases);
            AssertSingleValuesProvided(new object[]
            {
                MixedCases.A,
                MixedCases.B,
                MixedCases.C,
                MixedCases.NewI(0),
                MixedCases.NewI(1),
                MixedCases.NewT(0, 0),
                MixedCases.NewT(0, 1),
                MixedCases.NewT(1, 0),
                MixedCases.NewT(1, 1),
                MixedCases.NewEC(EnumCases.A),
                MixedCases.NewEC(EnumCases.B),
                MixedCases.NewEC(EnumCases.C),
                MixedCases.NewTC(TypeCases.NewI(0)),
                MixedCases.NewTC(TypeCases.NewI(1)),
                MixedCases.NewTC(TypeCases.NewB(false)),
                MixedCases.NewTC(TypeCases.NewB(true)),
                MixedCases.NewOI(null),
                MixedCases.NewOI(FSharpOption<int>.Some(0)),
                MixedCases.NewOI(FSharpOption<int>.Some(1)),
                MixedCases.NewOTC(null),
                MixedCases.NewOTC(FSharpOption<TypeCases>.Some(TypeCases.NewI(0))),
                MixedCases.NewOTC(FSharpOption<TypeCases>.Some(TypeCases.NewI(1))),
                MixedCases.NewOTC(FSharpOption<TypeCases>.Some(TypeCases.NewB(false))),
                MixedCases.NewOTC(FSharpOption<TypeCases>.Some(TypeCases.NewB(true)))
            }, data);
        }

        private static void FSharpMixedCases(MixedCases mc) { }
    }
}
