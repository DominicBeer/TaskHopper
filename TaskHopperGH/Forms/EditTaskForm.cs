using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskHopper.Core;

namespace TaskHopper.Forms
{
    public partial class EditTaskForm : Form
    {
        private TH_Task Source;
        private List<string> OwnerSource;
        private List<string> TagSource;
        public EditTaskForm(TH_Task source, IEnumerable<string> ownerSource, IEnumerable<string> tagSource)
        {
            OwnerSource = ownerSource.ToList();
            TagSource = tagSource.ToList();
            Source = source;
            InitializeComponent();
            NameTextBox.Text = source.Name;
            DescriptionTextBox.Text = source.Description;
            DatePicker.Value = source.Date;
            StatusPicker.Items.AddRange(TaskStatusWriter.All);
            StatusPicker.SelectedItem = new TaskStatusWriter(source.Status);
            OwnerComboBox.Items.AddRange(ownerSource.ToArray());
            OwnerComboBox.SelectedItem = source.Name;
            TagComboBox.Items.AddRange(tagSource.ToArray());
            foreach(var tag in source.Tags)
            {
                TagLayoutPanel.Controls.Add(new RemovableTagStrip(tag, TagLayoutPanel));
            }

            

        }

         
        private void EditTaskForm_Load(object sender, EventArgs e)
        {

        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void OwnerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DescriptionTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void StatusPicker_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TagPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TagLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AddTagButton_Click(object sender, EventArgs e)
        {
            var tagName = TagComboBox.Text;
            if (tagName != "")
            {
                var alreadyThere = false;
                foreach(var c in TagLayoutPanel.Controls)
                {
                    if(c is RemovableTagStrip r)
                    {
                        if(r.TagText == tagName)
                        {
                            alreadyThere = true;
                            break;
                        }
                    }
                }
                if (!alreadyThere)
                {
                    TagLayoutPanel.Controls.Add(new RemovableTagStrip(TagComboBox.Text, TagLayoutPanel));
                }
            }
        }
    }
}
