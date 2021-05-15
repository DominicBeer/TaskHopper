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

        public static Point3d FromIO(this GH_Point3D p)
        {
            return new Point3d(p.x, p.y, p.z);
        }
        public static Vector3d ToVector3d(this GH_Point3D p)
        {
            return new Vector3d(p.x, p.y, p.z);
        }
        public static Interval FromIO(this GH_Interval1D d)
        {
            return new Interval(d.a, d.b);
        }
        public static Grasshopper.Kernel.Types.UVInterval FromIO(this GH_Interval2D d)
        {
            return new Grasshopper.Kernel.Types.UVInterval(d.u.FromIO(), d.v.FromIO());
        }
        public static Line FromIO(this GH_Line l)
        {
            return new Line(l.A.FromIO(), l.B.FromIO());
        }
        public static BoundingBox FromIO(this GH_BoundingBox b)
        {
            return new BoundingBox(b.Min.FromIO(), b.Max.FromIO());
        }
        public static Plane FromIO(this GH_Plane p)
        {
            return new Plane(p.Origin.FromIO(), p.XAxis.ToVector3d(), p.YAxis.ToVector3d());
        }
    }
}
