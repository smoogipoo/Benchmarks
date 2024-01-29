// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    [MemoryDiagnoser]
    public class BenchmarkDevirtualisation
    {
        private readonly NormalClass normalClass = new NormalClass { Value = 5 };
        private readonly IInterface normalClassAsInterface = new NormalClass { Value = 5 };
        private readonly ClassWithVirtual classWithVirtual = new ClassWithVirtual { Value = 5 };
        private readonly IInterface classWithVirtualAsInterface = new ClassWithVirtual { Value = 5 };
        private readonly SealedClass sealedClass = new SealedClass { Value = 5 };
        private readonly ClassWithVirtual sealedClassAsBase = new SealedClass { Value = 5 };
        private readonly IInterface sealedClassAsInterface = new SealedClass { Value = 5 };
        private readonly Struct @struct = new Struct { Value = 5 };
        private readonly IInterface structAsInterface = new Struct { Value = 5 };

        [Benchmark]
        public int BenchmarkNormalClass()
        {
            int value = 0;

            for (int i = 0; i < 10000; i++)
                value += normalClass.Value;

            return value;
        }

        [Benchmark]
        public int BenchmarkNormalClassAsInterface()
        {
            int value = 0;

            for (int i = 0; i < 10000; i++)
                value += normalClassAsInterface.Value;

            return value;
        }

        [Benchmark]
        public int BenchmarkClassWithVirtual()
        {
            int value = 0;

            for (int i = 0; i < 10000; i++)
                value += classWithVirtual.Value;

            return value;
        }

        [Benchmark]
        public int BenchmarkClassWithVirtualAsInterface()
        {
            int value = 0;

            for (int i = 0; i < 10000; i++)
                value += classWithVirtualAsInterface.Value;

            return value;
        }

        [Benchmark]
        public int BenchmarkSealedClass()
        {
            int value = 0;

            for (int i = 0; i < 10000; i++)
                value += sealedClass.Value;

            return value;
        }

        [Benchmark]
        public int BenchmarkSealedClassAsBase()
        {
            int value = 0;

            for (int i = 0; i < 10000; i++)
                value += sealedClassAsBase.Value;

            return value;
        }

        [Benchmark]
        public int BenchmarkSealedClassAsInterface()
        {
            int value = 0;

            for (int i = 0; i < 10000; i++)
                value += sealedClassAsInterface.Value;

            return value;
        }

        [Benchmark]
        public int BenchmarkStruct()
        {
            int value = 0;

            for (int i = 0; i < 10000; i++)
                value += @struct.Value;

            return value;
        }

        [Benchmark]
        public int BenchmarkStructAsInterface()
        {
            int value = 0;

            for (int i = 0; i < 10000; i++)
                value += structAsInterface.Value;

            return value;
        }

        private interface IInterface
        {
            int Value { get; set; }
        }

        private class NormalClass : IInterface
        {
            public int Value { get; set; }
        }

        private class ClassWithVirtual : IInterface
        {
            public virtual int Value { get; set; }
        }

        private sealed class SealedClass : ClassWithVirtual
        {
        }

        private struct Struct : IInterface
        {
            public int Value { get; set; }
        }
    }
}
