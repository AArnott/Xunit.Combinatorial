namespace Xunit.Combinatorial.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class CombinatorialMemberDataAttributeTests
    {
        public void StubIntMethod(int p1)
        {
        }

        public void StubGuidMethod(Guid p1)
        {
        }

        [Fact]
        public void EnumerableOfIntReturnsValues()
        {
            var attribute = new CombinatorialMemberDataAttribute(nameof(GetValuesAsEnumerableOfInt));
            var testMethod = this.GetType().GetMethod(nameof(StubIntMethod));
            var parameter = testMethod.GetParameters()[0];
            var values = attribute.GetValues(parameter);
            Assert.Equal(new object[] { 1, 2, 3, 4 }, values);
        }

        [Fact]
        public void EnumerableOfArrayThrows()
        {
            var attribute = new CombinatorialMemberDataAttribute(nameof(GetValuesAsEnumerableOfIntArray));
            var testMethod = this.GetType().GetMethod(nameof(StubIntMethod));
            var parameter = testMethod.GetParameters()[0];

            var exception = Assert.Throws<ArgumentException>(() => attribute.GetValues(parameter));
            Assert.Equal("Member GetValuesAsEnumerableOfIntArray on Xunit.Combinatorial.Tests.CombinatorialMemberDataAttributeTests returned an IEnumerable<object[]>, which is not supported", exception.Message);
        }

        [Fact]
        public void EnumerableOfGuidReturnsValue()
        {
            var attribute = new CombinatorialMemberDataAttribute(nameof(GetValuesAsEnumerableOfGuid));
            var testMethod = this.GetType().GetMethod(nameof(StubGuidMethod));
            var parameter = testMethod.GetParameters()[0];
            var values = attribute.GetValues(parameter);

            Assert.Contains(values, obj => (Guid)obj != Guid.Empty);
        }

        [Fact]
        public void IncompatibleMemberDataTypeThrows()
        {
            var attribute = new CombinatorialMemberDataAttribute(nameof(GetValuesAsEnumerableOfGuid));
            var testMethod = this.GetType().GetMethod(nameof(StubIntMethod));
            var parameter = testMethod.GetParameters()[0];

            var exception = Assert.Throws<ArgumentException>(() => attribute.GetValues(parameter));
            Assert.Equal("Parameter type System.Int32 is not compatible with returned member type System.Guid", exception.Message);
        }

        public static IEnumerable<int> GetValuesAsEnumerableOfInt()
        {
            yield return 1;
            yield return 2;
            yield return 3;
            yield return 4;
        }

        public static IEnumerable<int[]> GetValuesAsEnumerableOfIntArray()
        {
            yield return new[] { 1 };
            yield return new[] { 2 };
            yield return new[] { 3 };
            yield return new[] { 4 };
            yield return new[] { 5 };
        }

        public static IEnumerable<Guid> GetValuesAsEnumerableOfGuid()
        {
            yield return Guid.NewGuid();
            yield return Guid.NewGuid();
            yield return Guid.NewGuid();
            yield return Guid.NewGuid();
            yield return Guid.NewGuid();
        }
    }
}