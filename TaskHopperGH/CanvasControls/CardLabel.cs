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
    class CardLabel : CanvasControl
    {
        Image Icon;
        string Text;
        Font Font;
        float PaddingH;
        float PaddingV;
        float IconWidth;
        float TextWidth;
        float TextAdjustV;
        Color FontColor;

        public CardLabel(
            CanvasControl host,
            Image icon,
            string text,
            Font font,
            Color fontColor,
            float height, 
            float maxTextWidth, 
            float paddingH = 2f, 
            float paddingV = 1f,
            float textAdjustV = 0f
            ) : base(host)
        {
            Icon = icon;
            Text = text;
            Font = font;
            PaddingH = paddingH;
            PaddingV = paddingV;
            TextAdjustV = textAdjustV;
            FontColor = fontColor;

            IconWidth = height / icon.Height * icon.Width;
            var textSize = TextRenderer.MeasureText(text, font);
            TextWidth = Math.Min(maxTextWidth, textSize.Width);
            var totalWidth = paddingH + IconWidth + paddingH + TextWidth + paddingH;

            Size = new SizeF(totalWidth, height + 2 * paddingV);

        }

        protected override void RenderBase(Graphics graphics, LevelOfDetail lod)
        {
            if (lod == LevelOfDetail.High)
            {
                RenderIcon(graphics);
                RenderText(graphics);
            }
            else if (lod == LevelOfDetail.Medium)
            {
                var brush = new SolidBrush(FontColor.Lighten());
                graphics.FillRectangle(brush, Bounds.Shrink(PaddingH, PaddingV));
                brush.Dispose();
            }

        }

        private void RenderText(Graphics graphics)
        {
            var textBounds = new RectangleF(
                            Pivot.X + 2 * PaddingH + IconWidth,
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

        private void RenderIcon(Graphics graphics)
        {
            var imageBounds = new RectangleF(
                            Pivot.X + PaddingH,
                            Pivot.Y + PaddingV,
                            IconWidth,
                            Size.Height - 2 * PaddingV);

            graphics.DrawImage(Icon, imageBounds);
        }
    }
}
