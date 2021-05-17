
namespace TaskHopper.Forms
{
    partial class EditTaskForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.DescriptionTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.OwnerComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.DatePicker = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.StatusPicker = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TagComboBox = new System.Windows.Forms.ComboBox();
            this.AddTagButton = new System.Windows.Forms.Button();
            this.TagLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(10, 26);
            this.NameTextBox.Multiline = true;
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(256, 47);
            this.NameTextBox.TabIndex = 0;
            this.NameTextBox.TextChanged += new System.EventHandler(this.NameTextBox_TextChanged);
            // 
            // DescriptionTextBox
            // 
            this.DescriptionTextBox.Location = new System.Drawing.Point(10, 96);
            this.DescriptionTextBox.Multiline = true;
            this.DescriptionTextBox.Name = "DescriptionTextBox";
            this.DescriptionTextBox.Size = new System.Drawing.Size(255, 73);
            this.DescriptionTextBox.TabIndex = 1;
            this.DescriptionTextBox.TextChanged += new System.EventHandler(this.DescriptionTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Task Name:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Task Description:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // OwnerComboBox
            // 
            this.OwnerComboBox.FormattingEnabled = true;
            this.OwnerComboBox.Location = new System.Drawing.Point(10, 193);
            this.OwnerComboBox.Name = "OwnerComboBox";
            this.OwnerComboBox.Size = new System.Drawing.Size(256, 21);
            this.OwnerComboBox.TabIndex = 3;
            this.OwnerComboBox.SelectedIndexChanged += new System.EventHandler(this.OwnerComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Owner:";
            // 
            // DatePicker
            // 
            this.DatePicker.Location = new System.Drawing.Point(101, 274);
            this.DatePicker.Name = "DatePicker";
            this.DatePicker.Size = new System.Drawing.Size(166, 20);
            this.DatePicker.TabIndex = 4;
            this.DatePicker.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 277);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Due By:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 225);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Status";
            // 
            // StatusPicker
            // 
            this.StatusPicker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.StatusPicker.FormattingEnabled = true;
            this.StatusPicker.Location = new System.Drawing.Point(10, 242);
            this.StatusPicker.Name = "StatusPicker";
            this.StatusPicker.Size = new System.Drawing.Size(256, 21);
            this.StatusPicker.TabIndex = 3;
            this.StatusPicker.TabStop = false;
            this.StatusPicker.SelectedIndexChanged += new System.EventHandler(this.StatusPicker_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 310);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Tags:";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // TagComboBox
            // 
            this.TagComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.TagComboBox.FormattingEnabled = true;
            this.TagComboBox.Location = new System.Drawing.Point(10, 331);
            this.TagComboBox.Name = "TagComboBox";
            this.TagComboBox.Size = new System.Drawing.Size(221, 21);
            this.TagComboBox.TabIndex = 3;
            this.TagComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // AddTagButton
            // 
            this.AddTagButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddTagButton.Location = new System.Drawing.Point(237, 331);
            this.AddTagButton.Name = "AddTagButton";
            this.AddTagButton.Size = new System.Drawing.Size(29, 21);
            this.AddTagButton.TabIndex = 5;
            this.AddTagButton.Text = "+";
            this.AddTagButton.UseVisualStyleBackColor = true;
            this.AddTagButton.Click += new System.EventHandler(this.AddTagButton_Click);
            // 
            // TagLayoutPanel
            // 
            this.TagLayoutPanel.AutoScroll = true;
            this.TagLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.TagLayoutPanel.Location = new System.Drawing.Point(11, 358);
            this.TagLayoutPanel.Name = "TagLayoutPanel";
            this.TagLayoutPanel.Size = new System.Drawing.Size(253, 148);
            this.TagLayoutPanel.TabIndex = 6;
            this.TagLayoutPanel.WrapContents = false;
            this.TagLayoutPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.TagLayoutPanel_Paint);
            // 
            // EditTaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 531);
            this.Controls.Add(this.TagLayoutPanel);
            this.Controls.Add(this.AddTagButton);
            this.Controls.Add(this.DatePicker);
            this.Controls.Add(this.StatusPicker);
            this.Controls.Add(this.TagComboBox);
            this.Controls.Add(this.OwnerComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DescriptionTextBox);
            this.Controls.Add(this.NameTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EditTaskForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Task";
            this.Load += new System.EventHandler(this.EditTaskForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.TextBox DescriptionTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox OwnerComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker DatePicker;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox StatusPicker;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox TagComboBox;
        private System.Windows.Forms.Button AddTagButton;
        private System.Windows.Forms.FlowLayoutPanel TagLayoutPanel;
    }
}