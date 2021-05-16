using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Attributes;

namespace TaskHopper.Components
{
    class TEST_ATTS : GH_ComponentAttributes
    {
        public TEST_ATTS(IGH_Component component) : base(component)
        {
        }

        protected override void Layout()
        {
            base.Layout();

            var pAtts = Owner.Params.Output[0].Attributes;
            var b = pAtts.Bounds;
            pAtts.Bounds = new RectangleF(b.X + 100f, b.Y, b.Width, b.Height);
        }

        public override RectangleF Bounds { get 
            { 
                var b = base.Bounds;
                return new RectangleF(b.Location, new SizeF(180f, b.Height));
            } }
    }
}
