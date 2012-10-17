namespace CommandsAndStats
{
    partial class AddActionForm
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
            this.actionTypeDropdown = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.actionDetailsTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.okAddButton = new System.Windows.Forms.Button();
            this.cancelAddButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.actionNameTextbox = new System.Windows.Forms.TextBox();
            this.actionDateStampCheckbox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // actionTypeDropdown
            // 
            this.actionTypeDropdown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.actionTypeDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.actionTypeDropdown.FormattingEnabled = true;
            this.actionTypeDropdown.Items.AddRange(new object[] {
            "External Command",
            "Python Snippet",
            "TCP Port Scan",
            "WMI Get Field",
            "WMI Send Command"});
            this.actionTypeDropdown.Location = new System.Drawing.Point(9, 19);
            this.actionTypeDropdown.Name = "actionTypeDropdown";
            this.actionTypeDropdown.Size = new System.Drawing.Size(461, 21);
            this.actionTypeDropdown.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.actionTypeDropdown);
            this.groupBox1.Location = new System.Drawing.Point(13, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(477, 45);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Action Type";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.actionDetailsTextbox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(13, 114);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(477, 167);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Action Details";
            // 
            // actionDetailsTextbox
            // 
            this.actionDetailsTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.actionDetailsTextbox.Location = new System.Drawing.Point(9, 47);
            this.actionDetailsTextbox.Multiline = true;
            this.actionDetailsTextbox.Name = "actionDetailsTextbox";
            this.actionDetailsTextbox.Size = new System.Drawing.Size(461, 114);
            this.actionDetailsTextbox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(467, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "If using an external command, place {0} where you want the node to be applied in " +
                "your command.";
            // 
            // okAddButton
            // 
            this.okAddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okAddButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okAddButton.Location = new System.Drawing.Point(328, 289);
            this.okAddButton.Name = "okAddButton";
            this.okAddButton.Size = new System.Drawing.Size(75, 23);
            this.okAddButton.TabIndex = 3;
            this.okAddButton.Text = "OK";
            this.okAddButton.UseVisualStyleBackColor = true;
            // 
            // cancelAddButton
            // 
            this.cancelAddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelAddButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelAddButton.Location = new System.Drawing.Point(409, 289);
            this.cancelAddButton.Name = "cancelAddButton";
            this.cancelAddButton.Size = new System.Drawing.Size(75, 23);
            this.cancelAddButton.TabIndex = 4;
            this.cancelAddButton.Text = "Cancel";
            this.cancelAddButton.UseVisualStyleBackColor = true;
            this.cancelAddButton.Click += new System.EventHandler(this.cancelAddButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.actionNameTextbox);
            this.groupBox3.Location = new System.Drawing.Point(12, 13);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(477, 44);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Action Name";
            // 
            // actionNameTextbox
            // 
            this.actionNameTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.actionNameTextbox.Location = new System.Drawing.Point(10, 18);
            this.actionNameTextbox.Name = "actionNameTextbox";
            this.actionNameTextbox.Size = new System.Drawing.Size(461, 20);
            this.actionNameTextbox.TabIndex = 0;
            // 
            // actionDateStampCheckbox
            // 
            this.actionDateStampCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.actionDateStampCheckbox.AutoSize = true;
            this.actionDateStampCheckbox.Location = new System.Drawing.Point(22, 293);
            this.actionDateStampCheckbox.Name = "actionDateStampCheckbox";
            this.actionDateStampCheckbox.Size = new System.Drawing.Size(231, 17);
            this.actionDateStampCheckbox.TabIndex = 6;
            this.actionDateStampCheckbox.Text = "Append Date/Time stamp to action results?";
            this.actionDateStampCheckbox.UseVisualStyleBackColor = true;
            // 
            // AddActionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 323);
            this.Controls.Add(this.actionDateStampCheckbox);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.cancelAddButton);
            this.Controls.Add(this.okAddButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "AddActionForm";
            this.Text = "Add Action";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox actionTypeDropdown;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox actionDetailsTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button okAddButton;
        private System.Windows.Forms.Button cancelAddButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox actionNameTextbox;
        private System.Windows.Forms.CheckBox actionDateStampCheckbox;
    }
}