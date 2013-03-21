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
            try
            {
                string OEMBackground = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Background";
                RegistryKey key = Registry.LocalMachine.CreateSubKey(OEMBackground);
                key.SetValue("OEMBackground", 1);
                key.Close();
            }
            catch (Exception ex)
            {
                Logging.log.Error("Unable to enable OEMBackground: " + ex.Message);
            }
        }
        public static void DisableOEMBackground()
        {
            try
            {
                string OEMBackground = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Background";
                RegistryKey key = Registry.LocalMachine.OpenSubKey(OEMBackground);
                key.SetValue("OEMBackground", 0);
                key.Close();
            }
            catch (Exception ex)
            {
                Logging.log.Error("Unable to disable OEMBackground: " + ex.Message);
            }
        }
    }
}
