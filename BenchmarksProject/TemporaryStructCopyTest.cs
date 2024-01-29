// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    public class TemporaryStructCopyTest
    {
        private readonly TestStruct[] structs = new TestStruct[10000];

        [GlobalSetup]
        public void GlobalSetup()
        {
            for (int i = 0; i < structs.Length; i++)
            {
                structs[i] = new TestStruct
                {
                    Value = 1,
                    Padding = new byte[128]
                };
            }
        }

        [Benchmark]
        public int WithTemporaryCopy()
        {
            int ret = 0;

            for (int i = 0; i < structs.Length; i++)
            {
                for (int j = 0; j < 10; j++)
                    ret += structs[i].Value;
            }

            return ret;
        }

        [Benchmark]
        public int WithLocalCopy()
        {
            int ret = 0;

            for (int i = 0; i < structs.Length; i++)
            {
                TestStruct s = structs[i];

                for (int j = 0; j < 10; j++)
                    ret += s.Value;
            }

            return ret;
        }

        [Benchmark]
        public int WithRefLocal()
        {
            int ret = 0;

            for (int i = 0; i < structs.Length; i++)
            {
                ref TestStruct s = ref structs[i];

                for (int j = 0; j < 10; j++)
                    ret += s.Value;
            }

            return ret;
        }

        private struct TestStruct
        {
            public int Value { get; set; }

            public byte[] Padding { get; set; }
        }
    }
}
