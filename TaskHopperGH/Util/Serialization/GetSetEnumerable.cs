using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GH_IO.Serialization;

using static TaskHopper.Util.Serialization.GetSetDelegates;
using static System.Linq.Enumerable;

namespace TaskHopper.Util.Serialization
{
    public static partial class GetSet
    {
        #region Writer
        public static void SetEnumerable<T>(this GH_IWriter writer, string itemName, int itemIndex, IEnumerable<T> data, Action<GH_IWriter, string,int, T> setter)
        {
            var dataWriter = writer.CreateChunk(itemName, itemIndex);
            WriteEnumberable<T>(dataWriter, data, setter);
        }
        public static void SetEnumerable<T>(this GH_IWriter writer, string itemName, IEnumerable<T> data, Action<GH_IWriter, string,int, T> setter)
        {
            var dataWriter = writer.CreateChunk(itemName);
            WriteEnumberable<T>(dataWriter, data, setter);
        }
        private static void WriteEnumberable<T>(GH_IWriter writer, IEnumerable<T> data, Action<GH_IWriter, string,int, T> setter)
        {
            writer.SetInt32("ListItemCount", data.Count());
            int i = 0;
            foreach(T item in data)
            {
                setter(writer, "ListItem", i, item);
                i++;
            }

        }
        #endregion

        #region Reader
        public static IEnumerable<T> GetEnumerable<T>(this GH_IReader reader, string itemName, int itemIndex, Func<GH_IReader, string,int, T> getter)
        {
            var dataReader = reader.FindChunk(itemName, itemIndex);
            return ReadEnumerable<T>(dataReader, getter);
        }
        public static IEnumerable<T> GetEnumerable<T>(this GH_IReader reader, string itemName, Func<GH_IReader, string,int, T> getter)
        {
            var dataReader = reader.FindChunk(itemName);
            return ReadEnumerable<T>(dataReader, getter);
        }
        private static IEnumerable<T> ReadEnumerable<T>(GH_IReader reader, Func<GH_IReader, string,int, T> getter)
        {
            int itemCount = reader.GetInt32("ListItemCount");
            return Range(0, itemCount).Select(i => getter(reader, "ListItem", i));
        }

        #endregion
    }
}
