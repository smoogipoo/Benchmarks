// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#if NETCOREAPP

using System.Collections.Generic;

namespace BenchmarksProject
{
    public static class ExtensionMethods
    {
        public static SlimReadOnlyCollection<T, IEnumerator<T>> AsSlimReadOnly<T>(this IList<T> list)
            => new SlimReadOnlyCollection<T, IEnumerator<T>>(list);

        public static SlimReadOnlyCollection<T, List<T>.Enumerator> AsSlimReadOnly<T>(this List<T> list)
            => new SlimReadOnlyCollection<T, List<T>.Enumerator>(list);
    }
}

#endif