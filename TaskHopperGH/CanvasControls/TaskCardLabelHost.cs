using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskHopper.CanvasControls
{
    class TaskCardLabelHost : FlowLayoutCanvasControlHost
    {
        public TaskCardLabelHost(CanvasControlHost host) : base(host)
        {
        }

        protected override void RenderBase(Graphics graphics)
        {
            throw new NotImplementedException();
        }
    }
}
