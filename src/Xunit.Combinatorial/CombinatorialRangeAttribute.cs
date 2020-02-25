// Copyright (c) Andrew Arnott. All rights reserved. Licensed under the Ms-PL.

namespace Xunit
{
    using System;

    /// <summary>
    /// Specifies which range of values for this parameter should be used for running the test method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class CombinatorialRangeAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CombinatorialRangeAttribute"/> class.
        /// </summary>
        /// <param name="from">The value at the beginning of the range.</param>
        /// <param name="count">The quantity of consecutive integer values to include.</param>
        public CombinatorialRangeAttribute(int from, int count)
        {
            if (count < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            object[] values = new object[count];
            for (int i = 0; i < count; i++)
            {
                values[i] = from + i;
            }

            this.Values = values;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CombinatorialRangeAttribute"/> class.
        /// </summary>
        /// <param name="from">The value at the beginning of the range.</param>
        /// <param name="to">The value at the end of the range.</param>
        /// <param name="step">The number of integers to step for each value in result.</param>
        public CombinatorialRangeAttribute(int from, int to, int step)
        {
            // to cannot be less than from
            if (to < from)
            {
                throw new ArgumentOutOfRangeException(nameof(to));
            }

            // step must be a positive integer
            if (step < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(step));
            }

            int count = ((to - from) / step) + 1;
            object[] values = new object[count];
            for (int i = 0; i < count; i++)
            {
                values[i] = from + (i * step);
            }

            this.Values = values;
        }

        /// <summary>
        /// Gets the values that should be passed to this parameter on the test method.
        /// </summary>
        /// <value>An array of values.</value>
        public object[] Values { get; }
    }
}
