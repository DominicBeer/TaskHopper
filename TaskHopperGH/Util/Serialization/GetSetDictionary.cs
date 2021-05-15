using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GH_IO.Serialization;
using static System.Linq.Enumerable;


namespace TaskHopper.Util.Serialization
{
    public static partial class GetSet
    {

        public static void SetDictionary<T1,T2>(
            this GH_IWriter writer, 
            string itemName, 
            int itemIndex, 
            IDictionary<T1, T2> data, 
            Action<GH_IWriter, string, int, T1> keySetter, 
            Action<GH_IWriter, string, int, T2> valueSetter
            )
        {
            var dataWriter = writer.CreateChunk(itemName, itemIndex);
            WriteDictionary(dataWriter, data, keySetter,valueSetter);
        }

        public static void SetDictionary<T1, T2>(
            this GH_IWriter writer, 
            string itemName, 
            IDictionary<T1, T2> data, 
            Action<GH_IWriter, string, int, T1> keySetter, 
            Action<GH_IWriter, string, int, T2> valueSetter
            )
        {
            var dataWriter = writer.CreateChunk(itemName);
            WriteDictionary(dataWriter, data, keySetter, valueSetter);
        }

        private static void WriteDictionary<T1, T2>(
            GH_IWriter writer, 
            IDictionary<T1, T2> data, 
            Action<GH_IWriter, string, int, T1> keySetter, 
            Action<GH_IWriter, string, int, T2> valueSetter
            )
        {
            writer.SetInt32("PairCount", data.Count());
            data.TapList((pair, i) => keySetter(writer, "Key", i, pair.Key));
            data.TapList((pair, i) => valueSetter(writer, "Value", i, pair.Value));
                            
        }



        public static IDictionary<T1, T2> GetDictionary<T1, T2>(
            this GH_IReader reader, 
            string itemName, 
            int itemIndex, 
            Func<GH_IReader, string, int, T1> keyGetter, 
            Func<GH_IReader, string, int, T2> valueGetter
            )
        {
            var dataReader = reader.FindChunk(itemName, itemIndex);
            return ReadDictionary(dataReader, keyGetter,valueGetter);
        }
        public static IDictionary<T1, T2> GetDictionary<T1, T2>(
            this GH_IReader reader, 
            string itemName, 
            Func<GH_IReader, string, int, T1> keyGetter, 
            Func<GH_IReader, string, int, T2> valueGetter
            )
        {
            var dataReader = reader.FindChunk(itemName);
            return ReadDictionary(dataReader, keyGetter, valueGetter);
        }
        private static IDictionary<T1, T2> ReadDictionary<T1, T2>(
            GH_IReader reader, 
            Func<GH_IReader, string, int, T1> keyGetter, 
            Func<GH_IReader, string, int, T2> valueGetter
            )
        {            
            int itemCount = reader.GetInt32("PairCount");
            var keys =  Range(0, itemCount).Select(i => keyGetter(reader, "Key", i));
            var values = Range(0, itemCount).Select(i => valueGetter(reader, "Value", i));
            return keys.Zip(values, (key, value) => (key, value))
                       .ToDictionary(pair => pair.key, pair => pair.value);
        }

    }
}
