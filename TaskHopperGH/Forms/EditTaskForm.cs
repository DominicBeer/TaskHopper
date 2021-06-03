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
using TaskHopper.Components;

namespace TaskHopper.Forms
{
    public partial class EditTaskForm : Form
    {
        private TH_Task Source;
        private List<string> OwnerSource;
        private List<string> TagSource;
        private TaskCardComponent Component;
        public EditTaskForm(TH_Task source, IEnumerable<string> ownerSource, IEnumerable<string> tagSource, TaskCardComponent component)
        {
            OwnerSource = ownerSource.ToList();
            TagSource = tagSource.ToList();
            Source = source;
            Component = component;
            InitializeComponent();
            NameTextBox.Text = source.Name;
            DescriptionTextBox.Text = source.Description;
            DatePicker.Value = source.Date;
            LinkTextBox.Text = source.Link;
            StatusPicker.Items.AddRange(TaskStatusWriter.All);
            StatusPicker.SelectedItem = new TaskStatusWriter(source.Status);
            OwnerComboBox.Items.AddRange(ownerSource.ToArray());
            ColourPicker.Colour = source.Color;
            OwnerComboBox.SelectedItem = source.Owner;
            TagComboBox.Items.AddRange(tagSource.ToArray());
            foreach(var tag in source.Tags)
            {
                TagLayoutPanel.Controls.Add(new RemovableTagStrip(tag, TagLayoutPanel));
            }

            this.FormClosing += ReturnUserData;

    
        }

        private void ReturnUserData(object sender, FormClosingEventArgs e)
        {
            var name = NameTextBox.Text;
            var description = DescriptionTextBox.Text;
            var owner = OwnerComboBox.Text;
            var link = LinkTextBox.Text;
            var date = DatePicker.Value;
            var status = ((TaskStatusWriter)StatusPicker.SelectedItem).Status;
            var tags = new HashSet<string>();
            var color = ColourPicker.Colour;
            foreach(var ctrl in TagLayoutPanel.Controls)
            {
                if(ctrl is RemovableTagStrip ts)
                {
                    tags.Add(ts.TagText);
                }
            }
            var returnTask = new TH_Task(name, description, owner, link, color, date, status, tags);
            Component.SetTask(returnTask);
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void gH_ColourPicker1_Load(object sender, EventArgs e)
        {

        }
    }
}
