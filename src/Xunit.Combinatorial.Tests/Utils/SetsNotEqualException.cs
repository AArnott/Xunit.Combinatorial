// Copyright (c) parts Andrew Arnott, parts Matthew Wilkinson. All rights reserved. Licensed under the Ms-PL.

namespace Xunit.Combinatorial.Tests.Utils
{
    using Xunit.Sdk;

    public sealed class SetsNotEqualException<T> : AssertActualExpectedException
    {
        /// <summary>
        /// Creates a new instance of the <see href="AssertActualExpectedException" /> class.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        /// <param name="userMessage">The user message to be shown</param>
        /// <param name="expectedTitle">The title to use for the expected value (defaults to "Expected")</param>
        /// <param name="actualTitle">The title to use for the actual value (defaults to "Actual")</param>
        public SetsNotEqualException(T[] expected, T[] actual, string userMessage, string expectedTitle = null, string actualTitle = null) : base(expected, actual, userMessage, expectedTitle, actualTitle)
        {
        }
    }
}
