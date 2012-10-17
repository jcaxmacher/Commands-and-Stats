using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Dynamic;
using System.Runtime.InteropServices;
using VMware;
using System.Collections.Specialized;
using LukeSkywalker.IPNetwork;

namespace CommandsAndStats
{
    public partial class MainForm : Form
    {
        private ActionRunner actionRunner;
        private string addActionKey = "Add Action...";
        private object gridLock = new object();
        private int gridModifier = 0;
        private string defaultVcenterServer = "";

        public VMware.Vim.VimClient VCenterClient { get; set; }
        public string VCenterServer { get; set; }
        public string VCenterUserName { get; set; }
        public string VCenterUserPassword { get; set; }
        public VMware.Vim.Datacenter ChosenDatacenter { get; set; }

        public MainForm()
        {
            InitializeComponent();

            resetServerGrid();
            actionRunner = new ActionRunner(this);
            actionRunner.Add(new Actionable("RDP Scan", null, new Func<string, string>(s => NetworkCommands.tcpScan(s, 3389)),
                null, ActionRunner.dateStampResult(), null));
            actionRunner.Add(new Actionable("SSH Scan", null, new Func<string, string>(s => NetworkCommands.tcpScan(s, 22)),
                null, ActionRunner.dateStampResult(), null));
            actionRunner.Add(new Actionable("Ping Scan", null, new Func<string, string>(s => NetworkCommands.pingScan(s)),
                null, ActionRunner.dateStampResult(), null));
            actionRunner.Add(new Actionable("Reboot Server", null, new Func<string, string>(s => NetworkCommands.sendWMIShutdownReboot(s, "reboot")),
                null, ActionRunner.dateStampResult(), null));
            actionRunner.Add(new Actionable("Shutdown Server", null, new Func<string, string>(s => NetworkCommands.sendWMIShutdownReboot(s, "shutdown")),
                null, ActionRunner.dateStampResult(), null));
            actionRunner.Add(new Actionable("Last BootTime", null, new Func<string, string>(s => NetworkCommands.sendWMIShutdownReboot(s, "lastboottime")),
                null, ActionRunner.dateStampResult(), null));
            actionRunner.Add(new Actionable("Power Off VM", null, new Func<string, string>(s => NetworkCommands.powerOffVM(VCenterClient, s)),
                null, ActionRunner.dateStampResult(), null));
            actionRunner.Add(new Actionable("Power On VM", null, new Func<string, string>(s => NetworkCommands.powerOnVM(VCenterClient, s)),
                null, ActionRunner.dateStampResult(), null));
            actionRunner.Add(new Actionable("VM Power State", null, new Func<string, string>(s => NetworkCommands.getPowerStatus(VCenterClient, s)),
                null, ActionRunner.dateStampResult(), null));
            actionRunner.Add(new Actionable("Forward DNS Lookup", null, new Func<string, string>(s => NetworkCommands.forwardDnsLookup(s)),
                null, null, null));
            actionRunner.Add(new Actionable("Reverse DNS Lookup", null, new Func<string, string>(s => NetworkCommands.reverseDnsLookup(s)),
                null, null, null));
            var acts = actionRunner.ActionKeys;
            //acts.Add(addActionKey);
            actionListComboBox.DataSource = acts;
            if (!actionRunner.IsRunning(actionListComboBox.SelectedValue.ToString()))
                startActionButton.Enabled = true;
        }

        #region click_methods
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        private void exportDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel spreadsheet|*.xlsx";
            saveFileDialog.FileName = DateTime.Now.Ticks.ToString() + ".xlsx";
            saveFileDialog.FilterIndex = 1;

            DialogResult saveFileResult = saveFileDialog.ShowDialog();
            string filename = saveFileDialog.FileName;

