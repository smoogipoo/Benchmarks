// | Method      | Job      | Runtime  | Mean        | Error    | StdDev   | Ratio | RatioSD | Gen0    | Allocated | Alloc Ratio |
// |------------ |--------- |--------- |------------:|---------:|---------:|------:|--------:|--------:|----------:|------------:|
// | ManualCast  | .NET 8.0 | .NET 8.0 |    91.96 us | 1.460 us | 1.681 us |  1.00 |    0.00 |       - |         - |          NA |
// | LinqCast    | .NET 8.0 | .NET 8.0 | 1,589.64 us | 3.195 us | 2.988 us | 17.25 |    0.30 | 13.6719 |   96001 B |          NA |
// | LinqCastSum | .NET 8.0 | .NET 8.0 | 1,600.00 us | 8.195 us | 6.843 us | 17.35 |    0.33 | 13.6719 |   96001 B |          NA |
// |             |          |          |             |          |          |       |         |         |           |             |
// | ManualCast  | .NET 9.0 | .NET 9.0 |    88.44 us | 0.835 us | 0.697 us |  1.00 |    0.00 |       - |         - |          NA |
// | LinqCast    | .NET 9.0 | .NET 9.0 | 1,485.74 us | 2.675 us | 2.088 us | 16.79 |    0.14 | 13.6719 |   88001 B |          NA |
// | LinqCastSum | .NET 9.0 | .NET 9.0 | 1,514.85 us | 9.197 us | 8.152 us | 17.13 |    0.17 | 13.6719 |   88001 B |          NA |

using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace BenchmarksProject
{
    [SimpleJob(RuntimeMoniker.Net80)]
    [SimpleJob(RuntimeMoniker.Net90)]
    [MemoryDiagnoser]
    public class BenchmarkEnumerableCast
    {
        private readonly List<object> objects = new List<object>();

        [GlobalSetup]
        public void GlobalSetup()
        {
            for (int i = 0; i < 100; i++)
                objects.Add(new C());
        }

        [Benchmark(Baseline = true)]
        public int ManualCast()
        {
            int result = 0;

            for (int i = 0; i < 1000; i++)
            {
                foreach (object obj in objects)
                    result += ((C)obj).I;
            }

            return result;
        }

        [Benchmark]
        public int LinqCast()
        {
            int result = 0;

            for (int i = 0; i < 1000; i++)
            {
                foreach (C obj in objects.Cast<C>())
                    result += obj.I;
            }

            return result;
        }

        [Benchmark]
        public int LinqCastSum()
        {
            int result = 0;

            for (int i = 0; i < 1000; i++)
                result += objects.Cast<C>().Sum(c => c.I);

            return result;
        }

        private class C
        {
            public int I { get; set; } = 1;
        }
    }
}
