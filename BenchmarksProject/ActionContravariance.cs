// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Threading;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    [MemoryDiagnoser]
    public class ActionContravariance
    {
        private readonly SendOrPostCallback del = _ => { };

        [Benchmark]
        public void CallbackWithNewAction()
        {
            for (int i = 0; i < 100; i++)
                finalFunc(new Action<object>(del));
        }

        [Benchmark]
        public void CallbackWithCast()
        {
            for (int i = 0; i < 100; i++)
                finalFunc(_ => del(_));
        }

        private void finalFunc(Action<object> action)
        {
        }
    }
}
