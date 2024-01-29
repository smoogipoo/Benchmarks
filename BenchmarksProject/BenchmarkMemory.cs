// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    [MemoryDiagnoser]
    public class BenchmarkMemory
    {
        private readonly int[] intArray = new int[10000];

        [Benchmark(Baseline = true)]
        public int IterateAsArray()
        {
            int res = 0;

            for (int i = 0; i < intArray.Length; i++)
                res += intArray[i];

            return res;
        }

        [Benchmark]
        public int IterateAsSpan()
        {
            var span = intArray.AsSpan();
            int res = 0;

            for (int i = 0; i < span.Length; i++)
                res += span[i];

            return res;
        }

        [Benchmark]
        public int IterateAsMemoryInline()
        {
            var mem = intArray.AsMemory();
            int res = 0;

            for (int i = 0; i < mem.Length; i++)
                res += mem.Span[i];

            return res;
        }

        [Benchmark]
        public int IterateAsMemoryOutOfLine()
        {
            var mem = intArray.AsMemory();
            var span = mem.Span;

            int res = 0;

            for (int i = 0; i < span.Length; i++)
                res += span[i];

            return res;
        }
    }
}
