using GH_IO.Serialization;
using Grasshopper.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskHopper.Core;

namespace TaskHopper.Components
{
    class TH_Task_Goo : GH_Goo<TH_Task>
    {
        public TH_Task_Goo(TH_Task internal_data) : base(internal_data)
        {
        }

        public override bool IsValid => true;

        public override string TypeName => "Task";

        public override string TypeDescription => "A TaskHopper task, can represent any process in a workflow";

        public override IGH_Goo Duplicate()
        {
            return new TH_Task_Goo(this.Value);
        }

        public override string ToString()
        {
            return "Task: " + Value.Name;
        }

        public override bool Read(GH_IReader reader)
        {
            Value.Read(reader);
            return base.Read(reader);
        }

        public override bool Write(GH_IWriter writer)
        {
            Value = new TH_Task();
            Value.Write(writer);
            return base.Write(writer);
        }
    }
}
