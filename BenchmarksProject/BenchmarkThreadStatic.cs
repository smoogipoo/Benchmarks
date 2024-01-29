// | Method | Job      | Runtime  | Mean      | Error     | StdDev    |
// |------- |--------- |--------- |----------:|----------:|----------:|
// | Local  | .NET 6.0 | .NET 6.0 | 0.9059 ns | 0.0064 ns | 0.0060 ns |
// | Static | .NET 6.0 | .NET 6.0 | 2.3395 ns | 0.0021 ns | 0.0016 ns |
// | Local  | .NET 8.0 | .NET 8.0 | 0.9831 ns | 0.0037 ns | 0.0034 ns |
// | Static | .NET 8.0 | .NET 8.0 | 0.9520 ns | 0.0043 ns | 0.0036 ns |

using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace BenchmarksProject;

[SimpleJob(RuntimeMoniker.Net60)]
[SimpleJob(RuntimeMoniker.Net80)]
public class BenchmarkThreadStatic
{
    [ThreadStatic] private static int staticValue;
    private int localValue;

    [Benchmark]
    public int Local() => ++localValue;

    [Benchmark]
    public int Static() => ++staticValue;
}