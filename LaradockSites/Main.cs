using System.Diagnostics;
using System.Configuration;
using System.Collections.Specialized;

namespace LaradockSites
{
    public partial class Main : Form
    {
        public static string laradock = ConfigurationManager.AppSettings.Get("Path");
        public Main()
        {
            if (isLDExists() && laradock == "0")
            {
                MessageBox.Show("Choose a folder including laradock folder", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                changePath(false);
            }
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            List<Site> siteler = Funcs.getSites();
            if (isLDExists() && siteler.Count == 0)
            {
                lbStatus.Text = "LARADOCK IS EXISTS IN DOCKER BUT FOLDER PATH IS WRONG";
                btnOpenDirectory.Enabled = false;
                btnOpenSite.Enabled = false;
                btnStartLD.Enabled = false;
                btnStopLD.Enabled = false;
                btnSiteEkle.Enabled = false;
                btnSiteSil.Enabled = false;
                btnInstall.Enabled = false;
                btnAccessCMD.Enabled = false;
                btnChangePath.Enabled = true;
                timer1.Enabled = false;
                return;
            }

            if (isLDExists() && siteler.Count > 0)
            {
                lbSiteler.DataSource = Funcs.getSites();
                btnChangePath.Enabled = true;
            }
            else
            {
                lbStatus.Text = "LARADOCK IS NOT EXISTS. TO INSTALL JUST CLICK";
                btnOpenDirectory.Enabled = false;
                btnOpenSite.Enabled = false;
                btnStartLD.Enabled = false;
                btnStopLD.Enabled = false;
                btnSiteEkle.Enabled = false;
                btnSiteSil.Enabled = false;
                btnInstall.Enabled = true;
                btnAccessCMD.Enabled = false;
                btnChangePath.Enabled = false;
                timer1.Enabled = false;

            }

        }

        

        private void lbSiteler_DoubleClick(object sender, EventArgs e)
        {
            openSite(lbServerName.Text.Split(":")[1].Trim());
        }
        private void lbSiteler_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbSiteler.SelectedItem != null)
            {
                Site selectedSite = (Site)lbSiteler.SelectedItem;
                lbSiteName.Text = "Site Adi: " + selectedSite.Name;
                lbServerName.Text = "Server Name: " + selectedSite.Server_Name;
                lbLocation.Text = "Location: " + selectedSite.Location;
                btnOpenDirectory.Enabled = true;
                btnOpenSite.Enabled = true;
              
            }
        }

        private void btnOpenSite_Click(object sender, EventArgs e)
        {
            openSite(lbServerName.Text.Split(":")[1].Trim());
        }

        private static void openSite(string url)
        {
            Process myProcess = new Process();
            try
            {
                myProcess.StartInfo.UseShellExecute = true;
                myProcess.StartInfo.FileName = "http://" + url;
                myProcess.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            } 
        }

        private void btnSiteEkle_Click(object sender, EventArgs e)
        {
            new AddSite(Path.Combine(laradock,@"laradock\nginx\sites")).ShowDialog();
            lbSiteler.DataSource = Funcs.getSites();
        }

