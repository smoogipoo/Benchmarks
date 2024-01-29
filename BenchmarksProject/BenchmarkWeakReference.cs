// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    public class BenchmarkWeakReference
    {
        private readonly object[] directObjects = new object[100000];
        private readonly WeakReference<object>[] weakRefObjects = new WeakReference<object>[100000];

        [GlobalSetup]
        public void GlobalSetup()
        {
            for (int i = 0; i < directObjects.Length; i++)
            {
                directObjects[i] = new object();
                weakRefObjects[i] = new WeakReference<object>(directObjects[i]);
            }
        }

        [Benchmark]
        public int TestDirectAccess()
        {
            int count = 0;

            for (int i = 0; i < directObjects.Length; i++)
            {
                if (directObjects[i] != null)
                    count++;
            }

            return count;
        }

        [Benchmark]
        public int TestWeakReferenceAccess()
        {
            int count = 0;

            for (int i = 0; i < weakRefObjects.Length; i++)
            {
                if (weakRefObjects[i].TryGetTarget(out _))
                    count++;
            }

            return count;
        }
    }
}
