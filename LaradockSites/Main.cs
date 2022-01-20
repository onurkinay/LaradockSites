using System.Diagnostics;
using System.Configuration;
using System.Collections.Specialized;

namespace LaradockSites
{
    public partial class Main : Form
    {
        public static string laradock = ConfigurationManager.AppSettings.Get("Path");
        Status status; 
        public Main()
        {
            if (Funcs.isLDExists() && laradock == "0")
            {
                MessageBox.Show("Choose a folder including laradock folder", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Funcs.changePath(false);
            }
            InitializeComponent(); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            List<Site> siteler = Funcs.getSites();
            if (Funcs.isLDExists() && siteler.Count == 0)
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

            if (Funcs.isLDExists() && siteler.Count > 0)
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
            Funcs.openSite(lbServerName.Text.Split(":")[1].Trim());
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
            Funcs.openSite(lbServerName.Text.Split(":")[1].Trim());
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
            status = Funcs.showStatus(this, "Laradock is starting. Please wait");
            
            Task.Run(() =>
            {
                Process p = new Process();
                p.StartInfo.WorkingDirectory = Path.Combine(laradock, "laradock");

                p.EnableRaisingEvents = true;
                p.Exited += myProcess_Exited;

                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.Arguments = "/C docker-compose start";
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.WaitForExit();
            });
              
        }
        private void btnStopLD_Click(object sender, EventArgs e)
        {
            status = Funcs.showStatus(this, "Laradock is stopping. Please wait");
           
            Task.Run(() =>
            {
                Process p = new Process();
                p.StartInfo.WorkingDirectory = Path.Combine(laradock, "laradock");

                p.EnableRaisingEvents = true;
                p.Exited += myProcess_Exited;

                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.Arguments = "/C docker-compose stop";
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.WaitForExit();
            });
        }
        private void myProcess_Exited(object sender, System.EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                status.Close();
                this.Enabled = true;
                timer1.Enabled = true;
                
            });
        }
         
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer1.Enabled)
                Task.Run(asyncRun);
        }

        public Task<bool> asyncRun()
        {
            bool isRunning = Funcs.isLDRunning();
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
        private void btnOpenDirectory_Click(object sender, EventArgs e)
        {
            if (lbSiteler.SelectedItem != null)
            {
                Process.Start("explorer.exe", Path.Combine(laradock, ((Site)lbSiteler.SelectedItem).Location.Split("/")[0]));

            }
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            Funcs.changePath(true);
            Status status = Funcs.showStatus(this, "Laradock is installing. Please wait.");
            Task.Run(() =>
            {
                Funcs.installLaradock();
            });
    
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
            Funcs.changePath(false);
        }
    }
}