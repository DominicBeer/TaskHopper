using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskHopper.Core;
using TaskHopper.Constants;

namespace TaskHopper.CanvasControls
{
    class KanbanBoard : CanvasControlHost
    {

        private Dictionary<string, List<TH_Task>> Board;
        private List<FlowLayoutCanvasControlHost> Columns;

        public KanbanBoard(IGH_Attributes parent, Dictionary<string, List<TH_Task>> board) : base(parent)
        {
        }

        protected override void RenderBase(Graphics graphics, LevelOfDetail lod)
        {
            throw new NotImplementedException();
        }
    }
}
