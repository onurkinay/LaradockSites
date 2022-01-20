using System.Diagnostics;

namespace LaradockSites
{
    public partial class Form1 : Form
    {
        string laradock = @"D:\projelerim\Web Site\firstlaravel\";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lbSiteler.DataSource = Funcs.getSites(laradock);

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
            lbSiteler.DataSource = Funcs.getSites(laradock);
        }

        private void btnSiteSil_Click(object sender, EventArgs e)
        {
            if (lbSiteler.SelectedItem != null)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure deleting site", "Confirm?", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    Site selectedSite = (Site)lbSiteler.SelectedItem;
                    Funcs.removeSite(laradock, selectedSite);
                    lbSiteler.DataSource = Funcs.getSites(laradock);
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

                }
                else
                {
                    lbStatus.Text = "LARADOCK IS NOT RUNNING";
                    btnStartLD.Enabled = true;
                    btnStopLD.Enabled = false; 
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

    }
}