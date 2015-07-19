namespace Xunit.Combinatorial.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text;
    using Validation;

    public class CombinatorialDataAttributeTests
    {
        [Fact]
        public void GetData_NoArguments()
        {
            var actual = GetData();
            Assert.Empty(actual);
        }

        [Fact]
        public void GetData_Bool()
        {
            AssertData(new object[][]
            {
                new object[] { true },
                new object[] { false },
            });
        }

        [Fact]
        public void GetData_BoolBool()
        {
            AssertData(new object[][]
            {
                new object[] { true, true },
                new object[] { true, false },
                new object[] { false, true },
                new object[] { false, false }
            });
        }

        [Fact]
        public void GetData_Int()
        {
            AssertData(new object[][]
            {
                new object[] { 0 },
                new object[] { 1 },
            });
        }

        [Fact]
        public void GetData_Int_35()
        {
            AssertData(new object[][]
            {
                new object[] { 3 },
                new object[] { 5 },
            });
        }

        [Fact]
        public void GetData_string_int_bool_Values()
        {
            AssertData(new object[][]
            {
                new object[] { "a", 2, true },
                new object[] { "a", 2, false },
                new object[] { "a", 4, true },
                new object[] { "a", 4, false },
                new object[] { "a", 6, true },
                new object[] { "a", 6, false },
                new object[] { "b", 2, true },
                new object[] { "b", 2, false },
                new object[] { "b", 4, true },
                new object[] { "b", 4, false },
                new object[] { "b", 6, true },
                new object[] { "b", 6, false },
            });
        }

        [Fact]
        public void GetData_DateTimeKind()
        {
            AssertData(new object[][]
            {
                new object[] { DateTimeKind.Unspecified },
                new object[] { DateTimeKind.Utc },
                new object[] { DateTimeKind.Local },
            });
        }

        [Fact]
        public void GetData_UnsupportedType()
        {
            Assert.Throws<NotSupportedException>(() => GetData());
        }

        private static void Suppose_NoArguments() { }
        private static void Suppose_Bool(bool p1) { }
        private static void Suppose_BoolBool(bool p1, bool p2) { }
        private static void Suppose_Int(int p1) { }
        private static void Suppose_Int_35([CombinatorialValues(3, 5)] int p1) { }
        private static void Suppose_string_int_bool_Values([CombinatorialValues("a", "b")]string p1, [CombinatorialValues(2, 4, 6)]int p2, bool p3) { }
        private static void Suppose_DateTimeKind(DateTimeKind p1) { }
        private static void Suppose_UnsupportedType(System.AggregateException p1) { }

        private static void AssertData(IEnumerable<object[]> expected, [CallerMemberName] string testMethodName = null)
        {
            IEnumerable<object[]> actual = GetData(testMethodName).ToArray();
            Assert.Equal(expected, actual);
        }

        private static IEnumerable<object[]> GetData([CallerMemberName] string testMethodName = null)
        {
            Requires.NotNullOrEmpty(testMethodName, nameof(testMethodName));

            string supposeMethodName = testMethodName.Replace("GetData_", "Suppose_");
            var methodInfo = typeof(CombinatorialDataAttributeTests).GetTypeInfo()
                .DeclaredMethods.First(m => m.Name == supposeMethodName);
            var attribute = new CombinatorialDataAttribute();
            return attribute.GetData(methodInfo);
        }
    }
}