            if (saveFileResult == DialogResult.OK)
            {
                var worker = new BackgroundWorker();
                worker.DoWork += new DoWorkEventHandler(new Action<object, DoWorkEventArgs>((o, args) =>
                {
                    addToActionLog("Started: Saving grid to excel file " + filename + " [" + DateTime.Now + "]");
                    // creating Excel Application
                    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();


                    // creating new WorkBook within Excel application
                    Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);


                    // creating new Excelsheet in workbook
                    Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

                    // see the excel sheet behind the program
                    app.Visible = false;

                    // get the reference of first sheet. By default its name is Sheet1.
                    // store its reference to worksheet
                    worksheet = workbook.Sheets["Sheet1"];
                    worksheet = workbook.ActiveSheet;

                    // changing the name of active sheet
                    worksheet.Name = "Exported from gridview";


                    lock (gridLock)
                    {
                        gridModifier++;
                    }
                    // storing header part in Excel
                    for (int i = 1; i < serverGridView.Columns.Count + 1; i++)
                    {
                        try
                        {
                            worksheet.Cells[1, i] = serverGridView.Columns[i - 1].HeaderText;
                        }
                        catch (Exception ge)
                        { }
                    }



                    // storing Each row and column value to excel sheet
                    for (int i = 0; i < serverGridView.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < serverGridView.Columns.Count; j++)
                        {
                            try
                            {
                                worksheet.Cells[i + 2, j + 1] = serverGridView.Rows[i].Cells[j].Value.ToString();
                                //Microsoft.Office.Interop.Excel.Range r = worksheet.get_Range(worksheet.Cells[i + 2, j + 1]);
                                //r.Font.Color = System.Drawing.ColorTranslator.ToOle(serverGridView.Rows[i].Cells[j].Style.BackColor);
                                //(RangeObject)
                            }
                            catch (Exception ge)
                            { }
                        }
                    }

                    lock (gridLock)
                    {
                        gridModifier--;
                    }
                    bool saveFailure = false;
                    // save the data
                    try
                    {
                        workbook.SaveAs(filename, Type.Missing, Type.Missing,
                            Type.Missing, Type.Missing, Type.Missing,
                            Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive,
                            Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    }
                    catch (COMException ce)
                    {
                        saveFailure = true;
                        if ((uint)ce.ErrorCode == 0x800A03EC)
                        {
                            addToActionLog("Cancelled: Saving of excel export was probably cancelled.");
                        }
                        else
                        {
                            addToActionLog("Error: Some error occurred saving excel export.");
                        }
                    }
                    if (!saveFailure)
                    {
                        addToActionLog("Completed: Saving grid to excel file.");
                    }
                    try
                    {
                        workbook.Close();
                    }
                    catch (Exception exc) { }
                }));
                worker.RunWorkerAsync();
            }
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create an instance of the open file dialog box
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set filter options and filter index
            openFileDialog.Filter = "CSV Files|*.csv|Text Files|*.txt";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;

            DialogResult openFileResult = openFileDialog.ShowDialog();
            string filename = openFileDialog.FileName;