        private void btnSiteSil_Click(object sender, EventArgs e)
        {
            if (lbSiteler.SelectedItem != null)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure deleting site", "Confirm?", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    Site selectedSite = (Site)lbSiteler.SelectedItem;
                    Funcs.removeSite(selectedSite);
                    lbSiteler.DataSource = Funcs.getSites();
                    lbSiteler.SelectedIndex = -1;
                } 
             
            }
            else
            {
                MessageBox.Show("Bir site seçiniz");
            }
        }

        private void btnStartLD_Click(object sender, EventArgs e)
        {
            // Start the child process.
            Process p = new Process(); 
            p.StartInfo.WorkingDirectory = Path.Combine(laradock, "laradock");
            p.StartInfo.FileName = "cmd.exe"; 
            p.StartInfo.Arguments = "/C docker-compose start";
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.Start();
            p.WaitForExit(); 
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer1.Enabled)
                Task.Run(asyncRun);

        }

        public Task<bool> asyncRun()
        {
            bool isRunning = isLDRunning();
            this.Invoke((MethodInvoker)delegate {

                if (isRunning)
                {

                    lbStatus.Text = "LARADOCK IS RUNNING";
                    btnStartLD.Enabled = false;
                    btnStopLD.Enabled = true;
                    btnAccessCMD.Enabled = true;

                }
                else
                {
                    lbStatus.Text = "LARADOCK IS NOT RUNNING";
                    btnStartLD.Enabled = true;
                    btnStopLD.Enabled = false; 
                    btnAccessCMD.Enabled = false;   
                }

            });
            return Task.FromResult(false);
        }

        public static bool isLDRunning()
        {
            // Start the child process.
            Process p = new Process();
            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "docker";
            p.StartInfo.Arguments = "ps";
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            // Do not wait for the child process to exit before
            // reading to the end of its redirected stream.
            // p.WaitForExit();
            // Read the output stream first and then wait.
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            if (output.Contains("laradock"))
            {

                //lbStatus.Text = "LARADOCK IS RUNNING";
                //btnStartLD.Enabled = false;
                //btnStopLD.Enabled = true;
                return true;
            }
            else
            {
                //lbStatus.Text = "LARADOCK IS NOT RUNNING";
                //btnStartLD.Enabled = true;
                //btnStopLD.Enabled = false;
                return false;
            }
        }

        public static bool isLDExists()
        {
            // Start the child process.
            Process p = new Process();
            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "docker";
            p.StartInfo.Arguments = "ps -a";
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.CreateNoWindow = true;
            p.Start(); 
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            if (output.Contains("laradock"))
            { 
                return true;
            }
            else
            { 
                return false;
            }
        }

        public static void AddOrUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

        public void changePath(bool isFirst)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();

            folderBrowserDialog1.ShowNewFolderButton = false;
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Personal;
            folderBrowserDialog1.InitialDirectory = laradock;

            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string folderName = folderBrowserDialog1.SelectedPath;
                if (isFirst)
                {
                    ConfigurationManager.AppSettings.Set("Path", folderName);
                    AddOrUpdateAppSettings("Path", folderName);
                    laradock = folderName;
                    return;
                }
                if (Directory.Exists(Path.Combine(folderName, "laradock")))
                {
                    ConfigurationManager.AppSettings.Set("Path", folderName);
                    AddOrUpdateAppSettings("Path", folderName);
                    laradock = folderName;
                    Application.Restart();
                }
                else if(isLDExists())
                {
                    DialogResult goingChoose = MessageBox.Show("Choose a folder including laradock folder","Error!",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
                    if (goingChoose == DialogResult.OK)
                        changePath(isFirst);
                    else Environment.Exit(0);
                }

            }
            else
            {
                if (laradock == "0")
                {
                    DialogResult goingChoose = MessageBox.Show("You must choose a folder", "Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    if (goingChoose == DialogResult.OK)
                        changePath(isFirst);
                    else Environment.Exit(0);
                }
            }
        }

        private void btnStopLD_Click(object sender, EventArgs e)
        {
            // Start the child process.
            Process p = new Process();
            p.StartInfo.WorkingDirectory = Path.Combine(laradock, "laradock");
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/C docker-compose stop";
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.Start();
            p.WaitForExit(); 
        }

        private void btnOpenDirectory_Click(object sender, EventArgs e)
        {
            if (lbSiteler.SelectedItem != null)
            {
                Process.Start("explorer.exe", Path.Combine(laradock, ((Site)lbSiteler.SelectedItem).Location.Split("/")[0]));

            }
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            changePath(true);
            Status status = new("Laradock is installing. Please wait...");
            status.Show();

            Funcs.installLaradock();

            status.Close();
        }

        private void btnAccessCMD_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            p.StartInfo.WorkingDirectory = Path.Combine(laradock, "laradock");
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/C docker-compose exec workspace bash";
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.Start();
           
        }

        private void btnChangePath_Click(object sender, EventArgs e)
        {
            changePath(false);
        }
    }
}