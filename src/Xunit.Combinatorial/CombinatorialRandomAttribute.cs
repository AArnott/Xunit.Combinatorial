// Copyright (c) Andrew Arnott. All rights reserved. Licensed under the Ms-PL.

namespace Xunit
{
    using System;

    /// <summary>
    /// Specifies which range of values for this parameter should be used for running the test method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class CombinatorialRandomAttribute : Attribute
    {
        /// <summary>
        /// Default quantity of values to generate.
        /// </summary>
        public const int DefaultCount = 5;

        /// <summary>
        /// Default minValue for System.Random.Next method.
        /// </summary>
        public const int DefaultMinValue = 0;

        /// <summary>
        /// Default maxValue for System.Random.Next method.
        /// </summary>
        public const int DefaultMaxValue = int.MaxValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="CombinatorialRandomAttribute"/> class.
        /// </summary>
        public CombinatorialRandomAttribute()
            : this(DefaultCount, DefaultMinValue, DefaultMaxValue, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CombinatorialRandomAttribute"/> class.
        /// </summary>
        /// <param name="count">
        /// The quantity of values to generate.
        /// Cannot be less than 1, which would conceptually result in zero test cases.
        /// </param>
        public CombinatorialRandomAttribute(int count)
            : this(count, DefaultMinValue, DefaultMaxValue, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CombinatorialRandomAttribute"/> class.
        /// </summary>
        /// <param name="count">
        /// The quantity of values to generate.
        /// Cannot be less than 1, which would conceptually result in zero test cases.
        /// </param>
        /// <param name="maxValue">
        /// maxValue for System.Random.Next method.
        /// </param>
        public CombinatorialRandomAttribute(int count, int maxValue)
            : this(count, DefaultMinValue, maxValue, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CombinatorialRandomAttribute"/> class.
        /// </summary>
        /// <param name="count">
        /// The quantity of values to generate.
        /// Cannot be less than 1, which would conceptually result in zero test cases.
        /// </param>
        /// <param name="minValue">
        /// minValue for System.Random.Next method.
        /// </param>
        /// <param name="maxValue">
        /// maxValue for System.Random.Next method.
        /// </param>
        public CombinatorialRandomAttribute(int count, int minValue, int maxValue)
            : this(count, minValue, maxValue, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CombinatorialRandomAttribute"/> class.
        /// </summary>
        /// <param name="count">
        /// The quantity of values to generate.
        /// Cannot be less than 1, which would conceptually result in zero test cases.
        /// </param>
        /// <param name="minValue">
        /// minValue for System.Random.Next method.
        /// </param>
        /// <param name="maxValue">
        /// maxValue for System.Random.Next method.
        /// </param>
        /// <param name="seed">
        /// seed for System.Random constructor.
        /// </param>
        public CombinatorialRandomAttribute(int count, int minValue, int maxValue, int? seed)
        {
            if (count < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            var random = seed.HasValue ? new Random(seed.Value) : new Random();

            object[] values = new object[count];
            for (int i = 0; i < count; i++)
            {
                values[i] = random.Next(minValue, maxValue);
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
