// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#if NETCOREAPP

using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    [MemoryDiagnoser]
    public class BenchmarkListEnumerator
    {
        private readonly List<int> list = new List<int> { 0, 1, 2, 3, 4, 5, 3, 2, 3, 1, 4, 5, -1 };
        private IList<int> iList;

        [GlobalSetup]
        public void GlobalSetup()
        {
            iList = list;
        }

        [Benchmark(Baseline = true)]
        public int List()
        {
            int sum = 0;

            for (int i = 0; i < 1000; i++)
            {
                foreach (var v in list)
                    sum += v;
            }

            return sum;
        }

        [Benchmark]
        public int ListAsReadOnly()
        {
            int sum = 0;

            for (int i = 0; i < 1000; i++)
            {
                foreach (var v in list.AsReadOnly())
                    sum += v;
            }

            return sum;
        }

        [Benchmark]
        public int ListAsSlimReadOnly()
        {
            int sum = 0;

            for (int i = 0; i < 1000; i++)
            {
                foreach (var v in list.AsSlimReadOnly())
                    sum += v;
            }

            return sum;
        }

        [Benchmark]
        // ReSharper disable once InconsistentNaming
        public int IList()
        {
            int sum = 0;

            for (int i = 0; i < 1000; i++)
            {
                foreach (var v in iList)
                    sum += v;
            }

            return sum;
        }

        [Benchmark]
        public int IListAsSlimReadOnly()
        {
            int sum = 0;

            for (int i = 0; i < 1000; i++)
            {
                foreach (var v in iList.AsSlimReadOnly())
                    sum += v;
            }

            return sum;
        }
    }
}

#endif