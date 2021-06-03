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
        Color FontColor = Color.Black;
        float Inset = 2f;

        public TagLabel(
            CanvasControl host,
            string text,
            Font font,
            float height,
            float maxTextWidth,
            float paddingH = 1f,
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
            var totalWidth = height/2 + TextWidth + paddingH + Inset/2;

            Size = new SizeF(totalWidth, height + 2 * paddingV);

        }

        PointF[] GetTagBorder()
        {
            var s = Size.Height / 3;
            var p = Inset;
            SizeF[] border = {
                new SizeF(s,p),
                new SizeF(Size.Width - p , p),
                new SizeF(Size.Width - p ,Size.Height - p),
                new SizeF(s,Size.Height - p ),
                new SizeF(p ,Size.Height - p/2 -s),
                new SizeF(p,s - p/2)};

            return border.Select(vector => Pivot + vector).ToArray();
        }

        protected override void RenderBase(Graphics graphics)
        {
            RenderTag(graphics);
            RenderText(graphics);

        }

        private void RenderTag(Graphics graphics)
        {
            var border = GetTagBorder();

            Pen borderPen = new Pen(TagEdge);
            borderPen.Width = 2 * Inset;
            borderPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
            graphics.DrawPolygon(borderPen, border);
            borderPen.Dispose();

            Brush innerBrush = new SolidBrush(TagBack);
            Pen innerPen = new Pen(innerBrush);
            innerPen.Width = Inset;
            innerPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
            graphics.DrawPolygon(innerPen, border);
            graphics.FillPolygon(innerBrush, border);
            innerBrush.Dispose();
            innerPen.Dispose();

            Brush holeBrush = new SolidBrush(TagEdge);
            var holeX = Pivot.X + Size.Height / 3 - Inset / 2;
            var holeY = Pivot.Y + Size.Height / 2 - Inset / 2;
            var hole = new RectangleF(holeX, holeY, Inset, Inset);
            graphics.FillEllipse(holeBrush, hole);
            holeBrush.Dispose();
        }

        private void RenderText(Graphics graphics)
        {
            var textBounds = new RectangleF(
                            Pivot.X + Size.Height/2,
                            Pivot.Y + PaddingV + TextAdjustV,
                            TextWidth,
                            Size.Height);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;
            format.LineAlignment = StringAlignment.Center;
            format.Trimming = StringTrimming.EllipsisCharacter;
            var brush = new SolidBrush(FontColor);
            graphics.DrawString(Text, Font, brush, textBounds, format);
            brush.Dispose();
        }



    }
}
