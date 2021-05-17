using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC = TaskHopper.RenderedGraphics.TaskCardConstants;
namespace TaskHopper.RenderedGraphics
{
    //Currently unused refactor - implementing the buttons is tricky
    class CardPartZone
    {
        List<CardPart> Parts;
        List<PointF> Separators;
        float Width;
        float Height;
        PointF Pivot;

        public SizeF Size => new SizeF(Width, Height);


        public CardPartZone(IEnumerable<CardPart> parts, float width, PointF pivot)
        {
            Parts = parts.ToList();
            Width = width;
            Pivot = pivot;
            Layout();
        }

        public void Layout()
        {
            var seps = new List<PointF>();
            var piv = new PointF(TCC.PaddingH, TCC.PaddingV);
            for (int i = 0; i < Parts.Count - 1; i++)
            {
                var part = Parts[i];
                var nextPart = Parts[i + 1];
                part.Pivot = piv;

                var doubleWidth = piv.X + part.Width + nextPart.Width + 4 * TCC.PaddingH;
                if (doubleWidth < Width) //next part fits on same row
                {
                    piv.X += part.Width + TCC.PaddingH;
                    seps.Add(new PointF(piv.X, piv.Y));
                    piv.X += TCC.PaddingH * 2;
                }
                else
                {
                    piv.X = TCC.PaddingH;
                    piv.Y += TCC.PaddingV + TCC.PartHeight;
                }
            }
            Parts.Last().Pivot = piv;
            Separators = seps.ToList();
            Height = piv.Y + TCC.PaddingH;
        }

        public void MoveBy(SizeF vector)
        {
            Pivot += vector;
            foreach (var part in Parts)
            {
                part.Pivot += vector;
            }
            for (int i = 0; i < Separators.Count; i++)
            {
                Separators[i] = Separators[i] + vector;
            }
        }

        public void Render(Graphics graphics)
        {
            foreach (var part in Parts)
            {
                part.Render(graphics);
            }
            var greyPen = new Pen(Brushes.LightGray, 1f);
            foreach (var p1 in Separators)
            {
                var p2 = new PointF(p1.X, p1.Y + TCC.PartHeight);
                graphics.DrawLine(greyPen, p1, p2);

            }
            greyPen.Dispose();
        }


    }
}
