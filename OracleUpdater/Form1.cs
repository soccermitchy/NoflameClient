using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Net;
using DotRas;
using NetFwTypeLib;
using System.ServiceProcess;
using System.Security.Cryptography;

namespace OracleUpdater
{
    struct Server
    {
        public string ipAddress;
        public string name;

        public Server(string ip, string name)
        {
            this.ipAddress = ip;
            this.name = name;
        }
    };

    public partial class Client : Form
    {
        private string psk = "REDACTED";
        private string userName;
        private string passWord;

        // Encryption
        static readonly string PassHash = "REDACTED"; // 8 Characters
        static readonly string saltKey = "REDACTED"; // 8 Characters
        static readonly string viKey = "REDACTED"; // 16 Characters

        private string registrationURL = "REDACTED";
        private string versionCheckURL = "http://thecateringguild.com/VERSION";

        private int selectedServer;

        Server[] servers = new Server[]
        {
            new Server("167.114.33.245", "US East 2"),
            //new Server("REDACTED", "Sinon (US West)"),
            //new Server("REDACTED", "Eugeo (US East)")
        };

        /* VPN */
        private string path = Environment.ExpandEnvironmentVariables("%AppData%\\Roaming\\MicrosoftNetworkConnections\\Pbk\\phonebook.pbk");
        private RasPhoneBook connections; //Stores Server Details
        private RasDialer currentVPN; //Connects to VPN

        /* Firewall */
        private INetFwProfile fireWallProfile = null;
        private int[] oraclePorts = { 500, 1701, 4500 };
        private string[] services = { "TapiSrv", "SstpSvc", "RasMan" };

        public Client()
        {
            InitializeComponent();

            connections = new RasPhoneBook();
            currentVPN = new RasDialer();

            CheckForUpdates();

            FormClosing += Client_Closing;
        }

