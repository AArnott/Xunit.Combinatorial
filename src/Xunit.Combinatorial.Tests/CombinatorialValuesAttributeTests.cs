namespace Xunit.Combinatorial.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CombinatorialValuesAttributeTests
    {
        [Fact]
        public void Ctor_SetsProperty()
        {
            object[] values = new object[1];
            var attribute = new CombinatorialValuesAttribute(values);
            Assert.Same(values, attribute.Values);
        }

        [Fact]
        public void NullArg()
        {
            var attribute = new CombinatorialValuesAttribute(null);
            Assert.Equal(new object[] { null }, attribute.Values);
        }

        [Fact]
        public void NullArgInArray()
        {
            var attribute = new CombinatorialValuesAttribute(new object[] { null });
            Assert.Equal(new object[] { null }, attribute.Values);
        }
    }
}
