// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
[MemoryDiagnoser]
public class StructInvokeThroughBox
{
    [Benchmark]
    public int ByIs()
    {
        int result = 0;

        for (int i = 0; i < 1_000_000; i++)
            result += getByIs(new MyStruct());

        return result;
    }

    [Benchmark]
    public int ByCast()
    {
        int result = 0;

        for (int i = 0; i < 1_000_000; i++)
            result += getByCast(new MyStruct());

        return result;
    }

    private int getByIs<T>(T val)
    {
        if (val is IMyInterface intfT)
            return intfT.Get();

        return 0;
    }

    private int getByCast<T>(T val)
    {
        if (typeof(T) == typeof(IMyInterface))
            return ((IMyInterface)val).Get();

        return 0;
    }

    private struct MyStruct : IMyInterface
    {
        public int Get() => 1;
    }

    private interface IMyInterface
    {
        int Get();
    }
}
}
