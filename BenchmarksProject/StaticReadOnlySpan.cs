// | Method | Mean       | Error    | StdDev   | Gen0   | Allocated |
// |------- |-----------:|---------:|---------:|-------:|----------:|
// | Span   |   321.8 ns |  3.95 ns |  3.50 ns |      - |         - |
// | Array  | 3,513.2 ns | 69.25 ns | 74.09 ns | 6.3744 |   40000 B |


using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace BenchmarksProject
{
    [MemoryDiagnoser]
    public class StaticReadOnlySpan
    {
        private static ReadOnlySpan<byte> span => [0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7, 0x8, 0x9, 0xA, 0xB, 0xC, 0xD, 0xE, 0xF];
        private static byte[] array => [0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7, 0x8, 0x9, 0xA, 0xB, 0xC, 0xD, 0xE, 0xF];

        private static readonly Consumer consumer = new Consumer();

        [Benchmark]
        public void Span()
        {
            for (int i = 0; i < 1000; i++)
                consumer.Consume(span.Length);
        }

        [Benchmark]
        public void Array()
        {
            for (int i = 0; i < 1000; i++)
                consumer.Consume(array.Length);
        }
    }
}
