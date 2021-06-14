using System;
using System.Collections.Generic;
using System.Linq;

namespace Xunit.Combinatorial.Tests
{
    public class CombinatorialMemberDataSampleUses
    {
        private static readonly Random Random = new Random();

        public static readonly IEnumerable<int> IntFieldValues = Enumerable.Range(0, 5).Select(_ => Random.Next());
        public static readonly IEnumerable<Guid> GuidFieldValues = Enumerable.Range(0, 5).Select(_ => Guid.NewGuid());

        public static IEnumerable<int> IntPropertyValues => GetIntMethodValues();

        public static IEnumerable<Guid> GuidPropertyValues => GetGuidMethodValues();

        [Theory, CombinatorialData]
        public void CombinatorialMemberDataFromParameterizedMethods(
            [CombinatorialMemberData(nameof(GetIntRange), 0, 5)] int p1,
            [CombinatorialMemberData(nameof(GetGuidRange), 5)] Guid p2)
        {
            Assert.True(true);
        }

        [Theory, CombinatorialData]
        public void CombinatorialMemberDataFromProperties(
            [CombinatorialMemberData(nameof(GuidPropertyValues))] Guid p1,
            [CombinatorialMemberData(nameof(IntPropertyValues))] int p2)
        {
            Assert.True(true);
        }

        [Theory, CombinatorialData]
        public void CombinatorialMemberDataFromMethods(
            [CombinatorialMemberData(nameof(GetGuidMethodValues))] Guid p1,
            [CombinatorialMemberData(nameof(GetIntMethodValues))] int p2)
        {
            Assert.True(true);
        }

        [Theory, CombinatorialData]
        public void CombinatorialMemberDataFromFields(
            [CombinatorialMemberData(nameof(GuidFieldValues))] Guid p1,
            [CombinatorialMemberData(nameof(IntFieldValues))] int p2)
        {
            Assert.True(true);
        }

        public static IEnumerable<int> GetIntMethodValues()
        {
            for (var i = 0; i < 5; i++)
            {
                yield return Random.Next();
            }
        }

        public static IEnumerable<Guid> GetGuidMethodValues()
        {
            for (var i = 0; i < 5; i++)
            {
                yield return Guid.NewGuid();
            }
        }

        public static IEnumerable<int> GetIntRange(int start, int count)
        {
            return Enumerable.Range(start, count);
        }

        public static IEnumerable<Guid> GetGuidRange(int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return Guid.NewGuid();
            }
        }
    }
}