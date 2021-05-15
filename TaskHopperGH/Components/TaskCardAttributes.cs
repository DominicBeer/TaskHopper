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

namespace TaskHopper.Components
{
    class TaskCardAttributes : GH_Attributes<TaskCardComponent>
    {
        private static float b = 4f;
        public PointF CardPivot
        {
            get
            {
                var pf = Pivot + new SizeF(b, b);
                var x = (float)(int)pf.X;
                var y = (float)(int)pf.Y;
                return new PointF(x, y);
            }
        }

        public RectangleF CardBounds => new RectangleF(CardPivot, new SizeF(Card.Width, Card.Height));
        
        public TaskCardAttributes(TaskCardComponent owner) : base(owner)
        {
            Card = new TaskCard(owner.SolvedTask, Pivot);
        }
        public TaskCard Card { get; }
        public override RectangleF Bounds 
        { 
            get => new RectangleF(Pivot, new SizeF(Card.Width + 2*b, Card.Height + 2*b)); 
        }

        public override GH_ObjectResponse RespondToMouseDoubleClick(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            var pt = e.CanvasLocation;
            foreach(var button in Card.Buttons)
            {
                if(button.Bounds.Contains(pt))
                {
                    button.ClickResponse();
                    return GH_ObjectResponse.Handled;
                }
            }
            if(CardBounds.Contains(pt))
            {
                var editForm = new Forms.EditTaskForm();
                editForm.ShowDialog();
                return GH_ObjectResponse.Handled;
            }
            return base.RespondToMouseDoubleClick(sender, e);
        }
        public override GH_ObjectResponse RespondToMouseDown(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            var pt = e.CanvasLocation;
            foreach (var button in Card.Buttons)
            {
                if (button.Bounds.Contains(pt) && e.Button == MouseButtons.Left)
                {
                    button.Press();
                    Owner.OnDisplayExpired(true);
                    return GH_ObjectResponse.Capture;
                }
                else
                {
                    button.NotPressed();
                }
            }
            
            return base.RespondToMouseDown(sender, e);
        }

        public override GH_ObjectResponse RespondToMouseUp(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            var pt = e.CanvasLocation;
            foreach (var button in Card.Buttons)
            {
                if (button.Bounds.Contains(pt) && e.Button == MouseButtons.Left && button.IsPressed)
                {
                    button.ClickResponse();
                    button.NotPressed();
                    Owner.OnDisplayExpired(true);
                    return GH_ObjectResponse.Release;
                }
                else
                {
                    button.NotPressed();
                }
            }

            return base.RespondToMouseUp(sender, e);
        }

        public override GH_ObjectResponse RespondToMouseMove(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            var pt = e.CanvasLocation;
            foreach (var button in Card.Buttons)
            {
                if (button.Bounds.Contains(pt) && e.Button == MouseButtons.None)
                {
                    button.Hover();
                    Owner.OnDisplayExpired(true);
                    return GH_ObjectResponse.Handled;
                }
                else
                {
                    button.NotHover();
                    Owner.OnDisplayExpired(true);
                }
            }
            return base.RespondToMouseMove(sender, e);
        }
        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {

            if (channel == GH_CanvasChannel.Objects)
            {
                var palette = Owner.SolvedTask.Status == Core.TaskStatus.Expired
                    ? GH_Palette.Warning
                    : GH_Palette.Normal;
                GH_Capsule capsule = GH_Capsule.CreateCapsule(Bounds, palette);
                capsule.AddInputGrip((int)Pivot.Y + (int)Bounds.Height / 2);
                capsule.AddOutputGrip((int)Pivot.Y + (int)Bounds.Height / 2);
                capsule.Render(graphics, Selected, Owner.Locked, true);
                capsule.Dispose();
                Card.Render(graphics, CardPivot);
            }
            
        }
    }
}
