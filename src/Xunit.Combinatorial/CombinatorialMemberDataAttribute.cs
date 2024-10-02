// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Reflection;

namespace Xunit;

/// <summary>
/// Specifies which member should provide data for this parameter used for running the test method.
/// </summary>
[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true)]
public class CombinatorialMemberDataAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CombinatorialMemberDataAttribute"/> class.
    /// </summary>
    /// <param name="memberName">The name of the public static member on the test class that will provide the test data.</param>
    /// <param name="arguments">The arguments for the member (only supported for methods; ignored for everything else).</param>
    public CombinatorialMemberDataAttribute(string memberName, params object?[]? arguments)
    {
        this.MemberName = memberName ?? throw new ArgumentNullException(nameof(memberName));
        this.Arguments = arguments;
    }

    /// <summary>
    /// Gets the member name.
    /// </summary>
    public string MemberName { get; }

    /// <summary>
    /// Gets or sets the type to retrieve the member from. If not set, then the property will be
    /// retrieved from the unit test class.
    /// </summary>
    public Type? MemberType { get; set; }

    /// <summary>
    /// Gets the arguments passed to the member. Only supported for static methods.
    /// </summary>
    public object?[]? Arguments { get; }

    /// <summary>
    /// Gets the values that should be passed to this parameter on the test method.
    /// </summary>
    /// <param name="parameterInfo">The parameter for which the data should be provided.</param>
    /// <returns>An array of values.</returns>
    public object?[] GetValues(ParameterInfo parameterInfo)
    {
        Requires.NotNull(parameterInfo, nameof(parameterInfo));

        MemberInfo? testMethod = parameterInfo.Member;

        Type? type = this.MemberType ?? testMethod?.DeclaringType;

        if (type is null)
        {
            return Array.Empty<object?>();
        }

        Func<object>? accessor = this.GetPropertyAccessor(type, parameterInfo) ?? this.GetMethodAccessor(type, parameterInfo) ?? this.GetFieldAccessor(type, parameterInfo);
        if (accessor is null)
        {
            string? parameterText = this.Arguments?.Length > 0 ? $" with parameter types: {string.Join(", ", this.Arguments.Select(p => p?.GetType().FullName ?? "(null)"))}" : string.Empty;
            throw new ArgumentException($"Could not find public static member (property, field, or method) named '{this.MemberName}' on {type.FullName}{parameterText}.");
        }

        var obj = (IEnumerable)accessor();
        return obj.Cast<object>().ToArray();
    }

    /// <summary>
    /// Gets the type of the value enumerated by a given type that is or implements <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <param name="enumerableType">An <see cref="IEnumerable{T}"/> type or a type that implements <see cref="IEnumerable{T}"/>.</param>
    /// <returns>The generic type argument for (one of) the <see cref="IEnumerable{T}"/> interface)s) implemented by the <paramref name="enumerableType"/>.</returns>
    private static TypeInfo? GetEnumeratedType(Type enumerableType)
    {
        if (enumerableType.IsGenericType)
        {
            if (enumerableType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                Type[] enumerableGenericTypeArgs = enumerableType.GetTypeInfo().GetGenericArguments();
                return enumerableGenericTypeArgs[0].GetTypeInfo();
            }

            if (enumerableType.GetGenericTypeDefinition() == typeof(TheoryData<>))
            {
                Type[] enumerableGenericTypeArgs = enumerableType.GetTypeInfo().GetGenericArguments();
                return enumerableGenericTypeArgs[0].GetTypeInfo();
            }
        }

        foreach (Type implementedInterface in enumerableType.GetTypeInfo().ImplementedInterfaces)
        {
            TypeInfo interfaceTypeInfo = implementedInterface.GetTypeInfo();
            if (interfaceTypeInfo.IsGenericType && interfaceTypeInfo.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                return interfaceTypeInfo.GetGenericArguments()[0].GetTypeInfo();
            }
        }

        return null;
    }

    private Func<object>? GetPropertyAccessor(Type type, ParameterInfo parameterInfo)
    {
        PropertyInfo? propInfo = null;
        for (Type? reflectionType = type; reflectionType is not null; reflectionType = reflectionType.GetTypeInfo().BaseType)
        {
            propInfo = reflectionType.GetRuntimeProperty(this.MemberName);
            if (propInfo is not null)
            {
                break;
            }
        }

        if (propInfo?.GetMethod is null || !propInfo.GetMethod.IsStatic)
        {
            return null;
        }

        this.EnsureValidMemberDataType(propInfo.PropertyType, propInfo.DeclaringType, parameterInfo);

        return () => propInfo.GetValue(null, null);
    }

    private Func<object>? GetMethodAccessor(Type type, ParameterInfo parameterInfo)
    {
        MethodInfo? methodInfo = null;
        for (Type? reflectionType = type; reflectionType is not null; reflectionType = reflectionType.GetTypeInfo().BaseType)
        {
            methodInfo = reflectionType.GetRuntimeMethods().FirstOrDefault(m => m.Name == this.MemberName && this.ParameterTypesCompatible(m.GetParameters(), this.Arguments));
            if (methodInfo is not null)
            {
                break;
            }
        }

        if (methodInfo is null || !methodInfo.IsStatic)
        {
            return null;
        }

        this.EnsureValidMemberDataType(methodInfo.ReturnType, methodInfo.DeclaringType, parameterInfo);

        return () => methodInfo.Invoke(null, this.Arguments);
    }

    private bool ParameterTypesCompatible(ParameterInfo[] parameters, object?[]? arguments)
    {
        if (arguments is null)
        {
            return parameters.Length == 0;
        }
        else if (parameters.Length != arguments.Length)
        {
            return false;
        }

        for (int i = 0; i < parameters.Length; i++)
        {
            if (arguments[i] is object arg)
            {
                if (!parameters[i].ParameterType.GetTypeInfo().IsAssignableFrom(arg.GetType().GetTypeInfo()))
                {
                    return false;
                }
            }
            else
            {
                if (parameters[i].ParameterType.IsValueType)
                {
                    // Cannot assign null to a value type parameter.
                    return false;
                }
            }
        }

        return true;
    }

    private Func<object>? GetFieldAccessor(Type type, ParameterInfo parameterInfo)
    {
        FieldInfo? fieldInfo = null;
        for (Type? reflectionType = type; reflectionType is not null; reflectionType = reflectionType.GetTypeInfo().BaseType)
        {
            fieldInfo = reflectionType.GetRuntimeField(this.MemberName);

            if (fieldInfo is not null)
            {
                break;
            }
        }

        if (fieldInfo is null || !fieldInfo.IsStatic)
        {
            return null;
        }

        this.EnsureValidMemberDataType(fieldInfo.FieldType, fieldInfo.DeclaringType, parameterInfo);

        return () => fieldInfo.GetValue(null);
    }

    /// <summary>
    /// Throws if a given type will not generate values that are compatible with a given parameter.
    /// </summary>
    /// <param name="enumerableType">The type of value stored by the field or property.</param>
    /// <param name="declaringType">The type on which the member is declared.</param>
    /// <param name="parameterInfo">The parameter that must receive the values generated by <paramref name="enumerableType"/>.</param>
    /// <exception cref="ArgumentException">Throw when <paramref name="enumerableType"/> does not conform to requirements or does not produce values assignable to <paramref name="parameterInfo"/>.</exception>
    private void EnsureValidMemberDataType(Type enumerableType, Type declaringType, ParameterInfo parameterInfo)
    {
        TypeInfo? enumeratedType = GetEnumeratedType(enumerableType);
        if (enumeratedType is null)
        {
            throw new ArgumentException($"Member {this.MemberName} on {declaringType.FullName} must return a type that implements IEnumerable<T>.");
        }

        if (enumeratedType.IsArray)
        {
            throw new ArgumentException(
                $"Member {this.MemberName} on {declaringType.FullName} returned an IEnumerable<{enumeratedType.Name}>, which is not supported.");
        }

        if (enumeratedType.IsGenericType && enumeratedType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
        {
            throw new ArgumentException(
                $"Member {this.MemberName} on {declaringType.FullName} returned an IEnumerable<IEnumerable<{enumeratedType.GetGenericArguments()[0].Name}>>, which is not supported.");
        }

        if (!enumeratedType.IsAssignableFrom(parameterInfo.ParameterType.GetTypeInfo()))
        {
            throw new ArgumentException(
                $"Parameter type {parameterInfo.ParameterType.FullName} is not compatible with returned member type {enumeratedType.FullName}.");
        }
    }
}
