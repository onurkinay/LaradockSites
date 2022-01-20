using System;
using System.Collections.Generic;
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
       * --add index html when added site 
       * 
       * */

        public static List<Site> getSites()
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

        public static bool removeSite(Site site)
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

        public static bool ExistsHosts(string site)
        {
            foreach (string line in File.ReadLines(@"C:\Windows\System32\drivers\etc\hosts"))
            {
                if (line.Contains("127.0.0.1") && line.Contains(site)) return true;
            }
            return false;
        }
        public static void RestartNginx()
        {
            if (Main.isLDRunning())
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

        public static void installLaradock()
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
    }
}
