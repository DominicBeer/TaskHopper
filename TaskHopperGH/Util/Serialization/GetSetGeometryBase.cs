using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using GH_IO.Serialization;
using Grasshopper.Kernel;

namespace TaskHopper.Util.Serialization
{
    public static partial class GetSet
    {
        #region Writer
        public static void SetGeometryBase(this GH_IWriter writer, string itemName, int itemIndex, GeometryBase data)
        {
            var dataWriter = writer.CreateChunk(itemName, itemIndex);
            WriteGeometryBase(dataWriter, data);
        }
        public static void SetGeometryBase(this GH_IWriter writer, string itemName, GeometryBase data)
        {
            var dataWriter = writer.CreateChunk(itemName);
            WriteGeometryBase(dataWriter, data);
        }
        private static void WriteGeometryBase(GH_IWriter writer, GeometryBase data)
        {
            writer.SetString("DataType", "GeometryBase");
            byte[] bytes = GH_Convert.CommonObjectToByteArray(data);
            writer.SetByteArray("Data", bytes);
        }
        #endregion

        #region Reader
        public static GeometryBase GetGeometryBase(this GH_IReader reader, string itemName, int itemIndex)
        {
            var dataReader = reader.FindChunk(itemName, itemIndex);
            return ReadGeometryBase(dataReader);
        }
        public static GeometryBase GetGeometryBase(this GH_IReader reader, string itemName)
        {
            var dataReader = reader.FindChunk(itemName);
            return ReadGeometryBase(dataReader);
        }
        private static GeometryBase ReadGeometryBase(GH_IReader reader)
        {
            var bytes = reader.GetByteArray("Data");
            var valueInstance = GH_Convert.ByteArrayToCommonObject<GeometryBase>(bytes);
            return valueInstance;
        }
        #endregion
    }
}
