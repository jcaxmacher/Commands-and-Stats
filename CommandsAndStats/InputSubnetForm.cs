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
    public partial class InputSubnetForm : Form
    {
        public string SubnetList { get; set; }
        public InputSubnetForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SubnetList = subnetTextBox.Text;
        }
    }
}
