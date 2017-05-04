namespace Xunit
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Represents the set of generators that are used for a parameter when no data source is explicitly provided
    /// </summary>
    public sealed class DefaultCombinatorialValues
    {
        private Dictionary<Type, Func<IEnumerable<object>>> ValueGenerators { get; } = new Dictionary<Type, Func<IEnumerable<object>>>();

        /// <summary>
        /// Sets up the default set of default values for common types
        /// </summary>
        internal DefaultCombinatorialValues()
        {
            RegisterGenerator<bool>(true, false);
            RegisterGenerator<byte>(0, 1);
            RegisterGenerator<sbyte>(0, 1);
            RegisterGenerator<short>(0, 1);
            RegisterGenerator<ushort>(0, 1);
            RegisterGenerator<int>(0, 1);
            RegisterGenerator<uint>(0, 1);
            RegisterGenerator<long>(0, 1);
            RegisterGenerator<ulong>(0, 1);
            RegisterGenerator<float>(0.0f, 1.0f);
            RegisterGenerator<double>(0.0d, 1.0d);
            RegisterGenerator<decimal>(0m, 1m);
            RegisterGenerator<DateTime>(DateTime.MinValue, new DateTime(1970, 01, 01, 00, 00, 00));
            RegisterGenerator<TimeSpan>(TimeSpan.Zero, TimeSpan.FromSeconds(1));
        }

        /// <summary>
        /// Register a new generator for the type <typeparamref name="T"/> in the form of a delegate to be called at generation time
        /// </summary>
        /// <typeparam name="T">The type to provide a value generator for</typeparam>
        /// <param name="generator">The generator to be used at value generation time</param>
        public void RegisterGenerator<T>(Func<IEnumerable<T>> generator)
        {
            if (generator == null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            lock (ValueGenerators)
            {
                ValueGenerators[typeof(T)] = () => generator()?.Cast<object>();
            }
        }

        /// <summary>
        /// Register a new generator for the type <typeparamref name="T"/> in the form of a fixed static array with all values provided up-front
        /// </summary>
        /// <typeparam name="T">The type to provide a value generator for</typeparam>
        /// <param name="staticValues">The array of values to be used at value generation time</param>
        /// <remarks>A full copy of the array is made to prevent any changes to the provided array interfering with generation</remarks>
        public void RegisterGenerator<T>(params T[] staticValues)
        {
            var copy = new object[staticValues.Length];
            for (int i = 0; i < staticValues.Length; i++)
            {
                copy[i] = (object)staticValues[i];
            }

            lock (ValueGenerators)
            {
                ValueGenerators[typeof(T)] = () => copy;
            }
        }

        /// <summary>
        /// Generates the set of default values for the given type, or throws an exception if no generator is known for that type
        /// </summary>
        /// <param name="type">The type of values to generate</param>
        /// <remarks>For certain special types, such as enums, a generator is automatically generated the first time that time is requested</remarks>
        /// <returns>The default values to be used for the given type</returns>
        internal IEnumerable<object> GenerateValues(Type type)
        {
            Func<IEnumerable<object>> generator;
            lock (ValueGenerators)
            {
                if (!ValueGenerators.TryGetValue(type, out generator))
                {
                    var typeInfo = type.GetTypeInfo();
                    if (typeInfo.IsEnum)
                    {
                        ValueGenerators[type] = generator = GetEnumValuesGenerator(type);
                    }
                    else if (typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        var enumerable = GetNullableValuesGenerator(typeInfo.GenericTypeArguments.Single());
                        ValueGenerators[type] = generator = () => enumerable;
                    }
                    else if (FSharpValueProvider.IsUnionType(type))
                    {
                        ValueGenerators[type] = generator = () => FSharpValueProvider.GetUnionCaseValues(typeInfo, t =>
                        {
                            var values = GenerateValues(t);
                            var valuesArray = values as object[];
                            return valuesArray ?? values.ToArray();
                        });
                    }
                }
            }

            if (generator == null)
            {
                throw new NotSupportedException(string.Format(Strings.CannotGenerateValues, type.FullName));
            }

            return generator();
        }

        /// <summary>
        /// Gets a single instance containing the default default values - for use with test classes that do not explicitly provide their own overrides
        /// </summary>
        internal static DefaultCombinatorialValues Default { get; } = new DefaultCombinatorialValues();

        private static Func<IEnumerable<object>> GetEnumValuesGenerator(Type enumType)
        {
            if (enumType.GetTypeInfo().GetCustomAttribute<FlagsAttribute>() != null)
            {
                throw new NotSupportedException(Strings.FlagEnumAutoGen);
            }

            var values = (IList)Enum.GetValues(enumType);
            var copy = new object[values.Count];
            for (int i = 0; i < values.Count; i++)
            {
                copy[i] = (object)values[i];
            }

            return () => copy;
        }

        private IEnumerable<object> GetNullableValuesGenerator(Type innerType)
        {
            yield return null;
            foreach (var value in GenerateValues(innerType))
            {
                yield return value;
            }
        }
    }
}
