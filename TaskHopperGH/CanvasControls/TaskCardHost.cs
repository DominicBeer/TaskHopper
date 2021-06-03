using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskHopper.CanvasControls
{
    class TaskCardHost : CanvasControlHost
    {

        public TaskCardHost(IGH_Attributes parent) : base(parent)
        {
        }

        protected override void RenderBase(Graphics graphics)
        {
            throw new NotImplementedException();
        }
    }
}
