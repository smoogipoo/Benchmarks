// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Diagnostics;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    public class BenchmarkStopwatch
    {
        private readonly Stopwatch stopwatch = Stopwatch.StartNew();

        [Benchmark]
        public void Restart() => stopwatch.Restart();

        [Benchmark]
        public long ElapsedMilliseconds() => stopwatch.ElapsedMilliseconds;

        [Benchmark]
        public long ElapsedTicks() => stopwatch.ElapsedTicks;

        [Benchmark]
        public int EnvironmentTickCount() => Environment.TickCount;

        [Benchmark]
        public long EnvironmentTickCount64() => Environment.TickCount64;
    }
}
