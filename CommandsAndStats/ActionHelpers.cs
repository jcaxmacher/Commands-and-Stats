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
using System.Diagnostics;

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

        public static string nmapOsScan(string serverName)
        {
            StringBuilder output;
            ProcessStartInfo processStartInfo;
            Process process;
            
            output = new StringBuilder();
            processStartInfo = new ProcessStartInfo();
            processStartInfo.CreateNoWindow = true;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardInput = true;
            processStartInfo.UseShellExecute = false;
            processStartInfo.Arguments = " -O --osscan-limit --osscan-guess --max-os-tries 1 " + serverName;
            processStartInfo.FileName = "nmap.exe";

            process = new Process();
            process.StartInfo = processStartInfo;
            // enable raising events because Process does not raise events by default
            process.EnableRaisingEvents = true;
            process.OutputDataReceived += (sender, args) => output.Append(args.Data + '\n');
            process.Start();
            process.BeginOutputReadLine();

            process.WaitForExit();
            process.CancelOutputRead();

            // use the output
            string result = output.ToString();
            string guesses = result.Split('\n').Where(x => x.StartsWith("Aggressive OS guesses: ")).FirstOrDefault().Replace("Aggressive OS guesses: ", "");
            return guesses;
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

        public static string runWMIQuery(string serverName, string command)
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
                else if (command == "winver")
                {
                    message = os.GetPropertyValue("Caption").ToString();
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
        
        public static string getWindowsInstallDate(string serverName)
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.RegistryKey.OpenRemoteBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, serverName);
            key = key.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", false);
            if (key == null)
                throw new Exception("Could not connect or get registry key.");

            DateTime startDate = new DateTime(1970, 1, 1, 0, 0, 0);
            Int64 regVal = Convert.ToInt64(key.GetValue("InstallDate").ToString());

            DateTime installDate = startDate.AddSeconds(regVal);

            return installDate.ToShortDateString();
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
        private Boolean _repeat;
        private BackgroundWorker _worker;
        private string _name;
        private int _maxrunning;
        public object runLock = new object();

        public Actionable(string name, Action pre, Func<string, string> work, Func<Exception, string> handleWorkError, Func<string, string> post, Action cancel, int maxrunning=-1)
        {
            _pre = pre;
            _work = work;
            _post = post;
            _cancel = cancel;
            _running = false;
            _name = name;
            _maxrunning = maxrunning;
        }
        public int MaxRunning
        {
            get { return _maxrunning; }
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
        public Boolean Repeat
        {
            get { return _repeat; }
            set { _repeat = value; }
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

                try
                {

                    int atOnce;
                    if (action.MaxRunning == -1)
                        atOnce = serverMap.Keys.Count;
                    else
                        atOnce = action.MaxRunning;

                    int segments = serverMap.Keys.Count / atOnce;
                    if (segments * atOnce < serverMap.Keys.Count) segments++;
                    var keys = serverMap.Keys.ToArray();

                    for (int i = 0; i < segments; i++)
                    {
                        var tasks = new List<Task>();
                        for (int j = i * atOnce; j < (i * atOnce) + atOnce; j++)
                        {
                            if (j < serverMap.Keys.Count)
                            {
                                string serverName = keys[j];
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
                        }
                        try
                        {
                            Task.WaitAll(tasks.ToArray(), token);
                        }
                        catch (OperationCanceledException opex)
                        {
                            throw opex;
                        }

                    }
                
                    formHandle.addToActionLog("Completed: " + action.Name);
                    action.Running = false;
                    formHandle.refreshActionListControls();
                    if (action.Repeat) this.Run(action.Name, formHandle.getEnabledServers());
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

        public Boolean WillRepeat(string key)
        {
            return actions[key].Repeat;
        }

        public void SetRepeat(string key, Boolean bo)
        {
            actions[key].Repeat = bo;
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
