// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

// | Method        | Job       | Runtime   | Mean     | Error     | StdDev    |
// |-------------- |---------- |---------- |---------:|----------:|----------:|
// | Min           | .NET 10.0 | .NET 10.0 | 8.150 us | 0.0017 us | 0.0014 us |
// | BranchlessMin | .NET 10.0 | .NET 10.0 | 8.149 us | 0.0022 us | 0.0019 us |
// | Min           | .NET 8.0  | .NET 8.0  | 8.156 us | 0.0061 us | 0.0057 us |
// | BranchlessMin | .NET 8.0  | .NET 8.0  | 8.152 us | 0.0040 us | 0.0034 us |

using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace BenchmarksProject
{
    [SimpleJob(RuntimeMoniker.Net80)]
    [SimpleJob(RuntimeMoniker.Net10_0)]
    public class BenchmarkBranchlessMin
    {
        [Benchmark]
        public float Min()
        {
            const float a = 1;
            const float b = 2;
            float c = 0;

            for (int i = 0; i < 10000; i++)
                c += min(a, b);

            return c;

            static float min(float value1, float value2)
                => Math.Min(value1, value2);
        }

        [Benchmark]
        public float BranchlessMin()
        {
            const float a = 1;
            const float b = 2;
            float c = 0;

            for (int i = 0; i < 10000; i++)
                c += branchlessMin(a, b);

            return c;

            static float branchlessMin(float value1, float value2)
            {
                int b = Convert.ToInt32(value1 < value2);
                return b * value1 + (1 - b) * value2;
            }
        }
    }
}
