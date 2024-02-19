// | Method            | Mean     | Error     | StdDev    | Allocated |
// |------------------ |---------:|----------:|----------:|----------:|
// | EmptyAsStatic     | 3.162 us | 0.0060 us | 0.0056 us |         - |
// | EmptyAsExpression | 3.169 us | 0.0219 us | 0.0194 us |         - |

#if NET8_0_OR_GREATER

using System;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    [MemoryDiagnoser]
    public class BenchmarkCollectionExpression
    {
        [Benchmark]
        public int EmptyAsStatic()
        {
            int length = 0;

            for (int i = 0; i < 10000; i++)
                length += getArray().Length;

            return length;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static int[] getArray() => Array.Empty<int>();
        }

        [Benchmark]
        public int EmptyAsExpression()
        {
            int length = 0;

            for (int i = 0; i < 10000; i++)
                length += getArray().Length;

            return length;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static int[] getArray() => [];
        }
    }
}

#endif
