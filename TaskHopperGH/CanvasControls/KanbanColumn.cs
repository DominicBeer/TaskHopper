using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskHopper.Constants;

namespace TaskHopper.CanvasControls
{
    class KanbanColumn : FlowLayoutCanvasControlHost
    {
        public KanbanColumn(CanvasControlHost host, List<CanvasControl> controls, float width, float paddingH, float paddingV) : base(host, controls, width, paddingH, paddingV)
        {
        }

        protected override void RenderBase(Graphics graphics, LevelOfDetail lod)
        {
            graphics.FillRectangle(TH_Colors.KanbanColumnBackground, this.Bounds);
            base.RenderBase(graphics, lod);
        }
    }
}
