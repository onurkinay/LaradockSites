using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaradockSites
{
    public partial class Status : Form
    {
        public Status(string status)
        {
            InitializeComponent();
          
            lbStatus.Text = status;
        }

        private void lbStatus_Click(object sender, EventArgs e)
        {

        }
    }
}
