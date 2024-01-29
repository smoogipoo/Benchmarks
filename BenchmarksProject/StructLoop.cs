// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    [MemoryDiagnoser]
    public class StructLoop
    {
        private readonly List<Class> list1 = new List<Class>();
        private readonly List<Struct> list2 = new List<Struct>();
        private readonly List<ReadOnlyStruct> list3 = new List<ReadOnlyStruct>();

        [GlobalSetup]
        public void GlobalSetup()
        {
            for (int i = 0; i < 100_000; i++)
            {
                list1.Add(new Class(1));
                list2.Add(new Struct(1));
                list3.Add(new ReadOnlyStruct(1));
            }
        }

        [Benchmark]
        public int ClassForEach()
        {
            int result = 0;

            foreach (var x in list1)
                result += x.I;

            return result;
        }

        [Benchmark]
        public int ClassFor()
        {
            int result = 0;

            for (int i = 0; i < list1.Count; i++)
                result += list1[i].I;

            return result;
        }

        [Benchmark]
        public int StructForEach()
        {
            int result = 0;

            foreach (var x in list2)
                result += x.I;

            return result;
        }

        [Benchmark]
        public int StructFor()
        {
            int result = 0;

            for (int i = 0; i < list2.Count; i++)
                result += list2[i].I;

            return result;
        }

        [Benchmark]
        public int ReadOnlyStructForEach()
        {
            int result = 0;

            foreach (var x in list3)
                result += x.I;

            return result;
        }

        [Benchmark]
        public int ReadOnlyStructFor()
        {
            int result = 0;

            for (int i = 0; i < list3.Count; i++)
                result += list3[i].I;

            return result;
        }

        private struct Struct
        {
            public readonly int I;

            public Struct(int i)
            {
                I = i;
            }
        }

        private readonly struct ReadOnlyStruct
        {
            public readonly int I;

            public ReadOnlyStruct(int i)
            {
                I = i;
            }
        }

        private class Class
        {
            public readonly int I;

            public Class(int i)
            {
                I = i;
            }
        }
    }
}
