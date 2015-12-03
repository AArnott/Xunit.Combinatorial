// Copyright (c) 2015 Andrew Arnott
// Licensed under the Ms-PL

namespace Xunit.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Abstractions;

    /// <summary>
    /// Provides test case discovery for the <see cref="CombinatorialDataAttribute"/>.
    /// </summary>
    public class CombinatorialDataDiscoverer : IDataDiscoverer
    {
        /// <inheritdoc />
        public IEnumerable<object[]> GetData(IAttributeInfo dataAttribute, IMethodInfo testMethod)
        {
            return new object[][]
            {
                new object[] { 1 },
                new object[] { 2 },
                new object[] { 3 },
            };
        }

        /// <inheritdoc />
        public bool SupportsDiscoveryEnumeration(IAttributeInfo dataAttribute, IMethodInfo testMethod)
        {
            return true;
        }
    }
}
