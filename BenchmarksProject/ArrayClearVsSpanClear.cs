// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    public class ArrayClearVsSpanClear
    {
        [Params(2, 4, 8, 16, 32)]
        public int N { get; set; }

        private int[] array = new int[3];

        [GlobalSetup]
        public void GlobalSetup()
        {
            array = new int[N];
        }

        [Benchmark(Baseline = true)]
        public int[] ArrayClear()
        {
            Array.Clear(array, 0, array.Length);
            return array;
        }

        [Benchmark]
        public int[] SpanClear()
        {
            array.AsSpan().Clear();
            return array;
        }
    }
}
