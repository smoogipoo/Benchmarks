// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    [MemoryDiagnoser]
    public class HashSetContains
    {
        private readonly HashSet<int> set = new HashSet<int>();

        private IEnumerable<int> enumerable => set;
        private ICollection<int> collection => set;

        [GlobalSetup]
        public void Setup()
        {
            for (int i = 0; i < 1000; i++)
                set.Add(i);
        }

        [Benchmark]
        public bool ContainsAsSet()
        {
            bool b = false;

            for (int i = 0; i < 1000; i++)
                b |= set.Contains(i);

            return b;
        }

        [Benchmark]
        public bool ContainsAsEnumerable()
        {
            bool b = false;

            for (int i = 0; i < 1000; i++)
                b |= enumerable.Contains(i);

            return b;
        }

        [Benchmark]
        public bool ContainsAsICollection()
        {
            bool b = false;

            for (int i = 0; i < 1000; i++)
                b |= collection.Contains(i);

            return b;
        }
    }
}
