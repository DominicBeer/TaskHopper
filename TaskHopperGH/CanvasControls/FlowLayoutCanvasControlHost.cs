using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskHopper.CanvasControls
{
    abstract class FlowLayoutCanvasControlHost : CanvasControlHost
    {
        float Width;
        float PaddingH;
        float PaddingV;
        bool includeSeparators = false;
        float sThk;
        Color sColor;


        public FlowLayoutCanvasControlHost(IGH_Attributes parent, List<CanvasControl> controls, float width, float paddingH, float paddingV) : base(parent)
        {
            controls.ForEach(c => AddControl(c, default));
            Width = width;
            PaddingH = paddingH;
            PaddingV = paddingV;
            Layout();
        }

        public FlowLayoutCanvasControlHost(CanvasControlHost host, List<CanvasControl> controls, float width, float paddingH, float paddingV) : 
            this(host.TopLevelAttributes, controls, width, paddingH, paddingV )
        {
        }

        public FlowLayoutCanvasControlHost(IGH_Attributes parent, List<CanvasControl> controls, float width, float paddingH, float paddingV, Separator separatorRef) :
            this(parent, controls, width, paddingH, paddingV)
        {
            includeSeparators = true;
            sThk = separatorRef.Thk;
            sColor = separatorRef.Color;
        }

        public void AddControl(CanvasControl control)
        {
            AddControl(control, default);
            Layout();
        }

        public void Layout()
        {
            var piv = new SizeF(PaddingH, PaddingV);
            var height = SubControls.Select(t => t.subControl.Size.Height).Max();
            var newControls = new List<(CanvasControl subControl, SizeF relPivot)>();
            for (int i = 0; i < SubControls.Count - 1; i++)
            {
                var T = SubControls[i];
                var nT = SubControls[i + 1];
                newControls.Add((T.subControl, piv));

                var W = T.subControl.Size.Width;
                var nW = nT.subControl.Size.Width;

                var doubleWidth = piv.Width + W + nW + 4 * PaddingH;
                if (doubleWidth < Width) //next part fits on same row
                {
                    piv.Width += W + PaddingH;
                    if (includeSeparators)
                    { 
                        newControls.Add((new Separator(this, sThk, height, sColor), piv));
                        piv.Width += PaddingH;
                    }
                }
                else
                {
                    piv.Width = PaddingH;
                    piv.Height += PaddingV + height;
                }
            }
            newControls.Add((SubControls.Last().subControl, piv));
            SubControls = newControls;
        }

    }
}
