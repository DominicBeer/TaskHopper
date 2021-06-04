using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TaskHopper.Core;
using System.Drawing.Drawing2D;
namespace TaskHopper.CanvasControls
{
    class StatusBar : CanvasControl
    {
        Color ColorIn;
        Color ColorOut;
        float BarHeight;
        const float splitParam = 0.3f;

        RectangleF BarBounds => new RectangleF(
            Pivot.X,
            Pivot.Y + Size.Height - BarHeight,
            Size.Width,
            BarHeight);

        RectangleF InBounds => new RectangleF(
            Pivot.X ,
            Pivot.Y + Size.Height - BarHeight,
            Size.Width*1.1f*splitParam,
            BarHeight);
        RectangleF TransitionBounds => new RectangleF(
            Pivot.X + Size.Width * splitParam,
            Pivot.Y + Size.Height - BarHeight,
            Size.Width*(1-2*splitParam),
            BarHeight);
        RectangleF OutBounds => new RectangleF(
            Pivot.X + Size.Width - Size.Width * 1.1f*splitParam,
            Pivot.Y + Size.Height - BarHeight,
            Size.Width * 1.1f * splitParam,
            BarHeight);
        public StatusBar(TaskStatus statusIn, TaskStatus taskStatus, float width, float height, float barHeight, CanvasControl host) : base(host)
        {
            var statusOut = (TaskStatus)Math.Min((int)statusIn, (int)taskStatus);
            ColorIn = TaskStatusColors.GetColor(statusIn);
            ColorOut = TaskStatusColors.GetColor(statusOut);
            Size = new SizeF(width, height);
            BarHeight = barHeight;
        }

        protected override void RenderBase(Graphics graphics)
        {
            RenderBackground(graphics);
            RenderBar(graphics);
        }
        void RenderBackground(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.White, Bounds);
        }

        void RenderBar(Graphics graphics)
        {
            if(ColorIn==ColorOut)
            {
                var brush = new SolidBrush(ColorIn);
                graphics.FillRectangle(brush, BarBounds);
                brush.Dispose();
            }
            else
            {
                var brushIn = new SolidBrush(ColorIn);
                var brushOut = new SolidBrush(ColorOut);
                var tb = TransitionBounds;
                var brushTrans = new LinearGradientBrush(
                    tb.Location,
                    new PointF(tb.Right + 0.5f, tb.Top),
                    ColorIn,
                    ColorOut);
                graphics.FillRectangle(brushIn, InBounds);
                graphics.FillRectangle(brushOut, OutBounds);
                graphics.FillRectangle(brushTrans, tb);
                brushIn.Dispose();
                brushOut.Dispose();
                brushTrans.Dispose();
            }
        }
    }
}
