// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    public class SystemNumericsIntrinsics
    {
        [Benchmark]
        public void MatMulManual()
        {
        }

        [Benchmark]
        public void MatMulSystemNumerics()
        {
        }
    }
}
