using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using TaskHopper.Core;
using Grasshopper.Kernel;
using TCC = TaskHopper.RenderedGraphics.TaskCardConstants;
using System.Windows.Forms;

namespace TaskHopper.RenderedGraphics
{
    class TaskCard
    {
        public CardPart[] Parts { get; }
        public ButtonCardPart[] Buttons { get; }
        public PointF[] Separators { get; private set; }
        public string Name { get; }
        private PointF Pivot;
        private Color Color;

        public float Width;
        public float Height;

        private string Description;
        private Size NameSize;
        private float BottomOfParts;
        private Size DescriptionSize;

        public TaskCard(TH_Task task, PointF pivot)
        {
            Name = task.Name;
            Description = task.Description;
            Color = task.Color;
            var parts = new List<CardPart>();

            if (task.Owner != "")
            {
                parts.Add(CardPart.PersonPart(task.Owner));
            }

            parts.Add(CardPart.StatusPart(task.Status));

            if (task.Date != default)
            {
                parts.Add(CardPart.DatePart(task.Date, task.IsLate));
            }

            if (task.Link != "")
            {
                parts.Add(ButtonCardPart.FolderButton(task.Link));
            }

            foreach(var tag in task.Tags)
            {
                parts.Add(new TagCardPart(tag));
            }

            Parts = parts.ToArray();
            Buttons = parts
                .Where(part => part is ButtonCardPart)
                .Select(btn => (ButtonCardPart)btn)
                .ToArray();
            
            Layout(pivot);
            
        }
        void MoveTo(PointF pivot)
        {
            var vector = new SizeF(pivot - new SizeF(Pivot));
            this.Pivot = pivot;
            foreach(var part in Parts)
            {
                part.Pivot = part.Pivot + vector;
            }
            for(int i =0; i< Separators.Length; i++)
            {
                Separators[i] = Separators[i] + vector;
            }
        }

        internal void Layout(PointF pivot) 
        {
            Pivot = new PointF(0f, 0f);
            var widths = new List<float>();
            widths.Add(TCC.Width);
            
            foreach(var part in Parts)
            {
                widths.Add(part.Width + 2 * TCC.PaddingH + 2 * TCC.BorderThickness);
            }


            Width = widths.Max();
            var seps = new List<PointF>();
            var rightEdgeX = TCC.PaddingH + TCC.BorderThickness;

            var nameTestBounds = new Size((int)(Width - 2 * TCC.PaddingH), int.MaxValue);
            NameSize = TextRenderer.MeasureText(Name, TCC.NameFont, nameTestBounds, TextFormatFlags.WordBreak);


            var piv = new PointF(TCC.PaddingH + TCC.BorderThickness, NameSize.Height + 3*TCC.PaddingV);
            for(int i = 0; i < Parts.Length-1; i++)
            {
                var part = Parts[i];
                var nextPart = Parts[i + 1];
                part.Pivot = piv;

                var doubleWidth = piv.X + part.Width + nextPart.Width + 4 * TCC.PaddingH;
                if(doubleWidth < Width) //next part fits on same row
                {
                    piv.X += part.Width + TCC.PaddingH;
                    seps.Add(new PointF(piv.X,piv.Y));
                    piv.X += TCC.PaddingH * 2;
                }
                else
                {
                    piv.X = rightEdgeX;
                    piv.Y += TCC.PaddingV + TCC.PartHeight;
                }
            }
            Separators = seps.ToArray();
            Parts.Last().Pivot = piv;
            BottomOfParts = piv.Y + TCC.PaddingV + TCC.PartHeight + TCC.BorderThickness;
            var dTestBounds = new Size((int)(Width - 2 * TCC.PaddingH -2*TCC.BorderThickness), int.MaxValue);
            DescriptionSize = TextRenderer.MeasureText(Description, TCC.PartFont, dTestBounds, TextFormatFlags.WordBreak);
            Height = BottomOfParts + 2 * TCC.PaddingV + DescriptionSize.Height;
            MoveTo(pivot);
        }

        public void Render(Graphics graphics, PointF pivot)
        {
            MoveTo(pivot);
            var border = new RectangleF(pivot, new SizeF(Width, Height));
            RenderBack(graphics, border);
            RenderBorder(graphics, border);
            RenderNameBar(graphics, pivot);
            RenderText(graphics, pivot);
            RenderDescription(graphics, pivot);

            foreach(var part in Parts)
            {
                part.Render(graphics);
            }
            var greyPen = new Pen(Brushes.LightGray, 1f);
            foreach (var p1 in Separators)
            {
                var p2 = new PointF(p1.X, p1.Y + TCC.PartHeight);
                graphics.DrawLine(greyPen,p1,p2);

            }
            greyPen.Dispose();
        }

        private void RenderText(Graphics graphics, PointF pivot)
        {
            var nameBounds = new RectangleF(pivot.X + TCC.PaddingH, pivot.Y + TCC.PaddingV, NameSize.Width, NameSize.Height);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;
            format.Trimming = StringTrimming.EllipsisWord;
            var txtBrush = new SolidBrush(TCC.OffBlack);
            graphics.DrawString(Name, TCC.NameFont, txtBrush, nameBounds, format);
            txtBrush.Dispose();
        }

        private void RenderDescription(Graphics graphics, PointF pivot)
        {
            var dBounds = new RectangleF(pivot.X + TCC.PaddingH + TCC.BorderThickness, pivot.Y + BottomOfParts + TCC.PaddingV, DescriptionSize.Width, DescriptionSize.Height);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;
            format.Trimming = StringTrimming.EllipsisWord;
            var txtBrush = new SolidBrush(TCC.OffBlack);
            graphics.DrawString(Description, TCC.PartFont, txtBrush, dBounds, format);
            txtBrush.Dispose();
        }

        private void RenderNameBar(Graphics graphics, PointF pivot)
        {
            var borderBrush = new SolidBrush(Color);
            var nameBar = new RectangleF(new PointF(pivot.X +1f, pivot.Y +1f), new SizeF(Width - 2f, NameSize.Height + TCC.PaddingV));
            graphics.FillRectangle(borderBrush, nameBar);

            borderBrush.Dispose();
        }

        private void RenderBorder(Graphics graphics, RectangleF border)
        {
            var blackPen = new Pen(Brushes.Black,TCC.BorderThickness);
            graphics.DrawRectangle(blackPen, border.X, border.Y, border.Width, border.Height);
            var borderBrush = new SolidBrush(Color);
            var borderPen = new Pen(borderBrush, TCC.BorderThickness);
            borderPen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
            graphics.DrawRectangle(borderPen, border.X, border.Y, border.Width, border.Height);
            borderPen.Dispose();
            borderBrush.Dispose();
            blackPen.Dispose();
        }

        private void RenderBack(Graphics graphics, RectangleF border)
        {
            var backColor = TCC.GetLightCentralColor(Color);
            var backBrush = new SolidBrush(backColor);
            graphics.FillRectangle(backBrush, border);
            backBrush.Dispose();
        }


    }
}
