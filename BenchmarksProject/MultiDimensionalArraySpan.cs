// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    [MemoryDiagnoser]
    public class MultiDimensionalArraySpan
    {
        private int[,] arr =
        {
            { 1, 1 },
            { 1, 1 }
        };

        [Benchmark]
        public int ViaUnsafe()
        {
            int totalLength = 0;

            for (int i = 0; i < 10000; i++)
                totalLength += asSpanViaUnsafe(arr).Length;

            return totalLength;

            static unsafe Span<int> asSpanViaUnsafe(int[,] array)
            {
                fixed (int* arrP = array)
                    return new Span<int>(arrP, array.Length);
            }
        }

        [Benchmark]
        public int ViaMemoryMarshal()
        {
            int totalLength = 0;

            for (int i = 0; i < 10000; i++)
                totalLength += asSpanViaMarshal<int>(arr).Length;

            return totalLength;

            static Span<T> asSpanViaMarshal<T>(Array array) => MemoryMarshal.CreateSpan(ref Unsafe.As<byte, T>(ref MemoryMarshal.GetArrayDataReference(array)), array.Length);
        }
    }
}
