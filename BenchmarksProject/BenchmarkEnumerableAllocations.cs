// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    [MemoryDiagnoser]
    public class BenchmarkEnumerableAllocations
    {
        private readonly int[] arr = { 1, 2, 3 };

        [Benchmark]
        public int EnumerateList() => getEnumerable(arr).FirstOrDefault();

        [Benchmark]
        public int EnumerateEmpty() => getEnumerable(null).FirstOrDefault();

        [MethodImpl(MethodImplOptions.NoInlining)]
        private IEnumerable<int> getEnumerable(IEnumerable<int> target)
            => target ?? Enumerable.Empty<int>();
    }
}
