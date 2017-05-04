// Copyright (c) parts Andrew Arnott, parts Matthew Wilkinson. All rights reserved. Licensed under the Ms-PL.

namespace Xunit.Combinatorial.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit.Combinatorial.Tests.Utils;

    public sealed class MemberTests
    {
        private static IEnumerable<int> Ints() => new[] { 1, 2, 3, 4, 5 };
        private static IEnumerable<int> IntsProp => new[] { 1, 2, 3, 4, 5 };
        private static IEnumerable<int> Ints(int i) => Ints().Take(i);

        [Fact]
        public void GetsValuesFromBasicMethod()
        {
            var data = Data.Generate<int>(new CombinatorialDataAttribute(), GetsValuesFromBasicMethod);
            var expected = new[] { 1, 2, 3, 4, 5 }
                .Select(i => new object[] { i })
                .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void GetsValuesFromBasicMethod([Member(nameof(Ints))] int i) { }

        [Fact]
        public void GetsValuesFromBasicProperty()
        {
            var data = Data.Generate<int>(new CombinatorialDataAttribute(), GetsValuesFromBasicProperty);
            var expected = new[] { 1, 2, 3, 4, 5 }
                .Select(i => new object[] { i })
                .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void GetsValuesFromBasicProperty([Member(nameof(IntsProp))] int i) { }

        [Fact]
        public void GetsValuesFromBasicMethodOnOtherClass()
        {
            var data = Data.Generate<int>(new CombinatorialDataAttribute(), GetsValuesFromBasicMethodOnOtherClass);
            var expected = new[] { 1, 2, 3, 4, 5 }
                .Select(i => new object[] { i })
                .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void GetsValuesFromBasicMethodOnOtherClass([Member(typeof(GetsValuesFromBasic), nameof(Ints))] int i) { }

        [Fact]
        public void GetsValuesFromBasicPropertyOnOtherClass()
        {
            var data = Data.Generate<int>(new CombinatorialDataAttribute(), GetsValuesFromBasicPropertyOnOtherClass);
            var expected = new[] { 1, 2, 3, 4, 5 }
                .Select(i => new object[] { i })
                .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void GetsValuesFromBasicPropertyOnOtherClass([Member(typeof(GetsValuesFromBasic), nameof(IntsProp))] int i) { }

        private static class GetsValuesFromBasic
        {
            private static IEnumerable<int> Ints() => new[] { 1, 2, 3, 4, 5 };
            private static IEnumerable<int> IntsProp => new[] { 1, 2, 3, 4, 5 };
        }

        [Fact]
        public void RejectsMethodWithWrongReturnType()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                Data.Generate<string>(new CombinatorialDataAttribute(), RejectsMethodWithWrongReturnType);
            });
        }

        private static void RejectsMethodWithWrongReturnType([Member(nameof(Ints))] string s) { }

        [Fact]
        public void RejectsPropertyWithWrongReturnType()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                Data.Generate<string>(new CombinatorialDataAttribute(), RejectsPropertyWithWrongReturnType);
            });
        }

        private static void RejectsPropertyWithWrongReturnType([Member(nameof(IntsProp))] string s) { }

        [Fact]
        public void GetsValuesFromMethodWithParams()
        {
            var data = Data.Generate<int>(new CombinatorialDataAttribute(), GetsValuesFromMethodWithParams);
            var expected = new[] { 1, 2, 3 }
                .Select(i => new object[] { i })
                .ToArray();

            AssertSets.Equal(expected, data);
        }

        private static void GetsValuesFromMethodWithParams([Member(nameof(Ints), 3)] int i) { }

        [Fact]
        public void RejectsWriteOnlyProperty()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                Data.Generate<int>(new CombinatorialDataAttribute(), RejectsWriteOnlyProperty);
            });
        }

        private static void RejectsWriteOnlyProperty([Member(nameof(WriteOnlyProp))] int i) { }
        private static IEnumerable<int> WriteOnlyProp { set { } }

        [Fact]
        public void RejectsInstanceMethod()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                Data.Generate<int>(new CombinatorialDataAttribute(), RejectsInstanceMethod);
            });
        }

        private static void RejectsInstanceMethod([Member(nameof(InstanceMethod))] int i) { }
        private IEnumerable<int> InstanceMethod() => new int[0];

        [Fact]
        public void RejectsInstanceProperty()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                Data.Generate<int>(new CombinatorialDataAttribute(), RejectsInstanceProperty);
            });
        }

        private static void RejectsInstanceProperty([Member(nameof(InstanceProp))] int i) { }
        private IEnumerable<int> InstanceProp => new int[0];

        [Fact]
        public void RejectsInstanceMethodOnOtherClass()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                Data.Generate<int>(new CombinatorialDataAttribute(), RejectsInstanceMethodOnOtherClass);
            });
        }

        private static void RejectsInstanceMethodOnOtherClass([Member(typeof(RejectsInstance), nameof(InstanceMethod))] int i) { }

        [Fact]
        public void RejectsInstancePropertyOnOtherClass()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                Data.Generate<int>(new CombinatorialDataAttribute(), RejectsInstancePropertyOnOtherClass);
            });
        }

        private static void RejectsInstancePropertyOnOtherClass([Member(typeof(RejectsInstance), nameof(InstanceProp))] int i) { }

        private class RejectsInstance
        {
            private IEnumerable<int> Ints() => new[] { 1, 2, 3, 4, 5 };
            private IEnumerable<int> IntsProp => new[] { 1, 2, 3, 4, 5 };
        }
    }
}
