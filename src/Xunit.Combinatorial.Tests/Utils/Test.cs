namespace Xunit.Combinatorial.Tests.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal static class Test
    {
        private static MethodInfo SetsEqualWithoutComparerMethod { get; } = typeof(Test).GetTypeInfo().DeclaredMethods
            .Single(m => m.IsGenericMethod && m.Name == nameof(SetsEqual) && m.GetParameters().Length == 2);

        private static MethodInfo SetsEqualWithComparerMethod { get; } = typeof(Test).GetTypeInfo().DeclaredMethods
            .Single(m => m.IsGenericMethod && m.Name == nameof(SetsEqual) && m.GetParameters().Length == 3);

        public static bool SetsEqual<T>(T[] expected, T[] actual)
        {
            if (typeof(T).IsArray)
            {
                var delegateType = typeof(Func<,,>).MakeGenericType(typeof(T), typeof(T), typeof(bool));
                var testDelegate = SetsEqualWithoutComparerMethod.MakeGenericMethod(typeof(T).GetElementType()).CreateDelegate(delegateType);
                return (bool)SetsEqualWithComparerMethod.MakeGenericMethod(typeof(T)).Invoke(null, new object[] { expected, actual, testDelegate });
            }
            else
            {
                return SetsEqual(expected, actual, EqualityComparer<T>.Default.Equals);
            }
        } 

        public static bool SetsEqual<T>(T[] expected, T[] actual, Func<T, T, bool> comparerFunc)
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

                    if (comparerFunc(expected[i], actual[j]))
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
    }
}
