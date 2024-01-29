// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;

namespace BenchmarksProject
{
    /// <summary>
    /// https://gist.github.com/smoogipoo/08a7e890794678ea1ff98e18b14790b3
    /// </summary>
    [HardwareCounters(HardwareCounter.BranchInstructions, HardwareCounter.BranchMispredictions)]
    public class BenchmarkBranching
    {
        private static int result;

        [Benchmark]
        public void Benchmark_True_FastPath_No_Precondition() => trueFastPathNoPrecondition(() => true, () => 1);

        [Benchmark]
        public void Benchmark_False_FastPath_No_Precondition() => falseFastPathNoPrecondition(() => false, () => 1);

        [Benchmark]
        public void Benchmark_True_FastPath_Precondition_Immediate() => trueFastPathPreconditionImmediate(() => true, () => 1);

        [Benchmark]
        public void Benchmark_False_FastPath_Precondition_Immediate() => falseFastPathPreconditionImmediate(() => false, () => 1);

        [Benchmark]
        public void Benchmark_True_FastPath_Precondition_Loop() => trueFastPathPreconditionLoop(() => true, () => 1);

        [Benchmark]
        public void Benchmark_False_FastPath_Precondition_Loop() => falseFastPathPreconditionLoop(() => false, () => 1);

        private int trueFastPathNoPrecondition(Func<bool> cond, Func<int> inc)
        {
            for (int i = 0; i < 100000; i++)
            {
                if (cond())
                    fastPath(inc);
                else
                    slowPath(inc);
            }

            return result;
        }

        private int falseFastPathNoPrecondition(Func<bool> cond, Func<int> inc)
        {
            for (int i = 0; i < 100000; i++)
            {
                if (cond())
                    slowPath(inc);
                else
                    fastPath(inc);
            }

            return result;
        }

        private static int trueFastPathPreconditionImmediate(Func<bool> cond, Func<int> inc)
        {
            for (int i = 0; i < 100000; i++)
                inner(cond, inc);
            return result;

            static void inner(Func<bool> cond, Func<int> inc)
            {
                if (!cond())
                {
                    slowPath(inc);
                    return;
                }

                fastPath(inc);
            }
        }

        private static int falseFastPathPreconditionImmediate(Func<bool> cond, Func<int> inc)
        {
            for (int i = 0; i < 100000; i++)
                inner(cond, inc);
            return result;

            static void inner(Func<bool> cond, Func<int> inc)
            {
                if (cond())
                {
                    slowPath(inc);
                    return;
                }

                fastPath(inc);
            }
        }

        private int trueFastPathPreconditionLoop(Func<bool> cond, Func<int> inc)
        {
            for (int i = 0; i < 100000; i++)
            {
                if (!cond())
                {
                    slowPath(inc);
                    continue;
                }

                fastPath(inc);
            }

            return result;
        }

        private int falseFastPathPreconditionLoop(Func<bool> cond, Func<int> inc)
        {
            for (int i = 0; i < 100000; i++)
            {
                if (cond())
                {
                    slowPath(inc);
                    continue;
                }

                fastPath(inc);
            }

            return result;
        }

        private static void fastPath(Func<int> inc) => result += inc();

        private static void slowPath(Func<int> inc)
        {
            for (int x = 0; x < 100000; x++)
                result += inc();
        }
    }
}
