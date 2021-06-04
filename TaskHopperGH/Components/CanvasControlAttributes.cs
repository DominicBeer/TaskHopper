using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Attributes;
using TaskHopper.RenderedGraphics;
using System.Windows.Forms;
using TaskHopper.Util;
using TaskHopper.Core;
using TaskHopper.CanvasControls;

namespace TaskHopper.Components
{
    class CanvasControlAttributes : GH_ComponentAttributes
    {
        public static CanvasControlAttributes TaskCardAttributes(IGH_Component parent, TH_Task task)
        {
            var atts = new CanvasControlAttributes(parent);
            var control = new TaskCardControl(atts, task);
            atts.Control = control;
            return atts;
        }
        public CanvasControlAttributes(IGH_Component component) : base(component)
        {
        }

        public void UpdateControl(CanvasControl control)
        {
            Control = control;
        }

        PointF IntPivot => new PointF((int)Pivot.X, (int)Pivot.Y);

        public override GH_ObjectResponse RespondToMouseDoubleClick(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            var response = Control.RespondToMouseDoubleClick(sender, e);
            if (response != GH_ObjectResponse.Ignore) 
            {
                return response;
            }
            return base.RespondToMouseDoubleClick(sender, e);
        }

        public override GH_ObjectResponse RespondToMouseDown(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            var response = Control.RespondToMouseDown(sender, e);
            if (response != GH_ObjectResponse.Ignore) 
            {
                return response;
            }
            return base.RespondToMouseDown(sender, e);
        }

        public override GH_ObjectResponse RespondToMouseUp(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            var response = Control.RespondToMouseUp(sender, e);
            if (response != GH_ObjectResponse.Ignore)
            {
                return response;
            }
            return base.RespondToMouseUp(sender, e);
        }

        public override GH_ObjectResponse RespondToMouseMove(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            var response = Control.RespondToMouseMove(sender, e);
            if (response != GH_ObjectResponse.Ignore)
            {
                return response;
            }
            return base.RespondToMouseMove(sender, e);
        }

        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {
            if (channel == GH_CanvasChannel.Wires)
            {
                var inAtts = Owner.Params.Input[0].Attributes;
                inAtts.Selected = this.Selected;
                inAtts.RenderToCanvas(canvas, channel);
            }
            if (channel == GH_CanvasChannel.Objects)
            {
                RenderGrips(graphics);
                Control.RenderAt(IntPivot, graphics);
                RenderOverlay(graphics);
            }
            else
            {
                base.Render(canvas, graphics, channel);
            }
        }

        private void RenderGrips(Graphics graphics)
        {
            var inPoints = Owner.Params.Input.Select(p => new PointF(p.Attributes.Bounds.Left, p.Attributes.Pivot.Y)).ToList();
            var outPoints = Owner.Params.Output.Select(p => new PointF(p.Attributes.Bounds.Right, p.Attributes.Pivot.Y)).ToList();
            inPoints.ForEach(pt => RenderGrip(graphics, pt));
            outPoints.ForEach(pt => RenderGrip(graphics, pt));
        }

        private static RectangleF SquareFromCentre(PointF centre, float sideLength)
        {
            return new RectangleF(
                centre.X - sideLength / 2,
                centre.Y - sideLength / 2,
                sideLength,
                sideLength);
        }

        private static void RenderGrip(Graphics graphics, PointF gripPoint)
        {
            graphics.FillEllipse(Brushes.Black, SquareFromCentre(gripPoint, 10f));
            graphics.FillEllipse(Brushes.White, SquareFromCentre(gripPoint, 6f));
        }

        float paramPad = 1f;

        protected override void Layout()
        {
            Bounds = new RectangleF(IntPivot, Control.Size);
            var inAtts = Owner.Params.Input.Select(p => p.Attributes).ToList();
            var outAtts = Owner.Params.Output.Select(p => p.Attributes).ToList();

            PositionParams(inAtts, Pivot.X + 2*paramPad);
            PositionParams(outAtts, Bounds.Right - 2*paramPad);
        }

        void PositionParams(List<IGH_Attributes> pAtts, float vAlign)
        {

            float pad = paramPad;
            var np = pAtts.Count;
            if (np != 0)
                { 
                var vSpace = Bounds.Height;

                var pV = (vSpace / np) - 2 * pad;

                foreach ((var att, var i) in pAtts.Enumerate())
                {
                    var corner = new PointF(vAlign - pad, Pivot.Y + i / np * vSpace);
                    att.Bounds = new RectangleF(corner, new SizeF(2 * pad, pV));
                    att.Pivot = att.Bounds.Centre();
                }
            }

        }

        private void RenderOverlay(Graphics graphics)
        {
            var condition = (Selected, Owner.RuntimeMessageLevel);
            var blankColor = Color.FromArgb(0, 255, 255, 255);
            SolidBrush sb = new SolidBrush(Color.FromArgb(0, 255, 255, 255));
            if (condition == (true, GH_RuntimeMessageLevel.Blank))
            {
                sb.Color = Constants.TH_Colors.SelectedNormal;
            }
            else if (condition == (true, GH_RuntimeMessageLevel.Remark))
            {
                sb.Color = Constants.TH_Colors.SelectedNormal;
            }
            else if (condition == (true, GH_RuntimeMessageLevel.Warning))
            {
                sb.Color = Constants.TH_Colors.SelectedWarning;
            }
            else if (condition == (true, GH_RuntimeMessageLevel.Error))
            {
                sb.Color = Constants.TH_Colors.SelectedError;
            }
            else if (condition == (false, GH_RuntimeMessageLevel.Warning))
            {
                sb.Color = Constants.TH_Colors.Warning;
            }
            else if (condition == (false, GH_RuntimeMessageLevel.Error))
            {
                sb.Color = Constants.TH_Colors.Error;
            }
            if (sb.Color != blankColor)
            {
                graphics.FillRectangle(sb, Bounds);
                
            }
            sb.Dispose();
        }

        public CanvasControl Control { get; private set; }
    }
}
