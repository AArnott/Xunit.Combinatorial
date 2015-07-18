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

        private static void Suppose_NoArguments() { }

        private static void Suppose_Bool(bool p1) { }

        private static void Suppose_BoolBool(bool p1, bool p2) { }

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
