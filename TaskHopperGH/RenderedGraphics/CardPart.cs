using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Grasshopper.Kernel;
using TaskHopper.Core;

namespace TaskHopper.RenderedGraphics
{
    class CardPart
    {
        float MaxTextWidth = 110f;
        private Bitmap Icon { get; }
        public SizeF Size { get; }
        public float Width => Size.Width;
        public float Height => Size.Height;

        private Color FontColor;
        private string Text { get; }
        public Font Font { get; }

        public RectangleF Bounds => new RectangleF(Pivot, Size);
        private bool HasIcon => Icon != null;
        private bool HasText => Text != "" || Text != null;

        public PointF Pivot { get; set; }

        
        public CardPart( Bitmap icon, string text, Color fontColor, float extraWidth, float maxTextWidth)
        {
            Icon = icon;
            Text = text;
            Font = TaskCardConstants.PartFont;
            FontColor = fontColor;
            MaxTextWidth = maxTextWidth;

            float width = extraWidth;

            if (HasIcon)
            {
                width += TaskCardConstants.PartIconWidth;
            }
            if (HasIcon && HasText)
            {
                width += TaskCardConstants.PaddingH; 
            }
            if (HasText)
            {
                width += Math.Min(MaxTextWidth, (float)GH_FontServer.StringWidth(text, Font));
            }
            Size = new SizeF(width, TaskCardConstants.PartHeight);
        }

        public CardPart(Bitmap icon, string text, Color fontColor, float extraWidth) 
            : this(icon, text, fontColor, extraWidth, 110f){ }

        public CardPart(Bitmap icon, string text, Color fontColor) 
            : this(icon, text, fontColor, 0f) { }




        public virtual void Render(Graphics graphics)
        {
            
            if (HasIcon)
            {
                RectangleF iconBounds = new RectangleF(new PointF(Pivot.X + 1f, Pivot.Y), new SizeF(TaskCardConstants.PartIconWidth, Size.Height));
                graphics.DrawImage(Icon, iconBounds);
            }
            if (HasText )
            {
                RectangleF textBounds;
                if (HasIcon)
                {
                    var gap = TaskCardConstants.PartIconWidth + TaskCardConstants.PaddingH;
                    var tPivot = new PointF(Pivot.X + gap, Pivot.Y);
                    textBounds = new RectangleF(tPivot, new SizeF(Size.Width - gap, Size.Height));
                }
                else
                {
                    textBounds = Bounds;
                }
                RenderText(graphics, textBounds);
            }
        }

        protected void RenderText(Graphics graphics, RectangleF textBounds)
        {
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;
            format.LineAlignment = StringAlignment.Center;
            format.Trimming = StringTrimming.EllipsisWord;
            var brush = new SolidBrush(FontColor);
            graphics.DrawString(Text, Font, brush, textBounds, format);
            brush.Dispose();
        }

        public static CardPart StatusPart(TaskStatus status) => new CardPart( status.GetIcon(), status.AsString(), status.GetColor());
        public static CardPart PersonPart(string personName) => new CardPart( Resources.IconOwner, personName, TaskCardConstants.OffBlack);
        public static CardPart DatePart(DateTime date, bool late)
        {
            var dateString = date.Year == DateTime.Now.Year
                ? date.ToString("MMMM dd")
                : date.ToString("MMMM dd, yyyy");
            return late
                ? new CardPart(Resources.IconDateTimeLate, $"{dateString}!", TaskStatus.Expired.GetColor())
                : new CardPart(Resources.IconDateTime, dateString, TaskCardConstants.OffBlack);
        }





    }
}
