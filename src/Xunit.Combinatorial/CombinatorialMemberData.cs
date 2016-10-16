// Copyright (c) Andrew Arnott. All rights reserved. Licensed under the Ms-PL.

/* Note: the code was adapted from XUnit - src\xunit.core\MemberDataAttributeBase.cs */

namespace Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Specifies a member which provides the values for the test runs.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class CombinatorialMemberData : Attribute, ICombinatorialValuesProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CombinatorialMemberData"/> class.
        /// </summary>
        /// <param name="memberName">The name of the public static member on the test class that will provide the test data</param>
        /// <param name="parameters">The parameters for the member (only supported for methods; ignored for everything else)</param>
        public CombinatorialMemberData(string memberName, params object[] parameters)
        {
            Requires.NotNull(memberName, nameof(memberName));

            this.MemberName = memberName;
            this.Parameters = parameters;
        }

        /// <summary>
        /// Gets the member name.
        /// </summary>
        public string MemberName { get; private set; }

        /// <summary>
        /// Gets or sets the type to retrieve the member from. If not set, then the property will be
        /// retrieved from the unit test class.
        /// </summary>
        public Type MemberType { get; set; }

        /// <summary>
        /// Gets the parameters passed to the member. Only supported for static methods.
        /// </summary>
        public object[] Parameters { get; private set; }

        object[] ICombinatorialValuesProvider.GetValues(ParameterInfo parameter)
        {
            var testMethod = parameter.Member;
            var type = this.MemberType ?? testMethod.DeclaringType;
            var accessor = this.GetPropertyAccessor(type) ?? this.GetFieldAccessor(type) ?? this.GetMethodAccessor(type);
            if (accessor == null)
            {
                var parameterText = this.Parameters?.Length > 0 ? $" with parameter types: {string.Join(", ", this.Parameters.Select(p => p?.GetType().FullName ?? "(null)"))}" : string.Empty;
                throw new ArgumentException($"Could not find public static member (property, field, or method) named '{this.MemberName}' on {type.FullName}{parameterText}");
            }

            var obj = accessor();
            if (obj == null)
            {
                return null;
            }

            var dataItems = obj as IEnumerable<object>;
            if (dataItems == null)
            {
                throw new ArgumentException($"Property {this.MemberName} on {type.FullName} did not return IEnumerable<object>");
            }

            return dataItems.ToArray();
        }

        private static bool ParameterTypesCompatible(ParameterInfo[] parameters, Type[] parameterTypes)
        {
            if (parameters?.Length != parameterTypes.Length)
            {
                return false;
            }

            for (int idx = 0; idx < parameters.Length; ++idx)
            {
                if (parameterTypes[idx] != null && !parameters[idx].ParameterType.GetTypeInfo().IsAssignableFrom(parameterTypes[idx].GetTypeInfo()))
                {
                    return false;
                }
            }

            return true;
        }

        private Func<object> GetFieldAccessor(Type type)
        {
            FieldInfo fieldInfo = null;
            for (var reflectionType = type; reflectionType != null; reflectionType = reflectionType.GetTypeInfo().BaseType)
            {
                fieldInfo = reflectionType.GetRuntimeField(this.MemberName);
                if (fieldInfo != null)
                {
                    break;
                }
            }

            if (fieldInfo == null || !fieldInfo.IsStatic)
            {
                return null;
            }

            return () => fieldInfo.GetValue(null);
        }

        private Func<object> GetMethodAccessor(Type type)
        {
            MethodInfo methodInfo = null;
            var parameterTypes = this.Parameters == null ? new Type[0] : this.Parameters.Select(p => p?.GetType()).ToArray();
            for (var reflectionType = type; reflectionType != null; reflectionType = reflectionType.GetTypeInfo().BaseType)
            {
                methodInfo = reflectionType.GetRuntimeMethods()
                                           .FirstOrDefault(m => m.Name == this.MemberName && ParameterTypesCompatible(m.GetParameters(), parameterTypes));
                if (methodInfo != null)
                {
                    break;
                }
            }

            if (methodInfo == null || !methodInfo.IsStatic)
            {
                return null;
            }

            return () => methodInfo.Invoke(null, this.Parameters);
        }

        private Func<object> GetPropertyAccessor(Type type)
        {
            PropertyInfo propInfo = null;
            for (var reflectionType = type; reflectionType != null; reflectionType = reflectionType.GetTypeInfo().BaseType)
            {
                propInfo = reflectionType.GetRuntimeProperty(this.MemberName);
                if (propInfo != null)
                {
                    break;
                }
            }

            if (propInfo == null || propInfo.GetMethod == null || !propInfo.GetMethod.IsStatic)
            {
                return null;
            }

            return () => propInfo.GetValue(null, null);
        }
    }
}
