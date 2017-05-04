// Copyright (c) parts Andrew Arnott, parts Matthew Wilkinson. All rights reserved. Licensed under the Ms-PL.

namespace Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

    internal static class FSharpValueProvider
    {
        private static Regex FSharpCoreQualifiedNamePattern { get; } = new Regex(@"Microsoft\.FSharp\.Core\.(.*?), FSharp\.Core, Version=([0-9]+\.[0-9]+\.[0-9]+\.[0-9]+), Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");

        private static bool IsInFSharpCore(this Type t) => FSharpCoreQualifiedNamePattern.IsMatch(t.AssemblyQualifiedName);

        private static bool IsCompilationMappingAttribute(this CustomAttributeData cad) => cad.AttributeType.IsInFSharpCore() && cad.AttributeType.FullName == "Microsoft.FSharp.Core.CompilationMappingAttribute";

        private static bool IsSourceConstructFlagArgument(this CustomAttributeTypedArgument arg, int flagValue)
        {
            return arg.ArgumentType.IsInFSharpCore() &&
                   arg.ArgumentType.FullName == "Microsoft.FSharp.Core.SourceConstructFlags" &&
                   arg.Value as int? == flagValue;
        }

        public static bool IsUnionType(Type type)
        {
            return type.GetTypeInfo().CustomAttributes.Any(cad =>
                cad.IsCompilationMappingAttribute() &&
                cad.ConstructorArguments.Count() == 1 &&
                cad.ConstructorArguments.Single().IsSourceConstructFlagArgument(1) // SumType
            );
        }

        private static IEnumerable<MethodInfo> GetUnionCaseFactories(TypeInfo type)
        {
            return type.DeclaredMethods.Where(m => m.CustomAttributes.Any(ca =>
                ca.IsCompilationMappingAttribute() &&
                ca.ConstructorArguments.Count == 2 &&
                ca.ConstructorArguments.First().IsSourceConstructFlagArgument(8) && // UnionCase
                ca.ConstructorArguments[1].ArgumentType == typeof(int)
            ));
        }

        public static IEnumerable<object> GetUnionCaseValues(TypeInfo type, Func<Type, object[]> typeValueGenerator)
        {
            return GetUnionCaseFactories(type).SelectMany(ucf => GenerateDefaultCasesFromUnionCaseFactory(ucf, typeValueGenerator));
        }

        private static IEnumerable<object> GenerateDefaultCasesFromUnionCaseFactory(MethodInfo factory, Func<Type, object[]> typeValueGenerator)
        {
            return GenerateDefaultCaseValuesFromUnionCaseFactory(factory, typeValueGenerator)
                .Select(parameterCombination => factory.Invoke(null, parameterCombination));
        }

        private static IEnumerable<object[]> GenerateDefaultCaseValuesFromUnionCaseFactory(MethodInfo factory, Func<Type, object[]> typeValueGenerator)
        {
            var parameters = factory.GetParameters();
            var values = typeValueGenerator;
            switch (parameters.Length)
            {
                case 0: return new object[][] { new object[] { } };
                case 1: return CombinationGenerator.Generate(values(parameters[0].ParameterType));
                case 2: return CombinationGenerator.Generate(values(parameters[0].ParameterType), values(parameters[1].ParameterType));
                case 3: return CombinationGenerator.Generate(values(parameters[0].ParameterType), values(parameters[1].ParameterType), values(parameters[2].ParameterType));
                case 4: return CombinationGenerator.Generate(values(parameters[0].ParameterType), values(parameters[1].ParameterType), values(parameters[2].ParameterType), values(parameters[3].ParameterType));
                case 5: return CombinationGenerator.Generate(values(parameters[0].ParameterType), values(parameters[1].ParameterType), values(parameters[2].ParameterType), values(parameters[3].ParameterType), values(parameters[4].ParameterType));
                case 6: return CombinationGenerator.Generate(values(parameters[0].ParameterType), values(parameters[1].ParameterType), values(parameters[2].ParameterType), values(parameters[3].ParameterType), values(parameters[4].ParameterType), values(parameters[5].ParameterType));
                case 7: return CombinationGenerator.Generate(values(parameters[0].ParameterType), values(parameters[1].ParameterType), values(parameters[2].ParameterType), values(parameters[3].ParameterType), values(parameters[4].ParameterType), values(parameters[5].ParameterType), values(parameters[6].ParameterType));
                case 8: return CombinationGenerator.Generate(values(parameters[0].ParameterType), values(parameters[1].ParameterType), values(parameters[2].ParameterType), values(parameters[3].ParameterType), values(parameters[4].ParameterType), values(parameters[5].ParameterType), values(parameters[6].ParameterType), values(parameters[7].ParameterType));
                case 9: return CombinationGenerator.Generate(values(parameters[0].ParameterType), values(parameters[1].ParameterType), values(parameters[2].ParameterType), values(parameters[3].ParameterType), values(parameters[4].ParameterType), values(parameters[5].ParameterType), values(parameters[6].ParameterType), values(parameters[7].ParameterType), values(parameters[8].ParameterType));
                case 10: return CombinationGenerator.Generate(values(parameters[0].ParameterType), values(parameters[1].ParameterType), values(parameters[2].ParameterType), values(parameters[3].ParameterType), values(parameters[4].ParameterType), values(parameters[5].ParameterType), values(parameters[6].ParameterType), values(parameters[7].ParameterType), values(parameters[8].ParameterType), values(parameters[9].ParameterType));
                default:
                {
                    var parameterValues = new object[parameters.Length][];
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        parameterValues[i] = typeValueGenerator(parameters[i].ParameterType);
                    }

                    return CombinationGenerator.Generate(parameterValues);
                }
            }
        }
    }
}
