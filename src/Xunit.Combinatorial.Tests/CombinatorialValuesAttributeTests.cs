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
        public void Ctor_ValidatesArgs()
        {
            Assert.Throws<ArgumentNullException>(() => new CombinatorialValuesAttribute(null));
        }

        [Fact]
        public void Ctor_SetsProperty()
        {
            object[] values = new object[1];
            var attribute = new CombinatorialValuesAttribute(values);
            Assert.Same(values, attribute.Values);
        }
    }
}
