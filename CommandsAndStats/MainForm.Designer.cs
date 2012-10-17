namespace CommandsAndStats
{
    partial class MainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importListFromVCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importListFromSubnetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetDataGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionListComboBox = new System.Windows.Forms.ComboBox();
            this.serverGridView = new System.Windows.Forms.DataGridView();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.statusBarLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.actionLogGroupBox = new System.Windows.Forms.GroupBox();
            this.actionLogTextBox = new System.Windows.Forms.RichTextBox();
            this.actionControlsGroupBox = new System.Windows.Forms.GroupBox();
            this.editActionButton = new System.Windows.Forms.Button();
            this.startActionButton = new System.Windows.Forms.Button();
            this.stopActionButton = new System.Windows.Forms.Button();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serverGridView)).BeginInit();
            this.actionLogGroupBox.SuspendLayout();
            this.actionControlsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(373, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.importListFromVCenterToolStripMenuItem,
            this.importListFromSubnetToolStripMenuItem,
            this.exportDataToolStripMenuItem,
            this.resetDataGridToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(332, 22);
            this.openFileToolStripMenuItem.Text = "Import List From File";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // importListFromVCenterToolStripMenuItem
            // 
            this.importListFromVCenterToolStripMenuItem.Name = "importListFromVCenterToolStripMenuItem";
            this.importListFromVCenterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.importListFromVCenterToolStripMenuItem.Size = new System.Drawing.Size(332, 22);
            this.importListFromVCenterToolStripMenuItem.Text = "Import List From VCenter or Connect only";
            this.importListFromVCenterToolStripMenuItem.Click += new System.EventHandler(this.importListFromVCenterToolStripMenuItem_Click);
            // 
            // importListFromSubnetToolStripMenuItem
            // 
            this.importListFromSubnetToolStripMenuItem.Name = "importListFromSubnetToolStripMenuItem";
            this.importListFromSubnetToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.importListFromSubnetToolStripMenuItem.Size = new System.Drawing.Size(332, 22);
            this.importListFromSubnetToolStripMenuItem.Text = "Import List From Subnet";
            this.importListFromSubnetToolStripMenuItem.Click += new System.EventHandler(this.importListFromSubnetToolStripMenuItem_Click);
            // 
            // exportDataToolStripMenuItem
            // 
            this.exportDataToolStripMenuItem.Name = "exportDataToolStripMenuItem";
            this.exportDataToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.exportDataToolStripMenuItem.Size = new System.Drawing.Size(332, 22);
            this.exportDataToolStripMenuItem.Text = "Export Data";
            this.exportDataToolStripMenuItem.Click += new System.EventHandler(this.exportDataToolStripMenuItem_Click);
            // 
            // resetDataGridToolStripMenuItem
            // 
            this.resetDataGridToolStripMenuItem.Name = "resetDataGridToolStripMenuItem";
            this.resetDataGridToolStripMenuItem.Size = new System.Drawing.Size(332, 22);
            this.resetDataGridToolStripMenuItem.Text = "Reset Data Grid";
            this.resetDataGridToolStripMenuItem.Click += new System.EventHandler(this.resetDataGridToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(329, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(332, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // actionListComboBox
            // 
            this.actionListComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.actionListComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.actionListComboBox.FormattingEnabled = true;
            this.actionListComboBox.Location = new System.Drawing.Point(6, 20);
            this.actionListComboBox.Name = "actionListComboBox";
            this.actionListComboBox.Size = new System.Drawing.Size(269, 21);
            this.actionListComboBox.TabIndex = 1;
            this.actionListComboBox.SelectedValueChanged += new System.EventHandler(this.actionListComboBox_SelectedValueChanged);
            // 
            // serverGridView
            // 
            this.serverGridView.AllowUserToAddRows = false;
            this.serverGridView.AllowUserToDeleteRows = false;
            this.serverGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.serverGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.serverGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.serverGridView.Location = new System.Drawing.Point(15, 173);
            this.serverGridView.Name = "serverGridView";
            this.serverGridView.Size = new System.Drawing.Size(343, 109);
            this.serverGridView.TabIndex = 5;
            this.serverGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.serverGridView_ColumnHeaderMouseClick);
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 296);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(373, 22);
            this.statusBar.TabIndex = 6;
            this.statusBar.Text = "statusStrip1";
            // 
            // statusBarLabel
            // 
            this.statusBarLabel.Name = "statusBarLabel";
            this.statusBarLabel.Size = new System.Drawing.Size(26, 17);
            this.statusBarLabel.Text = "Idle";
            // 
            // actionLogGroupBox
            // 
            this.actionLogGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.actionLogGroupBox.Controls.Add(this.actionLogTextBox);
            this.actionLogGroupBox.Location = new System.Drawing.Point(12, 84);
            this.actionLogGroupBox.Name = "actionLogGroupBox";
            this.actionLogGroupBox.Size = new System.Drawing.Size(349, 83);
            this.actionLogGroupBox.TabIndex = 7;
            this.actionLogGroupBox.TabStop = false;
            this.actionLogGroupBox.Text = "Action Log";
            // 
            // actionLogTextBox
            // 
            this.actionLogTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.actionLogTextBox.Location = new System.Drawing.Point(6, 19);
            this.actionLogTextBox.Name = "actionLogTextBox";
            this.actionLogTextBox.ReadOnly = true;
            this.actionLogTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.actionLogTextBox.Size = new System.Drawing.Size(337, 58);
            this.actionLogTextBox.TabIndex = 0;
            this.actionLogTextBox.Text = "";
            this.actionLogTextBox.TextChanged += new System.EventHandler(this.actionLogTextBox_TextChanged);
            // 
            // actionControlsGroupBox
            // 
            this.actionControlsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.actionControlsGroupBox.Controls.Add(this.editActionButton);
            this.actionControlsGroupBox.Controls.Add(this.actionListComboBox);
            this.actionControlsGroupBox.Controls.Add(this.startActionButton);
            this.actionControlsGroupBox.Controls.Add(this.stopActionButton);
            this.actionControlsGroupBox.Location = new System.Drawing.Point(12, 27);
            this.actionControlsGroupBox.Name = "actionControlsGroupBox";
            this.actionControlsGroupBox.Size = new System.Drawing.Size(349, 51);
            this.actionControlsGroupBox.TabIndex = 8;
            this.actionControlsGroupBox.TabStop = false;
            this.actionControlsGroupBox.Text = "Action Controls";
            // 
            // editActionButton
            // 
            this.editActionButton.Image = ((System.Drawing.Image)(resources.GetObject("editActionButton.Image")));
            this.editActionButton.Location = new System.Drawing.Point(6, 19);
            this.editActionButton.Name = "editActionButton";
            this.editActionButton.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.editActionButton.Size = new System.Drawing.Size(23, 22);
            this.editActionButton.TabIndex = 5;
            this.editActionButton.UseVisualStyleBackColor = true;
            this.editActionButton.Visible = false;
            // 
            // startActionButton
            // 
            this.startActionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.startActionButton.Enabled = false;
            this.startActionButton.Image = global::CommandsAndStats.Properties.Resources.glyphicons_174_play;
            this.startActionButton.Location = new System.Drawing.Point(281, 19);
            this.startActionButton.Name = "startActionButton";
            this.startActionButton.Size = new System.Drawing.Size(28, 22);
            this.startActionButton.TabIndex = 3;
            this.startActionButton.UseVisualStyleBackColor = true;
            this.startActionButton.Click += new System.EventHandler(this.startActionButton_Click);
            // 
            // stopActionButton
            // 
            this.stopActionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.stopActionButton.Enabled = false;
            this.stopActionButton.Image = global::CommandsAndStats.Properties.Resources.glyphicons_175_stop;
            this.stopActionButton.Location = new System.Drawing.Point(315, 19);
            this.stopActionButton.Name = "stopActionButton";
            this.stopActionButton.Size = new System.Drawing.Size(28, 22);
            this.stopActionButton.TabIndex = 4;
            this.stopActionButton.UseVisualStyleBackColor = true;
            this.stopActionButton.Click += new System.EventHandler(this.stopActionButton_Click);
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem1.Text = "File";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 318);
            this.Controls.Add(this.actionControlsGroupBox);
            this.Controls.Add(this.actionLogGroupBox);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.serverGridView);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(297, 354);
            this.Name = "MainForm";
            this.Text = "Commands and Stats";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serverGridView)).EndInit();
            this.actionLogGroupBox.ResumeLayout(false);
            this.actionControlsGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importListFromVCenterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ComboBox actionListComboBox;
        private System.Windows.Forms.Button startActionButton;
        private System.Windows.Forms.Button stopActionButton;
        private System.Windows.Forms.DataGridView serverGridView;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.GroupBox actionLogGroupBox;
        private System.Windows.Forms.RichTextBox actionLogTextBox;
        private System.Windows.Forms.GroupBox actionControlsGroupBox;
        private System.Windows.Forms.ToolStripStatusLabel statusBarLabel;
        private System.Windows.Forms.Button editActionButton;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem resetDataGridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importListFromSubnetToolStripMenuItem;
    }
}

