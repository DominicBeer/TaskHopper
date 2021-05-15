using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using GH_IO.Serialization;
using Grasshopper.Kernel;
using System.Drawing;
using Grasshopper.Kernel.Types;

using GH_IO;

namespace TaskHopper.Util.Serialization
{
    public static partial class GetSetDelegates
    {
        public static void WriteBoolean(GH_IWriter writer, string itemName, int itemIndex, bool data)
        {
            writer.SetBoolean(itemName, itemIndex, data);
        }
        public static void WriteBoolean(GH_IWriter writer, string itemName, bool data)
        {
            writer.SetBoolean(itemName, data);
        }
        public static bool ReadBoolean(GH_IReader reader, string itemName, int itemIndex)
        {
            return reader.GetBoolean(itemName, itemIndex);
        }
        public static bool ReadBoolean(GH_IReader reader, string itemName)
        {
            return reader.GetBoolean(itemName);
        }

        public static void WriteInt32(GH_IWriter writer, string itemName, int itemIndex, int data)
        {
            writer.SetInt32(itemName, itemIndex, data);
        }
        public static void WriteInt32(GH_IWriter writer, string itemName, int data)
        {
            writer.SetInt32(itemName, data);
        }
        public static int ReadInt32(GH_IReader reader, string itemName, int itemIndex)
        {
            return reader.GetInt32(itemName, itemIndex);
        }
        public static int ReadInt32(GH_IReader reader, string itemName)
        {
            return reader.GetInt32(itemName);
        }

        public static void WriteDouble(GH_IWriter writer, string itemName, int itemIndex, double data)
        {
            writer.SetDouble(itemName, itemIndex, data);
        }
        public static void WriteDouble(GH_IWriter writer, string itemName, double data)
        {
            writer.SetDouble(itemName, data);
        }
        public static double ReadDouble(GH_IReader reader, string itemName, int itemIndex)
        {
            return reader.GetDouble(itemName, itemIndex);
        }
        public static double ReadDouble(GH_IReader reader, string itemName)
        {
            return reader.GetDouble(itemName);
        }

        public static void WriteDate(GH_IWriter writer, string itemName, int itemIndex, DateTime data)
        {
            writer.SetDate(itemName, itemIndex, data);
        }
        public static void WriteDate(GH_IWriter writer, string itemName, DateTime data)
        {
            writer.SetDate(itemName, data);
        }
        public static DateTime ReadDate(GH_IReader reader, string itemName, int itemIndex)
        {
            return reader.GetDate(itemName, itemIndex);
        }
        public static DateTime ReadDate(GH_IReader reader, string itemName)
        {
            return reader.GetDate(itemName);
        }

        public static void WriteGuid(GH_IWriter writer, string itemName, int itemIndex, Guid data)
        {
            writer.SetGuid(itemName, itemIndex, data);
        }
        public static void WriteGuid(GH_IWriter writer, string itemName, Guid data)
        {
            writer.SetGuid(itemName, data);
        }
        public static Guid ReadGuid(GH_IReader reader, string itemName, int itemIndex)
        {
            return reader.GetGuid(itemName, itemIndex);
        }
        public static Guid ReadGuid(GH_IReader reader, string itemName)
        {
            return reader.GetGuid(itemName);
        }

        public static void WriteString(GH_IWriter writer, string itemName, int itemIndex, string data)
        {
            writer.SetString(itemName, itemIndex, data);
        }
        public static void WriteString(GH_IWriter writer, string itemName, string data)
        {
            writer.SetString(itemName, data);
        }
        public static string ReadString(GH_IReader reader, string itemName, int itemIndex)
        {
            return reader.GetString(itemName, itemIndex);
        }
        public static string ReadString(GH_IReader reader, string itemName)
        {
            return reader.GetString(itemName);
        }

        public static void WriteDrawingColor(GH_IWriter writer, string itemName, int itemIndex, Color data)
        {
            writer.SetDrawingColor(itemName, itemIndex, data);
        }
        public static void WriteDrawingColor(GH_IWriter writer, string itemName, Color data)
        {
            writer.SetDrawingColor(itemName, data);
        }
        public static Color ReadDrawingColor(GH_IReader reader, string itemName, int itemIndex)
        {
            return reader.GetDrawingColor(itemName, itemIndex);
        }
        public static Color ReadDrawingColor(GH_IReader reader, string itemName)
        {
            return reader.GetDrawingColor(itemName);
        }

        public static void WritePoint3D(GH_IWriter writer, string itemName, int itemIndex, Point3d data)
        {
            writer.SetPoint3D(itemName, itemIndex, data.ToIO());
        }
        public static void WritePoint3D(GH_IWriter writer, string itemName, Point3d data)
        {
            writer.SetPoint3D(itemName, data.ToIO());
        }
        public static Point3d ReadPoint3D(GH_IReader reader, string itemName, int itemIndex)
        {
            return reader.GetPoint3D(itemName, itemIndex).FromIO();
        }
        public static Point3d ReadPoint3D(GH_IReader reader, string itemName)
        {
            return reader.GetPoint3D(itemName).FromIO();
        }

