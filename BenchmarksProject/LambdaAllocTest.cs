// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    [MemoryDiagnoser]
    public class LambdaAllocTest
    {
        [Benchmark]
        public void TestInstanceMethodLambdaCapture()
        {
            for (int i = 0; i < 1000; i++)
                method1(() => method2());
        }

        [Benchmark]
        public void TestInstanceMethodDirectCapture()
        {
            for (int i = 0; i < 1000; i++)
                method1(method2);
        }

        [Benchmark]
        public void TestStaticMethodLambdaCapture()
        {
            for (int i = 0; i < 1000; i++)
                method1(() => staticMethod2());
        }

        [Benchmark]
        public void TestStaticMethodDirectCapture()
        {
            for (int i = 0; i < 1000; i++)
                method1(staticMethod2);
        }

        [Benchmark]
        public void TestLocalNonCapturingMethodLambdaCapture()
        {
            void localMethod2()
            {
            }

            for (int i = 0; i < 1000; i++)
                method1(() => localMethod2());
        }

        [Benchmark]
        public void TestLocalNonCapturingMethodDirectCapture()
        {
            void localMethod2()
            {
            }

            for (int i = 0; i < 1000; i++)
                method1(localMethod2);
        }

        [Benchmark]
        public void TestLocalNonCapturingStaticMethodLambdaCapture()
        {
            static void localMethod2()
            {
            }

            for (int i = 0; i < 1000; i++)
                method1(() => localMethod2());
        }

        [Benchmark]
        public void TestLocalNonCapturingStaticMethodDirectCapture()
        {
            static void localMethod2()
            {
            }

            for (int i = 0; i < 1000; i++)
                method1(localMethod2);
        }

        [Benchmark]
        public void TestLocalCapturingMethodLambdaCapture()
        {
            int x = 0;
            void method2() => x++;

            for (int i = 0; i < 1000; i++)
                method1(() => method2());
        }

        [Benchmark]
        public void TestLocalCapturingMethodDirectCapture()
        {
            int x = 0;
            void method2() => x++;

            for (int i = 0; i < 1000; i++)
                method1(method2);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void method1(Action action)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void method2()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void staticMethod2()
        {
        }
    }
}
