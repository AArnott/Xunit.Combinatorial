namespace Xunit.Combinatorial.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text;
    using Sdk;
    using Validation;
    public class CombinatorialDataAttributeTests
    {
        [Fact]
        public void GetData_NoArguments()
        {
            AssertData(new object[][]
            {
            });
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
        public void GetData_Byte()
        {
            AssertData(new object[][]
            {
                new object[] { (byte)0 },
                new object[] { (byte)1 },
            });
        }

        [Fact]
        public void GetData_SByte()
        {
            AssertData(new object[][]
            {
                new object[] { (sbyte)0 },
                new object[] { (sbyte)1 },
            });
        }

        [Fact]
        public void GetData_Short()
        {
            AssertData(new object[][]
            {
                new object[] { (short)0 },
                new object[] { (short)1 },
            });
        }

        [Fact]
        public void GetData_UShort()
        {
            AssertData(new object[][]
            {
                new object[] { (ushort)0 },
                new object[] { (ushort)1 },
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
        public void GetData_UInt()
        {
            AssertData(new object[][]
            {
                new object[] { (uint)0 },
                new object[] { (uint)1 },
            });
        }

        [Fact]
        public void GetData_Long()
        {
            AssertData(new object[][]
            {
                new object[] { 0l },
                new object[] { 1l },
            });
        }

        [Fact]
        public void GetData_ULong()
        {
            AssertData(new object[][]
            {
                new object[] { (ulong)0 },
                new object[] { (ulong)1 },
            });
        }

        [Fact]
        public void GetData_Float()
        {
            AssertData(new object[][]
            {
                new object[] { 0f },
                new object[] { 1f },
            });
        }

        [Fact]
        public void GetData_Double()
        {
            AssertData(new object[][]
            {
                new object[] { 0d },
                new object[] { 1d },
            });
        }

        [Fact]
        public void GetData_Decimal()
        {
            AssertData(new object[][]
            {
                new object[] { 0m },
                new object[] { 1m },
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
        public void GetData_DateTime()
        {
            AssertData(new object[][]
            {
                new object[] { DateTime.MinValue },
                new object[] { new DateTime(1970, 01, 01, 00, 00, 00) }
            });
        }

        [Fact]
        public void GetData_TimeSpan()
        {
            AssertData(new object[][]
            {
                new object[] { TimeSpan.Zero },
                new object[] { TimeSpan.FromSeconds(1) }
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
        public void GetData_RejectsFlagsEnum()
        {
            Assert.Throws<NotSupportedException>(() => GetData(new CombinatorialDataAttribute()));
            Assert.Throws<NotSupportedException>(() => GetData(new PairwiseDataAttribute()));
        }

        [Fact]
        public void GetData_AllFlags()
        {
            AssertData(new object[][]
            {
                new object[] { (AllFlagTestEnum) 0 },
                new object[] { AllFlagTestEnum.A },
                new object[] { AllFlagTestEnum.B },
                new object[] { AllFlagTestEnum.C },
                new object[] { AllFlagTestEnum.A | AllFlagTestEnum.B },
                new object[] { AllFlagTestEnum.A | AllFlagTestEnum.C },
                new object[] { AllFlagTestEnum.B | AllFlagTestEnum.C },
                new object[] { AllFlagTestEnum.A | AllFlagTestEnum.B | AllFlagTestEnum.C }
            });
        }

        [Flags]
        public enum AllFlagTestEnum
        {
            A = 1, B = 2, C = 4
        }

        [Fact]
        public void GetData_UnsupportedType()
        {
            Assert.Throws<NotSupportedException>(() => GetData(new CombinatorialDataAttribute()));
            Assert.Throws<NotSupportedException>(() => GetData(new PairwiseDataAttribute()));
        }

        private static void Suppose_NoArguments() { }
        private static void Suppose_Bool(bool p1) { }
        private static void Suppose_BoolBool(bool p1, bool p2) { }
        private static void Suppose_Byte(byte b) { }
        private static void Suppose_SByte(sbyte sb) { }
        private static void Suppose_Short(short s) { }
        private static void Suppose_UShort(ushort us) { }
        private static void Suppose_Int(int p1) { }
        private static void Suppose_UInt(uint ui) { }
        private static void Suppose_Long(long l) { }
        private static void Suppose_ULong(ulong ul) { }
        private static void Suppose_Float(float f) { }
        private static void Suppose_Double(double d) { }
        private static void Suppose_Decimal(decimal d) { }
        private static void Suppose_Int_35([CombinatorialValues(3, 5)] int p1) { }
        private static void Suppose_string_int_bool_Values([CombinatorialValues("a", "b")]string p1, [CombinatorialValues(2, 4, 6)]int p2, bool p3) { }
        private static void Suppose_DateTime(DateTime dt) { }
        private static void Suppose_TimeSpan(TimeSpan ts) { }
        private static void Suppose_DateTimeKind(DateTimeKind p1) { }
        private static void Suppose_RejectsFlagsEnum(BindingFlags bf) { }
        private static void Suppose_AllFlags([AllFlags] AllFlagTestEnum afte) { }
        private static void Suppose_UnsupportedType(System.AggregateException p1) { }

        private static void AssertData(object[][] expectedCombinatorial, [CallerMemberName] string testMethodName = null)
        {
            object[][] actualCombinatorial = GetData(new CombinatorialDataAttribute(), testMethodName).ToArray();
            object[][] actualPairwise = GetData(new PairwiseDataAttribute(), testMethodName).ToArray();

            // Verify that the combinatorial result is as expected.
            AssertSetofSetsEqual(expectedCombinatorial, actualCombinatorial);

            if (expectedCombinatorial.Any())
            {
                // Verify that the pairwise result covers every pair.
                HashSet<object>[] possibleValues = ExtractPossibleValues(expectedCombinatorial);

                for (int i = 0; i < possibleValues.Length - 1; i++)
                {
                    for (int j = i + 1; j < possibleValues.Length; j++)
                    {
                        foreach (object iValue in possibleValues[i])
                        {
                            foreach (object jValue in possibleValues[j])
                            {
                                Assert.True(actualPairwise.Any(
                                    testCase => 
                                        EqualityComparer<object>.Default.Equals(testCase[i], iValue) &&
                                        EqualityComparer<object>.Default.Equals(testCase[j], jValue)));
                            }
                        }
                    }
                }
            }
        }

        private static void AssertSetofSetsEqual(object[][] expected, object[][] actual)
        {
            Assert.Equal(expected.Length, actual.Length);
            var matchedItems = new HashSet<int>();
            for(int i = 0; i < expected.Length; i++)
            {
                bool matched = false;
                for (int j = 0; j < actual.Length; j++)
                {
                    if (matchedItems.Contains(j))
                    {
                        continue;
                    }

                    if (SetsEqual(expected[i], actual[j]))
                    {
                        matchedItems.Add(j);
                        matched = true;
                        break;
                    }
                }

                Assert.True(matched);
            }
        }

        private static bool SetsEqual(object[] expected, object[] actual)
        {
            if (expected.Length != actual.Length)
            {
                return false;
            }

            var matchedItems = new HashSet<int>();
            for (int i = 0; i < expected.Length; i++)
            {
                bool matched = false;
                for (int j = 0; j < actual.Length; j++)
                {
                    if (matchedItems.Contains(j))
                    {
                        continue;
                    }

                    if (i.Equals(j))
                    {
                        matchedItems.Add(j);
                        matched = true;
                        break;
                    }
                }

                if (!matched)
                {
                    return false;
                }
            }

            return true;
        }

        private static HashSet<object>[] ExtractPossibleValues(IEnumerable<object[]> combinatorialTestCases)
        {
            Requires.NotNull(combinatorialTestCases, nameof(combinatorialTestCases));

            HashSet<object>[] possibleValues = new HashSet<object>[combinatorialTestCases.First().Length];
            for (int i = 0; i < possibleValues.Length; i++)
            {
                possibleValues[i] = new HashSet<object>();
            }

            foreach (object[] combination in combinatorialTestCases)
            {
                for (int i = 0; i < combination.Length; i++)
                {
                    possibleValues[i].Add(combination[i]);
                }
            }

            return possibleValues;
        }

        private static IEnumerable<object[]> GetData(DataAttribute dataAttribute, [CallerMemberName] string testMethodName = null)
        {
            Requires.NotNull(dataAttribute, nameof(dataAttribute));
            Requires.NotNullOrEmpty(testMethodName, nameof(testMethodName));

            string supposeMethodName = testMethodName.Replace("GetData_", "Suppose_");
            var methodInfo = typeof(CombinatorialDataAttributeTests).GetTypeInfo()
                .DeclaredMethods.First(m => m.Name == supposeMethodName);
            return dataAttribute.GetData(methodInfo);
        }
    }
}
