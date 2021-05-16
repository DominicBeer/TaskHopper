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
using TaskHopper.Util;

namespace TaskHopper.Components
{
    class TaskCardAttributes : GH_ComponentAttributes
    {
        private static float b = 8f;
        private static float h = 4f;
        public PointF CardPivot
        {
            get
            {
                var pf = Pivot + new SizeF(b, h);
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
        public TaskCard Card { get; private set; }
        public override RectangleF Bounds 
        { 
            get => new RectangleF(Pivot, new SizeF(Card.Width + 2*b, Card.Height + 2*h)); 
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
                var editForm = new Forms.EditTaskForm(((TaskCardComponent)Owner).SolvedTask, Util.TestData.names, TestData.tags);
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
                    if(button.IsPressed)
                    {
                        button.NotPressed();
                        Owner.OnDisplayExpired(true);
                        return GH_ObjectResponse.Release;
                    }
                    
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

        protected override void Layout()
        {
            float pb = 2f;
            var inAtts = Owner.Params.Input[0].Attributes;
            var outAtts = Owner.Params.Output[0].Attributes;
            inAtts.Bounds = new RectangleF(Pivot.X + pb, Pivot.Y + pb, pb, Bounds.Height - 2 * pb);
            outAtts.Bounds = new RectangleF(Pivot.X + Bounds.Width - 2 * pb, Pivot.Y + pb, pb, Bounds.Height - 2 * pb);
            inAtts.Pivot = inAtts.Bounds.Centre();
            outAtts.Pivot = outAtts.Bounds.Centre();

        }
        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {
            if (channel == GH_CanvasChannel.Wires)
            {
                var inAtts =  Owner.Params.Input[0].Attributes;
                inAtts.RenderToCanvas(canvas, channel);
            }
            if (channel == GH_CanvasChannel.Objects)
            {
                GH_Capsule capsule = GH_Capsule.CreateCapsule(Bounds, GH_Palette.Normal);
                capsule.AddInputGrip((int)Pivot.Y + (int)(Bounds.Height / 2f));
                capsule.AddOutputGrip((int)Pivot.Y + (int)(Bounds.Height / 2f));
                capsule.Render(graphics, Selected, Owner.Locked, true);
                capsule.Dispose();
                Card.Render(graphics, CardPivot);
            }
            
        }
    }
}
