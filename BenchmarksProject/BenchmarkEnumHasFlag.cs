// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    [MemoryDiagnoser]
    public class BenchmarkEnumHasFlag
    {
        [Benchmark]
        public bool HasFlag()
        {
            bool result = false;

            for (int i = 0; i < 1000000; i++)
                result |= getFlags(i).HasFlag((FlagsEnum)i);

            return result;
        }

        [Benchmark]
        public bool BitwiseAnd()
        {
            bool result = false;

            for (int i = 0; i < 1000000; i++)
                result |= (getFlags(i) & (FlagsEnum)i) > 0;

            return result;
        }

        [Benchmark]
        public bool HasFlagFast()
        {
            bool result = false;

            for (int i = 0; i < 1000000; i++)
                result |= hasFlagFast(getFlags(i), (FlagsEnum)i);

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe bool hasFlagFast<T>(T value, T flag) where T : unmanaged, Enum
        {
            // Note: Using a switch statement would eliminate inlining.

            if (sizeof(T) == 1)
            {
                var value1 = Unsafe.As<T, byte>(ref value);
                var value2 = Unsafe.As<T, byte>(ref flag);
                return (value1 & value2) > 0;
            }

            if (sizeof(T) == 2)
            {
                var value1 = Unsafe.As<T, short>(ref value);
                var value2 = Unsafe.As<T, short>(ref flag);
                return (value1 & value2) > 0;
            }

            if (sizeof(T) == 4)
            {
                var value1 = Unsafe.As<T, int>(ref value);
                var value2 = Unsafe.As<T, int>(ref flag);
                return (value1 & value2) > 0;
            }

            if (sizeof(T) == 8)
            {
                var value1 = Unsafe.As<T, long>(ref value);
                var value2 = Unsafe.As<T, long>(ref flag);
                return (value1 & value2) > 0;
            }

            throw new ArgumentException($"Invalid enum type provided: {typeof(T)}.");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private FlagsEnum getFlags(int i) => (FlagsEnum)i;

        [Flags]
        private enum FlagsEnum
        {
            Flag1 = 1,
            Flag2 = 2,
            Flag3 = 4,
            Flag4 = 8
        }
    }
}
