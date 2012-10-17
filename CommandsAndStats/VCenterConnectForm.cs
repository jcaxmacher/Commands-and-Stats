using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMware.Vim;

namespace CommandsAndStats
{
    public partial class VCenterConnectForm : Form
    {
        public VimClient VCenterClient { get; set; }
        public string VCenterServer { get; set; }
        public string VCenterUserName { get; set; }
        public string VCenterUserPassword { get; set; }
        public string DCMoRef { get; set; }
        public string PowerState { get; set; }
        public bool Connected { get; set; }

        public VCenterConnectForm()
        {
            InitializeComponent();
            Connected = false;
        }
        public VCenterConnectForm(string vcenterServer)
        {
            InitializeComponent();
            vcenterServerName.Text = vcenterServer;
            Connected = false;
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            connectButton.Enabled = false;
            connectButton.Text = "Connecting. Please wait...";
            connectButton.Refresh();
            VCenterUserName = userIDTextbox.Text;
            VCenterUserPassword = passwordTextbox.Text;
            VCenterServer = vcenterServerName.Text;

            try
            {
                VCenterClient = new VimClient();
                VCenterClient.Connect(String.Format("https://{0}/sdk", vcenterServerName.Text));
                VCenterClient.Login(VCenterUserName, VCenterUserPassword);

                IList<EntityViewBase> dataCenterList = VCenterClient.FindEntityViews(typeof(Datacenter), null, null, null);
                foreach (Datacenter dc in dataCenterList)
                {
                    datacenterCombobox.Items.Add(new ComboboxItem(dc.Name, dc.MoRef.Value));
                }
                connectButton.Text = "Connected. Click OK or Select a datacenter and click Import.";
                button2.Enabled = true;
                datacenterCombobox.Enabled = true;
                datacenterCombobox.SelectedIndex = 0;
                powerStateCombobox.Enabled = true;
                powerStateCombobox.SelectedIndex = 0;
                DCMoRef = ((ComboboxItem)datacenterCombobox.SelectedItem).Value;
                PowerState = powerStateCombobox.SelectedText;
                button1.Enabled = true;
                Connected = true;
            }
            catch (Exception ex)
            {
                connectButton.Text = "Error occurred. Adjust settings and Connect.";
                connectButton.Enabled = true;
            }
            finally
            { }
        }

        private void vcenterServerName_TextChanged(object sender, EventArgs e)
        {
            if (connectButton.Text.StartsWith("Connected") && !connectButton.Enabled)
            {
                connectButton.Text = "Connect";
                connectButton.Enabled = true;
            }
        }

        private void userIDTextbox_TextChanged(object sender, EventArgs e)
        {
            if (connectButton.Text.StartsWith("Connected") && !connectButton.Enabled)
            {
                connectButton.Text = "Connect";
                connectButton.Enabled = true;
            }
        }

        private void passwordTextbox_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            if (connectButton.Text.StartsWith("Connected") && !connectButton.Enabled)
            {
                connectButton.Text = "Connect";
                connectButton.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DCMoRef = ((ComboboxItem)datacenterCombobox.SelectedItem).Value;
            PowerState = powerStateCombobox.SelectedText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DCMoRef = ((ComboboxItem)datacenterCombobox.SelectedItem).Value;
            PowerState = powerStateCombobox.SelectedText;
        }




    }
    public class ComboboxItem
    {

        public string Text { get; set; }
        public string Value { get; set; }

        public ComboboxItem(string name, string value)
        {
            Text = name;
            Value = value;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
