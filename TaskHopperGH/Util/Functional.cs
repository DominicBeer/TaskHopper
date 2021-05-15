using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskHopper.Util
{
    public static class Functional
    {
        
        public static IEnumerable<T> TapList<T>(this IEnumerable<T> @this, Action<T> action)
        {
            var returnList = @this.ToList();
            foreach (var item in returnList)
            {
                action(item);
            }
            return returnList; ;
        }
        public static IEnumerable<T> TapList<T>(this IEnumerable<T> @this, Action<T, int> action)
        {
            int i = 0;
            var returnList = @this.ToList();
            foreach (var item in returnList)
            {
                action(item, i);
                i++;
            }
            return returnList;
        }
        public static T Tap<T>(this T @this, Action<T> action)
        {
            action(@this);
            return @this;
        }
        public static IEnumerable<(T Item, int i)> Enumerate<T>(this IEnumerable<T> @this)
        {
            return @this.Select((item, index) => (item, index));
        }
        public static ReadOnlyDictionary<T1, T2> ToReadOnly<T1, T2>(this IDictionary<T1, T2> @this)
        {
            return new ReadOnlyDictionary<T1, T2>(@this);
        }
        public static Dictionary<T1, T2> ToDict<T1, T2>(this IEnumerable<(T1 key, T2 value)> @this)
        {
            var dict = new Dictionary<T1, T2>();
            @this.TapList(t => dict.Add(t.key, t.value));
            return dict;
        }
        public static Dictionary<T1, T2> ToDict<T1, T2>(this IEnumerable<KeyValuePair<T1, T2>> @this)
        {
            var dict = new Dictionary<T1, T2>();
            @this.TapList(pair => dict.Add(pair.Key, pair.Value));
            return dict;
        }
     
        public static Dictionary<T1, T2> ToDict<T1, T2>(this IDictionary<T1, T2> @this)
        {
            return new Dictionary<T1, T2>(@this);
        }
        public static KeyValuePair<TKey,TValue> KVPair<TKey, TValue>(TKey key, TValue value)
        {
            return new KeyValuePair<TKey, TValue>(key, value);
        }

        public static List<(T,T)> LowerTrianglePairs<T>(this IEnumerable<T> @this)
        {
            var l = @this.ToArray();
            var n = l.Length;
            var lOut = new List<(T,T)>((n * (n - 1) )/ 2);
            for(int i = 0; i< n - 1; i++)
                for (int j = i+1; j < n; j++)
                    lOut.Add((l[i],l[j]));
            return lOut;
        }
        public static (int I, int J, int K)[] Range3d(int nI, int nJ, int nK)
        {
            var indices = new (int, int, int)[nI * nJ * nK];
            int count = 0;
            for (int i = 0; i < nI; i++)
            {
                for (int j = 0; j < nJ; j++)
                {
                    for (int k = 0; k < nK; k++)
                    {
                        indices[count] = (i, j, k);
                        count++;
                    }
                }
            }
            return indices;
        }
        public static (long I, long J, long K)[] Range3d(long nI, long nJ, long nK)
        {
            var indices = new (long, long, long)[nI * nJ * nK];
            long count = 0;
            for (long i = 0; i < nI; i++)
            {
                for (long j = 0; j < nJ; j++)
                {
                    for (long k = 0; k < nK; k++)
                    {
                        indices[count] = (i, j, k);
                        count++;
                    }
                }
            }
            return indices;
        }


    }
}
