// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    [MemoryDiagnoser]
    public class ReadyOnlyCharSpanEquality
    {
        private static ReadOnlySpan<char> span => "1";

        [Benchmark]
        public bool BenchmarkEquals() => compareByEquals(span);

        [Benchmark]
        public bool BenchmarkSequenceEquals() => compareBySequenceEquals(span);

        [Benchmark]
        public bool BenchmarkIs() => compareByIs(span);

        private static bool compareByEquals(ReadOnlySpan<char> span) => span == "1";
        private static bool compareBySequenceEquals(ReadOnlySpan<char> span) => span.SequenceEqual("1");
        private static bool compareByIs(ReadOnlySpan<char> span) => span is "1";
    }
}
