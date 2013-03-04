using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace FHSSystemRenameService
{
    class ComputerRename
    {
        public static uint SetComputerName(String Name)
        {
            uint ret;
            ManagementObject ob = new ManagementObject();
            using (ManagementObject wmiObject = new ManagementObject(new ManagementPath(String.Format("Win32_ComputerSystem.Name='{0}'", System.Environment.MachineName))))
            {
                ManagementBaseObject inputArgs = wmiObject.GetMethodParameters("Rename");
                inputArgs["Name"] = Name;

                ManagementBaseObject outParams = wmiObject.InvokeMethod("Rename", inputArgs, null);
                ret = (uint)(outParams.Properties["ReturnValue"].Value);
            }
            return ret;
        }
    }
}
