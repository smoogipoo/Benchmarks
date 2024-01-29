// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    [MemoryDiagnoser]
    public class BenchmarkListRemoveFromStart
    {
        [Params(100000, 1000000, 1000000)]
        public int Items { get; set; }

        private readonly List<int> items = new List<int>();

        [GlobalSetup]
        public void GlobalSetup()
        {
            for (int i = 0; i < Items; i++)
                items.Add(i);
        }

        [Benchmark]
        public void RemoveAt()
        {
            while (items.Count > 0)
                items.RemoveAt(0);
        }
    }
}
