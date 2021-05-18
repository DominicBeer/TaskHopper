using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GH_IO.Serialization;
using Grasshopper.Kernel.Types;
using TaskHopper.Core;

namespace TaskHopper.Types
{
    public class GH_Task : GH_Goo<TH_Task>
    {
        public GH_Task() : base() { }

        internal GH_Task(TH_Task task, Guid source) : base()
        {
            Value = task;
            SourceComponentID = source;
        }

        internal readonly Guid SourceComponentID;
        public override bool IsValid => throw new NotImplementedException();

        public override string TypeName => "Task";

        public override string TypeDescription => "A TaskHopper Task";

        public override IGH_Goo Duplicate()
        {
            return (IGH_Goo)this.MemberwiseClone();
        }

        public override string ToString()
        {
            return $"TaskHopper task: {Value.Name}";
        }

        public override bool Read(GH_IReader reader)
        {
            Value = new TH_Task();
            Value.Read(reader);
            return true;
        }

        public override bool Write(GH_IWriter writer)
        {
            Value.Write(writer);
            return true;
        }
    }
}
