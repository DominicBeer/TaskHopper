using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskHopper.Util;

namespace TaskHopper.CanvasControls
{
    class IconButton : CanvasButton
    {
        Image Icon;
        float PaddingH;
        float PaddingV;

        public IconButton(
            CanvasControl host,
            Image icon,
            Action onClick,
            float height = 12f, 
            float paddingH = 2f, 
            float paddingV = 1f ) 
            : base(host, onClick)
        {
            var width = height / icon.Height * icon.Width;
            PaddingH = paddingH;
            PaddingV = paddingV;
            Size = new SizeF(width + 2 * PaddingH, height + 2 * PaddingV);
            Icon = icon;
        }

        protected override void RenderBase(Graphics graphics, LevelOfDetail lod)
        {
            if (lod == LevelOfDetail.High)
            {
                var imageBounds = new RectangleF(
                    Pivot.X + PaddingH,
                    Pivot.Y + PaddingV,
                    Size.Width - 2 * PaddingH,
                    Size.Height - 2 * PaddingV);
                graphics.DrawImage(Icon, imageBounds);
            }
            else if (lod == LevelOfDetail.Medium)
            {
                var brush = new SolidBrush(Color.Gray);
                graphics.FillRectangle(brush, Bounds.Shrink(PaddingH, PaddingV));
                brush.Dispose();
            }
        }



    }
}
