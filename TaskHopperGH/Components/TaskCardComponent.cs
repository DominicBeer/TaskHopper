using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using TaskHopper.Core;

namespace TaskHopper.Components
{
    public class TaskCardComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the TaskCardComponent class.
        /// </summary>
        public TaskCardComponent()
          : base("TaskCardComponent", "Nickname",
              "Description",
              "TaskHopper", "Tasks")
        {
            
            var tags = new List<string>() { "Test", "Will It Work??","Probably Not" };
            SolvedTask = new TH_Task(
                "A really really really long name, far too long, silly in fact",
                "Not much fun",
                "Dom Beer",
                @"C:\Users\Dominic\source\repos\Taskhopper\TaskHopperGH\Images",
                Color.LightGreen,
                new DateTime(2021, 5, 12),
                TaskStatus.InProgress,
                tags);
            Attributes = new TaskCardAttributes(this); 
        }

        public TH_Task SolvedTask { get; }
        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
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
            get { return new Guid("a76203c9-68a8-4f2a-a559-53c728fa5d15"); }
        }
    }
}