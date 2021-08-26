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

        public static RectangleF Shrink(this RectangleF r, float x, float y)
        {
            return new RectangleF(r.X + x, r.Y + y, r.Width - 2 * x, r.Height - 2 * y);
        }

        public static RectangleF Grow(this RectangleF r, float x, float y)
        {
            return new RectangleF(r.X - x, r.Y - y, r.Width + 2 * x, r.Height + 2 * y);
        }
    }
}
