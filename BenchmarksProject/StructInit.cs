// | Method                | Mean     | Error     | StdDev    |
// |---------------------- |---------:|----------:|----------:|
// | RecordInit            | 1.371 ns | 0.0057 ns | 0.0051 ns |
// | CtorInit              | 1.381 ns | 0.0068 ns | 0.0060 ns |
// | CtorInitPartial       | 1.336 ns | 0.0009 ns | 0.0008 ns |
// | SkipLocalsInit        | 1.333 ns | 0.0039 ns | 0.0036 ns |
// | SkipLocalsInitPartial | 1.303 ns | 0.0016 ns | 0.0014 ns |

using System;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace BenchmarksProject
{
    public class StructInit
    {
        private readonly Consumer consumer = new Consumer();

        [Benchmark]
        public void RecordInit() => consumer.Consume(new RecordStruct(1, 1));

        [Benchmark]
        public void CtorInit() => consumer.Consume(new CtorStruct(1, 1));

        [Benchmark]
        public void CtorInitPartial() => consumer.Consume(new CtorStruct(1));

        [Benchmark]
        public void SkipLocalsInit() => consumer.Consume(SkipLocalsInitStruct.Init(1, 1));

        [Benchmark]
        public void SkipLocalsInitPartial() => consumer.Consume(SkipLocalsInitStruct.Init(1));

        private readonly record struct RecordStruct(int A, int B);

        private readonly struct CtorStruct
        {
            public readonly int A;
            public readonly int B;

            public CtorStruct(int a, int b)
            {
                A = a;
                B = b;
            }

            public CtorStruct(int a)
            {
                A = a;
            }
        }

        private struct SkipLocalsInitStruct
        {
            public int A;
            public int B;

            [Obsolete("no", true)]
            public SkipLocalsInitStruct()
            {
            }

            public static SkipLocalsInitStruct Init(int a, int b)
            {
                Unsafe.SkipInit(out SkipLocalsInitStruct c);
                c.A = a;
                c.B = b;
                return c;
            }

            public static SkipLocalsInitStruct Init(int a)
            {
                Unsafe.SkipInit(out SkipLocalsInitStruct c);
                c.A = a;
                c.B = 0;
                return c;
            }
        }
    }
}
