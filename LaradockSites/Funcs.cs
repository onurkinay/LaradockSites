using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LaradockSites
{
    internal class Funcs
    { /* 
       * docker is exists or not
       * package installing
       * reinstall laradock if laradock folder exists
       * one function for process and status
       * --add index html when added site 
       * 
       * */

        public static List<Site> getSites()//get info from nginx sites folder
        {
            List<Site> list = new();
            if (Directory.Exists(Path.Combine(Main.laradock, "laradock")))
            { 
                foreach (string path in Directory.GetFiles(Path.Combine(Main.laradock, @"laradock\nginx\sites")))
                {
                    if (path.Contains(".conf") && !path.Contains(".conf.example"))
                    {
                        Site site = new()
                        {
                            Name = path.Split('\\').Last().Replace(".conf", ""),

                        };
                        foreach (string line in File.ReadLines(path))
                        {
                            if (line.Contains("server_name") && site.Server_Name == null)
                            {
                                site.Server_Name = line.Replace("server_name", "").Replace(";", "").Trim();
                            }
                            else if (line.Contains("root /var/www/") && site.Location == null)
                            {
                                site.Location = line.Replace("root /var/www/", "").Replace(";", "").Trim();
                            }
                        }
                        site.isInLocalhost = ExistsHosts(site.Server_Name);
                        list.Add(site);
                    }

                }
            }
            return list;
        }

        public static bool removeSite(Site site)//remove site from nginx sites
        {
            string path = Path.Combine(Path.Combine(Main.laradock, @"laradock\nginx\sites"), site.Name + ".conf");
            if (File.Exists(path)) File.Delete(path);
            
            string toRemoveLine = "127.0.0.1 " + site.Server_Name;
            string text = File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "drivers/etc/hosts"), Encoding.UTF8);

            text = text.Replace(toRemoveLine, string.Empty);
            File.WriteAllLines(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "drivers/etc/hosts"), new[] { text });
            RestartNginx();
            return true;
        }

        public static bool ExistsHosts(string site)//remove site from hosts
        {
            foreach (string line in File.ReadLines(@"C:\Windows\System32\drivers\etc\hosts"))
            {
                if (line.Contains("127.0.0.1") && line.Contains(site)) return true;
            }
            return false;
        }
        public static void RestartNginx()//restart nginx
        {
            if (isLDRunning())
            {   // Start the child process.
                Status status = new("NGINX is restarting. Please wait...");
                status.Show();
                Process p = new Process();
                p.StartInfo.WorkingDirectory = Path.Combine(Main.laradock, "laradock");
                p.StartInfo.FileName = "docker-compose";
                p.StartInfo.Arguments = "restart nginx";
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.WaitForExit();
                status.Close();
            }
        }

        public static void installLaradock()//install laradock from stratch
        {
           
            using (var client = new WebClient())
            {
                client.DownloadFile("https://github.com/laradock/laradock/archive/refs/heads/master.zip", "latest.zip");
            }
            if(Directory.Exists("cont")) Directory.Delete("cont", true);

            System.IO.Compression.ZipFile.ExtractToDirectory("latest.zip", "cont");
            CopyDirectory(@"cont\laradock-master", Path.Combine(Main.laradock, "laradock"),true); 
            Directory.Delete("cont",true);

            File.Copy(Path.Combine(Main.laradock, @"laradock\.env.example"), Path.Combine(Main.laradock, @"laradock\.env"));

            File.AppendAllText(Path.Combine(Main.laradock, @"laradock\.env"), "\nDB_HOST=mysql\nREDIS_HOST=redis\nQUEUE_HOST=beanstalkd ");
           
            Process p = new Process();
            p.StartInfo.WorkingDirectory = Path.Combine(Main.laradock, "laradock");
            p.StartInfo.FileName = "docker-compose";
            p.StartInfo.Arguments = "up -d nginx mysql phpmyadmin redis workspace ";
        
            p.Start();
            p.WaitForExit();
            Application.Restart();
            
           
        }

        public static Status showStatus(Form form, string statusText)
        {
            Status status = new(statusText);
            status.StartPosition = FormStartPosition.Manual;
            status.Location = new Point(form.Location.X + (form.Width - status.Width) / 2, form.Location.Y + (form.Height - status.Height) / 2);
            form.Enabled = false;
            ((Main)form).timer1.Enabled = false;
            status.Show();
            return status;
        }
         
        static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
        {
            // Get information about the source directory
            var dir = new DirectoryInfo(sourceDir);

            // Check if the source directory exists
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            // Cache directories before we start copying
            DirectoryInfo[] dirs = dir.GetDirectories();

            // Create the destination directory
            Directory.CreateDirectory(destinationDir);

            // Get the files in the source directory and copy to the destination directory
            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath);
            }

            // If recursive and copying subdirectories, recursively call this method
            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                    CopyDirectory(subDir.FullName, newDestinationDir, true);
                }
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
        public static bool isLDExists()// is exists laradock in docker
        {  
            if (runCmd("docker","ps -a", true).Contains("laradock")) 
                return true;
            else
                return false; 
        }

        public static bool isLDRunning()
        { 
            if (runCmd("docker","ps",true).Contains("laradock")) 
                return true;
            else
                return false; 
        }

        public static string runCmd(string program, string args,bool isCreateNoWindow)
        {
            // Start the child process.
            Process p = new Process();
            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = program;
            p.StartInfo.Arguments = args;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.CreateNoWindow = isCreateNoWindow;
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            return output;
        }

        public static void changePath(bool isFirst)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();

            folderBrowserDialog1.ShowNewFolderButton = false;
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Personal;
            folderBrowserDialog1.InitialDirectory = Main.laradock;

            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string folderName = folderBrowserDialog1.SelectedPath;
                if (isFirst)//first time?
                {
                    ConfigurationManager.AppSettings.Set("Path", folderName);
                    AddOrUpdateAppSettings("Path", folderName);
                    Main.laradock = folderName;
                    return;
                }
                if (Directory.Exists(Path.Combine(folderName, "laradock")))//check laradock folder exists
                {
                    ConfigurationManager.AppSettings.Set("Path", folderName);
                    AddOrUpdateAppSettings("Path", folderName);
                    Main.laradock = folderName;
                    Application.Restart();
                }
                else if (isLDExists())//chech laradock conts. in docker
                {
                    DialogResult goingChoose = MessageBox.Show("Choose a folder including laradock folder", "Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    if (goingChoose == DialogResult.OK)
                        changePath(isFirst);
                    else Environment.Exit(0);
                }

            }
            else
            {
                if (Main.laradock == "0")
                {
                    DialogResult goingChoose = MessageBox.Show("You must choose a folder", "Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    if (goingChoose == DialogResult.OK)
                        changePath(isFirst);
                    else Environment.Exit(0);
                }
            }
        }
        public static void openSite(string url)
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

        public static bool isDockerInstalled()
        {
            string docker = runCmd("docker", "--version",true);

            if (docker.Contains("Docker") && docker.Contains("version") && docker.Contains("build")) return true;
            return false;
        }

    }
}
