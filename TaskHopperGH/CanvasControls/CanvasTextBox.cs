using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskHopper.CanvasControls
{
    class CanvasTextBox : CanvasControl
    {
        string Text;
        Font Font;
        float PaddingH;
        float PaddingV;
        float TextAdjustV;
        Color FontColor;

        public CanvasTextBox(
            CanvasControl host,
            string text,
            Font font,
            Color fontColor,
            float height,
            float width,
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
            FontColor = fontColor;
            Size = new SizeF(width + 2 * paddingH, height + 2 * paddingV);

        }

        protected override void RenderBase(Graphics graphics)
        {
            RenderText(graphics);
        }

        private void RenderText(Graphics graphics)
        {
            var textBounds = new RectangleF(
                            Pivot.X + PaddingH,
                            Pivot.Y + PaddingV + TextAdjustV,
                            Size.Width - 2 * PaddingH,
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
