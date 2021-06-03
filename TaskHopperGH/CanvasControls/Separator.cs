using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskHopper.CanvasControls
{
    class Separator : CanvasControl
    {
        internal readonly float Thk;
        internal readonly float Height;
        internal readonly Color Color;
        public Separator(CanvasControl host, float thickness, float height,  Color color) : base(host)
        {
            Thk = thickness;
            Height = height;
            Color = color;
        }

        protected override void RenderBase(Graphics graphics)
        {
            var brush = new SolidBrush(Color);
            var pen = new Pen(brush, Thk);
            graphics.DrawLine(pen, Pivot, Pivot + new SizeF(0, Height));
            brush.Dispose();
            pen.Dispose();
        }
    }
}
