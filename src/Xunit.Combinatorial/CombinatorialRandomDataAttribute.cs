// Copyright (c) Andrew Arnott. All rights reserved. Licensed under the Ms-PL.

namespace Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Specifies which range of values for this parameter should be used for running the test method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class CombinatorialRandomDataAttribute : Attribute
    {
        /// <summary>
        /// Special seed value to create System.Random class without seed.
        /// </summary>
        public const int NoSeed = 0;

        private object[]? values;

        /// <summary>
        /// Gets or sets the number of values to generate. Must be positive.
        /// </summary>
        /// <value>The default value is 5.</value>
        public int Count { get; set; } = 5;

        /// <summary>
        /// Gets or sets the minimum value (inclusive) that may be randomly generated.
        /// </summary>
        /// <value>The default value is 0.</value>
        public int Minimum { get; set; }

        /// <summary>
        /// Gets or sets the maximum value (inclusive) that may be randomly generated.
        /// </summary>
        /// <value>The default value is <c><see cref="int.MaxValue"/> - 1</c>, which is the maximum allowable value.</value>
        public int Maximum { get; set; } = int.MaxValue - 1;

        /// <summary>
        /// Gets or sets the seed to use for random number generation.
        /// </summary>
        /// <value>The default value of <see cref="NoSeed"/> allows for a new seed to be used each time.</value>
        public int Seed { get; set; } = NoSeed;

        /// <summary>
        /// Gets the values that should be passed to this parameter on the test method.
        /// </summary>
        /// <value>An array of values.</value>
        public object[] Values => this.values ??= this.GenerateValues();

        private object[] GenerateValues()
        {
            if (this.Count < 1)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.ValueMustBePositive, nameof(this.Count)));
            }

            if (this.Minimum > this.Maximum)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.XMustNotBeGreaterThanY, nameof(this.Minimum), nameof(this.Maximum)));
            }

            int maxPossibleValues = this.Maximum - this.Minimum + 1;
            if (this.Count > maxPossibleValues)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.MoreRandomValuesRequestedThanPossibleOnes, nameof(this.Count), nameof(this.Minimum), nameof(this.Maximum)));
            }

            Random random = this.Seed != NoSeed ? new Random(this.Seed) : new Random();

            HashSet<int> collisionChecker = new HashSet<int>();
            object[] values = new object[this.Count];
            int collisionCount = 0;
            int i = 0;
            while (collisionChecker.Count < this.Count)
            {
                int value = random.Next(this.Minimum, this.Maximum + 1);
                if (collisionChecker.Add(value))
                {
                    values[i++] = value;
                }
                else
                {
                    collisionCount++;
                }

                if (collisionCount > collisionChecker.Count * 5)
                {
                    // We have collided in random values far more than we have successfully generated values.
                    // Rather than spin in this loop, throw.
                    throw new InvalidOperationException(Strings.TooManyRandomCollisions);
                }
            }

            return values;
        }
    }
}
