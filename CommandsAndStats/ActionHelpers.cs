using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.ComponentModel;
using System.Dynamic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Net.NetworkInformation;
using System.Drawing;
using System.Threading.Tasks;
using System.Management;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace CommandsAndStats
{
    public class ActionResult
    {
        public string Message { get; set; }
        public ActionStatus Status { get; set; }
        public ActionResult(string msg, ActionStatus stat)
        {
            this.Message = msg;
            this.Status = stat;
        }
    }

    public class NetworkCommands
    {
        public static string forwardDnsLookup(string serverName)
        {
            IPAddress[] addrs = Dns.GetHostAddresses(serverName);
            string message = "";
            foreach (var i in addrs)
            {
                message += i.ToString() + Environment.NewLine;
            }
            return message;
        }

        public static string reverseDnsLookup(string serverName)
        {
            IPHostEntry host = Dns.GetHostEntry(serverName);
            string message = host.HostName;
            if (host.Aliases.Length > 0)
            {
                message += " Aliases: ";
                foreach (var a in host.Aliases)
                {
                    message += a + ", ";
                }
            }
            return message;
        }

        public static string tcpScan(string serverName, int portNumber)
        {
            TcpClient tcpClient = new TcpClient();
            string message = "Unknown";
            IPAddress ipAddress = Dns.GetHostEntry(serverName).AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, portNumber);
            tcpClient.Connect(ipEndPoint);
            message = "Port open";
            tcpClient.GetStream().Close();
            tcpClient.Close();
            return message;
        }

        public static string pingScan(string serverName)
        {
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();

            // Use default TTL value (128), but change frag behavior
            options.DontFragment = true;

            // Create 32 byte buffer
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            PingReply reply;
            string message = "Unknown";

            int count = 5;
            bool success = false;
            while (count > 0 && !success)
            {
                reply = pingSender.Send(serverName, timeout, buffer, options);
                if (reply.Status == IPStatus.Success)
                {
                    message = "RoundTrip time: " + reply.RoundtripTime;
                    success = true;
                }
                count--;
            }
            if (!success)
            {
                throw new Exception("Request timed out");
            }

            return message;
        }

        public static string sendWMIShutdownReboot(string serverName, string command)
        {
            string message = "Unknown";
            ConnectionOptions options = new ConnectionOptions();
            options.EnablePrivileges = true;
            // To connect to the remote computer using a different account, specify these values:
            // options.Username = "USERNAME";
            // options.Password = "PASSWORD";
            // options.Authority = "ntlmdomain:DOMAIN";

            ManagementScope scope = new ManagementScope(
              "\\\\" + serverName + "\\root\\CIMV2", options);
            scope.Connect();

            SelectQuery query = new SelectQuery("Win32_OperatingSystem");
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher(scope, query);

            foreach (ManagementObject os in searcher.Get())
            {
                if (command == "shutdown1" || command == "reboot1")
                {
                    // Obtain in-parameters for the method
                    ManagementBaseObject inParams =
                        os.GetMethodParameters("Win32Shutdown");
                    inParams["Flags"] = (command == "shutdown") ? 1 : 2;
                    // Execute the method and obtain the return values.
                    ManagementBaseObject outParams =
                        os.InvokeMethod("Win32Shutdown", inParams, null);
                    message = " command successfully submitted";
                    message = (command == "shutdown") ? "Shutdown" + message : "Reboot" + message;
                }
                else if (command == "lastboottime")
                {
                    message = ParseCIM(os.GetPropertyValue("LastBootUpTime").ToString()).ToString();
                }
                else
                {
                    throw new Exception("Unknown command.");
                }
            }
            return message;
        }

        private static DateTime ParseCIM(string date)
        {
            //datetime object to store the return value
            DateTime parsed = DateTime.MinValue;

            //check date integrity
            if (date != null && date.IndexOf('.') != -1)
            {
                //obtain the date with miliseconds
                string newDate = date.Substring(0, date.IndexOf('.') + 4);

                //check the length
                if (newDate.Length == 18)
                {
                    //extract each date component
                    int y = Convert.ToInt32(newDate.Substring(0, 4));
                    int m = Convert.ToInt32(newDate.Substring(4, 2));
                    int d = Convert.ToInt32(newDate.Substring(6, 2));
                    int h = Convert.ToInt32(newDate.Substring(8, 2));
                    int mm = Convert.ToInt32(newDate.Substring(10, 2));
                    int s = Convert.ToInt32(newDate.Substring(12, 2));
                    int ms = Convert.ToInt32(newDate.Substring(15, 3));

                    //compose the new datetime object
                    parsed = new DateTime(y, m, d, h, mm, s, ms);
                }
            }

            //return datetime
            return parsed;
        }

        public static string powerOffVM(VMware.Vim.VimClient client, string vmName)
        {
            if (client == null) throw new Exception("Not connected to VCenter.");
            NameValueCollection filter = new NameValueCollection();
            filter.Add("Name", String.Format("^{0}$", Regex.Escape(vmName)));
            VMware.Vim.VirtualMachine vm =
                (VMware.Vim.VirtualMachine)client.FindEntityView(typeof(VMware.Vim.VirtualMachine), null, filter, null);
            if (vm == null)
                throw new Exception("Virtual machine not found.");
            vm.PowerOffVM();
            return "Power Off command sucessfully submitted.";
        }

        public static string powerOnVM(VMware.Vim.VimClient client, string vmName)
        {
            if (client == null) throw new Exception("Not connected to VCenter.");
            NameValueCollection filter = new NameValueCollection();
            filter.Add("Name", String.Format("^{0}$", Regex.Escape(vmName)));
            VMware.Vim.VirtualMachine vm =
                (VMware.Vim.VirtualMachine)client.FindEntityView(typeof(VMware.Vim.VirtualMachine), null, filter, null);
            if (vm == null)
                throw new Exception("Virtual machine not found.");
            vm.PowerOnVM(null);
            return "Power On command sucessfully submitted.";
        }

        public static string getPowerStatus(VMware.Vim.VimClient client, string vmName)
        {
            if (client == null) throw new Exception("Not connected to VCenter.");
            NameValueCollection filter = new NameValueCollection();
            filter.Add("Name", String.Format("^{0}$", Regex.Escape(vmName)));
            VMware.Vim.VirtualMachine vm =
                (VMware.Vim.VirtualMachine)client.FindEntityView(typeof(VMware.Vim.VirtualMachine), null, filter, null);
            if (vm == null)
                throw new Exception("Virtual machine not found.");
            return vm.Runtime.PowerState.ToString();
        }
    }

    public enum ActionStatus { None, Queued, Pending, Success, Failure, Unknown }

    public class Actionable
    {
        private Action _pre;
        private Func<string, string> _work;
        private Func<string, string> _post;
        private Action _cancel;
        private Boolean _running;
        private BackgroundWorker _worker;
        private string _name;
        public object runLock = new object();

        public Actionable(string name, Action pre, Func<string, string> work, Func<Exception, string> handleWorkError, Func<string, string> post, Action cancel)
        {
            _pre = pre;
            _work = work;
            _post = post;
            _cancel = cancel;
            _running = false;
            _name = name;
        }
        public Boolean Running
        {
            get { return _running; }
            set { _running = value; }
        }
        public BackgroundWorker Worker
        {
            get { return _worker; }
            set { _worker = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public Func<string, string> WorkFunc
        {
            get { return _work; }
        }
        public Func<string, string> Post
        {
            get { return _post; }
        }
    }

    public class ActionRunner
    {
        private Dictionary<string, Actionable> actions;
        private MainForm _form;
        private object actionLock = new object();

        public ActionRunner(MainForm form)
        {
            actions = new Dictionary<string, Actionable>();
            _form = form;
        }

        public void Add(Actionable a)
        {
            actions[a.Name] = a;
        }

        private Func<ActionResult> wrapFunc(Func<string, string> func, string serverName)
        {
            return new Func<ActionResult>(() =>
            {
                var result = "Unknown";
                var status = ActionStatus.Unknown;
                try
                {
                    result = func.Invoke(serverName);
                    status = ActionStatus.Success;
                }
                catch (Exception e)
                {
                    if (e.InnerException != null)
                    {
                        result = e.InnerException.Message;
                    }
                    else
                    {
                        result = e.Message;
                    }
                    status = ActionStatus.Failure;
                }
                return new ActionResult(result, status);
            });
        }

        private Action mapAction(MainForm formHandle, Actionable action, Dictionary<string, int> serverMap, CancellationToken token)
        {
            return new Action(() =>
            {
                var gridTasks = new List<Task>();
                foreach (string serverName in serverMap.Keys)
                {
                    int x = serverMap[serverName];
                    gridTasks.Add(Task.Factory.StartNew(() =>
                        formHandle.setServerData(x, action.Name, "\u231B", getColorForActionStatus(ActionStatus.Queued))));
                }
                Task.WaitAll(gridTasks.ToArray());

                var tasks = new List<Task>();
                foreach (string serverName in serverMap.Keys)
                {
                    int x = serverMap[serverName];
                    string name = serverName;
                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            formHandle.setServerData(x, action.Name, "In progress...", getColorForActionStatus(ActionStatus.Pending));
                            Task<ActionResult> t = Task.Factory.StartNew<ActionResult>(wrapFunc(action.WorkFunc, name), token);
                            t.Wait(token);
                            string result;
                            if (action.Post != null)
                            {
                                result = action.Post.Invoke(t.Result.Message);
                            }
                            else
                            {
                                result = t.Result.Message;
                            }
                            formHandle.setServerData(x, action.Name, result, getColorForActionStatus(t.Result.Status));
                        }
                        catch (Exception e)
                        {
                            formHandle.setServerData(x, action.Name, "Operation cancelled.", getColorForActionStatus(ActionStatus.Unknown));
                        }
                    }, token));
                }

                try
                {
                    Task.WaitAll(tasks.ToArray(), token);
                    formHandle.addToActionLog("Completed: " + action.Name);
                    action.Running = false;
                    formHandle.refreshActionListControls();
                }
                catch (OperationCanceledException opex)
                {
                    var indices = serverMap.Values.ToList();
                    var stillPending = formHandle.getPendingIndices(indices, action.Name);
                    foreach (int i in stillPending)
                    {
                        formHandle.setServerData(i, action.Name, "Operation cancelled.", getColorForActionStatus(ActionStatus.Unknown));
                    }
                    formHandle.addToActionLog("Cancelled: " + action.Name);
                    action.Running = false;
                    formHandle.refreshActionListControls();
                }
                lock (action.runLock)
                {
                    action.Running = false;
                }
            });
        }

        public void Run(string key, Dictionary<string, int> serverMap)
        {
            _form.addToActionLog("Started: " + key);
            var action = actions[key];
            if (!action.Running)
            {
                lock (action.runLock)
                {
                    action.Running = true;
                }

                CancellationTokenSource cts = new CancellationTokenSource();
                CancellationToken token = cts.Token;

                action.Worker = new BackgroundWorker();
                action.Worker.WorkerSupportsCancellation = true;
                action.Worker.DoWork += new DoWorkEventHandler(new Action<object, DoWorkEventArgs>((o, args) =>
                {
                    var bw = o as BackgroundWorker;
                    var a = mapAction(_form, action, serverMap, token);
                    var t = Task.Factory.StartNew(a, token);
                    while (t.IsCompleted != true)
                    {
                        if (bw.CancellationPending)
                        {
                            args.Cancel = true;
                            cts.Cancel();
                        }
                        Thread.Sleep(100);
                    }
                    _form.refreshActionListControls();
                }));
                action.Worker.RunWorkerAsync();
            }
        }

        public void Stop(string key)
        {
            var action = actions[key];
            action.Worker.CancelAsync();
        }

        public Boolean IsRunning(string key)
        {
            return actions[key].Running;
        }

        public Boolean AnyRunning()
        {
            return actions.Values.ToArray<Actionable>().Any(v => v.Running);
        }
        public List<string> ActionKeys
        {
            get { return actions.Keys.ToList<string>(); }
        }

        public static Func<string, string> dateStampResult()
        {
            return new Func<string, string>(s => s + " [" + DateTime.Now + "]");
        }
        public static Color getColorForActionStatus(ActionStatus stat)
        {
            if (stat == ActionStatus.None)
            {
                return Color.White;
            }
            else if (stat == ActionStatus.Queued)
            {
                return Color.Aqua;
            }
            else if (stat == ActionStatus.Pending)
            {
                return Color.Yellow;
            }
            else if (stat == ActionStatus.Success)
            {
                return Color.FromArgb(0x4b, 0xe5, 0x9b);
            }
            else if (stat == ActionStatus.Failure)
            {
                return Color.FromArgb(0xff, 0x6e, 0x53);
            }
            else
            {
                return Color.Orange;
            }
        }
    }
}
