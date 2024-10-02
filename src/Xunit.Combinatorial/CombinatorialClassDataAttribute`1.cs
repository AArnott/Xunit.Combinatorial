// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

using System.Collections;

namespace Xunit;

#if NETSTANDARD2_0_OR_GREATER
/// <inheritdoc />
/// <typeparam name="TValueSource">The type of the class that provides the values for a combinatorial test.</typeparam>
/// <remarks><typeparamref name="TValueSource" /> must implement <see cref="IEnumerable" />.</remarks>
public class CombinatorialClassDataAttribute<TValueSource> : CombinatorialClassDataAttribute
    where TValueSource : IEnumerable<object?[]>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CombinatorialClassDataAttribute{TValueSource}" /> class.
    /// </summary>
    /// <param name="arguments">The arguments to pass to the constructor of <typeparamref name="TValueSource" />.</param>
    public CombinatorialClassDataAttribute(params object[]? arguments)
        : base(typeof(TValueSource), arguments)
    {
    }
}
#endif
