namespace Xunit.Combinatorial.Tests
{
    using System;
    using Xunit.Combinatorial.Tests.Utils;

    public sealed class CombinatorialValuesTests
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

        [Fact]
        public void Empty()
        {
            var data = Data.Generate<int>(new CombinatorialDataAttribute(), Empty);
            Assert.Empty(data);
        }

        private static void Empty([CombinatorialValues()] int i) { }

        [Fact]
        public void Int_35()
        {
            var data = Data.Generate<int>(new CombinatorialDataAttribute(), Int_35);
            AssertSets.Equal(new []
            {
                new object[] { 3 },
                new object[] { 5 }
            }, data);
        }

        private static void Int_35([CombinatorialValues(3, 5)] int p1) { }

        [Fact]
        public void String_AB()
        {
            var data = Data.Generate<string>(new CombinatorialDataAttribute(), String_AB);
            AssertSets.Equal(new []
            {
                new object[] { "A" }, 
                new object[] { "B" }
            }, data);
        }

        private static void String_AB([CombinatorialValues("A", "B")] string s) { }
    }
}
