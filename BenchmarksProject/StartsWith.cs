// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    [MemoryDiagnoser]
    public class StartsWith
    {
        private static ReadOnlySpan<byte> commentStart => new[] { (byte)'/', (byte)'/' };

        private const string target_string = "// abc";

        [Benchmark]
        public bool ByInlineString()
        {
            bool b = false;
            for (int i = 0; i < 1000; i++)
                b |= byInlineString();
            return b;
        }

        [Benchmark]
        public bool ByReadOnlySpanOfByte()
        {
            bool b = false;
            for (int i = 0; i < 1000; i++)
                b |= byReadOnlySpanOfByte();
            return b;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private bool byInlineString() => target_string.AsSpan().TrimStart().StartsWith("//".AsSpan(), StringComparison.Ordinal);

        [MethodImpl(MethodImplOptions.NoInlining)]
        private bool byReadOnlySpanOfByte() => MemoryMarshal.AsBytes(target_string.AsSpan().TrimStart()).StartsWith(commentStart);
    }
}
