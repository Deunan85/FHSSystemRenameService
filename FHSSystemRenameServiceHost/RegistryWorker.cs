using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace FHSSystemRenameServiceHost
{
    public class RegistryWorker
    {
        public static void EnableOEMBackground()
        {
            string OEMBackground = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Background";
            RegistryKey key = Registry.LocalMachine.CreateSubKey(OEMBackground);
            key.SetValue("OEMBackground", 1);
            key.Close();
        }
        public static void DisableOEMBackground()
        {
            string OEMBackground = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Background";
            RegistryKey key = Registry.LocalMachine.OpenSubKey(OEMBackground);
            key.SetValue("OEMBackground", 0);
            key.Close();
        }
    }
}
