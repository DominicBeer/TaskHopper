using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TaskHopper.Constants
{
    static class TH_Dimensions
    {
        public static float LabelHeight = 12f;
        public static float LabelMaxTextWidth = 100f;
        public static float CardWidth = 250f;
        public static float InnerWidth = 235f;
        public static float TitleHeight = 16f;
        public static float LabelsPadH = 2f;
        public static float LabelsPadV = 2f;
        public static float SeparatorThk = 2f;
        public static float CardLeftMargin = 5f;
        public static float CardPadV = 5f;
        public static float ChinHeight = 15f;
        public static float FlowBarTotalHeight = 13f;
        public static float FlowBarHeight = 10f;
        public static float BorderWidth = 2f;
        public static float KanbanColumnWidth => CardWidth + 2 * KanbanPadding;
        public static float KanbanPadding => 15f;

        public static float DescriptionMaxTextHeight = 60f;
    }
}
