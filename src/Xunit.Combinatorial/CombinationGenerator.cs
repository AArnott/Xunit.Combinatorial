// Copyright (c) parts Andrew Arnott, parts Matthew Wilkinson. All rights reserved. Licensed under the Ms-PL.

namespace Xunit
{
    using System;
    using System.Collections.Generic;

    internal static class CombinationGenerator
    {
        public static IEnumerable<object[]> Generate(params object[][] values) => Generate(values, 0);

        private static IEnumerable<object[]> Generate(object[][] values, int depth)
        {
            // If we're at the last 10 parameter elements, we can use the fast generator for the last 10 parameters
            if (values.Length - depth == 10)
            {
#pragma warning disable SA1519 // Braces must not be omitted from multi-line child statement
                for (int a = 0; a < values[depth + 0].Length; a++)
                for (int b = 0; b < values[depth + 1].Length; b++)
                for (int c = 0; c < values[depth + 2].Length; c++)
                for (int d = 0; d < values[depth + 3].Length; d++)
                for (int e = 0; e < values[depth + 4].Length; e++)
                for (int f = 0; f < values[depth + 5].Length; f++)
                for (int g = 0; g < values[depth + 6].Length; g++)
                for (int h = 0; h < values[depth + 7].Length; h++)
                for (int i = 0; i < values[depth + 8].Length; i++)
                for (int j = 0; j < values[depth + 9].Length; j++)
                {
                    var comboArr = new object[values.Length];
                    comboArr[values.Length - 10] = values[depth + 0][a];
                    comboArr[values.Length - 9] = values[depth + 1][b];
                    comboArr[values.Length - 8] = values[depth + 2][c];
                    comboArr[values.Length - 7] = values[depth + 3][d];
                    comboArr[values.Length - 6] = values[depth + 4][e];
                    comboArr[values.Length - 5] = values[depth + 5][f];
                    comboArr[values.Length - 4] = values[depth + 6][g];
                    comboArr[values.Length - 3] = values[depth + 7][h];
                    comboArr[values.Length - 2] = values[depth + 8][i];
                    comboArr[values.Length - 1] = values[depth + 9][j];
                    yield return comboArr;
                }
#pragma warning restore SA1519 // Braces must not be omitted from multi-line child statement
            }
            else
            {
                var thisDepthArr = values[depth];
                foreach (var combo in Generate(values, depth + 1))
                {
                    for (int i = 0; i < thisDepthArr.Length - 1; i++)
                    {
                        var newComboArr = new object[values.Length];
                        Array.Copy(combo, depth, newComboArr, depth, values.Length - depth);
                        newComboArr[depth] = thisDepthArr[i];
                        yield return newComboArr;
                    }

                    combo[depth] = thisDepthArr[thisDepthArr.Length - 1];
                    yield return combo;
                }
            }
        }

#pragma warning disable SA1519 // Braces must not be omitted from multi-line child statement

        public static IEnumerable<object[]> Generate(object[] pos0Values)
        {
            for (int a = 0; a < pos0Values.Length; a++)
            {
                yield return new[] { pos0Values[a] };
            }
        }

        public static IEnumerable<object[]> Generate(object[] pos0Values, object[] pos1Values)
        {
            for (int a = 0; a < pos0Values.Length; a++)
            for (int b = 0; b < pos1Values.Length; b++)
            {
                yield return new[] { pos0Values[a], pos1Values[b] };
            }
        }

        public static IEnumerable<object[]> Generate(object[] pos0Values, object[] pos1Values, object[] pos2Values)
        {
            for (int a = 0; a < pos0Values.Length; a++)
            for (int b = 0; b < pos1Values.Length; b++)
            for (int c = 0; c < pos2Values.Length; c++)
            {
                yield return new[] { pos0Values[a], pos1Values[b], pos2Values[c] };
            }
        }

        public static IEnumerable<object[]> Generate(object[] pos0Values, object[] pos1Values, object[] pos2Values, object[] pos3Values)
        {
            for (int a = 0; a < pos0Values.Length; a++)
            for (int b = 0; b < pos1Values.Length; b++)
            for (int c = 0; c < pos2Values.Length; c++)
            for (int d = 0; d < pos3Values.Length; d++)
            {
                yield return new[] { pos0Values[a], pos1Values[b], pos2Values[c], pos3Values[d] };
            }
        }

        public static IEnumerable<object[]> Generate(object[] pos0Values, object[] pos1Values, object[] pos2Values, object[] pos3Values, object[] pos4Values)
        {
            for (int a = 0; a < pos0Values.Length; a++)
            for (int b = 0; b < pos1Values.Length; b++)
            for (int c = 0; c < pos2Values.Length; c++)
            for (int d = 0; d < pos3Values.Length; d++)
            for (int e = 0; e < pos4Values.Length; e++)
            {
                yield return new[] { pos0Values[a], pos1Values[b], pos2Values[c], pos3Values[d], pos4Values[e] };
            }
        }

