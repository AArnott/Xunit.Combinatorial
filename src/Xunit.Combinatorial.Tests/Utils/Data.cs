// Copyright (c) parts Andrew Arnott, parts Matthew Wilkinson. All rights reserved. Licensed under the Ms-PL.

namespace Xunit.Combinatorial.Tests.Utils
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Xunit.Sdk;

    internal static class Data
    {
        private static object[][] Generate(DataAttribute dataAttr, MethodInfo method) => dataAttr.GetData(method).ToArray();

        public static object[][] Generate(DataAttribute dataAttr, Action methodDelegate) => Generate(dataAttr, methodDelegate.GetMethodInfo());
        public static object[][] Generate<T1>(DataAttribute dataAttr, Action<T1> methodDelegate) => Generate(dataAttr, methodDelegate.GetMethodInfo());
        public static object[][] Generate<T1, T2>(DataAttribute dataAttr, Action<T1, T2> methodDelegate) => Generate(dataAttr, methodDelegate.GetMethodInfo());
        public static object[][] Generate<T1, T2, T3>(DataAttribute dataAttr, Action<T1, T2, T3> methodDelegate) => Generate(dataAttr, methodDelegate.GetMethodInfo());
        public static object[][] Generate<T1, T2, T3, T4>(DataAttribute dataAttr, Action<T1, T2, T3, T4> methodDelegate) => Generate(dataAttr, methodDelegate.GetMethodInfo());
        public static object[][] Generate<T1, T2, T3, T4, T5>(DataAttribute dataAttr, Action<T1, T2, T3, T4, T5> methodDelegate) => Generate(dataAttr, methodDelegate.GetMethodInfo());
        public static object[][] Generate<T1, T2, T3, T4, T5, T6>(DataAttribute dataAttr, Action<T1, T2, T3, T4, T5, T6> methodDelegate) => Generate(dataAttr, methodDelegate.GetMethodInfo());
        public static object[][] Generate<T1, T2, T3, T4, T5, T6, T7>(DataAttribute dataAttr, Action<T1, T2, T3, T4, T5, T6, T7> methodDelegate) => Generate(dataAttr, methodDelegate.GetMethodInfo());
        public static object[][] Generate<T1, T2, T3, T4, T5, T6, T7, T8>(DataAttribute dataAttr, Action<T1, T2, T3, T4, T5, T6, T7, T8> methodDelegate) => Generate(dataAttr, methodDelegate.GetMethodInfo());
        public static object[][] Generate<T1, T2, T3, T4, T5, T6, T7, T8, T9>(DataAttribute dataAttr, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> methodDelegate) => Generate(dataAttr, methodDelegate.GetMethodInfo());
    }
}
