// Copyright (c) Andrew Arnott. All rights reserved. Licensed under the Ms-PL.

namespace Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Validation;

    /// <summary>
    /// Utility methods for generating values for test parameters.
    /// </summary>
    internal static class ValuesUtilities
    {
        /// <summary>
        /// Gets a sequence of values that should be tested for the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter to get possible values for.</param>
        /// <returns>A sequence of values for the parameter.</returns>
        internal static IEnumerable<object> GetValuesFor(ParameterInfo parameter)
        {
            Requires.NotNull(parameter, nameof(parameter));

            var valuesAttribute = parameter.GetCustomAttribute<CombinatorialValuesAttribute>();
            if (valuesAttribute != null)
            {
                return valuesAttribute.Values;
            }

            return GetValuesFor(parameter.ParameterType);
        }

        /// <summary>
        /// Gets a sequence of values that should be tested for the specified type.
        /// </summary>
        /// <param name="dataType">The type to get possible values for.</param>
        /// <returns>A sequence of values for the <paramref name="dataType"/>.</returns>
        internal static IEnumerable<object> GetValuesFor(Type dataType)
        {
            Requires.NotNull(dataType, nameof(dataType));

            if (dataType == typeof(bool))
            {
                yield return true;
                yield return false;
            }
            else if (dataType == typeof(int))
            {
                yield return 0;
                yield return 1;
            }
            else if (dataType.GetTypeInfo().IsEnum)
            {
                foreach (string name in Enum.GetNames(dataType))
                {
                    yield return Enum.Parse(dataType, name);
                }
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}
