using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using GH_IO.Serialization;
using Grasshopper.Kernel;
using GH_IO;

namespace TaskHopper.Util.Serialization
{
    public static partial class GetSet
    {

        public static void SetGH_ISerializable(this GH_IWriter writer, string itemName, int itemIndex, GH_ISerializable data)
        {
            var dataWriter = writer.CreateChunk(itemName, itemIndex);
            WriteGH_ISerializable(dataWriter, data);
        }
        public static void SetGH_ISerializable(this GH_IWriter writer, string itemName, GH_ISerializable data)
        {
            var dataWriter = writer.CreateChunk(itemName);
            WriteGH_ISerializable(dataWriter, data);
        }
        private static void WriteGH_ISerializable(GH_IWriter writer, GH_ISerializable data)
        {
            writer.SetString("TypeString", data.GetType().FullName);
            writer.SetString("AssemblyString", data.GetType().Assembly.FullName);
            var objectWriter = writer.CreateChunk("Data");
            data.Write(objectWriter);
        }



        public static GH_ISerializable GetGH_ISerializable(this GH_IReader reader, string itemName, int itemIndex)
        {
            var dataReader = reader.FindChunk(itemName, itemIndex);
            return ReadGH_ISerializable(dataReader);
        }
        public static GH_ISerializable GetGH_ISerializable(this GH_IReader reader, string itemName)
        {
            var dataReader = reader.FindChunk(itemName);
            return ReadGH_ISerializable(dataReader);
        }
        private static GH_ISerializable ReadGH_ISerializable(GH_IReader reader)
        {
            var valueTypeString = reader.GetString("TypeString");
            var valueAssemblyString = reader.GetString("AssemblyString");
            var valueInstance = (GH_ISerializable)Activator.CreateInstance(valueAssemblyString, valueTypeString).Unwrap();
            var instanceReader = reader.FindChunk("Data");
            valueInstance.Read(instanceReader);
            return valueInstance;
        }
    }
}
