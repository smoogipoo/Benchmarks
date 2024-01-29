// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    public class GetHashCode
    {
        [Benchmark]
        public int ObjectGetHashCode()
        {
            int x = 123456;

            for (int i = 0; i < 10000; i++)
                x = getHashCode(x);

            return x;
        }

        [Benchmark]
        public int StaticEqualityComparer()
        {
            int x = 123456;

            for (int i = 0; i < 10000; i++)
                x = EqualityComparer<int>.Default.GetHashCode(x);

            return x;
        }

        [Benchmark]
        public int CachedEqualityComparer()
        {
            var comparer = EqualityComparer<int>.Default;

            int x = 123456;

            for (int i = 0; i < 10000; i++)
                x = comparer.GetHashCode(x);

            return x;
        }

        [Benchmark]
        public int ViaRuntimeHelpers()
        {
            object x = new object();
            int result = 0;

            for (int i = 0; i < 10000; i++)
                result += RuntimeHelpers.GetHashCode(x);

            return result;
        }

        private int getHashCode<T>(T value) => value.GetHashCode();
    }
}
