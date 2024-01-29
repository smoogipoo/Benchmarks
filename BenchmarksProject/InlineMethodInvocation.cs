// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    [MemoryDiagnoser]
    public class InlineMethodInvocation
    {
        [Benchmark]
        public int ByInstanceMethod()
        {
            int res = 0;

            for (int i = 0; i < 10000; i++)
                res += q(n);

            return res;
        }

        [Benchmark]
        public int ByStaticMethod()
        {
            int res = 0;

            for (int i = 0; i < 10000; i++)
                res += q(n2);

            return res;
        }

        [Benchmark]
        public int ByInstanceLambda()
        {
            int res = 0;

            for (int i = 0; i < 10000; i++)
                res += q(() => n());

            return res;
        }

        [Benchmark]
        public int ByStaticLambda()
        {
            int res = 0;

            for (int i = 0; i < 10000; i++)
                res += q(() => n2());

            return res;
        }

        private void n()
        {
        }

        private static void n2()
        {
        }

        private int q(Action a) => 1;
    }
}
