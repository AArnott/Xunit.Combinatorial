// Copyright (c) parts Andrew Arnott, parts Matthew Wilkinson. All rights reserved. Licensed under the Ms-PL.

namespace Xunit.Combinatorial.Tests
{
    using Xunit.Combinatorial.Tests.Utils;
    using Xunit.Sdk;

    public sealed partial class DefaultCombinatorialValuesTests
    {
        [Theory, MemberData(nameof(DataAttributes))]
        public void NullableBool(DataAttribute dataAttr)
        {
            var data = Data.Generate<bool?>(dataAttr, NullableBool);
            AssertSingleValuesProvided(new object[] { null, true, false }, data);
        }

        private static void NullableBool(bool? b) { }

        [Theory, MemberData(nameof(DataAttributes))]
        public void NullableByte(DataAttribute dataAttr)
        {
            var data = Data.Generate<byte?>(dataAttr, NullableByte);

            AssertSingleValuesProvided(new object[] { null, (byte)0, (byte)1 }, data);
        }

        private static void NullableByte(byte? b) { }

        [Theory, MemberData(nameof(DataAttributes))]
        public void NullableSByte(DataAttribute dataAttr)
        {
            var data = Data.Generate<sbyte?>(dataAttr, NullableSByte);
            AssertSingleValuesProvided(new object[] { null, (sbyte)0, (sbyte)1 }, data);
        }

        private static void NullableSByte(sbyte? sb) { }

        [Theory, MemberData(nameof(DataAttributes))]
        public void NullableShort(DataAttribute dataAttr)
        {
            var data = Data.Generate<short?>(dataAttr, NullableShort);
            AssertSingleValuesProvided(new object[] { null, (short)0, (short)1 }, data);
        }

        private static void NullableShort(short? s) { }

        [Theory, MemberData(nameof(DataAttributes))]
        public void NullableUShort(DataAttribute dataAttr)
        {
            var data = Data.Generate<ushort?>(dataAttr, NullableUShort);
            AssertSingleValuesProvided(new object[] { null, (ushort)0, (ushort)1 }, data);
        }

        private static void NullableUShort(ushort? us) { }

        [Theory, MemberData(nameof(DataAttributes))]
        public void NullableInt(DataAttribute dataAttr)
        {
            var data = Data.Generate<int?>(dataAttr, NullableInt);
            AssertSingleValuesProvided(new object[] { null, 0, 1 }, data);
        }

        private static void NullableInt(int? i) { }

        [Theory, MemberData(nameof(DataAttributes))]
        public void NullableUInt(DataAttribute dataAttr)
        {
            var data = Data.Generate<uint?>(dataAttr, NullableUInt);
            AssertSingleValuesProvided(new object[] { null, 0U, 1U }, data);
        }

        private static void NullableUInt(uint? ui) { }

        [Theory, MemberData(nameof(DataAttributes))]
        public void NullableLong(DataAttribute dataAttr)
        {
            var data = Data.Generate<long?>(dataAttr, NullableLong);
            AssertSingleValuesProvided(new object[] { null, 0L, 1L }, data);
        }

        private static void NullableLong(long? l) { }

        [Theory, MemberData(nameof(DataAttributes))]
        public void NullableULong(DataAttribute dataAttr)
        {
            var data = Data.Generate<ulong?>(dataAttr, NullableULong);
            AssertSingleValuesProvided(new object[] { null, 0UL, 1UL }, data);
        }

        private static void NullableULong(ulong? ul) { }


        [Theory, MemberData(nameof(DataAttributes))]
        public void NullableFloat(DataAttribute dataAttr)
        {
            var data = Data.Generate<float?>(dataAttr, NullableFloat);
            AssertSingleValuesProvided(new object[] { null, 0F, 1F }, data);
        }

        private static void NullableFloat(float? f) { }

        [Theory, MemberData(nameof(DataAttributes))]
        public void NullableDouble(DataAttribute dataAttr)
        {
            var data = Data.Generate<double?>(dataAttr, NullableDouble);
            AssertSingleValuesProvided(new object[] { null, 0D, 1D }, data);
        }

        private static void NullableDouble(double? d) { }

        [Theory, MemberData(nameof(DataAttributes))]
        public void NullableDecimal(DataAttribute dataAttr)
        {
            var data = Data.Generate<decimal?>(dataAttr, NullableDecimal);
            AssertSingleValuesProvided(new object[] { null, 0M, 1M }, data);
        }

        private static void NullableDecimal(decimal? d) { }

        [Theory, MemberData(nameof(DataAttributes))]
        public void NullableDateTime(DataAttribute dataAttr)
        {
            var data = Data.Generate<System.DateTime?>(dataAttr, NullableDateTime);
            AssertSingleValuesProvided(new object[] { null, System.DateTime.MinValue, new System.DateTime(1970, 01, 01, 00, 00, 00) }, data);
        }

        private static void NullableDateTime(System.DateTime? dt) { }

        [Theory, MemberData(nameof(DataAttributes))]
        public void NullableTimeSpan(DataAttribute dataAttr)
        {
            var data = Data.Generate<System.TimeSpan?>(dataAttr, NullableTimeSpan);
            AssertSingleValuesProvided(new object[] { null, System.TimeSpan.Zero, System.TimeSpan.FromSeconds(1) }, data);
        }

        private static void NullableTimeSpan(System.TimeSpan? ts) { }

        [Theory, MemberData(nameof(DataAttributes))]
        public void NullableDateTimeKind(DataAttribute dataAttr)
        {
            var data = Data.Generate<System.DateTimeKind?>(dataAttr, NullableDateTimeKind);
            AssertSingleValuesProvided(new object[] { null, System.DateTimeKind.Unspecified, System.DateTimeKind.Utc, System.DateTimeKind.Local }, data);
        }

        private static void NullableDateTimeKind(System.DateTimeKind? dtk) { }

    }
}
