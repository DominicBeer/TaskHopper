using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace TaskHopper.CanvasControls
{
  

    abstract class CanvasButton : CanvasControl
    {
        bool pressed = false;
        bool pressedOff = false;
        bool hover = false;

        Action OnClick;
        Action OnDoubleClick;

        protected CanvasButton(CanvasControlHost host, Action onClick, Action onDoubleClick) : base(host)
        {
            OnClick = onClick;
            OnDoubleClick = onDoubleClick;
        }

        /// <summary>
        /// Can be overridden for non rectangular button. 
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        protected virtual bool OnButton(PointF pt)
        {
            return (new RectangleF(Pivot, Size)).Contains(pt);
        }

        public override void RenderAt(PointF pivot, Graphics graphics)
        {
            Pivot = pivot;
            if (pressed && hover)
            {
                RenderPressedOn(graphics);
            }
            else if (pressed)
            {
                RenderPressedOff(graphics);
            }
            else if (hover)
            {
                RenderHover(graphics);
            }
            else
            {
                RenderBase(graphics);
            }
        }

        protected virtual void RenderHover(Graphics graphics)
        {
            RenderTransparentBounds(graphics, 40);
            RenderBase(graphics);
        }
        protected virtual void RenderPressedOn(Graphics graphics)//mouse is pressed on button
        {
            RenderTransparentBounds(graphics, 70);
            RenderBase(graphics);
        }
        protected virtual void RenderPressedOff(Graphics graphics)//mouse has pressed on button but cursor since moved out of buttons bounds.
        {
            RenderTransparentBounds(graphics, 40);
            RenderBase(graphics);
        }

        private void RenderTransparentBounds(Graphics graphics, int transparency)
        {
            var brush = new SolidBrush(Color.FromArgb(transparency, Color.Black));
            graphics.FillRectangle(brush, Bounds);
            brush.Dispose();
        }
        internal override GH_ObjectResponse RespondToMouseDoubleClick(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            var pt = e.CanvasLocation;
            if (OnButton(pt))
            {
                OnDoubleClick();
                return GH_ObjectResponse.Handled;
            }
            return GH_ObjectResponse.Ignore;
        }
        internal override GH_ObjectResponse RespondToMouseDown(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            var pt = e.CanvasLocation;
            if (OnButton(pt) && e.Button == MouseButtons.Left)
            {
                pressed = true;
                TriggerRedraw();
                return GH_ObjectResponse.Capture;
            }
            pressed = false;
            return GH_ObjectResponse.Ignore;
        }
        internal override GH_ObjectResponse RespondToMouseUp(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            var pt = e.CanvasLocation;
            if (OnButton(pt) && pressed)
            {
                pressed = false;
                OnClick();
                return GH_ObjectResponse.Release;
            }
            if (pressed) //mouse held down then moved off button
            {
                pressed = false;
                return GH_ObjectResponse.Release;
            }
            return GH_ObjectResponse.Ignore;
        }

        internal override GH_ObjectResponse RespondToMouseMove(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            var pt = e.CanvasLocation;
            var onBtn = OnButton(pt);
            pressedOff = pressed && !onBtn;
            if (onBtn)
            {
                hover = true;
                TriggerRedraw();
                return GH_ObjectResponse.Handled;
            }
            if (hover && !onBtn)
            {
                hover = false;
                TriggerRedraw();
                return GH_ObjectResponse.Handled;
            }
            hover = false;
            return GH_ObjectResponse.Ignore;
        }
    }



    
}
