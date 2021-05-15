using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GH_IO.Types;
using Rhino.Geometry;

namespace TaskHopper.Util.Serialization
{
    public static partial class UtilityMethods
    {
        public static GH_Point3D ToIO(this Point3d p)
        {
            return new GH_Point3D(p.X, p.Y, p.Z);
        }
        public static GH_Point3D ToIO(this Vector3d v)
        {
            return new GH_Point3D(v.X, v.Y, v.Z);
        }
        public static GH_Interval1D ToIO(this Interval d)
        {
            return new GH_Interval1D(d.T0, d.T1);
        }
        public static GH_Interval2D ToIO(this Grasshopper.Kernel.Types.UVInterval d)
        {
            return new GH_Interval2D(d.U0, d.U1, d.V0, d.V1);
        }
        public static GH_Line ToIO(this Line l)
        {
            return new GH_Line(l.FromX, l.FromY, l.FromZ, l.ToX, l.ToY, l.ToZ);
        }
        public static GH_BoundingBox ToIO(this BoundingBox b)
        {
            return new GH_BoundingBox(b.Min.ToIO(), b.Max.ToIO());
        }
        public static GH_Plane ToIO(this Plane p)
        {
            return new GH_Plane(p.Origin.ToIO(), p.XAxis.ToIO(), p.YAxis.ToIO());
        }
    }
}
