using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace TaskHopper
{
    public class TaskHopperGHInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "TaskHopperGH";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return null;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("74f69bc4-2ab8-4080-adf9-6be9fc9f2c1d");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "";
            }
        }
    }
}
