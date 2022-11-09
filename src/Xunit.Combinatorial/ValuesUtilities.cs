// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Xunit
{
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
        internal static IEnumerable<object?> GetValuesFor(ParameterInfo parameter)
        {
            Requires.NotNull(parameter, nameof(parameter));
            {
                CombinatorialValuesAttribute? attribute = parameter.GetCustomAttribute<CombinatorialValuesAttribute>();
                if (attribute is not null)
                {
                    return attribute.Values;
                }
            }

            {
                CombinatorialRangeAttribute? attribute = parameter.GetCustomAttribute<CombinatorialRangeAttribute>();
                if (attribute is not null)
                {
                    return attribute.Values;
                }
            }

            {
                CombinatorialRandomDataAttribute? attribute = parameter.GetCustomAttribute<CombinatorialRandomDataAttribute>();
                if (attribute is not null)
                {
                    return attribute.Values;
                }
            }

            {
                CombinatorialMemberDataAttribute? attribute = parameter.GetCustomAttribute<CombinatorialMemberDataAttribute>();
                if (attribute is not null)
                {
                    return attribute.GetValues(parameter);
                }
            }

            return GetValuesFor(parameter.ParameterType);
        }

        /// <summary>
        /// Gets a sequence of values that should be tested for the specified type.
        /// </summary>
        /// <param name="dataType">The type to get possible values for.</param>
        /// <returns>A sequence of values for the <paramref name="dataType"/>.</returns>
        internal static IEnumerable<object?> GetValuesFor(Type dataType)
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
            else if (IsNullable(dataType, out Type? innerDataType))
            {
                yield return null;
                foreach (object? value in GetValuesFor(innerDataType))
                {
                    yield return value;
                }
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Determines whether <paramref name="dataType"/> is <see cref="Nullable{T}"/>
        /// and extracts the inner type, if any.
        /// </summary>
        /// <param name="dataType">
        /// The type to test whether it is <see cref="Nullable{T}"/>.
        /// </param>
        /// <param name="innerDataType">
        /// When this method returns, contains the inner type of the Nullable, if the
        /// type is Nullable is found; otherwise, null.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the type is a Nullable type; otherwise <see langword="false"/>.
        /// </returns>
        private static bool IsNullable(Type dataType, [NotNullWhen(true)] out Type? innerDataType)
        {
            innerDataType = null;

            TypeInfo? ti = dataType.GetTypeInfo();

            if (!ti.IsGenericType)
            {
                return false;
            }

            if (ti.GetGenericTypeDefinition() != typeof(Nullable<>))
            {
                return false;
            }

            innerDataType = ti.GenericTypeArguments[0];
            return true;
        }
    }
}
