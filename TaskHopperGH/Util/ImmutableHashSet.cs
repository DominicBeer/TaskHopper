using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskHopper.Util
{
    internal class ImmutableHashSet<T> : IReadOnlyCollection<T>
    {
        private readonly HashSet<T> baseSet;
        public int Count => baseSet.Count;

        public IEnumerator<T> GetEnumerator() => baseSet.GetEnumerator();
        private IEnumerator GetEnumerator1() => GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator1();


        public bool Contains(T item) => baseSet.Contains(item);
        public bool SetEquals(IEnumerable<T> other) => baseSet.SetEquals(other);
        public bool IsSubsetOf(IEnumerable<T> other) => baseSet.IsSubsetOf(other);
        public bool IsProperSubsetOf(IEnumerable<T> other) => baseSet.IsProperSubsetOf(other);
        public bool IsSupersetOf(IEnumerable<T> other) => baseSet.IsSupersetOf(other);
        public bool IsProperSupersetOf(IEnumerable<T> other) => baseSet.IsProperSupersetOf(other);

        /* Using Linq methods not in .Net 4.5 :'(
        public ImmutableHashSet<T> Add(T item) => baseSet.Append(item).ToHashSet().ToImmutable();
        public ImmutableHashSet<T> Remove(T item) => baseSet.ToHashSet().Tap(set => set.Remove(item)).ToImmutable();
        public ImmutableHashSet<T> UnionWith(IEnumerable<T> other) => baseSet.Concat(other).ToHashSet().ToImmutable();
        public ImmutableHashSet<T> IntersectWith(IEnumerable<T> other) => baseSet.ToHashSet().Tap(set => set.IntersectWith(other)).ToImmutable();
        public ImmutableHashSet<T> ExceptWith(IEnumerable<T> other) => baseSet.ToHashSet().Tap(set => set.ExceptWith(other)).ToImmutable();
        public ImmutableHashSet<T> SymmetricExceptWith(IEnumerable<T> other) => baseSet.ToHashSet().Tap(set => set.SymmetricExceptWith(other)).ToImmutable();*/

        public ImmutableHashSet(IEnumerable<T> collection)
        {
            baseSet = new HashSet<T>(collection);
        }
    }

    public static class ReadOnlyHashSetUtil
    {
        internal static ImmutableHashSet<T> ToImmutable<T>(this HashSet<T> @this)
        {
            return new ImmutableHashSet<T>(@this);
        }
    }
}
