// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    public class BenchmarkPow
    {
        private readonly double[] vals = new double[100000];

        [GlobalSetup]
        public void GlobalSetup()
        {
            var r = new Random();

            for (int i = 0; i < vals.Length; i++)
                vals[i] = r.NextDouble() * 20;
        }

        [Benchmark(Baseline = true)]
        public double MathPow()
        {
            double result = 0;

            for (int i = 0; i < vals.Length; i++)
                result += Math.Pow(2, vals[i]);

            return result;
        }

        [Benchmark]
        public double CustomPow()
        {
            double result = 0;

            for (int i = 0; i < vals.Length; i++)
                result += Power2(vals[i]);

            return result;
        }

        private static readonly double[] coeffs =
        {
            1.0000000060371126,
            0.693146840098149,
            0.2402310826131064,
            0.05547894683131716,
            0.009686150703032881,
            0.0012382531241478965,
            0.00021871427263121524,
        };

        /// <summary>
        /// Fast approximation of 2^x. Accurate to around 9-10 significant figures, around 6x faster than Math.Pow or Math.Exp
        /// Calculates the integer part using a bit shift and fraction part using a polynomial approximation
        /// </summary>
        public static double Power2(double x)
        {
            if (x < 0)
            {
                return 1 / Power2(-x);
            }

            if (x > 60)
            {
                return double.PositiveInfinity;
            }

            int floor = (int)x;
            double frac = x - floor;
            double frac2 = frac * frac;
            double frac3 = frac * frac2;
            double frac4 = frac * frac3;
            double frac5 = frac * frac4;
            double frac6 = frac * frac5;

            return (1L << floor) * (coeffs[0] + coeffs[1] * frac + coeffs[2] * frac2
                                    + coeffs[3] * frac3 + coeffs[4] * frac4 + coeffs[5] * frac5 + coeffs[6] * frac6);
        }
    }
}
