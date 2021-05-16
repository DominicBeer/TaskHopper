using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using TaskHopper.Util;

namespace TaskHopper.RenderedGraphics
{
    static class TaskCardConstants
    {
        public static float Width => 180f;
        public static float NameBarHeight => 17f;
        public static float BorderThickness => 5f;
        public static float PartHeight => 14f;
        public static float PartIconWidth => 14f;
        public static float PaddingV => 3f;
        public static float PaddingH => 2f;
        public static Color GetLightCentralColor(Color color)
        {
            return Color.White;
        }

        public static Font PartFont => new Font("Arial", 8.0f);
        public static Font NameFont => new Font("Arial", 10.0f);

        public static Color OffBlack => Color.FromArgb(20, 20, 20);
        public static Color LightGrey => Color.FromArgb(190, 190, 190);


    }
}
