using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel;

namespace TaskHopper.Core
{
    static class Servers
    {
        private static Dictionary<Guid, HashSet<string>> TagServer = new Dictionary<Guid, HashSet<string>>();

        public static void AddTag(GH_Document doc, string tag)
        {
            var id = doc.DocumentID;
            if (!TagServer.ContainsKey(id))
            {
                TagServer.Add(id, new HashSet<string>());
            }
            TagServer[id].Add(tag);
        }

        public static void RemoveTag(GH_Document doc, string tag)
        {
            var id = doc.DocumentID;
            if (TagServer.ContainsKey(id))
            {
                TagServer[id].Remove(tag);
            }
        }


    }
}
