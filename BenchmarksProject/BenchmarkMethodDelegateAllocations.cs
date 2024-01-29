// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    [MemoryDiagnoser]
    public class BenchmarkMethodDelegateAllocations
    {
        private readonly List<int> list = new List<int> { 1, 2, 3 };
        private int field = 2;

        [Benchmark(Baseline = true)]
        public int SafeMethod()
        {
            int res = 0;

            for (int i = 0; i < 1000; i++)
                res += safeMethod();

            return res;
        }

        [Benchmark]
        public int AllocatingMethod()
        {
            int res = 0;

            for (int i = 0; i < 1000; i++)
                res += allocatingMethod();

            return res;
        }

        private int safeMethod()
        {
            return 0;

            int local = 2;
            return list.Count(_ => 0 > local);
        }

        private int allocatingMethod()
        {
            return 0;

            int local = 2;
            return list.Count(_ => field > local);
        }
    }
}
