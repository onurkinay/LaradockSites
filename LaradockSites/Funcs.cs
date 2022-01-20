using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaradockSites
{
    internal class Funcs
    {

        public static List<Site> getSites(string dic)
        {
            List<Site> list = new();

            foreach (string path in Directory.GetFiles(dic+ @"laradock\nginx\sites"))
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
            return list;
        }

        public static bool removeSite(string dic ,Site site)
        {

            if(File.Exists(Path.Combine(dic + @"laradock\nginx\sites", site.Name + ".conf")))
            {
                File.Delete(Path.Combine(dic + @"laradock\nginx\sites", site.Name + ".conf"));
            }



            string toRemoveLine = "127.0.0.1 " + site.Server_Name;
            string text = File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "drivers/etc/hosts"), Encoding.UTF8);

            text = text.Replace(toRemoveLine, string.Empty);
            File.WriteAllLines(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "drivers/etc/hosts"), new[] { text });
            RestartNginx(dic + @"laradock");
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
        public static void RestartNginx(string path)
        {
            if (Form1.isLDRunning())
            {   // Start the child process.
                Status status = new("NGINX is restarting. Please wait...");
                status.Show();
                Process p = new Process();
                p.StartInfo.WorkingDirectory = path.Replace(@"nginx\sites", "");
                p.StartInfo.FileName = "docker-compose";
                p.StartInfo.Arguments = "restart nginx";
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.WaitForExit();
                status.Close();
            }
        }
    }
}
