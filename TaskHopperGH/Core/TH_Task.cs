using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskHopper.Util;
using GH_IO;
using GH_IO.Serialization;
using TaskHopper.Util.Serialization;
using static TaskHopper.Util.Serialization.GetSetDelegates;
using System.Diagnostics;
using System.Windows.Forms;
using TaskHopper.Components;

namespace TaskHopper.Core
{
    public class TH_Task: GH_ISerializable
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Owner { get; private set; }
        public string Link { get; private set; }
        public List<string> Tags => _tags.ToList();
        public Color Color { get; private set; }
        public bool HasDate { get; private set; }
        public DateTime Date { get; private set; }
        public TaskStatus Status { get; private set; }
        public TaskStatus StatusIn { get; private set; }
        internal TaskCardComponent Source { get;  set; }

        private ImmutableHashSet<string> _tags;

        public bool IsLate => DateTime.Now.Ticks > Date.Ticks && Status != TaskStatus.Done;
        public string StatusString => Status.AsString();

        public TH_Task ChangeColor(Color color)
        {
            var outTask = (TH_Task)this.MemberwiseClone();
            outTask.Color = color;
            return outTask;
        }
        public TH_Task SetStatusIn(TaskStatus status)
        {
            var outTask = (TH_Task)this.MemberwiseClone();
            outTask.StatusIn = status;
            return outTask;
        }
        /// <summary>
        /// Constructor for serialization only
        /// </summary>
        internal TH_Task() { } 
        public TH_Task(string name, string description, string owner, string link, Color color, DateTime date, TaskStatus status, IEnumerable<string> tags, TaskCardComponent source)
        {
            Name = name;
            Description = description;
            Owner = owner;
            Link = link;
            Color = color;
            HasDate = true;
            Date = date;
            Status = status;
            StatusIn = status;
            Source = source;
            _tags = new ImmutableHashSet<string>(tags);
        }

        public TH_Task(string name, string description, string owner, string link, Color color, TaskStatus status, IEnumerable<string> tags, TaskCardComponent source)
        {
            Name = name;
            Description = description;
            Owner = owner;
            Link = link;
            Color = color;
            HasDate = false;
            Date = default;
            Status = status;
            StatusIn = status;
            Source = source;
            _tags = new ImmutableHashSet<string>(tags);
        }

        public bool Write(GH_IWriter writer)
        {
            writer.SetString("tName", Name);
            writer.SetString("tDescription", Description);
            writer.SetString("tOwner", Owner);
            writer.SetString("tLink", Link);
            writer.SetBoolean("tHasDate", HasDate);
            writer.SetDrawingColor("tColor", Color);
            if(HasDate)
            {
                writer.SetDate("tDate", Date);
            }
            writer.SetInt32("tStatus", (int)Status);
            writer.SetEnumerable("tTags", _tags, WriteString);
            return true;
        }

        public bool Read(GH_IReader reader)
        {
            Name = reader.GetString("tName");
            Description = reader.GetString("tDescription");
            Owner = reader.GetString("tOwner");
            Link = reader.GetString("tLink");
            Color = reader.GetDrawingColor("tColor");
            HasDate = reader.ItemExists("tHasDate") 
                ? reader.GetBoolean("tHasDate")
                : false;
            if (HasDate)
            {
                Date = reader.GetDate("tDate");
            }
            Status = (TaskStatus)reader.GetInt32("tStatus");
            StatusIn = Status;
            _tags = new ImmutableHashSet<string>(reader.GetEnumerable("tTags",ReadString));
            return true;
        }
    }
}
