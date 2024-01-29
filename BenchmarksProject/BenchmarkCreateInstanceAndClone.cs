// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    [MemoryDiagnoser]
    public class BenchmarkCreateInstanceAndClone
    {
        [Benchmark]
        public object ViaCreateInstance() => new ObjA().CloneViaActivator();

        [Benchmark]
        public object ViaMemberwiseClone() => new ObjA().CloneViaMemberwiseClone();

        private class ObjA
        {
            public double A { get; set; }
            public double B { get; set; }
            public double C { get; set; }
            public double D { get; set; }
            public double E { get; set; }

            public object CloneViaActivator()
            {
                var newObj = (ObjA)Activator.CreateInstance(GetType());
                newObj.A = A;
                newObj.B = B;
                newObj.C = C;
                newObj.D = D;
                newObj.E = E;
                return newObj;
            }

            public object CloneViaMemberwiseClone() => (ObjA)MemberwiseClone();
        }
    }
}
