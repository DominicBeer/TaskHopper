using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using TaskHopper.Core;
using System.Linq;
using TaskHopper.Parameters;
using TaskHopper.Util;
using GH_IO.Serialization;
using System.Windows.Forms;
using Grasshopper.GUI;
using Grasshopper.GUI.Base;
using TaskHopper.CanvasControls;
using Grasshopper.Kernel.Types;

namespace TaskHopper.Components
{
    public class TaskCardComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the TaskCardComponent class.
        /// </summary>
        /// 

        public TaskCardComponent()
          : base("TaskCardComponent", "Nickname",
              "Description",
              "TaskHopper", "Tasks")
        {
            
            var tags = new List<string>() { "Test", "Will It Work??","Probably Not" };
            InternalTask = new TH_Task(
                "A really really really long name, far too long, silly in fact",
                "Some really boring task, sorry but you're not going to enjoy, better start grinding",
                "Dom Beer",
                @"C:\Users\Dominic\source\repos\Taskhopper\TaskHopperGH\Images",
                Color.LawnGreen,
                new DateTime(2021, 5, 25),
                TaskStatus.InProgress,
                tags,
                this);
            SolvedTask = InternalTask;
            Attributes = CanvasControlAttributes.TaskCardAttributes(this,InternalTask); 
        }

        public TH_Task SolvedTask { get; private set; }
        internal TH_Task InternalTask { get; private set; }
        public  void SetTask(TH_Task task)
        {
            InternalTask = task;
            UpdateAttributeTask();
            ExpireSolution(true);

            OnDisplayExpired(true);
        }

        private void UpdateAttributeTask()
        {
            ((CanvasControlAttributes)Attributes).UpdateControl(new TaskCardControl(this.Attributes, InternalTask));
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("", "", "Card", GH_ParamAccess.tree);
            var param = this.Params.Input[0];
            param.Optional = true;

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)

        {
            pManager.AddGenericParameter("", "", "Card", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Grasshopper.Kernel.Data.GH_Structure<IGH_Goo> tree;
            DA.GetDataTree(0, out tree);
            var statsIn = tree.Where(goo => goo is TH_Task_Goo).Select(goo => (int)((TH_Task_Goo)goo).Value.Status);
            if (statsIn.Count() != 0)
            {
                var statusIn = (TaskStatus)statsIn.Min();
                InternalTask = InternalTask.SetStatusIn(statusIn);
                UpdateAttributeTask();
            }
            else
            {
                InternalTask = InternalTask.SetStatusIn(InternalTask.Status);
                UpdateAttributeTask();
            }
            
            DA.SetData(0, new TH_Task_Goo(InternalTask));
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

        internal void LaunchEditForm()
        {
            var tuple = ScrapeOwnersAndTags();  
            var editForm = new Forms.EditTaskForm(InternalTask, tuple.names, tuple.tags, this);
            editForm.ShowDialog();
        }

        private (List<string> tags, List<string> names) ScrapeOwnersAndTags()
        {
            var doc = this.OnPingDocument();
            var cards = doc.Objects
                .Where(obj => obj is TaskCardComponent)
                .Select(obj => (TaskCardComponent)obj);
            var tags = new HashSet<string>(cards
                .Select(card => card.InternalTask.Tags)
                .SelectMany(x => x));
            var names = new HashSet<string>(cards
                .Select(card => card.InternalTask.Owner));
            return (tags.ToList(), names.ToList());
        }
        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("a76203c9-68a8-4f2a-a559-53c728fa5d15"); }
        }


        private void SetColor(Color color)
        {
            var newTask = InternalTask.ChangeColor(color);
            SetTask(newTask);
        }

        public override bool Read(GH_IReader reader)
        {
            InternalTask = new TH_Task();
            InternalTask.Read(reader);
            InternalTask.Source = this;
            SolvedTask = InternalTask;
            base.Read(reader);
            UpdateAttributeTask();
            return true;
        }

        public override bool Write(GH_IWriter writer)
        {
            InternalTask.Write(writer);
            return base.Write(writer);
        }
    }
}