using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using TaskHopper.Core;
using System.Linq;
using Grasshopper.Kernel.Types;

namespace TaskHopper.Components
{

    internal enum KanbanBoardMode
    {
        ByPerson,
        ByStatus
    }
    public class KanbanBoardComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the KanbanBoardComponent class.
        /// </summary>
        public KanbanBoardComponent()
          : base("NOT FINISHED Kanban Board", "NOT FINISHED Kanban Board",
              "DO NOT USE - NOT FINISHED - WILL NOT WORK - A Kanban Board for organising tasks",
              "TaskHopper", "Kanban Board")
        {
        }

        private KanbanBoardMode Mode = KanbanBoardMode.ByStatus;

        private Dictionary<string, List<TH_Task>> Board;
        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("", "", "Cards", GH_ParamAccess.tree);
            var param = this.Params.Input[0];
            param.Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Grasshopper.Kernel.Data.GH_Structure<IGH_Goo> tree;
            DA.GetDataTree(0, out tree);
            var tasksIn = tree.Where(goo => goo is TH_Task_Goo).Select(goo => ((TH_Task_Goo)goo).Value).ToArray();

            Dictionary<string, List<TH_Task>> board = new Dictionary<string, List<TH_Task>>();

            if(Mode == KanbanBoardMode.ByStatus)
            {
                var statusNames = Enum.GetValues(typeof(TaskStatus))
                    .Cast<TaskStatus>()
                    .Select(status => status.AsString())
                    .ToList();
                statusNames.ForEach(name => board.Add(name, new List<TH_Task>()));

                foreach(var task in tasksIn)
                {
                    board[task.Status.AsString()].Add(task);
                }
            }
            if(Mode == KanbanBoardMode.ByPerson)
            {
                foreach (var task in tasksIn)
                {
                    if(task.Owner == "")
                    {
                        string noOwnerName = "Unassigned";
                        if (!board.ContainsKey(noOwnerName))
                        {
                            board.Add(noOwnerName, new List<TH_Task>());
                        }
                        board[noOwnerName].Add(task);
                    }
                    else if(!board.ContainsKey(task.Owner))
                    {
                        board.Add(task.Owner, new List<TH_Task>());
                    }
                    board[task.Owner].Add(task);
                }
            }
            foreach(var column in board.Values)
            {
                column.Sort(taskSorter);
            }
            Board = board;

        }

        private int taskSorter(TH_Task x, TH_Task y)
        {
            if(x.Status.CompareTo(y.Status)!=0)
            {
                return x.Status.CompareTo(y.Status);
            }
            else if(x.HasDate && y.HasDate )
            {
                return x.Date.CompareTo(y.Date);
            }
            else if((!x.HasDate) && y.HasDate)
            {
                return 1;
            }
            else if (x.HasDate && (!y.HasDate))
            {
                return -1;
            }
            else { return 0; }
        }



        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("55eef061-791a-416e-a009-7f7787698651"); }
        }
    }
}