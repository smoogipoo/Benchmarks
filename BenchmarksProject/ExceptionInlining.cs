// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#if NET6_0_OR_GREATER

using System;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    [MemoryDiagnoser]
    public class ExceptionInlining
    {
        [Benchmark]
        public A TestRawString()
        {
            A a = null;

            for (int i = 0; i < 1000; i++)
                a = new A(null);

            return a;
        }

        [Benchmark]
        public B TestNameOf()
        {
            B b = null;

            for (int i = 0; i < 1000; i++)
                b = new B(null);

            return b;
        }

        [Benchmark]
        public C TestHelper()
        {
            C c = null;

            for (int i = 0; i < 1000; i++)
                c = new C(null);

            return c;
        }

        public class A
        {
            public A(object obj)
            {
                if (obj == null)
                    // ReSharper disable once UseNameofExpression
                    throw new ArgumentNullException("obj");
            }
        }

        public class B
        {
            public B(object obj)
            {
                if (obj == null)
                    throw new ArgumentNullException(nameof(obj));
            }
        }

        public class C
        {
            public C(object obj)
            {
                ArgumentNullException.ThrowIfNull(obj);
            }
        }
    }
}

#endif
