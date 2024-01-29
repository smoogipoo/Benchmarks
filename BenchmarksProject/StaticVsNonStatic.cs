// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    public class StaticVsNonStatic
    {
        [Benchmark]
        public int CreateWithStaticConstructor()
        {
            int result = 0;

            for (int i = 0; i < 10000; i++)
                result += new ClassWithStaticCtor().Value;

            return result;
        }

        [Benchmark]
        public int CreateWithoutStaticConstructor()
        {
            int result = 0;

            for (int i = 0; i < 10000; i++)
                result += new ClassWithoutStaticCtor().Value;

            return result;
        }

        private class ClassWithStaticCtor
        {
            // ReSharper disable once ConvertToConstant.Local
            public readonly int Value = 1;

            static ClassWithStaticCtor()
            {
            }
        }

        private class ClassWithoutStaticCtor
        {
            // ReSharper disable once ConvertToConstant.Local
            public readonly int Value = 1;
        }
    }
}
