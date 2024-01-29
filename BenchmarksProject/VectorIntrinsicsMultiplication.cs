// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Numerics;
using System.Linq;
using System.Runtime.Intrinsics;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    public class VectorIntrinsicsMultiplication
    {
        [Params(1024)]
        public int ElementsCount { get; set; }

        private float[] elements;

        [GlobalSetup]
        public void GlobalSetup()
        {
            elements = Enumerable.Repeat(1f, ElementsCount).ToArray();
        }

        [Benchmark]
        public float AsManual()
        {
            float sum = 0;

            foreach (float t in elements)
                sum += t * 128;

            return sum;
        }

        [Benchmark]
        public float AsVectorT()
        {
            (new Vector<float>(elements) * 128).CopyTo(elements);

            float sum = 0;

            foreach (float t in elements)
                sum += t;

            return sum;
        }

        [Benchmark]
        public float AsVector4()
        {
            Vector4 c = new Vector4(128);

            float sum = 0;

            for (int i = 0; i < ElementsCount; i += 4)
                sum += Vector4.Dot(new Vector4(elements[i]), c);

            return sum;
        }
    }
}
