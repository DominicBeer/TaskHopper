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


    abstract class CanvasControl
    {
        public SizeF Size { get; protected set; }
        protected PointF Pivot;
        protected RectangleF Bounds => new RectangleF(Pivot, Size);
        internal IGH_Attributes TopLevelAttributes { get; private set; }
        internal void SetTopLevelAttributes(IGH_Attributes attributes) => TopLevelAttributes = attributes;
        public virtual void RenderAt(PointF pivot, Graphics graphics)
        {
            Pivot = pivot;
            RenderBase(graphics);
        }

        protected CanvasControl(IGH_Attributes parent)
        {
            TopLevelAttributes = parent;
        }

        protected CanvasControl(CanvasControl host)
        {
            TopLevelAttributes = host.TopLevelAttributes;
        }

        protected void TriggerRedraw() => TopLevelAttributes.DocObject.OnDisplayExpired(true);

        /// <summary>
        /// Abstract method where rendering is implemented
        /// </summary>
        /// <param name="graphics"></param>
        protected abstract void RenderBase(Graphics graphics);

        /// <summary>
        /// Double clicks should only return "Handled" or "Ignore"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        internal virtual GH_ObjectResponse RespondToMouseDoubleClick(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            return GH_ObjectResponse.Ignore;
        }

        /// <summary>
        /// Mouse down will typically capture a subobject, this capture will be then passed back down the stack, otherwise ignore.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        internal virtual GH_ObjectResponse RespondToMouseDown(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            return GH_ObjectResponse.Ignore;
        }

        /// <summary>
        /// If captured the mouse up should release the mouse event. Typically will need to check if mouse is still in bounds.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        internal virtual GH_ObjectResponse RespondToMouseUp(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            return GH_ObjectResponse.Ignore;
        }

        /// <summary>
        /// Moves will be handled if they do something 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        internal virtual GH_ObjectResponse RespondToMouseMove(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            return GH_ObjectResponse.Ignore;
        }


    }
}
