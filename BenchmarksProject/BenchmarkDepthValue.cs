// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    public class BenchmarkDepthValue
    {
        [Benchmark]
        public float BenchmarkCurrent()
        {
            var dv = new DepthValueCurrent();

            for (int i = 0; i < 1000; i++)
                dv.Increment();

            return dv;
        }

        [Benchmark]
        public float BenchmarkNew()
        {
            var dv = new DepthValueNew();

            for (int i = 0; i < 1000; i++)
                dv.Increment();

            return dv;
        }

        private class DepthValueNew
        {
            private const ushort minus_one = 0xBC00;
            private const ushort plus_one = 0x3C00;
            private const ushort negative_zero = 0x8000;

            private ushort current = minus_one;

            /// <summary>
            /// Increments the depth value.
            /// </summary>
            /// <returns>The post-incremented depth value.</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal float Increment()
            {
                if (current == plus_one)
                    return ushortToFloat(current);

                if (current == negative_zero)
                {
                    // If -0.0, the next value should be +eps, otherwise we'll have doubling.
                    return ushortToFloat(current = 1);
                }

                // More "negative" values (> negative_zero) need to be decremented.
                // More "positive" values (< negative_zero) need to be incremented.
                // Zero case handled above.

                if (current > negative_zero)
                    current--;
                else
                    current++;

                return ushortToFloat(current);
            }

            /// <summary>
            /// Whether the depth value can be incremented.
            /// </summary>
            internal bool CanIncrement
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => current != plus_one;
            }

            private static unsafe float ushortToFloat(ushort value)
            {
                // We need to convert the 16b value to 32b. This is done per-section (sign, exponent, mantissa).
                uint bits = (uint)
                (
                    ((value & 0x8000) << 16)
                    | (((value & 0x7C00) + 0x1C000) << 13)
                    | ((value & 0x3FF) << 13)
                );

                return *((float*)&bits);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static implicit operator float(DepthValueNew d) => d.current;
        }

        private class DepthValueCurrent
        {
            private const float increment = 0.001f;
            private const int max_count = 1999;

            private float depth = -1;
            private int count;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal float Increment()
            {
                if (count == max_count)
                    return depth;

                depth += increment;
                count++;

                return depth;
            }

            internal bool CanIncrement
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => count < max_count;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static implicit operator float(DepthValueCurrent d) => d.depth;
        }
    }
}
