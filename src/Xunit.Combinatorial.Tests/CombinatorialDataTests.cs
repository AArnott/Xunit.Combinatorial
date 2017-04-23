namespace Xunit.Combinatorial.Tests
{
    using System;
    using Xunit.Combinatorial.Tests.Utils;

    public class CombinatorialDataTests
    {
        [Fact]
        public void NoArguments()
        {
            var data = Data.Generate(new CombinatorialDataAttribute(), NoArguments);
            Assert.Empty(data);
        }
        
        [Fact]
        public void BoolBool()
        {
            var data = Data.Generate<bool, bool>(new CombinatorialDataAttribute(), BoolBool);
            AssertSets.Equal(new []
            {
                new object[] { true, true },
                new object[] { true, false },
                new object[] { false, true },
                new object[] { false, false }
            }, data, Test.SetsEqual);
        }

        private static void BoolBool(bool p1, bool p2) { }
        
        [Fact]
        public void string_int_bool()
        {
            var data = Data.Generate<string, int, bool>(new CombinatorialDataAttribute(), string_int_bool);
            AssertSets.Equal(new []
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
                new object[] { "b", 6, false }
            }, data);
        }

        private static void string_int_bool([CombinatorialValues("a", "b")]string p1, [CombinatorialValues(2, 4, 6)]int p2, bool p3) { }
        
        [Fact]
        public void RejectsUnsupportedType()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                Data.Generate<UnknownType>(new CombinatorialDataAttribute(), UnsupportedType);
            });
        }

        private static void UnsupportedType(UnknownType ut) { }

        private sealed class UnknownType { }
    }
}