        public static void WriteInterval1D(GH_IWriter writer, string itemName, int itemIndex, Interval data)
        {
            writer.SetInterval1D(itemName, itemIndex, data.ToIO());
        }
        public static void WriteInterval1D(GH_IWriter writer, string itemName, Interval data)
        {
            writer.SetInterval1D(itemName, data.ToIO());
        }
        public static Interval ReadInterval1D(GH_IReader reader, string itemName, int itemIndex)
        {
            return reader.GetInterval1D(itemName, itemIndex).FromIO();
        }
        public static Interval ReadInterval1D(GH_IReader reader, string itemName)
        {
            return reader.GetInterval1D(itemName).FromIO();
        }

        public static void WriteInterval2D(GH_IWriter writer, string itemName, int itemIndex, UVInterval data)
        {
            writer.SetInterval2D(itemName, itemIndex, data.ToIO());
        }
        public static void WriteInterval2D(GH_IWriter writer, string itemName, UVInterval data)
        {
            writer.SetInterval2D(itemName, data.ToIO());
        }
        public static UVInterval ReadInterval2D(GH_IReader reader, string itemName, int itemIndex)
        {
            return reader.GetInterval2D(itemName, itemIndex).FromIO();
        }
        public static UVInterval ReadInterval2D(GH_IReader reader, string itemName)
        {
            return reader.GetInterval2D(itemName).FromIO();
        }

        public static void WriteLine(GH_IWriter writer, string itemName, int itemIndex, Line data)
        {
            writer.SetLine(itemName, itemIndex, data.ToIO());
        }
        public static void WriteLine(GH_IWriter writer, string itemName, Line data)
        {
            writer.SetLine(itemName, data.ToIO());
        }
        public static Line ReadLine(GH_IReader reader, string itemName, int itemIndex)
        {
            return reader.GetLine(itemName, itemIndex).FromIO();
        }
        public static Line ReadLine(GH_IReader reader, string itemName)
        {
            return reader.GetLine(itemName).FromIO();
        }

        public static void WriteBoundingBox(GH_IWriter writer, string itemName, int itemIndex, BoundingBox data)
        {
            writer.SetBoundingBox(itemName, itemIndex, data.ToIO());
        }
        public static void WriteBoundingBox(GH_IWriter writer, string itemName, BoundingBox data)
        {
            writer.SetBoundingBox(itemName, data.ToIO());
        }
        public static BoundingBox ReadBoundingBox(GH_IReader reader, string itemName, int itemIndex)
        {
            return reader.GetBoundingBox(itemName, itemIndex).FromIO();
        }
        public static BoundingBox ReadBoundingBox(GH_IReader reader, string itemName)
        {
            return reader.GetBoundingBox(itemName).FromIO();
        }

        public static void WritePlane(GH_IWriter writer, string itemName, int itemIndex, Plane data)
        {
            writer.SetPlane(itemName, itemIndex, data.ToIO());
        }
        public static void WritePlane(GH_IWriter writer, string itemName, Plane data)
        {
            writer.SetPlane(itemName, data.ToIO());
        }
        public static Plane ReadPlane(GH_IReader reader, string itemName, int itemIndex)
        {
            return reader.GetPlane(itemName, itemIndex).FromIO();
        }
        public static Plane ReadPlane(GH_IReader reader, string itemName)
        {
            return reader.GetPlane(itemName).FromIO();
        }

        public static void WriteGeometryBase(GH_IWriter writer, string itemName, int itemIndex, GeometryBase data)
        {
            writer.SetGeometryBase(itemName, itemIndex, data);
        }
        public static void WriteGeometryBase(GH_IWriter writer, string itemName, GeometryBase data)
        {
            writer.SetGeometryBase(itemName, data);
        }
        public static GeometryBase ReadGeometryBase(GH_IReader reader, string itemName, int itemIndex)
        {
            return reader.GetGeometryBase(itemName, itemIndex);
        }
        public static GeometryBase ReadGeometryBase(GH_IReader reader, string itemName)
        {
            return reader.GetGeometryBase(itemName);
        }

        public static void WriteGH_ISerializable(GH_IWriter writer, string itemName, int itemIndex, GH_ISerializable data)
        {
            writer.SetGH_ISerializable(itemName, itemIndex, data);
        }
        public static void WriteGH_ISerializable(GH_IWriter writer, string itemName, GH_ISerializable data)
        {
            writer.SetGH_ISerializable(itemName, data);
        }
        public static GH_ISerializable ReadGH_ISerializable(GH_IReader reader, string itemName, int itemIndex)
        {
            return reader.GetGH_ISerializable(itemName, itemIndex);
        }
        public static GH_ISerializable ReadGH_ISerializable(GH_IReader reader, string itemName)
        {
            return reader.GetGH_ISerializable(itemName);
        }



        //Special Case Not Generated
        public static void WriteVector3D(GH_IWriter writer, string itemName, int itemIndex, Vector3d data)
        {
            writer.SetPoint3D(itemName, itemIndex, data.ToIO());
        }
        public static void WriteVector3D(GH_IWriter writer, string itemName, Vector3d data)
        {
            writer.SetPoint3D(itemName, data.ToIO());
        }
        public static Vector3d ReadVector3D(GH_IReader reader, string itemName, int itemIndex)
        {
            return reader.GetPoint3D(itemName, itemIndex).ToVector3d();
        }
        public static Vector3d ReadVector3D(GH_IReader reader, string itemName)
        {
            return reader.GetPoint3D(itemName).ToVector3d();
        }
        //End Special Case

       
    }
}
