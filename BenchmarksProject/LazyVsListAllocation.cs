// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
[MemoryDiagnoser]
public class LazyVsListAllocation
{
    [Benchmark]
    public object CreateLazy() => new Lazy<List<object>>();

    [Benchmark]
    public object CreateList() => new List<object>();

    [Benchmark]
    public int GetListCountViaLazy()
    {
        var lazy = new Lazy<List<object>>();

        int c = 0;

        for (int i = 0; i < 1000; i++)
            c += lazy.Value.Count;

        return c;
    }

    [Benchmark]
    public int GetListCountDirect()
    {
        var list = new List<object>();

        int c = 0;

        for (int i = 0; i < 1000; i++)
            c += list.Count;

        return c;
    }
}
}