            if (openFileResult == DialogResult.OK)
            {
                var worker = new BackgroundWorker();
                worker.DoWork += new DoWorkEventHandler(new Action<object, DoWorkEventArgs>((o, args) =>
                {
                    addToActionLog("Started: Loading server list from file " + filename);
                    var serverNames = new HashSet<string>();
                    string line;
                    string fileName = args.Argument as string;
                    System.IO.StreamReader file =
                        new System.IO.StreamReader(fileName);
                    int lineCount = 0;
                    lock (gridLock)
                    {
                        gridModifier++;
                    }
                    while ((line = file.ReadLine()) != null)
                    {
                        lineCount++;
                        string serverName = line.Trim().ToUpper();
                        if (!serverNames.Contains(serverName))
                        {
                            addNodeToGrid(serverName);
                            serverNames.Add(serverName);
                        }
                    } 
                    lock (gridLock)
                    {
                        gridModifier--;
                    }

                    file.Close();
                    refreshActionListControls();
                    addToActionLog("Complete: Loading server list from file.");
                }));
                worker.RunWorkerAsync(filename);
            }
        }

        private void importListFromSubnetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var subnetForm = new InputSubnetForm();
            var result = subnetForm.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                string subnetList = subnetForm.SubnetList;
                var worker = new BackgroundWorker();
                worker.DoWork += new DoWorkEventHandler(new Action<object, DoWorkEventArgs>((o, args) =>
                {
                    string[] subnets = subnetList.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    foreach (string net in subnets)
                    {
                        IPNetwork netw = IPNetwork.Parse(net);
                        IPAddressCollection ips = IPNetwork.ListIPAddress(netw);
                        foreach (var addr in ips)
                        {
                            addNodeToGrid(addr.ToString());
                        }
                    }
                }));
                worker.RunWorkerAsync();
            }
        }

        private void importListFromVCenterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var vcenterConnectForm = new VCenterConnectForm(defaultVcenterServer);
            var result = vcenterConnectForm.ShowDialog(this);
            if (result == DialogResult.Yes)
            {
                VCenterClient = vcenterConnectForm.VCenterClient;
                VCenterServer = vcenterConnectForm.VCenterServer;
                VCenterUserName = vcenterConnectForm.VCenterUserName;
                VCenterUserPassword = vcenterConnectForm.VCenterUserPassword;
                var moRef = new VMware.Vim.ManagedObjectReference();
                moRef.Type = "Datacenter";
                moRef.Value = vcenterConnectForm.DCMoRef;
                NameValueCollection filter = null;
                if (vcenterConnectForm.PowerState.ToLower() != "any")
                {
                    filter = new NameValueCollection();
                    filter.Add("Runtime.PowerState", vcenterConnectForm.PowerState);
                }
                var worker = new BackgroundWorker();
                worker.DoWork += new DoWorkEventHandler(new Action<object, DoWorkEventArgs>((o, args) =>
                {
                    addToActionLog("Started: Loading server list from VCenter");
                    IList<VMware.Vim.EntityViewBase> vmList =
                        VCenterClient.FindEntityViews(typeof(VMware.Vim.VirtualMachine), moRef, filter, null);
                    lock (gridLock)
                    {
                        gridModifier++;
                    }
                    foreach (VMware.Vim.VirtualMachine vm in vmList)
                    {
                        addVMNodeToGrid(vm.Name, ActionRunner.dateStampResult().Invoke(vm.Runtime.PowerState.ToString()));
                    }
                    lock (gridLock)
                    {
                        gridModifier--;
                    }
                    addToActionLog("Complete: Loading server list from VCenter");
                }));
                worker.RunWorkerAsync();
            }
            else if (result == DialogResult.OK && vcenterConnectForm.Connected)
            {
                VCenterClient = vcenterConnectForm.VCenterClient;
                VCenterServer = vcenterConnectForm.VCenterServer;
                VCenterUserName = vcenterConnectForm.VCenterUserName;
                VCenterUserPassword = vcenterConnectForm.VCenterUserPassword;
                addToActionLog("Completed: Connected to VCenter.  VM actions should now work.");
            }
            vcenterConnectForm.Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void startActionButton_Click(object sender, EventArgs e)
        {
            startActionButton.Enabled = false;
            startActionButton.Refresh();
            if (!serverGridView.Columns.Contains(actionListComboBox.SelectedValue.ToString()))
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.Name = actionListComboBox.SelectedValue.ToString();
                col.HeaderText = actionListComboBox.SelectedValue.ToString();
                col.DataPropertyName = actionListComboBox.SelectedValue.ToString();
                col.SortMode = DataGridViewColumnSortMode.Programmatic;
                serverGridView.Columns.Add(col);
            }
            var serverMap = getEnabledServers();
            actionRunner.Run(actionListComboBox.SelectedValue.ToString(), serverMap);
            refreshActionListControls();
        }

        private void stopActionButton_Click(object sender, EventArgs e)
        {
            stopActionButton.Enabled = false;
            stopActionButton.Refresh();
            addToActionLog("Cancellation In Progress: " + actionListComboBox.SelectedValue.ToString());
            actionRunner.Stop(actionListComboBox.SelectedValue.ToString());
        }
        #endregion

        #region event_methods
        private void actionLogTextBox_TextChanged(object sender, EventArgs e)
        {
            actionLogTextBox.ScrollToCaret();
        }
        private void actionListComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            var actionCombo = sender as ComboBox;
            if (actionCombo.SelectedValue.ToString() == addActionKey)
            {
                var addActionForm = new AddActionForm("Fill in action name...", false);
                addActionForm.ShowDialog(this);
                addActionForm.Close();
            }
            else
            {
                refreshActionListControls();
            }
        }

        private bool serversChecked = true;
        private void serverGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                foreach (DataGridViewRow row in serverGridView.Rows)
                {
                    row.Cells[0].Value = !serversChecked;
                }
                serversChecked = !serversChecked;
            }
            else if (!actionRunner.AnyRunning() && gridModifier == 0)
            {
                var removeGlyphs = new Action(() =>
                {
                    for (int i = 0; i < serverGridView.Columns.Count; i++)
                    {
                        if (i != e.ColumnIndex)
                        {
                            serverGridView.Columns[i].HeaderCell.SortGlyphDirection = SortOrder.None;
                        }
                    }
                });
                condInvoke(removeGlyphs);
                
                if (serverGridView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Ascending)
                {
                    var desc = new Action(() =>
                    {
                        serverGridView.Sort(serverGridView.Columns[e.ColumnIndex], ListSortDirection.Descending);
                        serverGridView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;

                    });
                    condInvoke(desc);
                }
                else
                {
                    var asc = new Action(() =>
                    {
                        serverGridView.Sort(serverGridView.Columns[e.ColumnIndex], ListSortDirection.Ascending);
                        serverGridView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    });
                    condInvoke(asc);
                }
            }
            else
            {
                MessageBox.Show("You cannot sort or change enabled rows while an action is in progress.", "Action In Progress", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void serverGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (actionRunner.AnyRunning() || gridModifier != 0)
            {
                MessageBox.Show("You cannot change the data while an action is in progress.", "Action In Progress", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }
        #endregion

        #region helper_methods
        private void condInvoke(Action a)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(a);
            }
            else
            {
                a.Invoke();
            }
        }

        public void addToActionLog(string message, bool addDateStamp=true)
        {
            var a = new Action(() =>
            {
                string logText = (addDateStamp) ? "[" + DateTime.Now + "] " + message: message;
                actionLogTextBox.AppendText(logText + "\n");
            });
            condInvoke(a);
        }

        public List<int> getPendingIndices(List<int> targeted, string columnName)
        {
            var result = new List<int>();
            foreach (int i in targeted)
            {
                if (serverGridView.Rows[i].Cells[columnName].Style.BackColor == ActionRunner.getColorForActionStatus(ActionStatus.Pending) ||
                    serverGridView.Rows[i].Cells[columnName].Style.BackColor == ActionRunner.getColorForActionStatus(ActionStatus.Queued))
                {
                    result.Add(i);
                }
            }
            return result;
        }

        public void refreshActionListControls()
        {
            var a = new Action(() =>
            {
                if (!actionRunner.IsRunning(actionListComboBox.SelectedValue.ToString()))
                {
                    startActionButton.Enabled = true;
                    stopActionButton.Enabled = false;
                }
                else
                {
                    stopActionButton.Enabled = true;
                    startActionButton.Enabled = false;
                }
            });
            condInvoke(a);
        }

        private void addNodeToGrid(string nodeName)
        {
            var a = new Action(() =>
            {
                var row = new DataGridViewRow();

                var c = new DataGridViewCheckBoxCell();
                c.Value = true;
                row.Cells.Add(c);

                var s = new DataGridViewTextBoxCell();
                s.Value = nodeName;
                row.Cells.Add(s);

                serverGridView.Rows.Add(row);
            });
            condInvoke(a);
        }

        private void addVMNodeToGrid(string nodeName, string powerState)
        {
            var addPowerStateCol = new Action(() =>
            {
                if (!serverGridView.Columns.Contains("VM Power State"))
                {
                    DataGridViewTextBoxColumn powerStateCol = new DataGridViewTextBoxColumn();
                    powerStateCol.DataPropertyName = "VM Power State";
                    powerStateCol.HeaderText = "VM Power State";
                    powerStateCol.Name = "VM Power State";
                    powerStateCol.SortMode = DataGridViewColumnSortMode.Programmatic;
                    serverGridView.Columns.Add(powerStateCol);
                }
                var row = new DataGridViewRow();
                var c = new DataGridViewCheckBoxCell();
                c.Value = true;
                row.Cells.Add(c);

                var s = new DataGridViewTextBoxCell();
                s.Value = nodeName;
                row.Cells.Add(s);
                int index = serverGridView.Rows.Add(row);
                serverGridView.Rows[index].Cells["VM Power State"].Value = powerState;
            });
            condInvoke(addPowerStateCol);
        }

        private Dictionary<string, int> getEnabledServers()
        {
            var serverMap = new Dictionary<string, int>();
            for (int i = 0; i < serverGridView.Rows.Count; i++)
            {
                if ((bool)serverGridView.Rows[i].Cells["Enabled"].Value)
                {
                    serverMap[(string)serverGridView.Rows[i].Cells["Node"].Value] = i;
                }
            }
            return serverMap;
        }

        public void setServerData(int index, string column, string text, Color backgroundColor)
        {
            var a = new Action(() =>
            {
                serverGridView.Rows[index].Cells[column].Value = text;
                serverGridView.Rows[index].Cells[column].Style.BackColor = backgroundColor;
            });
            condInvoke(a);
        }

        public void resetServerGrid()
        {
            var a = new Action(() =>
            {
                serverGridView.Rows.Clear();
                serverGridView.Columns.Clear();

                DataGridViewCheckBoxColumn selectedColumn = new DataGridViewCheckBoxColumn();
                selectedColumn.DataPropertyName = "Enabled";
                selectedColumn.HeaderText = "Send Commands?";
                selectedColumn.Name = "Enabled";
                selectedColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                selectedColumn.FlatStyle = FlatStyle.Standard;
                selectedColumn.ThreeState = false;
                selectedColumn.CellTemplate = new DataGridViewCheckBoxCell();
                selectedColumn.CellTemplate.Style.BackColor = Color.Beige;
                selectedColumn.SortMode = DataGridViewColumnSortMode.NotSortable;

                DataGridViewTextBoxColumn serverNameColumn = new DataGridViewTextBoxColumn();
                serverNameColumn.DataPropertyName = "Node";
                serverNameColumn.HeaderText = "Node Name / I.P. Address";
                serverNameColumn.Name = "Node";
                serverNameColumn.SortMode = DataGridViewColumnSortMode.Programmatic;

                serverGridView.Columns.Add(selectedColumn);
                serverGridView.Columns.Add(serverNameColumn);
            });
            condInvoke(a);
        }
        #endregion

        private void resetDataGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resetServerGrid();
        }




    }
}
