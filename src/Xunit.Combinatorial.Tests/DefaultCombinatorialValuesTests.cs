namespace Xunit.Combinatorial.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit.Combinatorial.Tests.Utils;
    using Xunit.Sdk;

    public sealed class DefaultCombinatorialValuesTests
    {
        public static IEnumerable<object[]> DataAttributes()
        {
            yield return new object[] { new CombinatorialDataAttribute() };
            yield return new object[] { new PairwiseDataAttribute() };
        }

        [Theory, MemberData(nameof(DataAttributes))]
        public void Bool(DataAttribute dataAttr)
        {
            var data = Data.Generate<bool>(dataAttr, Bool);
            AssertSingleValuesProvided(new object[] { true, false }, data);
        }

        private static void Bool(bool b) { }

        [Theory, MemberData(nameof(DataAttributes))]
        public void Byte(DataAttribute dataAttr)
        {
            var data = Data.Generate<byte>(dataAttr, Byte);

            AssertSingleValuesProvided(new object[] { (byte)0, (byte)1 }, data);
        }

        private static void Byte(byte b) { }

        [Theory, MemberData(nameof(DataAttributes))]
        public void SByte(DataAttribute dataAttr)
        {
            var data = Data.Generate<sbyte>(dataAttr, SByte);
            AssertSingleValuesProvided(new object[] { (sbyte)0, (sbyte)1 }, data);
        }

        private static void SByte(sbyte sb) { }

        [Theory, MemberData(nameof(DataAttributes))]
        public void Short(DataAttribute dataAttr)
        {
            var data = Data.Generate<short>(dataAttr, Short);
            AssertSingleValuesProvided(new object[] { (short)0, (short)1 }, data);
        }

        private static void Short(short s) { }

        [Theory, MemberData(nameof(DataAttributes))]
        public void UShort(DataAttribute dataAttr)
        {
            var data = Data.Generate<ushort>(dataAttr, UShort);
            AssertSingleValuesProvided(new object[] { (ushort)0, (ushort)1 }, data);
        }

        private static void UShort(ushort us) { }

        [Theory, MemberData(nameof(DataAttributes))]
        public void Int(DataAttribute dataAttr)
        {
            var data = Data.Generate<int>(dataAttr, Int);
            AssertSingleValuesProvided(new object[] { 0, 1 }, data);
        }

        private static void Int(int i) { }

        [Theory, MemberData(nameof(DataAttributes))]
        public void UInt(DataAttribute dataAttr)
        {
            var data = Data.Generate<uint>(dataAttr, UInt);
            AssertSingleValuesProvided(new object[] { 0U, 1U }, data);
        }

        private static void UInt(uint ui) { }

        [Theory, MemberData(nameof(DataAttributes))]
        public void Long(DataAttribute dataAttr)
        {
            var data = Data.Generate<long>(dataAttr, Long);
            AssertSingleValuesProvided(new object[] { 0L , 1L }, data);
        }

        private static void Long(long l) { }

        [Theory, MemberData(nameof(DataAttributes))]
        public void ULong(DataAttribute dataAttr)
        {
            var data = Data.Generate<ulong>(dataAttr, ULong);
            AssertSingleValuesProvided(new object[] { 0UL, 1UL }, data);
        }

        private static void ULong(ulong ul) { }


        [Theory, MemberData(nameof(DataAttributes))]
        public void Float(DataAttribute dataAttr)
        {
            var data = Data.Generate<float>(dataAttr, Float);
            AssertSingleValuesProvided(new object[] { 0F, 1F }, data);
        }

        private static void Float(float f) { }

        [Theory, MemberData(nameof(DataAttributes))]
        public void Double(DataAttribute dataAttr)
        {
            var data = Data.Generate<double>(dataAttr, Double);
            AssertSingleValuesProvided(new object[] { 0D, 1D }, data);
        }

        private static void Double(double d) { }

        [Theory, MemberData(nameof(DataAttributes))]
        public void Decimal(DataAttribute dataAttr)
        {
            var data = Data.Generate<decimal>(dataAttr, Decimal);
            AssertSingleValuesProvided(new object[] { 0M, 1M }, data);
        }

        private static void Decimal(decimal d) { }

        [Theory, MemberData(nameof(DataAttributes))]
        public void DateTime(DataAttribute dataAttr)
        {
            var data = Data.Generate<DateTime>(dataAttr, DateTime);
            AssertSingleValuesProvided(new object[] { System.DateTime.MinValue, new DateTime(1970, 01, 01, 00, 00, 00) }, data);
        }

        private static void DateTime(DateTime dt) { }

        [Theory, MemberData(nameof(DataAttributes))]
        public void TimeSpan(DataAttribute dataAttr)
        {
            var data = Data.Generate<TimeSpan>(dataAttr, TimeSpan);
            AssertSingleValuesProvided(new object[] { System.TimeSpan.Zero, System.TimeSpan.FromSeconds(1) }, data);
        }

        private static void TimeSpan(TimeSpan ts) { }

        [Theory, MemberData(nameof(DataAttributes))]
        public void DateTimeKind(DataAttribute dataAttr)
        {
            var data = Data.Generate<DateTimeKind>(dataAttr, DateTimeKind);
            AssertSingleValuesProvided(new object[] { System.DateTimeKind.Unspecified, System.DateTimeKind.Utc, System.DateTimeKind.Local }, data);
        }

        private static void DateTimeKind(DateTimeKind dtk) { }

        [Theory, MemberData(nameof(DataAttributes))]
        public void RejectsFlagsEnum(DataAttribute dataAttr)
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                Data.Generate<FlagsEnum>(dataAttr, RejectsFlagsEnum);
            });
        }

        private static void RejectsFlagsEnum(FlagsEnum fe) { }

        [Flags]
        public enum FlagsEnum { }

        private static void AssertSingleValuesProvided(object[] defaults, object[][] actualTestCases)
        {
            AssertSets.Equal(defaults.Select(d => new [] { d }).ToArray(), actualTestCases);
        }
    }
}
