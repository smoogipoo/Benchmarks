// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Runtime.InteropServices;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    public class BenchmarkFalseSharing
    {
        [Params(1, 4, 8, 16, 32, 64, 128)]
        public int Separation { get; set; }

        private readonly int[] data = new int[256];
        private Unblittable unblittable;

        // [Benchmark]
        // public void FalseSharing()
        // {
        //     Parallel.Invoke(incX, incY);
        //
        //     void incX()
        //     {
        //         for (int i = 0; i < 1_000_000; i++)
        //             data[0]++;
        //     }
        //
        //     void incY()
        //     {
        //         for (int i = 0; i < 1_000_000; i++)
        //             data[Separation]++;
        //     }
        // }

        [Benchmark]
        public void FalseSharingUnblittalbeStruct()
        {
            Parallel.Invoke(incX, incY);

            void incX()
            {
                for (int i = 0; i < 1_000_000; i++)
                    unblittable.X++;
            }

            void incY()
            {
                for (int i = 0; i < 1_000_000; i++)
                    unblittable.Y++;
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct Unblittable
        {
            [FieldOffset(0)]
            public int X;

            [FieldOffset(64)]
            public int Y;

            [FieldOffset(128)]
            public bool Unused1;

            [FieldOffset(192)]
            public string Unused2;

            [FieldOffset(256)]
            public object Unused3;

            public Unblittable()
            {
                X = 0;
                Y = 0;
                Unused1 = false;
                Unused2 = null;
                Unused3 = new object();
            }
        }
    }
}
