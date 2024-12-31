// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

using System.Collections;

namespace Xunit;

/// <summary>
/// Helper class for retrieving data from theory data instances and types.
/// </summary>
internal static class TheoryDataHelper
{
    /// <summary>
    /// Tries to get the data from a theory data instance, if it is of such a type.
    /// </summary>
    /// <param name="source">The potential TheoryData source instance.</param>
    /// <param name="theoryDataValues">The data as an object array.</param>
    /// <returns><see langword="true"/> if the instance was TheoryData, otherwise <see langword="false"/>.</returns>
    public static bool TryGetTheoryDataValues(IEnumerable source, out object?[]? theoryDataValues)
    {
        if (IsTheoryDataType(source.GetType()))
        {
            theoryDataValues = source.Cast<ITheoryDataRow>().SelectMany(row => row.GetData()).ToArray();
            return true;
        }

        theoryDataValues = [];
        return false;
    }

    /// <summary>
    /// Checks if the source type is an implementation of an IEnumerable of <see cref="ITheoryDataRow"/>.
    /// </summary>
    /// <param name="sourceType">The type to check.</param>
    /// <returns><see langword="true"/> if the type is an implementation of IEnumerable of <see cref="ITheoryDataRow"/>.</returns>
    public static bool IsTheoryDataType(Type sourceType)
    {
        return sourceType.GetInterfaces()
            .Any(interfaceType => interfaceType.IsGenericType &&
                                  interfaceType.GetGenericTypeDefinition() == typeof(IEnumerable<>) &&
                                  IsTheoryDataRowType(interfaceType.GetGenericArguments()[0]));
    }

    /// <summary>
    /// Checks if the source type is a <see cref="ITheoryDataRow"/>.
    /// </summary>
    /// <param name="sourceType">The type to check.</param>
    /// <returns><see langword="true"/> if the type is a <see cref="ITheoryDataRow"/>, otherwise <see langword="false"/>.</returns>
    public static bool IsTheoryDataRowType(Type? sourceType)
    {
        return sourceType is not null && typeof(ITheoryDataRow).IsAssignableFrom(sourceType);
    }
}
