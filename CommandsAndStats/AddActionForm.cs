using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommandsAndStats
{
    public partial class AddActionForm : Form
    {
        public AddActionForm(string actionName, bool builtin)
        {
            InitializeComponent();
            actionNameTextbox.Text = actionName;
            if (builtin)
            {
                actionNameTextbox.Enabled = false;
                actionTypeDropdown.Enabled = false;
                actionDetailsTextbox.Enabled = false;
            }
        }

        private void cancelAddButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public string ActionName
        {
            get { return actionNameTextbox.Text; }
        }
        public string ActionType
        {
            get { return actionTypeDropdown.SelectedValue.ToString(); }
        }
        public string ActionDetails
        {
            get { return actionDetailsTextbox.Text; }
        }
        public bool ActionDateStamp
        {
            get { return actionDateStampCheckbox.Checked; }
        }
    }
}
