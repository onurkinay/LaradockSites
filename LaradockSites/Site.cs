using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaradockSites
{
    public class Site
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Server_Name { get; set; }
        public bool isInLocalhost { get; set; }


        public override string ToString()
        {
            return "Site: " + Name + " (" +Server_Name+ ")";
        }
    }
}
