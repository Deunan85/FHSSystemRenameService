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
                string OEMBackground = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Background";
                RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                key = key.CreateSubKey(OEMBackground);
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
                string OEMBackground = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Background";
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
