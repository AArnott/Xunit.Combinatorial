// Copyright (c) parts Andrew Arnott, parts Matthew Wilkinson. All rights reserved. Licensed under the Ms-PL.

namespace Xunit
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Generates values by calling a named static method or property
    /// </summary>
    public sealed class MemberAttribute : ParameterValuesAttribute
    {
        private static object[] EmptyParameters { get; } = new object[0];

        private Type Class { get; }

        private string MemberName { get; }

        private object[] Parameters { get; }

        /// <summary>Generate values by calling the named static method or property on the specified <paramref name="class"/></summary>
        /// <param name="class">The class in which the member is defined</param>
        /// <param name="memberName">The name of the member to call</param>
        public MemberAttribute(Type @class, string memberName)
            : this(@class, memberName, EmptyParameters)
        {
        }

        /// <summary>Generate values by calling the named static method or property on the class this test defined in</summary>
        /// <param name="memberName">The name of the member to call</param>
        public MemberAttribute(string memberName)
            : this(memberName, EmptyParameters)
        {
        }

        /// <summary>Generate values by calling the named static method or property on the specified <paramref name="class"/> with the specified <paramref name="parameters"/> given as arguments</summary>
        /// <param name="class">The class in which the member is defined</param>
        /// <param name="memberName">The name of the member to call</param>
        /// <param name="parameters">The parameters to be passed to the method or property as arguments</param>
        public MemberAttribute(Type @class, string memberName, params object[] parameters)
        {
            if (@class == null)
            {
                throw new ArgumentNullException(nameof(@class));
            }

            if (memberName == null)
            {
                throw new ArgumentNullException(nameof(memberName));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            Class = @class;
            MemberName = memberName;
            Parameters = parameters;
        }

        /// <summary>Generate values by calling the named static method or property on the class this test defined in with the specified <paramref name="parameters"/> given as arguments</summary>
        /// <param name="memberName">The name of the member to call</param>
        /// <param name="parameters">The parameters to be passed to the method or property as arguments</param>
        public MemberAttribute(string memberName, params object[] parameters)
        {
            if (memberName == null)
            {
                throw new ArgumentNullException(nameof(memberName));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            MemberName = memberName;
            Parameters = parameters;
        }

        /// <inheritdoc />
        public override IEnumerable<object> GetValues(ParameterInfo parameter)
        {
            var type = (Class ?? parameter.Member.DeclaringType).GetTypeInfo();
            var candidateMembers = type.DeclaredMembers
                .Where(m => m.Name == MemberName)
                .Where(m => m is MethodInfo || m is PropertyInfo)
                .Where(m => SignatureMatches(m, parameter))
                .Select(m => (m as MethodInfo) ?? (m as PropertyInfo).GetMethod)
                .ToList();

            if (!candidateMembers.Any())
            {
                throw new InvalidOperationException(string.Format(Strings.MemberNoMethodOrProp, MemberName, type.FullName));
            }
            else if (candidateMembers.Count > 1)
            {
                // This shouldn't ever really happen since multiple members with the same name and signature shouldn't technically exist
                throw new InvalidOperationException(string.Format(Strings.MemberMoreThanOne, MemberName, type.FullName));
            }

            var method = candidateMembers.Single();

            var values = (IEnumerable)method.Invoke(null, Parameters);

            return values.Cast<object>();
        }

        private bool SignatureMatches(MemberInfo member, ParameterInfo parameter)
        {
            {
                var property = member as PropertyInfo;
                if (property != null)
                {
                    if (!property.CanRead)
                    {
                        return false;
                    }

                    member = property.GetMethod;
                }
            }

            var method = (MethodInfo)member;

            if (!method.IsStatic)
            {
                return false;
            }

            var methodParameters = method.GetParameters();
            if (methodParameters.Length != Parameters.Length)
            {
                return false;
            }

            for (int i = 0; i < methodParameters.Length; i++)
            {
                if (methodParameters[i].ParameterType != Parameters[i].GetType())
                {
                    return false;
                }
            }

            if (method.ReturnType != typeof(IEnumerable<>).MakeGenericType(parameter.ParameterType))
            {
                return false;
            }

            return true;
        }
    }
}
