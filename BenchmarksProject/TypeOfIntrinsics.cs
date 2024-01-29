// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    [MemoryDiagnoser]
    public class TypeOfIntrinsicsTest
    {
        [Benchmark]
        public bool IsFinite() => isFinite(1.0f);

        [Benchmark]
        public bool IsFiniteJitIntrinsics() => isFiniteJitIntrinsics(1.0f);

        private static bool isFinite(object value)
        {
            switch (value)
            {
                case float floatValue:
                    return float.IsFinite(floatValue);

                case double doubleValue:
                    return double.IsFinite(doubleValue);
            }

            return true;
        }

        private static bool isFiniteJitIntrinsics<T>(T value)
        {
            if (typeof(T) == typeof(float))
                return float.IsFinite((float)(object)value);
            if (typeof(T) == typeof(double))
                return double.IsFinite((double)(object)value);

            return true;
        }
    }
}
