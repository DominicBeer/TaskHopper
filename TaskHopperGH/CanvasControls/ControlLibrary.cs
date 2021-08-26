using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static TaskHopper.Constants.TH_Fonts;
using static TaskHopper.Constants.TH_Dimensions;
using TaskHopper.Core;
using System.Drawing;
using TaskHopper.Util;
using System.Windows.Forms;

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
                : new CardLabel(host, Resources.IconDateTime, dateString, LabelFont, Color.Black, LabelHeight, LabelMaxTextWidth);
        }

        public static IconButton FolderButton(string link, CanvasControl host) => 
            new IconButton(host, Resources.IconFolder, LinkOpening.GetLinkOpener(link), LabelHeight);

        public static CanvasTextBox TaskName(string taskName, CanvasControl host) =>
            new CanvasTextBox(host, taskName, TitleFont, Color.Black, TitleHeight, InnerWidth);
        public static CanvasTextBox DescriptionBox(string description, CanvasControl host)
        {
            var dTestBounds = new Size((int)InnerWidth, int.MaxValue);
            var descriptionSize = TextRenderer.MeasureText(description, LabelFont, dTestBounds, TextFormatFlags.WordBreak);
            var dHeight = Math.Min(DescriptionMaxTextHeight, (float)descriptionSize.Height);
            return new CanvasTextBox(host, description, LabelFont, Color.Black, dHeight, InnerWidth);
        }

        public static FlowLayoutCanvasControlHost LabelHost(TH_Task task, CanvasControl host)
        {
            var labels = new List<CanvasControl>();

            //nulls are used as each label's initial, as this is later added during the Host object construction

            if (task.Owner != "")
            {
                labels.Add(PersonLabel(task.Owner, null));
            }

            labels.Add(StatusLabel(task.Status, null));

            if (task.HasDate)
            {
                labels.Add(DatePart(task.Date, task.IsLate, null));
            }

            if (task.Link != "")
            {
                labels.Add(FolderButton(task.Link, null));
            }

            foreach (var tag in task.Tags)
            {
                labels.Add(Tag(tag, null));
            }

            return new FlowLayoutCanvasControlHost(
                host.TopLevelAttributes, 
                labels, 
                InnerWidth, 
                LabelsPadH, 
                LabelsPadV,
                new Separator(null,SeparatorThk,LabelHeight,Color.LightGray));
        }
    }
}
