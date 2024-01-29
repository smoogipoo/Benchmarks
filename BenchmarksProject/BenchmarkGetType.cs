// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    [MemoryDiagnoser]
    public class BenchmarkGetType
    {
        private object obj;

        [GlobalSetup]
        public void GlobalSetup()
        {
            obj = new CustomType { Value = 1 };
        }

        [Benchmark]
        public int ViaGetType()
        {
            int result = 0;
            for (int i = 0; i < 10000; i++)
                // ReSharper disable once OperatorIsCanBeUsed
                result += obj.GetType() == typeof(CustomType) ? ((CustomType)obj).Value : 0;
            return result;
        }

        [Benchmark]
        public int ViaIsOperator()
        {
            int result = 0;
            for (int i = 0; i < 10000; i++)
                result += obj is CustomType ct ? ct.Value : 0;
            return result;
        }

        private struct CustomType
        {
            public int Value;
        }
    }
}
