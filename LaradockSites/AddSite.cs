using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaradockSites
{
    public partial class AddSite : Form
    {
        string path;
        Status status;
        public AddSite(string path)
        {
            InitializeComponent();
            this.path = path;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
           
            Site site = new Site()
            {
                Name = tbSiteAdi.Text,
                Server_Name = tbUrl.Text,
                Location = tbKlasorAdi.Text
            };
            string? package = cbPackage.SelectedItem.ToString();
            if (Directory.Exists(Path.Combine(path.Replace(@"laradock\nginx\sites", ""), site.Location)))
            {
                MessageBox.Show("Directory already exists", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            status = Funcs.showStatus(this, "Site is creating. Please wait");
            Task.Run(() =>
            {

                string ExamplePath = Path.Combine(path, "app.conf.example");
                using (StreamWriter sw = File.CreateText(Path.Combine(path, site.Server_Name.Split(".")[0].ToLower() + ".conf")))
                {
                    foreach (string line in File.ReadLines(ExamplePath))
                    {
                        string setLine = line;
                        if (line.Contains("server_name app.test;"))
                        {
                            setLine = setLine.Replace("app.test", site.Server_Name);
                        }
                        else if (line.Contains("root /var/www/app;"))
                        {
                            if (package == "Laravel")
                            {
                                setLine = setLine.Replace("app", site.Location + "/public");
                            }
                            else
                            {
                                setLine = setLine.Replace("app", site.Location);
                            }

                        }
                        else if (line.Contains("error_log /var/log/nginx/app_error.log;"))
                        {
                            setLine = setLine.Replace("app_error", site.Server_Name.Split(".")[0].ToLower() + "_error");
                        }
                        else if (line.Contains("access_log /var/log/nginx/app_access.log;"))
                        {
                            setLine = setLine.Replace("app_access", site.Server_Name.Split(".")[0].ToLower() + "_access");
                        }

                        sw.WriteLine(setLine);
                    }
                }

                string texttowrite = "127.0.0.1 " + site.Server_Name;
                string text = File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "drivers/etc/hosts"), Encoding.UTF8);
                if (!text.Contains(texttowrite))
                {
                    using (StreamWriter w = File.AppendText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "drivers/etc/hosts")))
                    {
                        w.WriteLine(texttowrite);
                    }
                }
                Directory.CreateDirectory(Path.Combine(path.Replace(@"laradock\nginx\sites", ""), site.Location));
                if (package == null)
                {
                    File.Create(Path.Combine(path.Replace(@"laradock\nginx\sites", ""), site.Location, "index.html")).Close();
                }
                else if (package == "Laravel")
                {
                    using (var client = new WebClient())
                    {
                        client.DownloadFile("https://github.com/laravel/laravel/archive/refs/tags/v8.6.10.zip", "laravel.zip");
                    }

                    if (Directory.Exists("cont")) Directory.Delete("cont", true);

                    System.IO.Compression.ZipFile.ExtractToDirectory("laravel.zip", "cont");

                    Funcs.CopyDirectory(@"cont\laravel-8.6.10", Path.Combine(path.Replace(@"laradock\nginx\sites", ""), site.Location.Replace("public", "")), true);
                    Directory.Delete("cont", true);
                }
                else if (package == "Wordpress")
                {
                    using (var client = new WebClient())
                    {
                        client.DownloadFile("https://wordpress.org/latest.zip", "wordpress.zip");
                    }

                    if (Directory.Exists("cont")) Directory.Delete("cont", true);

                    System.IO.Compression.ZipFile.ExtractToDirectory("wordpress.zip", "cont");
                    Funcs.CopyDirectory(@"cont\wordpress", Path.Combine(path.Replace(@"laradock\nginx\sites", ""), site.Location), true);
                    Directory.Delete("cont", true);
                }

                Funcs.RestartNginx();
            }).ContinueWith(t =>
            {
                this.Invoke((MethodInvoker)delegate
                { 
                    status.Close();
                    this.Close();

                });
            });


        }

        private void AddSite_Load(object sender, EventArgs e)
        {

        }
    }
}
