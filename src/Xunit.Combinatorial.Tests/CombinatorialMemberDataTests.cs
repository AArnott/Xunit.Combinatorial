namespace Xunit.Combinatorial.Tests
{
    using Internal;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CombinatorialMemberDataTests
    {
        public static string[] ArrayProperty => new[] { "A", "b", "C" };
        public void M_ArrayProperty([CombinatorialMemberData("ArrayProperty")]string s) { }

        public static string[] ArrayField = new[] { "x", "Y", "z" };
        public void M_ArrayField([CombinatorialMemberData("ArrayField")]string s) { }


        [Fact]
        public void Property_Array()
        {
            AssertData(nameof(M_ArrayProperty), ArrayProperty);
        }

        [Fact]
        public void Field_Array()
        {
            AssertData(nameof(M_ArrayField), ArrayField);
        }

        private void AssertData(string methodName, IEnumerable<object> expectedData)
        {
            var actual = ValuesUtilities.GetValuesFor(this.GetType().GetMethod(methodName).GetParameters()[0]);
            var expected = new HashSet<object>(expectedData);
            Assert.True(expected.SetEquals(actual));
        }
    }
}
