// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    public class BenchmarkSafeHandles
    {
        [Benchmark]
        public SafeHandle CreateOwns() => new CustomSafeHandle(true);

        [Benchmark]
        public SafeHandle Create() => new CustomSafeHandle(false);

        [Benchmark]
        public void DisposeOwns() => new CustomSafeHandle(true).Dispose();

        [Benchmark]
        public void Dispose() => new CustomSafeHandle(false).Dispose();

        private class CustomSafeHandle : SafeHandle
        {
            public CustomSafeHandle(bool ownsHandle)
                : base(IntPtr.Zero, ownsHandle)
            {
            }

            protected override bool ReleaseHandle() => true;

            public override bool IsInvalid => false;
        }
    }
}