        public static IEnumerable<object[]> Generate(object[] pos0Values, object[] pos1Values, object[] pos2Values, object[] pos3Values, object[] pos4Values, object[] pos5Values)
        {
            for (int a = 0; a < pos0Values.Length; a++)
            for (int b = 0; b < pos1Values.Length; b++)
            for (int c = 0; c < pos2Values.Length; c++)
            for (int d = 0; d < pos3Values.Length; d++)
            for (int e = 0; e < pos4Values.Length; e++)
            for (int f = 0; f < pos5Values.Length; f++)
            {
                yield return new[] { pos0Values[a], pos1Values[b], pos2Values[c], pos3Values[d], pos4Values[e], pos5Values[f] };
            }
        }

        public static IEnumerable<object[]> Generate(object[] pos0Values, object[] pos1Values, object[] pos2Values, object[] pos3Values, object[] pos4Values, object[] pos5Values, object[] pos6Values)
        {
            for (int a = 0; a < pos0Values.Length; a++)
            for (int b = 0; b < pos1Values.Length; b++)
            for (int c = 0; c < pos2Values.Length; c++)
            for (int d = 0; d < pos3Values.Length; d++)
            for (int e = 0; e < pos4Values.Length; e++)
            for (int f = 0; f < pos5Values.Length; f++)
            for (int g = 0; g < pos6Values.Length; g++)
            {
                yield return new[] { pos0Values[a], pos1Values[b], pos2Values[c], pos3Values[d], pos4Values[e], pos5Values[f], pos6Values[g] };
            }
        }

        public static IEnumerable<object[]> Generate(object[] pos0Values, object[] pos1Values, object[] pos2Values, object[] pos3Values, object[] pos4Values, object[] pos5Values, object[] pos6Values, object[] pos7Values)
        {
            for (int a = 0; a < pos0Values.Length; a++)
            for (int b = 0; b < pos1Values.Length; b++)
            for (int c = 0; c < pos2Values.Length; c++)
            for (int d = 0; d < pos3Values.Length; d++)
            for (int e = 0; e < pos4Values.Length; e++)
            for (int f = 0; f < pos5Values.Length; f++)
            for (int g = 0; g < pos6Values.Length; g++)
            for (int h = 0; h < pos7Values.Length; h++)
            {
                yield return new[] { pos0Values[a], pos1Values[b], pos2Values[c], pos3Values[d], pos4Values[e], pos5Values[f], pos6Values[g], pos7Values[h] };
            }
        }

        public static IEnumerable<object[]> Generate(object[] pos0Values, object[] pos1Values, object[] pos2Values, object[] pos3Values, object[] pos4Values, object[] pos5Values, object[] pos6Values, object[] pos7Values, object[] pos8Values)
        {
            for (int a = 0; a < pos0Values.Length; a++)
            for (int b = 0; b < pos1Values.Length; b++)
            for (int c = 0; c < pos2Values.Length; c++)
            for (int d = 0; d < pos3Values.Length; d++)
            for (int e = 0; e < pos4Values.Length; e++)
            for (int f = 0; f < pos5Values.Length; f++)
            for (int g = 0; g < pos6Values.Length; g++)
            for (int h = 0; h < pos7Values.Length; h++)
            for (int i = 0; i < pos8Values.Length; i++)
            {
                yield return new[] { pos0Values[a], pos1Values[b], pos2Values[c], pos3Values[d], pos4Values[e], pos5Values[f], pos6Values[g], pos7Values[h], pos8Values[i] };
            }
        }

        public static IEnumerable<object[]> Generate(object[] pos0Values, object[] pos1Values, object[] pos2Values, object[] pos3Values, object[] pos4Values, object[] pos5Values, object[] pos6Values, object[] pos7Values, object[] pos8Values, object[] pos9Values)
        {
            for (int a = 0; a < pos0Values.Length; a++)
            for (int b = 0; b < pos1Values.Length; b++)
            for (int c = 0; c < pos2Values.Length; c++)
            for (int d = 0; d < pos3Values.Length; d++)
            for (int e = 0; e < pos4Values.Length; e++)
            for (int f = 0; f < pos5Values.Length; f++)
            for (int g = 0; g < pos6Values.Length; g++)
            for (int h = 0; h < pos7Values.Length; h++)
            for (int i = 0; i < pos8Values.Length; i++)
            for (int j = 0; j < pos9Values.Length; j++)
            {
                yield return new[] { pos0Values[a], pos1Values[b], pos2Values[c], pos3Values[d], pos4Values[e], pos5Values[f], pos6Values[g], pos7Values[h], pos8Values[i], pos9Values[j] };
            }
        }

#pragma warning restore SA1519 // Braces must not be omitted from multi-line child statement
    }
}
