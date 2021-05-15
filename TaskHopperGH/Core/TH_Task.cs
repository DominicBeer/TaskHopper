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
        public DateTime Date { get; private set; }
        public TaskStatus Status { get; private set; }

        private ImmutableHashSet<string> _tags;

        public bool IsLate => DateTime.Now.Ticks > Date.Ticks && Status != TaskStatus.Done;
        public string StatusString => Status.AsString();

        public TH_Task(string name, string description, string owner, string link, Color color, DateTime date, TaskStatus status, IEnumerable<string> tags)
        {
            Name = name;
            Description = description;
            Owner = owner;
            Link = link;
            Color = color;
            Date = date;
            Status = status;
            _tags = new ImmutableHashSet<string>(tags);
        }

        public bool Write(GH_IWriter writer)
        {
            writer.SetString("Name", Name);
            writer.SetString("Description", Description);
            writer.SetString("Owner", Owner);
            writer.SetString("Link", Link);
            writer.SetDrawingColor("Color", Color);
            writer.SetDate("Date", Date);
            writer.SetInt32("Status", (int)Status);
            writer.SetEnumerable("Tags", _tags, WriteString);
            return true;
        }

        public bool Read(GH_IReader reader)
        {
            Name = reader.GetString("Name");
            Description = reader.GetString("Description");
            Owner = reader.GetString("Owner");
            Link = reader.GetString("Link");
            Color = reader.GetDrawingColor("Color");
            Date = reader.GetDate("Date");
            Status = (TaskStatus)reader.GetInt32("Status");
            _tags = new ImmutableHashSet<string>(reader.GetEnumerable("Tags",ReadString));
            return true;
        }
    }
}
