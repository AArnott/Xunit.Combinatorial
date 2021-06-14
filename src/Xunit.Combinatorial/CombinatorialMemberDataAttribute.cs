namespace Xunit
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Specifies which member should provide data for this parameter used for running the test method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true)]
    public class CombinatorialMemberDataAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CombinatorialMemberDataAttribute"/> class.
        /// </summary>
        /// <param name="memberName">The name of the public static member on the test class that will provide the test data</param>
        /// <param name="parameters">The parameters for the member (only supported for methods; ignored for everything else)</param>
        public CombinatorialMemberDataAttribute(string memberName, params object[] parameters)
        {
            this.MemberName = memberName ?? throw new ArgumentNullException(nameof(memberName));
            this.Parameters = parameters;
        }

        /// <summary>
        /// Gets the member name.
        /// </summary>
        public string MemberName { get; }

        /// <summary>
        /// Gets or sets the type to retrieve the member from. If not set, then the property will be
        /// retrieved from the unit test class.
        /// </summary>
        public Type MemberType { get; set; }

        /// <summary>
        /// Gets or sets the parameters passed to the member. Only supported for static methods.
        /// </summary>
        public object[] Parameters { get; }

        /// <summary>
        /// Gets the values that should be passed to this parameter on the test method.
        /// </summary>
        /// <param name="parameterInfo">The parameter for which the data should be provided</param>
        /// <returns>An array of values.</returns>
        public object[] GetValues(ParameterInfo parameterInfo)
        {
            var testMethod = parameterInfo.Member;

            var type = this.MemberType ?? testMethod?.DeclaringType;

            if (type == null)
            {
                return new object[0];
            }

            var accessor = this.GetPropertyAccessor(type, parameterInfo) ?? this.GetMethodAccessor(type, parameterInfo) ?? this.GetFieldAccessor(type, parameterInfo);
            if (accessor == null)
            {
                var parameterText = this.Parameters?.Length > 0 ? $" with parameter types: {string.Join(", ", this.Parameters.Select(p => p?.GetType().FullName ?? "(null)"))}" : string.Empty;
                throw new ArgumentException($"Could not find public static member (property, field, or method) named '{this.MemberName}' on {type.FullName}{parameterText}");
            }

            var obj = (IEnumerable)accessor();
            return obj.Cast<object>().ToArray();
        }

        private Func<object> GetPropertyAccessor(Type type, ParameterInfo parameterInfo)
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

            if (propInfo?.GetMethod == null || !propInfo.GetMethod.IsStatic)
            {
                return null;
            }

            this.EnsureValidMemberDataType(propInfo.PropertyType, propInfo.DeclaringType, parameterInfo);

            return () => propInfo.GetValue(null, null);
        }

        private Func<object> GetMethodAccessor(Type type, ParameterInfo parameterInfo)
        {
            MethodInfo methodInfo = null;
            var parameterTypes = this.Parameters == null
                ? new Type[0]
                : this.Parameters.Select(p => p.GetType()).ToArray();
            for (var reflectionType = type; reflectionType != null; reflectionType = reflectionType.GetTypeInfo().BaseType)
            {
                methodInfo = reflectionType.GetRuntimeMethods().FirstOrDefault(m => m.Name == this.MemberName && this.ParameterTypesCompatible(m.GetParameters(), parameterTypes));

                if (methodInfo != null)
                {
                    break;
                }
            }

            if (methodInfo == null || !methodInfo.IsStatic)
            {
                return null;
            }

            this.EnsureValidMemberDataType(methodInfo.ReturnType, methodInfo.DeclaringType, parameterInfo);

            return () => methodInfo.Invoke(null, this.Parameters);
        }

        private bool ParameterTypesCompatible(ParameterInfo[] parameters, Type[] parameterTypes)
        {
            if (parameters.Length != parameterTypes.Length)
            {
                return false;
            }

            for (var i = 0; i < parameters.Length; i++)
            {
                if (parameterTypes[i] != null && !parameters[i].ParameterType.GetTypeInfo()
                    .IsAssignableFrom(parameterTypes[i].GetTypeInfo()))
                {
                    return false;
                }
            }

            return true;
        }

        private Func<object> GetFieldAccessor(Type type, ParameterInfo parameterInfo)
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

            this.EnsureValidMemberDataType(fieldInfo.FieldType, fieldInfo.DeclaringType, parameterInfo);

            return () => fieldInfo.GetValue(null);
        }

        private void EnsureValidMemberDataType(Type type, Type declaringType, ParameterInfo parameterType)
        {
            var enumerableTypeInfo = typeof(IEnumerable).GetTypeInfo();

            if (!enumerableTypeInfo.IsAssignableFrom(type.GetTypeInfo()))
            {
                throw new ArgumentException($"Member {this.MemberName} on {type.FullName} did not return IEnumerable");
            }

            var enumerableGenericType = this.GetEnumerableType(type);
            if (enumerableTypeInfo.IsAssignableFrom(enumerableGenericType))
            {
                throw new ArgumentException(
                    $"Member {this.MemberName} on {declaringType.FullName} returned an IEnumerable<object[]>, which is not supported");
            }

            if (!enumerableGenericType.IsAssignableFrom(parameterType.ParameterType.GetTypeInfo()))
            {
                throw new ArgumentException(
                    $"Parameter type {parameterType.ParameterType.FullName} is not compatible with returned member type {enumerableGenericType.FullName}");
            }
        }

        private TypeInfo GetEnumerableType(Type enumerableType)
        {
            var enumerableGenericTypeDefinition = enumerableType.GetTypeInfo().GetGenericArguments();
            if (enumerableGenericTypeDefinition != null)
            {
                return enumerableGenericTypeDefinition[0].GetTypeInfo();
            }

            foreach (var implementedInterface in enumerableType.GetTypeInfo().ImplementedInterfaces)
            {
                var interfaceTypeInfo = implementedInterface.GetTypeInfo();
                if (interfaceTypeInfo.IsGenericType && interfaceTypeInfo.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    return interfaceTypeInfo.GetGenericArguments()[0].GetTypeInfo();
                }
            }

            return null;
        }
    }
}