// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#if NETCOREAPP

#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;

namespace BenchmarksProject
{
    public readonly struct SlimReadOnlyCollection<T, TEnumerator> : IList<T>, IList, IReadOnlyList<T>
        where TEnumerator : IEnumerator<T>
    {
        private readonly IList<T> list;

        public SlimReadOnlyCollection(IList<T> list)
        {
            this.list = list;
        }

        public TEnumerator GetEnumerator()
        {
            if (list is List<T> l)
                return (TEnumerator)(object)l.GetEnumerator();

            return (TEnumerator)list.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public int Add(object? value) => throw new NotImplementedException();

        void IList.Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(object? value) => ((IList)list).Contains(value);

        public int IndexOf(object? value) => ((IList)list).IndexOf(value);

        public void Insert(int index, object? value)
        {
            throw new NotImplementedException();
        }

        public void Remove(object? value)
        {
            throw new NotImplementedException();
        }

        void IList.RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public bool IsFixedSize => false;

        bool IList.IsReadOnly => true;

        object? IList.this[int index]
        {
            get => list[index];
            set => throw new NotImplementedException();
        }

        void ICollection<T>.Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item) => list.Contains(item);

        public void CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item) => throw new NotImplementedException();

        public void CopyTo(Array array, int index)
        {
            ((ICollection)list).CopyTo(array, index);
        }

        int ICollection.Count => list.Count;

        public bool IsSynchronized => ((ICollection)list).IsSynchronized;

        public object SyncRoot => ((ICollection)list).SyncRoot;

        int ICollection<T>.Count => list.Count;

        bool ICollection<T>.IsReadOnly => true;

        public int IndexOf(T item) => list.IndexOf(item);

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        void IList<T>.RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public T this[int index]
        {
            get => list[index];
            set => throw new NotImplementedException();
        }

        int IReadOnlyCollection<T>.Count => list.Count;
    }
}

#endif