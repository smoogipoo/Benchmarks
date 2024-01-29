// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    [MemoryDiagnoser]
    public class ArrayCopyVsSpanCopy
    {
        private readonly int[] array1 = new int[1024];
        private readonly int[] array2 = new int[1024];

        [Benchmark]
        public void CopySpan() => array1.AsSpan().CopyTo(array2);

        [Benchmark]
        public void CopyArray() => array1.CopyTo(array2, 0);
    }
}
