using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static TaskHopper.Constants.TH_Fonts;
using static TaskHopper.Constants.TH_Dimensions;
using TaskHopper.Core;
using System.Drawing;

namespace TaskHopper.CanvasControls
{
    static class ControlLibrary
    {
        public static TagLabel Tag(string text, CanvasControl host) => 
            new TagLabel(host, text, LabelFont,LabelHeight, LabelMaxTextWidth);

        public static CardLabel StatusLabel(TaskStatus status, CanvasControl host) => 
            new CardLabel(host, status.GetIcon(), status.AsString(), LabelFont, status.GetColor(), LabelHeight, LabelMaxTextWidth);

        public static CardLabel PersonLabel(string personName, CanvasControl host) =>
            new CardLabel(host, Resources.IconOwner, personName, LabelFont, Color.Black, LabelHeight, LabelMaxTextWidth);
        public static CardLabel DatePart(DateTime date, bool late, CanvasControl host)
        {
            var dateString = date.Year == DateTime.Now.Year
                ? date.ToString("MMMM dd")
                : date.ToString("MMMM dd, yyyy");
            return late
                ? new CardLabel(host, Resources.IconDateTimeLate, $"{dateString}!", LabelFont, TaskStatus.Expired.GetColor(), LabelHeight, LabelMaxTextWidth)
                : new CardLabel(host, Resources.IconDateTimeLate, dateString, LabelFont, Color.Black, LabelHeight, LabelMaxTextWidth);
        }
    }
}
