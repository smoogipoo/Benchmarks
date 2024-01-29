// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    [MemoryDiagnoser]
    public class BenchmarkNullable
    {
        private readonly ConcurrentDictionary<Type, Type> cache = new ConcurrentDictionary<Type, Type>();

        [Benchmark]
        public Type NonNullable() => System.Nullable.GetUnderlyingType(typeof(int));

        [Benchmark]
        public Type Nullable() => System.Nullable.GetUnderlyingType(typeof(int?));

        // ReSharper disable once ConvertClosureToMethodGroup
        [Benchmark]
        public Type NullableCached() => cache.GetOrAdd(typeof(int?), t => System.Nullable.GetUnderlyingType(t));
    }
}
