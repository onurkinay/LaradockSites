using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaradockSites
{
    public partial class AddSite : Form
    {
        string path;
        public AddSite(string path)
        {
            InitializeComponent();
            this.path = path;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string ExamplePath = Path.Combine(path, "app.conf.example");
            using (StreamWriter sw = File.CreateText(Path.Combine(path, tbKlasorAdi.Text + ".conf")))
            {
                foreach (string line in File.ReadLines(ExamplePath))
                {
                    string setLine = line;
                    if (line.Contains("server_name app.test;"))
                    {
                        setLine = setLine.Replace("app.test", tbUrl.Text);
                    }
                    else if (line.Contains("root /var/www/app;"))
                    {
                        setLine = setLine.Replace("app", tbKlasorAdi.Text);
                    }
                    else if (line.Contains("error_log /var/log/nginx/app_error.log;"))
                    {
                        setLine = setLine.Replace("app_error", tbKlasorAdi.Text.ToLower() + "_error");
                    }
                    else if (line.Contains("access_log /var/log/nginx/app_access.log;"))
                    {
                        setLine = setLine.Replace("app_access", tbKlasorAdi.Text.ToLower() + "_access");
                    }

                    sw.WriteLine(setLine);
                }
            }

            string texttowrite = "127.0.0.1 "+tbUrl.Text;
            string text = File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "drivers/etc/hosts"), Encoding.UTF8);
            if (!text.Contains(texttowrite))
            {
                using (StreamWriter w = File.AppendText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "drivers/etc/hosts")))
                {
                    w.WriteLine(texttowrite);
                }
            }
            Directory.CreateDirectory(Path.Combine(path.Replace(@"laradock\nginx\sites", ""), tbKlasorAdi.Text));
            Funcs.RestartNginx(path);
         

            this.Close();

        }

        private void AddSite_Load(object sender, EventArgs e)
        {

        }
    }
}
