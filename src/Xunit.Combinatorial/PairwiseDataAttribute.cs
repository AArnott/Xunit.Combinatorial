namespace Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Sdk;
    using Validation;

    /// <summary>
    /// Provides a test method decorated with a <see cref="TheoryAttribute"/>
    /// with arguments to run various combination of values for the
    /// parameters taken by the test method using a pairwise strategy.
    /// </summary>
    public class PairwiseDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            Requires.NotNull(testMethod, nameof(testMethod));

            var parameters = testMethod.GetParameters();
            if (parameters.Length == 0)
            {
                return Enumerable.Empty<object[]>();
            }

            return Enumerable.Empty<object[]>();
        }
    }
}
