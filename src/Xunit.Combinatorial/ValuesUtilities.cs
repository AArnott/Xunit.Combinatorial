// Copyright (c) Andrew Arnott. All rights reserved. Licensed under the Ms-PL.

namespace Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Utility methods for generating values for test parameters.
    /// </summary>
    internal static class ValuesUtilities
    {
        private static Dictionary<Type, DefaultCombinatorialValues> TypeDefaultValues { get; } = new Dictionary<Type, DefaultCombinatorialValues>();

        /// <summary>
        /// Gets a sequence of values that should be tested for the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter to get possible values for.</param>
        /// <returns>A sequence of values for the parameter.</returns>
        internal static object[] GetValuesFor(ParameterInfo parameter)
        {
            Requires.NotNull(parameter, nameof(parameter));

            var valuesAttributes = parameter.GetCustomAttributes<ParameterValuesAttribute>()?.ToArray();
            if (valuesAttributes == null || valuesAttributes.Length == 0)
            {
                var configuration = GetDefaultConfiguration(parameter.Member.DeclaringType);
                var values = configuration.GenerateValues(parameter.ParameterType);
                return values as object[] ?? values.ToArray();
            }
            else if (valuesAttributes.Length == 1)
            {
                var values = valuesAttributes.Single().GetValues(parameter);
                return values as object[] ?? values.ToArray();
            }
            else
            {
                return valuesAttributes.SelectMany(va => va.GetValues(parameter)).ToArray();
            }
        }

        private static DefaultCombinatorialValues GetDefaultConfiguration(Type testClass)
        {
            lock (TypeDefaultValues)
            {
                DefaultCombinatorialValues values;
                if (TypeDefaultValues.TryGetValue(testClass, out values))
                {
                    return values;
                }
                else
                {
                    values = DiscoverConfiguration(testClass);
                    TypeDefaultValues[testClass] = values;
                    return values;
                }
            }
        }

        private static DefaultCombinatorialValues DiscoverConfiguration(Type type)
        {
            var configMember = type.GetTypeInfo().DeclaredMethods
                        .Where(m => m.GetCustomAttributes<CombinatorialDefaultsAttribute>()?.Any() == true)
                        .ToList();

            if (configMember.Count == 0)
            {
                return DefaultCombinatorialValues.Default;
            }
            else if (configMember.Count > 1)
            {
                throw new InvalidOperationException(string.Format(Strings.DefaultsAttributeSingleMethod, nameof(CombinatorialDefaultsAttribute)));
            }
            else if (configMember[0].ReturnType != typeof(void) || configMember[0].GetParameters().Length != 1 || configMember[0].GetParameters()[0].ParameterType != typeof(DefaultCombinatorialValues))
            {
                throw new InvalidOperationException(string.Format(Strings.DefaultsMethodSignature, nameof(CombinatorialDefaultsAttribute), nameof(DefaultCombinatorialValues)));
            }
            else if (!configMember[0].IsStatic)
            {
                throw new InvalidOperationException(string.Format(Strings.DefaultsMethodNonStatic, nameof(CombinatorialDefaultsAttribute)));
            }
            else
            {
                var dlgt = (Action<DefaultCombinatorialValues>)configMember[0].CreateDelegate(typeof(Action<DefaultCombinatorialValues>));
                var values = new DefaultCombinatorialValues();
                dlgt(values);
                return values;
            }
        }
    }
}
