namespace Xunit.Combinatorial
{
    using System;
    using Validation;

    [AttributeUsage(AttributeTargets.Parameter)]
    public class CombinatorialValuesAttribute : Attribute
    {
        public CombinatorialValuesAttribute(params object[] values)
        {
            Requires.NotNull(values, nameof(values));

            this.Values = values;
        }

        public object[] Values { get; }
    }
}
