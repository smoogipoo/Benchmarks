// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    public class BenchmarkDisposal
    {
        [Benchmark]
        public void TestBasicDisposal() => new A().Dispose();

        [Benchmark]
        public void TestWithSuppressFinalization() => new B().Dispose();

        private class A : IDisposable
        {
            public void Dispose()
            {
            }
        }

        private class B : IDisposable
        {
            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }
    }
}
