using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskHopper.RenderedGraphics
{
    class TagCardPart : CardPart
    {
        const float startPad = 4f;
        static float gap => startPad + TaskCardConstants.PaddingH - 1.5f;
        public TagCardPart(string text) 
            : base(null, text, TaskCardConstants.OffBlack, gap)
        {
        }
        public TagCardPart(string text, float maxTextWidth)
            : base(null, text, TaskCardConstants.OffBlack, gap,maxTextWidth)
        {
        }

        PointF[] GetTagBorder()
        { 
            var h = TaskCardConstants.PartHeight;
            SizeF[] border = {
                new SizeF(startPad,0f),
                new SizeF(Size.Width,0f),
                new SizeF(Size.Width,Size.Height),
                new SizeF(startPad,Size.Height),
                new SizeF(0f,Size.Height-startPad),
                new SizeF(0f,startPad)};

            return border.Select(vector => Pivot + vector).ToArray();
        }
        public override void Render(Graphics graphics)
        {
            //Render tag card
            var brush = new SolidBrush(TaskCardConstants.LightGrey);
            graphics.FillPolygon(brush, GetTagBorder());
            //Render text
            var tPiv = Pivot + new SizeF(startPad, 0f);
            var textBounds = new RectangleF(tPiv, new SizeF(Size.Width - gap, Size.Height));
            RenderText(graphics, textBounds);
            brush.Dispose();

        }
    }
    
}
