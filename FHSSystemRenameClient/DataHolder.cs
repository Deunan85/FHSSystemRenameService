using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FHSSystemRenameClient
{
    public class DataHolder
    {
        public string IPAddress {get; set;}
        public string ComputerName { get; set; }
        public bool Rename { get; set; }

        public DataHolder(string IPAddress, string ComputerName, bool Rename)
        {
            this.IPAddress = IPAddress;
            this.ComputerName = ComputerName;
            this.Rename = Rename;
        }
    }
}
