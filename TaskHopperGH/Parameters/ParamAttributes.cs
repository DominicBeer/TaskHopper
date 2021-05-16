using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using TaskHopper.Components;

namespace TaskHopper.Parameters
{
    class InputParamAttributes : GH_Attributes<IGH_Param>
    {
        
        public InputParamAttributes(IGH_Param owner, IGH_Attributes parentAtts) : base(owner)
        {
            Parent = parentAtts;
            
        }
        public override PointF Pivot => ((TaskCardAttributes)Parent).Pivot + new SizeF(0f, Parent.Bounds.Height / 2.0f);
        public override RectangleF Bounds => new RectangleF(Pivot,new SizeF( 0f, 0f));
        public override bool HasInputGrip => true;
        public override bool HasOutputGrip => false;
        public override PointF InputGrip => Pivot;
        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {
        }
    }
    class OutputParamAttributes : GH_Attributes<IGH_Param>
    {

        public OutputParamAttributes(IGH_Param owner, IGH_Attributes parentAtts) : base(owner)
        {
            Parent = parentAtts;

        }
        public override PointF Pivot => ((TaskCardAttributes)Parent).Pivot + new SizeF(Parent.Bounds.Width, Parent.Bounds.Height / 2.0f);
        public override RectangleF Bounds => new RectangleF(Pivot, new SizeF(0f, 0f));
        public override bool HasInputGrip => false;
        public override bool HasOutputGrip => true;
        public override PointF OutputGrip => Pivot;
        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {
        }
    }
}
