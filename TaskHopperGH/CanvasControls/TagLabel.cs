using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TaskHopper.Constants.TH_Colors;

namespace TaskHopper.CanvasControls
{
    class TagLabel : CanvasControl
    {
        string Text;
        Font Font;
        float PaddingH;
        float PaddingV;
        float TextWidth;
        float TextAdjustV;
        float Inset = TagBrushes.Inset;

        public TagLabel(
            CanvasControl host,
            string text,
            Font font,
            float height,
            float maxTextWidth,
            float paddingH = 2f,
            float paddingV = 1f,
            float textAdjustV = 0f
            ) : base(host)
        {
            Text = text;
            Font = font;
            PaddingH = paddingH;
            PaddingV = paddingV;
            TextAdjustV = textAdjustV;

            var textSize = TextRenderer.MeasureText(text, font);
            TextWidth = Math.Min(maxTextWidth, textSize.Width);
            var totalWidth = height/2 + TextWidth + 2*paddingH + Inset/2;

            Size = new SizeF(totalWidth, height + 2 * paddingV);

        }

        PointF[] GetTagBorder()
        {
            var s = Size.Height / 2.5f;
            var p = Inset;
            var h = PaddingH;
            SizeF[] border = {
                new SizeF(s,p),
                new SizeF(Size.Width - 2*h - p , p),
                new SizeF(Size.Width - 2*h - p ,Size.Height - p),
                new SizeF(s,Size.Height - p ),
                new SizeF(p ,0.6f*Size.Height),
                new SizeF(p, 0.4f*Size.Height)};

            return border.Select(vector => Pivot + vector + new SizeF(h,0)).ToArray();
        }

        protected override void RenderBase(Graphics graphics)
        {
            RenderTag(graphics);
            RenderText(graphics);

        }

        private void RenderTag(Graphics graphics)
        {
            var border = GetTagBorder();
            graphics.DrawPolygon(TagBrushes.BorderPen, border);
            graphics.DrawPolygon(TagBrushes.InnerPen, border);
            graphics.FillPolygon(TagBrushes.InnerBrush, border);

            var holeX = Pivot.X + Size.Height / 3 - Inset / 2 + PaddingH;
            var holeY = Pivot.Y + Size.Height / 2 - Inset / 2;
            var hole = new RectangleF(holeX, holeY, Inset, Inset);
            graphics.FillEllipse(TagBrushes.HoleBrush, hole);
        }

        private void RenderText(Graphics graphics)
        {
            var textBounds = new RectangleF(
                            Pivot.X + Size.Height/2 + PaddingH,
                            Pivot.Y + PaddingV + TextAdjustV,
                            TextWidth,
                            Size.Height);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;
            format.LineAlignment = StringAlignment.Center;
            format.Trimming = StringTrimming.EllipsisCharacter;
            graphics.DrawString(Text, Font, Brushes.Black, textBounds, format);
        }

        static class TagBrushes
        {
            private static Pen GenBorderPen()
            {
                var bp = new Pen(TagEdge, 2 * Inset);
                bp.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                return bp;
            }

            public readonly static Pen BorderPen = GenBorderPen();

            private static Pen GenInnerPen()
            {
                Pen innerPen = new Pen(TagBack);
                innerPen.Width = Inset;
                innerPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                return innerPen;
            }

            public readonly static Pen InnerPen = GenInnerPen();

            public readonly static SolidBrush InnerBrush = new SolidBrush(TagBack);

            public readonly static SolidBrush HoleBrush = new SolidBrush(TagEdge);

            public const float Inset = 2f;
        }


    }
}
