﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskHopper.Core
{
    public enum TaskStatus
    {
        Expired,
        ToDo,
        InProgress,
        Review,
        Done 
    }

    static class TaskStatusData
    {
        public static string AsString(this TaskStatus status) => mapper[status];

        private static Dictionary<TaskStatus, string> mapper = new Dictionary<TaskStatus, string>()
        {
            {TaskStatus.Expired, "Expired" },
            {TaskStatus.ToDo, "To Do" },
            {TaskStatus.InProgress, "In Progress" },
            {TaskStatus.Review, "Review" },
            {TaskStatus.Done, "Done" },
        };
    }

    static class TaskStatusColors
    {
        public static Color GetColor(this TaskStatus status) => mapper[status];

        private static Dictionary<TaskStatus, Color> mapper = new Dictionary<TaskStatus, Color>()
        {
            {TaskStatus.Expired, Color.FromArgb(198,22,22) },
            {TaskStatus.ToDo, Color.FromArgb(231,60,0) },
            {TaskStatus.InProgress, Color.FromArgb(6,118,198) },
            {TaskStatus.Review, Color.FromArgb(245,189,7) },
            {TaskStatus.Done, Color.FromArgb(28,175,20) },
        };
    }
    static class TaskStatusIcons
    {
        public static Bitmap GetIcon(this TaskStatus status) => mapper[status];

        private static Dictionary<TaskStatus, Bitmap> mapper = new Dictionary<TaskStatus, Bitmap>()
        {
            {TaskStatus.Expired, Resources.IconExpired },
            {TaskStatus.ToDo, Resources.IconToDo  },
            {TaskStatus.InProgress, Resources.IconInProgress  },
            {TaskStatus.Review, Resources.IconReview  },
            {TaskStatus.Done, Resources.IconDone },
        };
    }
}
