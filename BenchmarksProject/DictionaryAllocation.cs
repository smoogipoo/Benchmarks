// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    [MemoryDiagnoser]
    [BaselineColumn]
    public class DictionaryAllocation
    {
        private readonly List<MidiKey> keys = new List<MidiKey>();

        [GlobalSetup]
        public void GlobalSetup()
        {
            foreach (var v in Enum.GetValues(typeof(MidiKey)))
                keys.Add((MidiKey)v);
        }

        [Benchmark(Baseline = true)]
        public List<(MidiKey, int)> InstantiateList()
        {
            var list = new List<(MidiKey, int)>();

            foreach (var v in keys)
                list.Add((v, 10));

            return list;
        }

        [Benchmark]
        public Dictionary<MidiKey, int> InstantiateDictionary()
        {
            var dict = new Dictionary<MidiKey, int>();

            foreach (var v in keys)
                dict[v] = 10;

            return dict;
        }

        public enum MidiKey
        {
            A0 = 21,
            ASharp0,
            B0,

            C1,
            CSharp1,
            D1,
            DSharp1,
            E1,
            F1,
            FSharp1,
            G1,
            GSharp1,
            A1,
            ASharp1,
            B1,

            C2,
            CSharp2,
            D2,
            DSharp2,
            E2,
            F2,
            FSharp2,
            G2,
            GSharp2,
            A2,
            ASharp2,
            B2,

            C3,
            CSharp3,
            D3,
            DSharp3,
            E3,
            F3,
            FSharp3,
            G3,
            GSharp3,
            A3,
            ASharp3,
            B3,

            C4,
            CSharp4,
            D4,
            DSharp4,
            E4,
            F4,
            FSharp4,
            G4,
            GSharp4,
            A4,
            ASharp4,
            B4,

            C5,
            CSharp5,
            D5,
            DSharp5,
            E5,
            F5,
            FSharp5,
            G5,
            GSharp5,
            A5,
            ASharp5,
            B5,

            C6,
            CSharp6,
            D6,
            DSharp6,
            E6,
            F6,
            FSharp6,
            G6,
            GSharp6,
            A6,
            ASharp6,
            B6,

            C7,
            CSharp7,
            D7,
            DSharp7,
            E7,
            F7,
            FSharp7,
            G7,
            GSharp7,
            A7,
            ASharp7,
            B7,

            C8,
        }
    }
}
