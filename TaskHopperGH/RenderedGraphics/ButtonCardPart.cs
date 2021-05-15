using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using TaskHopper.Util;
namespace TaskHopper.RenderedGraphics
{
    class ButtonCardPart:CardPart
    {
        bool mouseHover = false;
        bool mousePressed = false;
        bool PointInButton(PointF pt) => Bounds.Contains(pt);
        private Action OnClick;

        public bool IsPressed => mousePressed;
        public void Press() => mousePressed = true;
        public void NotPressed() => mousePressed = false;
        public void Hover() => mouseHover = true;
        public void NotHover() => mouseHover = false;

        public ButtonCardPart(Bitmap icon, string text, Color fontColor, float extraWidth,Action onClick)
            : base(icon, text, fontColor, extraWidth) 
        {
            OnClick = onClick;
        }
        public ButtonCardPart(Bitmap icon, string text, Color fontColor, Action onClick)
            : base(icon, text, fontColor) 
        {
            OnClick = onClick;
        }

        public override void Render(Graphics graphics)
        {
            if(mouseHover && !mousePressed)
            {
                var brush = new SolidBrush(Color.FromArgb(40, Color.Black));
                graphics.FillRectangle(brush, Bounds);
                brush.Dispose();
            }
            if(mousePressed)
            {
                var brush = new SolidBrush(Color.FromArgb(70, Color.Black));
                graphics.FillRectangle(brush, Bounds);
                brush.Dispose();
            }
            base.Render(graphics);
        }

        public void ClickResponse() => OnClick();

        public static ButtonCardPart FolderButton(string link) => new ButtonCardPart(Resources.IconFolder, "", default, LinkOpening.GetLinkOpener(link));
    }
}
