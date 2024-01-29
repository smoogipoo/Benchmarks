// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#if NET6_0_OR_GREATER
using System.Reflection;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace BenchmarksProject
{
    [MemoryDiagnoser]
    public class BenchmarkNullabilityInfoContext
    {
        private readonly NullabilityInfoContext ctx = new NullabilityInfoContext();
        private readonly Consumer consumer = new Consumer();
        private FieldInfo fieldInfo;
#pragma warning disable CS0649
        private int field;
#pragma warning restore CS0649

        [GlobalSetup]
        public void GlobalSetup()
        {
            // BenchmarkDotNet redirects this class such that GetType() != typeof(...).
            fieldInfo = typeof(BenchmarkNullabilityInfoContext).GetField(nameof(field), BindingFlags.Instance | BindingFlags.NonPublic);
        }

        [Benchmark]
        public void CreateContext() => consumer.Consume(new NullabilityInfoContext());

        [Benchmark]
        public void GetInfo() => ctx.Create(fieldInfo);
    }
}

#endif
