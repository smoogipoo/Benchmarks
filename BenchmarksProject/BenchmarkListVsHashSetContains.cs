// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    [MemoryDiagnoser]
    public class BenchmarkListVsHashSetContains
    {
        [Params(1, 10, 100, 1000)]
        public int N { get; set; }

        private TestedClass<MyEnum> testedClass;

        [GlobalSetup]
        public void GlobalSetup()
        {
            testedClass = new TestedClass<MyEnum>(N);
        }

        [Benchmark]
        public bool List() => testedClass.List();

        [Benchmark]
        public bool HashSet() => testedClass.HashSet();

        private enum MyEnum
        {
            First
        }

        private class TestedClass<T>
            where T : struct
        {
            private readonly HashSet<T> hashSet1 = new HashSet<T>();
            private readonly HashSet<T> hashSet2 = new HashSet<T>();

            private readonly List<T> list1 = new List<T>();
            private readonly List<T> list2 = new List<T>();

            public TestedClass(int n)
            {
                hashSet1.Clear();
                hashSet2.Clear();
                list1.Clear();
                list2.Clear();

                for (int i = 0; i < n; i++)
                {
                    hashSet1.Add((T)(object)i);
                    hashSet2.Add((T)(object)(n + i));
                    list1.Add((T)(object)i);
                    list2.Add((T)(object)(n + i));
                }
            }

            public bool List()
            {
                foreach (var v in list1)
                {
                    if (list2.Contains(v))
                        return true;
                }

                return false;
            }

            public bool HashSet()
            {
                foreach (var v in hashSet1)
                {
                    if (hashSet2.Contains(v))
                        return true;
                }

                return false;
            }
        }
    }
}
