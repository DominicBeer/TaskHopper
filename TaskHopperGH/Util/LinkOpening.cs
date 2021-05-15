using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskHopper.Util
{
    static class LinkOpening
    {
        public static void OpenLink(string link)
        {
            try
            {
                Process.Start(link);
            }
            catch
            {
                MessageBox.Show("The provided link could not be opened", "Link Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public static Action GetLinkOpener(string link) => CurryAction(OpenLink, link);

        static Action CurryAction<T1>(Action<T1> action, T1 arg1)
        {
            return () => action(arg1);
        }
                
    }
}