        /* Encrypt String */
        public static string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PassHash, Encoding.ASCII.GetBytes(saltKey)).GetBytes(256 / 8);
			var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
			var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(viKey));
			
			byte[] cipherTextBytes;

			using (var memoryStream = new MemoryStream())
			{
				using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
				{
					cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
					cryptoStream.FlushFinalBlock();
					cipherTextBytes = memoryStream.ToArray();
					cryptoStream.Close();
				}
				memoryStream.Close();
			}
			return Convert.ToBase64String(cipherTextBytes);
        }

        public static string Decrypt(string encryptedText)
		{
			byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
			byte[] keyBytes = new Rfc2898DeriveBytes(PassHash, Encoding.ASCII.GetBytes(saltKey)).GetBytes(256 / 8);
			var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

			var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(viKey));
			var memoryStream = new MemoryStream(cipherTextBytes);
			var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
			byte[] plainTextBytes = new byte[cipherTextBytes.Length];

			int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
			memoryStream.Close();
			cryptoStream.Close();
			return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
		}

        /* Login Button */
        private void button1_Click(object sender, EventArgs e)
        {
            //LoadConsoleApp();
            if( storeCredsCheckBox.Checked )
            {
                SaveCredentials();
            }
            connectionErrorLabel.Text = "";
            Connect();
        }

        private void Init()
        {
            LoadCredentials();

            connections.Open(path);

            OpenFirewall();
            StartServices();
            HangUpAllCalls();

            //Build Phonebook
            for(int i = 0; i < servers.Length; i++)
            {
                RasEntry newConnection = RasEntry.CreateVpnEntry(servers[i].name, servers[i].ipAddress, RasVpnStrategy.L2tpOnly, RasDevice.GetDeviceByName("(L2TP)", RasDeviceType.Vpn));
                newConnection.Options.UsePreSharedKey = true;
                newConnection.IPv4InterfaceMetric = 1;
                newConnection.Options.RemoteDefaultGateway = false;
                newConnection.Options.RequireChap = false;
                newConnection.EncryptionType = RasEncryptionType.RequireMax;
                newConnection.Options.ReconnectIfDropped = true;

                //If entry exists, remove it
                if (connections.Entries.Contains(servers[i].name))
                {
                    HangUpAllCalls();
                }
                else
                {
                    connections.Entries.Add(newConnection);
                    newConnection.UpdateCredentials(RasPreSharedKey.Client, psk);
                }
            }
        }

        private void Connect()
        {
            selectedServer = serverSelectionComboBox.SelectedIndex;

            bool detailsEntered = true;

            if(legacyCheck.Checked == false && userTxt.Text == "" || legacyCheck.Checked == false && passTxt.Text == "")
            {
                detailsEntered = false;
            }
            if (detailsEntered)
            {
                if (legacyCheck.Checked)
                {
                    this.userName = "halo";
                    this.passWord = "halo";
                }
                else
                {
                    //Set Login Credentials
                    this.userName = userTxt.Text;
                    this.passWord = passTxt.Text;
                }

                currentVPN.DialCompleted += DialCompleted;
                currentVPN.EntryName = servers[selectedServer].name;
                currentVPN.PhoneBookPath = path;
                currentVPN.Credentials = new System.Net.NetworkCredential(this.userName, this.passWord);
                currentVPN.AutoUpdateCredentials = RasUpdateCredential.User;
                currentVPN.AllowUseStoredCredentials = false;
                currentVPN.Timeout = 10000;

                currentVPN.DialAsync();

                loginBTN.Text = "Connecting";
                loginBTN.Enabled = false;
            }
        }

        void Client_Closing(object sender, FormClosingEventArgs e)
        {
            HangUpAllCalls();
            System.Threading.Thread.Sleep(3000);
        }

        private void DialCompleted(object sender, DialCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                connectionErrorLabel.ForeColor = Color.Red;
                connectionErrorLabel.Text = "Error: Connection Cancelled";
                loginBTN.Text = "Login";
                loginBTN.Enabled = true;
                currentVPN.DialCompleted -= DialCompleted;
            }
            else if (e.TimedOut)
            {
                connectionErrorLabel.ForeColor = Color.Red;
                connectionErrorLabel.Text = "Error: Timed Out.";
                loginBTN.Text = "Login";
                loginBTN.Enabled = true;
                currentVPN.DialCompleted -= DialCompleted;
            }

            else if (e.Connected)
            {
                //Show Disconnect Button
                disconnectBtn.Enabled = true;
                disconnectBtn.Show();

                //Hide Login Button
                loginBTN.Text = "Connecting";
                loginBTN.Enabled = false;
                loginBTN.Hide();

                //DO GET REQUEST

                string ipRegUrl = string.Format("REDACTED", this.userName);

                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ipRegUrl);
                    request.Timeout = 4000;
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream stream = response.GetResponseStream();
                }
                catch
                {
                    //MessageBox.Show("Error")
                }
                


                //Switch Window Title to Connected
                this.Text = string.Format("NoFlame VPN Client : Connected - {0}", GetOracleIP());
                
                //Show MessageBox
               // if (MessageBox.Show("Proceed?", "Confirm", MessageBoxButtons.ok))
                MessageBox.Show("Connected! Launch your games now!", "Success!", MessageBoxButtons.OK);
                currentVPN.DialCompleted -= DialCompleted;
            }
            else if(e.Error != null)
            {
                loginBTN.Text = "Login";
                MessageBox.Show(e.Error.ToString(), "Error!", MessageBoxButtons.OK);
                loginBTN.Enabled = true;
                currentVPN.DialCompleted -= DialCompleted;
            }          
        }

        private void Disconnect()
        {


            HangUpAllCalls();

            //Show Login Button
            loginBTN.Enabled = true;
            loginBTN.Text = "Login";
            loginBTN.Show();

            //Hide Disconnect
            disconnectBtn.Text = "Disconnect";
            disconnectBtn.Enabled = false;
            disconnectBtn.Hide();

            //Set Window Title
            this.Text = "NoFlame VPN Client";
        }

        private void HangUpAllCalls()
        {
            currentVPN.DialAsyncCancel();

            for( int i = 0; i < servers.Length; i++)
            {
                RasConnection currentConnection = RasConnection.GetActiveConnectionByName(servers[i].name, path);
                if (currentConnection != null)
                {
                    currentConnection.HangUp();
                }
            }
        }


        private void CheckForUpdates()
        {
            string newVersion = "1.3.0.0"; //Get This from the server

            string clientVersion = AssemblyName.GetAssemblyName("NoflameLauncher.exe").Version.ToString();

            //Reenable once version.txt is available.
            using (WebClient vs = new WebClient())
            {
                Stream file = vs.OpenRead(versionCheckURL);
                StreamReader parse = new StreamReader(file);
                newVersion = parse.ReadToEnd();
            }

            newVersion = newVersion.Trim();

            if( clientVersion.Equals(newVersion) )
            {
                //Up to date
                upToDateLbl.Text = "Up to Date";

                //Hide Button since not required.
                updateBtn.Enabled = false;
                updateBtn.Hide();

                //Init OracleNet
                Init();
            }
            else
            {
                //Not up to date
                upToDateLbl.Text = "Update Available!";

                //Show Update Button
                updateBtn.Show();
                updateBtn.Enabled = true;
                loginBTN.Enabled = false; //Disable Login
            }

            label3.Text = string.Format("Client Version: {0}", clientVersion);
        }

        private string GetOracleIP()
        {
            string ip = "";
            /* Oracle Assigned IP */
            foreach (RasConnection connection in RasConnection.GetActiveConnections())
            {
                for (int i = 0; i < servers.Length; i++)
                {
                    if (connection.EntryName == servers[i].name)
                    {
                        RasIPInfo info = (RasIPInfo)connection.GetProjectionInfo(RasProjectionType.IP);
                        if (info != null)
                        {
                            ip = info.IPAddress.ToString();
                        }
                    }
                }
            }
            return ip;
        }

        private void OpenFirewall()
        {
            /* Open the Firewall */
            INetFwAuthorizedApplications allowedApps = null;
            INetFwAuthorizedApplication allowedApp = null;
            INetFwOpenPorts openPorts = null;
            INetFwOpenPort openPort = null;
            try
            {
                if (CheckOracleAllowed("NoflameLauncher") == false)
                {
                    firewallLabel.ForeColor = Color.Red;
                    firewallLabel.Text = "Firewall: Adding NoFlame VPN";

                    SetFirewallProfile();
                    allowedApps = fireWallProfile.AuthorizedApplications;
                    allowedApp = GetFirewallInstance("INetAuthApp") as INetFwAuthorizedApplication;
                    allowedApp.ProcessImageFileName = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    allowedApp.Name = "NoflameClient";
                    allowedApps.Add(allowedApp);
                }

                //Double Check App
                if (CheckOracleAllowed("NoflameClient") == true)
                {
                    //FIREWALLTXT UPDATE
                    firewallLabel.ForeColor = Color.Cyan;
                    firewallLabel.Text = "Firewall: Okay!";
                }

                //Check If Ports are Open
                for (int i = 0; i < oraclePorts.Length; i++)
                {
                    if (CheckFirewallPort(oraclePorts[i]) == false)
                    {
                        portsLabel.ForeColor = Color.Red;
                        portsLabel.Text = "Ports: Closed";

                        SetFirewallProfile();
                        openPorts = fireWallProfile.GloballyOpenPorts;
                        openPort = GetFirewallInstance("INetOpenPort") as INetFwOpenPort;
                        openPort.Port = oraclePorts[i];
                        openPort.Protocol = NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP;
                        openPort.Name = string.Format("OracleNet{0}", i);
                        openPorts.Add(openPort);
                    }
                }

                //Double Check If Ports are Open
                for (int i = 0; i < oraclePorts.Length; i++)
                {
                    if (CheckFirewallPort(oraclePorts[i]) == true)
                    {
                        portsLabel.ForeColor = Color.Cyan;
                        portsLabel.Text = "Ports: Open!";
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (allowedApps != null) allowedApps = null;
                if (allowedApp != null) allowedApp = null;
                if (openPorts != null) openPorts = null;
                if (openPort != null) openPort = null;
            }
        }

        private void CloseFirewall()
        {
            /* Close the Firewall */
            INetFwAuthorizedApplications apps = null;
            INetFwOpenPorts ports = null;
            try
            {
                /* TODO APP EXCEPTION REMOVAL */
                if (CheckOracleAllowed("OracleLauncher") == true)
                {
                    SetFirewallProfile();
                    apps = fireWallProfile.AuthorizedApplications;
                    apps.Remove("OracleLauncher.exe");
                }

                // Close Ports
                for (int i = 0; i < oraclePorts.Length; i++)
                {
                    if (CheckFirewallPort(oraclePorts[i]) == true)
                    {
                        SetFirewallProfile();
                        ports = fireWallProfile.GloballyOpenPorts;
                        ports.Remove(oraclePorts[0], NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if (apps != null) apps = null;
                if (ports != null) ports = null;
            }
        }

        private bool CheckOracleAllowed(string appName)
        {
            bool result = false;
            Type programID = null;
            INetFwMgr firewall = null;
            INetFwAuthorizedApplications appsAllowed = null;
            INetFwAuthorizedApplication appAllowed = null;

            try
            {
                programID = Type.GetTypeFromProgID("HNetCfg.FwMgr");
                firewall = Activator.CreateInstance(programID) as INetFwMgr;
                if (firewall.LocalPolicy.CurrentProfile.FirewallEnabled)
                {
                    appsAllowed = firewall.LocalPolicy.CurrentProfile.AuthorizedApplications;
                    IEnumerator appEnum = appsAllowed.GetEnumerator();
                    while ((appEnum.MoveNext()))
                    {
                        appAllowed = appEnum.Current as INetFwAuthorizedApplication;
                        if (appAllowed.Name == appName)
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if (programID != null) programID = null;
                if (firewall != null) firewall = null;
                if (appsAllowed != null) appsAllowed = null;
                if (appAllowed != null) appAllowed = null;
            }

            return result;
        }

        private bool CheckFirewallPort(int portNum)
        {
            bool result = false;
            INetFwOpenPorts ports = null;
            Type programID = null;
            INetFwMgr firewall = null;
            INetFwOpenPort port = null;

            try
            {
                programID = Type.GetTypeFromProgID("HNetCfg.FwMgR");
                firewall = Activator.CreateInstance(programID) as INetFwMgr;
                ports = firewall.LocalPolicy.CurrentProfile.GloballyOpenPorts;
                IEnumerator portEnumerate = ports.GetEnumerator();
                while ((portEnumerate.MoveNext()))
                {
                    port = portEnumerate.Current as INetFwOpenPort;
                    if (port.Port == portNum)
                    {
                        result = true;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (ports != null) ports = null;
                if (programID != null) programID = null;
                if (firewall != null) firewall = null;
                if (port != null) port = null;
            }

            return result;
        }

        private void SetFirewallProfile()
        {
            INetFwMgr firewallManager = null;
            INetFwPolicy firewallPolicy = null;

            try
            {
                firewallManager = GetFirewallInstance("INetFwMgr") as INetFwMgr;
                firewallPolicy = firewallManager.LocalPolicy;
                fireWallProfile = firewallPolicy.CurrentProfile;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if (firewallManager != null) firewallManager = null;
                if (firewallPolicy != null) firewallPolicy = null;
            }
        }

        private object GetFirewallInstance(string name)
        {
            Type result = null;
            switch (name)
            {
                case "INetFwMgr":
                    result = Type.GetTypeFromCLSID(new Guid("{304CE942-6E39-40D8-943A-B913C40C9CD4}"));
                    return Activator.CreateInstance(result);
                case "INetAuthApp":
                    result = Type.GetTypeFromCLSID(new Guid("{EC9846B3-2762-4A6B-A214-6ACB603462D2}"));
                    return Activator.CreateInstance(result);
                case "INetOpenPort":
                    result = Type.GetTypeFromCLSID(new Guid("{0CA545C6-37AD-4A6C-BF92-9F7610067EF5}"));
                    return Activator.CreateInstance(result);
                default:
                    return null;
            }
        }

        private void StartServices()
        {
            ServiceController[] allServices = ServiceController.GetServices();

            for (int i = 0; i < services.Length; i++)
            {
                foreach (ServiceController service in allServices)
                {
                    if (service.ServiceName == services[i] && service.Status == ServiceControllerStatus.Running)
                    {
                        servicesLabel.ForeColor = Color.Cyan;
                        servicesLabel.Text = "Services: Running!";
                    }
                    else if (service.ServiceName == services[i] && service.Status == ServiceControllerStatus.Stopped)
                    {
                        TimeSpan serviceTimeout = TimeSpan.FromMilliseconds(10000);
                        service.Start();
                        service.WaitForStatus(ServiceControllerStatus.Running, serviceTimeout);
                    }
                }
            }

            //Check Again
            for (int i = 0; i < services.Length; i++)
            {
                foreach (ServiceController service in allServices)
                {
                    if (service.ServiceName == services[i] && service.Status == ServiceControllerStatus.Running)
                    {
                        servicesLabel.ForeColor = Color.Cyan;
                        servicesLabel.Text = "Services: Running!";
                    }
                    else if (service.ServiceName == services[i] && service.Status == ServiceControllerStatus.Stopped)
                    {
                        servicesLabel.ForeColor = Color.Red;
                        servicesLabel.Text = "Services: Error";
                    }
                }
            }
        }

        private void LoadCredentials()
        {
            bool saved = false;

            if( File.Exists("credentials.txt"))
            {
                string[] credentials = System.IO.File.ReadAllLines("credentials.txt");

                if (Boolean.TryParse(credentials[0], out saved))
                {
                    storeCredsCheckBox.Checked = saved;
                }

                userTxt.Text = credentials[1];
                passTxt.Text = Decrypt(credentials[2]);//credentials[2];

                int result;
                if(int.TryParse( credentials[3], out result))
                {
                    serverSelectionComboBox.SelectedIndex = result;
                }
            }
            else
            {
                serverSelectionComboBox.SelectedIndex = 0;
            }
        }

        private void SaveCredentials()
        {
            string[] credentials = { "true", userTxt.Text, Encrypt(passTxt.Text), serverSelectionComboBox.SelectedIndex.ToString() };
        
            //Write To file
            System.IO.File.WriteAllLines("credentials.txt", credentials);
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            Process.Start("OracleUpdater.exe");

            Application.Exit();
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            Process.Start(registrationURL);
        }

        private void disconnectBtn_Click(object sender, EventArgs e)
        {
            disconnectBtn.Text = "Disconnecting...";
            disconnectBtn.Enabled = false;
            Disconnect();
        }
        private void label4_Click(object sender, EventArgs e)
        {
            Process.Start("http://thecateringguild.com");
        }
        /* ------------ NOT USED, VS PREGEN ------------  */

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Client_Load(object sender, EventArgs e)
        {
        }

        private void Client_Load_1(object sender, EventArgs e)
        {

        }
    }
}
