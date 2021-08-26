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
    abstract class CanvasControlHost : CanvasControl
    {
        protected List<(CanvasControl subControl, SizeF relPivot)> SubControls;
        bool MouseCaptured = false;

        CanvasControl CapturedControl = null;

        protected CanvasControlHost(IGH_Attributes parent) : base(parent)
        {
            SubControls = new List<(CanvasControl subControl, SizeF relPivot)>();
        }
        protected CanvasControlHost(CanvasControlHost host) : base(host.TopLevelAttributes)
        {
            SubControls = new List<(CanvasControl subControl, SizeF relPivot)>();
        }

        protected virtual void AddControl(CanvasControl control, SizeF pivot)
        {
            control.SetTopLevelAttributes(TopLevelAttributes);
            SubControls.Add((control, pivot));
        }

        public virtual void RemoveControl(CanvasControl control)
        {
            var pairs = SubControls.Where(t => t.subControl == control).ToList();
            pairs.ForEach(pair => SubControls.Remove(pair));
        }

        public override void RenderAt(PointF pivot, Graphics graphics, LevelOfDetail lod)
        {
            Pivot = pivot;
            RenderBase(graphics, lod);
            SubControls.ForEach(s => s.subControl.RenderAt(pivot + s.relPivot, graphics, lod));
        }


        internal override GH_ObjectResponse RespondToMouseDoubleClick(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            var pt = e.CanvasLocation;
            foreach ((var sub, var relPivot) in SubControls)
            {
                if (SubBounds(sub, relPivot).Contains(pt))
                {
                    var response = sub.RespondToMouseDoubleClick(sender, e);
                    if (response != GH_ObjectResponse.Ignore) //Hence will be "Handled"
                    {
                        return response;
                    }
                    break;
                }
            }
            return GH_ObjectResponse.Ignore;
        }


        internal override GH_ObjectResponse RespondToMouseDown(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            var pt = e.CanvasLocation;
            ClearUnhandledCaptures();
            foreach ((var sub, var relPivot) in SubControls)
            {
                if (SubBounds(sub, relPivot).Contains(pt))
                {
                    var response = sub.RespondToMouseDown(sender, e);
                    HandleCapture(sub, response);
                    return response;
                }
            }
            return GH_ObjectResponse.Ignore;
        }

        internal override GH_ObjectResponse RespondToMouseUp(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            if (MouseCaptured)
            {
                var response = CapturedControl.RespondToMouseUp(sender, e);
                if (response is GH_ObjectResponse.Release) //It really should be
                {
                    MouseCaptured = false;
                }
                return response;
            }
            var pt = e.CanvasLocation;
            foreach ((var sub, var relPivot) in SubControls)
            {
                if (SubBounds(sub, relPivot).Contains(pt))
                {
                    var response = sub.RespondToMouseUp(sender, e);
                    if (response != GH_ObjectResponse.Ignore)
                    {
                        return response;
                    }
                    break;
                }
            }
            return GH_ObjectResponse.Ignore;
        }

        internal override GH_ObjectResponse RespondToMouseMove(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            if (MouseCaptured)
            {
                var response = CapturedControl.RespondToMouseMove(sender, e);
                if (response is GH_ObjectResponse.Release)
                {
                    MouseCaptured = false;
                }
                return response;
            }
            var pt = e.CanvasLocation;
            foreach ((var sub, var relPivot) in SubControls)
            {
                var response = sub.RespondToMouseMove(sender, e);
                if (response != GH_ObjectResponse.Ignore)
                {
                    return response;
                }
            }
            return base.RespondToMouseMove(sender, e);
        }

        private void ClearUnhandledCaptures()
        {
            if (MouseCaptured)
            {
                MouseCaptured = false;
            }
        }

        private void HandleCapture(CanvasControl sub, GH_ObjectResponse response)
        {
            if (response is GH_ObjectResponse.Capture)
            {
                MouseCaptured = true;
                CapturedControl = sub;
            }
        }

        private RectangleF SubBounds(CanvasControl sub, SizeF relPivot) =>
            new RectangleF(Pivot + relPivot, sub.Size);

    }
}
