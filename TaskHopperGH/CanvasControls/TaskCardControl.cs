using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskHopper.Core;
using static TaskHopper.Constants.TH_Dimensions;
namespace TaskHopper.CanvasControls
{
    class TaskCardControl : CanvasControlHost
    {
        private Color Color;
        private TH_Task _task;
        public TaskCardControl(
            IGH_Attributes parent,
            TH_Task task) 
            : base(parent)
        {
            _task = task;
            AddControl(ControlLibrary.TaskName(task.Name, this),new SizeF(CardLeftMargin,CardPadV));
            var labelHost = ControlLibrary.LabelHost(task, this);
            AddControl(labelHost, new SizeF(CardLeftMargin, 2*CardPadV + TitleHeight));

            float h = 3 * CardPadV + TitleHeight + labelHost.Size.Height;
            
            if (task.Description != "")
            {
                h += CardPadV;
                var dBox = ControlLibrary.DescriptionBox(task.Description, this);
                AddControl(dBox, new SizeF(CardLeftMargin, h));
                h += dBox.Size.Height + CardPadV;
            }

            h += ChinHeight;

            Size = new SizeF(CardWidth, h);

            var statusBar = new StatusBar(task.StatusIn, task.Status, CardWidth, FlowBarTotalHeight, FlowBarHeight, this);
            AddControl(statusBar, new SizeF(0f, h - FlowBarTotalHeight));

            Color = task.Color;
        }

        internal override GH_ObjectResponse RespondToMouseDoubleClick(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            var response = base.RespondToMouseDoubleClick(sender, e);
            if (response != GH_ObjectResponse.Ignore) { return response; }
            if(Bounds.Contains(e.CanvasLocation))
            {
                LaunchEditForm();
                return GH_ObjectResponse.Handled;

            }
            return GH_ObjectResponse.Ignore;

        }

        private void LaunchEditForm()
        {
            if (_task.Source != null)
            {
                _task.Source.LaunchEditForm();
            }
            else
            {
                MessageBox.Show("Task has lost connection to its source component");
            }
        }
        
        protected override void RenderBase(Graphics graphics, LevelOfDetail lod)
        {
            
            var borderBrush = new SolidBrush(Color);
            var b = Bounds;
            float t = 3f;
            graphics.FillRectangle(borderBrush, b);
            graphics.FillRectangle(Brushes.White, b.X + t , b.Y + t, b.Width - 2*t, b.Height - 2*t - ChinHeight);
            borderBrush.Dispose();
           
        }
    }
}
