using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskHopper.RenderedGraphics;

namespace TaskHopper.Forms
{
    public partial class RemovableTagStrip : UserControl
    {
        TagCardPart TagPart;
        FlowLayoutPanel Host;
        public readonly string TagText;
        public RemovableTagStrip(string tag, FlowLayoutPanel host)
        {
            InitializeComponent();
            TagPart = new TagCardPart(tag,160f);
            TagPart.Pivot = new PointF(2, 2);
            Host = host;
            TagText = tag;
            this.Paint += RenderCard;
        }

        private void RenderCard(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            TagPart.Render(graphics);
        }

        private void TagCard_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Host.Controls.Remove(this);
        }
    }
}
