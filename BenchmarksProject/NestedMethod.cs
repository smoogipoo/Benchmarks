// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    [MemoryDiagnoser]
    public class NestedMethod
    {
        private int value;

        [Benchmark]
        public int NonStaticCapture()
        {
            for (int i = 0; i < 1000; i++)
                updateNonStatic(1);

            return value;
        }

        [Benchmark]
        public int Static()
        {
            for (int i = 0; i < 1000; i++)
                updateStatic(1);

            return value;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void updateNonStatic(int add)
        {
            update();
            void update() => value += add;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void updateStatic(int add)
        {
            update(this, add);
            static void update(NestedMethod c, int add) => c.value += add;
        }
    }
}
