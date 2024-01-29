// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Collections.Immutable;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    [MemoryDiagnoser]
    public class BenchmarkImmutableArray
    {
        // [Benchmark]
        // public ImmutableArray<int> ArrayToImmutable() => new[] { 1, 2, 3 }.ToImmutableArray();
        //
        // [Benchmark]
        // public ImmutableArray<int> CreateImmutable() => ImmutableArray.Create(1, 2, 3);

        [Benchmark]
        public ImmutableArray<int> ListThenToArray()
        {
            var list = new List<int>();

            for (int i = 0; i < 10; i++)
                list.Add(i);

            return list.ToImmutableArray();
        }

        [Benchmark]
        public ImmutableArray<int> ImmutableBuilder()
        {
            var builder = ImmutableArray.CreateBuilder<int>();

            for (int i = 0; i < 10; i++)
                builder.Add(i);

            return builder.ToImmutableArray();
        }
    }
}
