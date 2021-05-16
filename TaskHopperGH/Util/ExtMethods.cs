using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskHopper.Util
{
    static class ExtMethods
    {
        public static PointF Centre(this RectangleF r)
        {
            return new PointF(r.X + r.Width / 2f, r.Y + r.Height / 2f);
        }
    }
}
