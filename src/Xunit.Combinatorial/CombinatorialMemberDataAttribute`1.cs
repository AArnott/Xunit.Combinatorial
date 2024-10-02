// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

namespace Xunit;

#if NETSTANDARD2_0_OR_GREATER
/// <inheritdoc />
/// <typeparam name="T">The type of the class that provides the values for a combinatorial test.</typeparam>
public class CombinatorialMemberDataAttribute<T> : CombinatorialMemberDataAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CombinatorialMemberDataAttribute{T}" /> class.
    /// </summary>
    /// <param name="memberName">The name of the public static member on the test class that will provide the test data.</param>
    /// <param name="arguments">The arguments for the member (only supported for methods; ignored for everything else).</param>
    /// <remarks>Optional parameters on methods are not supported.</remarks>
    public CombinatorialMemberDataAttribute(string memberName, params object?[]? arguments)
        : base(memberName, arguments)
    {
        base.MemberType = typeof(T);
    }

    /// <summary>
    /// Gets type to retrieve the member from.
    /// </summary>
    public new Type? MemberType => base.MemberType;
}
#endif
