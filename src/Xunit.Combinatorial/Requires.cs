// Copyright (c) Andrew Arnott. All rights reserved. Licensed under the Ms-PL.

#pragma warning disable SA1611 // Element parameters must be documented

namespace Xunit
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;

    /// <summary>
    /// Common runtime checks that throw ArgumentExceptions upon failure.
    /// </summary>
    internal static class Requires
    {
        /// <summary>
        /// Throws an exception if the specified parameter's value is null.
        /// </summary>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        /// <param name="value">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
        /// <returns>The value of the parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <c>null</c></exception>
        [DebuggerStepThrough]
        public static T NotNull<T>(T value, string parameterName)
            where T : class // ensures value-types aren't passed to a null checking method
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        /// <summary>
        /// Throws an exception if the specified parameter's value is IntPtr.Zero.
        /// </summary>
        /// <param name="value">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
        /// <returns>The value of the parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is IntPtr.Zero</exception>
        [DebuggerStepThrough]
        public static IntPtr NotNull(IntPtr value, string parameterName)
        {
            if (value == IntPtr.Zero)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

#if !NET35
        /// <summary>
        /// Throws an exception if the specified parameter's value is null.
        /// </summary>
        /// <param name="value">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <c>null</c></exception>
        /// <remarks>
        /// This method allows async methods to use Requires.NotNull without having to assign the result
        /// to local variables to avoid C# warnings.
        /// </remarks>
        [DebuggerStepThrough]
        public static void NotNull(System.Threading.Tasks.Task value, string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        /// <summary>
        /// Throws an exception if the specified parameter's value is null.
        /// </summary>
        /// <typeparam name="T">The type of the return value of the task.</typeparam>
        /// <param name="value">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <c>null</c></exception>
        /// <remarks>
        /// This method allows async methods to use Requires.NotNull without having to assign the result
        /// to local variables to avoid C# warnings.
        /// </remarks>
        [DebuggerStepThrough]
        public static void NotNull<T>(System.Threading.Tasks.Task<T> value, string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }
#endif

        /// <summary>
        /// Throws an exception if the specified parameter's value is null.
        /// </summary>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        /// <param name="value">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
        /// <returns>The value of the parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <c>null</c></exception>
        /// <remarks>
        /// This method exists for callers who themselves only know the type as a generic parameter which
        /// may or may not be a class, but certainly cannot be null.
        /// </remarks>
        [DebuggerStepThrough]
        public static T NotNullAllowStructs<T>(T value, string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        /// <summary>
        /// Throws an exception if the specified parameter's value is null or empty.
        /// </summary>
        /// <param name="value">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is <c>null</c> or empty.</exception>
        [DebuggerStepThrough]
        public static void NotNullOrEmpty(string value, string parameterName)
        {
            // To the guy that is doing random code cleaning:
            // Consider the perfomance when changing the code to delegate to NotNull.
            // In general do not chain call to another function, check first and return as earlier as possible.
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            if (value.Length == 0 || value[0] == '\0')
            {
                throw new ArgumentException(Format(Strings.Argument_EmptyString, parameterName), parameterName);
            }
        }

#if !NET35
        /// <summary>
        /// Throws an exception if the specified parameter's value is null, empty, or whitespace.
        /// </summary>
        /// <param name="value">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is <c>null</c> or empty.</exception>
        [DebuggerStepThrough]
        public static void NotNullOrWhiteSpace(string value, string parameterName)
        {
            // To the guy that is doing random code cleaning:
            // Consider the perfomance when changing the code to delegate to NotNull.
            // In general do not chain call to another function, check first and return as earlier as possible.
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            if (value.Length == 0 || value[0] == '\0')
            {
                throw new ArgumentException(Format(Strings.Argument_EmptyString, parameterName), parameterName);
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(Format(Strings.Argument_Whitespace, parameterName));
            }
        }
#endif

        /// <summary>
        /// Throws an exception if the specified parameter's value is null,
        /// has no elements or has an element with a null value.
        /// </summary>
        /// <param name="values">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
        /// <exception cref="ArgumentException">Thrown if the tested condition is false.</exception>
        [DebuggerStepThrough]
        public static void NotNullOrEmpty(System.Collections.IEnumerable values, string parameterName)
        {
            // To the guy that is doing random code cleaning:
            // Consider the perfomance when changing the code to delegate to NotNull.
            // In general do not chain call to another function, check first and return as earlier as possible.
            if (values == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            ICollection collection = values as ICollection;

            if (collection != null)
            {
                if (collection.Count > 0)
                {
                    return;
                }

                throw new ArgumentException(Format(Strings.Argument_EmptyArray, parameterName), parameterName);
            }

            IEnumerator enumerator = values.GetEnumerator();

            using (enumerator as IDisposable)
            {
                if (enumerator.MoveNext())
                {
                    return;
                }
            }

            throw new ArgumentException(Format(Strings.Argument_EmptyArray, parameterName), parameterName);
        }

        /// <summary>
        /// Throws an exception if the specified parameter's value is null,
        /// has no elements or has an element with a null value.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
        /// <param name="values">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
        /// <exception cref="ArgumentException">Thrown if the tested condition is false.</exception>
        [DebuggerStepThrough]
        public static void NotNullEmptyOrNullElements<T>(IEnumerable<T> values, string parameterName)
            where T : class // ensures value-types aren't passed to a null checking method
        {
            NotNull(values, parameterName);

            bool hasElements = false;
            foreach (T value in values)
            {
                hasElements = true;

                if (value == null)
                {
                    throw new ArgumentException(Format(Strings.Argument_NullElement, parameterName), parameterName);
                }
            }

            if (!hasElements)
            {
                throw new ArgumentException(Format(Strings.Argument_EmptyArray, parameterName), parameterName);
            }
        }

        /// <summary>
        /// Throws an exception if the specified parameter's value is not null
        /// <em>and</em> has an element with a null value.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
        /// <param name="values">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
        /// <exception cref="ArgumentException">Thrown if the tested condition is false.</exception>
        [DebuggerStepThrough]
        public static void NullOrNotNullElements<T>(IEnumerable<T> values, string parameterName)
        {
            if (values != null)
            {
                foreach (T value in values)
                {
                    if (value == null)
                    {
                        throw new ArgumentException(Format(Strings.Argument_NullElement, parameterName), parameterName);
                    }
                }
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if a condition does not evaluate to true.
        /// </summary>
        [DebuggerStepThrough]
        public static void Range(bool condition, string parameterName, string message = null)
        {
            if (!condition)
            {
                FailRange(parameterName, message);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if a condition does not evaluate to true.
        /// </summary>
        /// <returns>Nothing.  This method always throws.</returns>
        [DebuggerStepThrough]
        public static Exception FailRange(string parameterName, string message = null)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
            else
            {
                throw new ArgumentOutOfRangeException(parameterName, message);
            }
        }

        /// <summary>
        /// Throws an ArgumentException if a condition does not evaluate to true.
        /// </summary>
        [DebuggerStepThrough]
        public static void Argument(bool condition, string parameterName, string message)
        {
            if (!condition)
            {
                throw new ArgumentException(message, parameterName);
            }
        }

        /// <summary>
        /// Throws an ArgumentException if a condition does not evaluate to true.
        /// </summary>
        [DebuggerStepThrough]
        public static void Argument(bool condition, string parameterName, string message, object arg1)
        {
            if (!condition)
            {
                throw new ArgumentException(Format(message, arg1), parameterName);
            }
        }

        /// <summary>
        /// Throws an ArgumentException if a condition does not evaluate to true.
        /// </summary>
        [DebuggerStepThrough]
        public static void Argument(bool condition, string parameterName, string message, object arg1, object arg2)
        {
            if (!condition)
            {
                throw new ArgumentException(Format(message, arg1, arg2), parameterName);
            }
        }

        /// <summary>
        /// Throws an ArgumentException if a condition does not evaluate to true.
        /// </summary>
        [DebuggerStepThrough]
        public static void Argument(bool condition, string parameterName, string message, params object[] args)
        {
            if (!condition)
            {
                throw new ArgumentException(Format(message, args), parameterName);
            }
        }

        /// <summary>
        /// Validates some expression describing the acceptable condition for an argument evaluates to true.
        /// </summary>
        /// <param name="condition">The expression that must evaluate to true to avoid an <see cref="ArgumentException"/>.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="unformattedMessage">The unformatted message.</param>
        /// <param name="args">Formatting arguments.</param>
        [DebuggerStepThrough]
        public static void That(bool condition, string parameterName, string unformattedMessage, params object[] args)
        {
            if (!condition)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, unformattedMessage, args), parameterName);
            }
        }

        /// <summary>
        /// Validates some expression describing the acceptable condition for an argument evaluates to true.
        /// </summary>
        /// <param name="condition">The expression that must evaluate to true to avoid an <see cref="InvalidOperationException"/>.</param>
        /// <param name="message">The message to include with the exception.</param>
        [DebuggerStepThrough]
        public static void ValidState(bool condition, string message)
        {
            if (!condition)
            {
                throw new InvalidOperationException(message);
            }
        }

        /// <summary>
        /// Throws an ArgumentException.
        /// </summary>
        /// <returns>Nothing.  It always throws.</returns>
        [DebuggerStepThrough]
        public static Exception Fail(string message)
        {
            throw new ArgumentException(message);
        }

        /// <summary>
        /// Throws an ArgumentException.
        /// </summary>
        /// <returns>Nothing.  It always throws.</returns>
        [DebuggerStepThrough]
        public static Exception Fail(string unformattedMessage, params object[] args)
        {
            throw Fail(Format(unformattedMessage, args));
        }

        /// <summary>
        /// Throws an ArgumentException.
        /// </summary>
        /// <returns>Nothing.  This method always throws.  But the signature allows calling code to "throw" this method for C# syntax reasons.</returns>
        [DebuggerStepThrough]
        public static Exception Fail(Exception innerException, string unformattedMessage, params object[] args)
        {
            throw new ArgumentException(Format(unformattedMessage, args), innerException);
        }

        /// <summary>
        /// Helper method that formats string arguments.
        /// </summary>
        /// <returns>The formatted string.</returns>
        private static string Format(string format, params object[] arguments)
        {
            return PrivateErrorHelpers.Format(format, arguments);
        }
    }
}
