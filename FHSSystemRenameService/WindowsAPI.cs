using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Management;


namespace FHSSystemRenameService
{
    class WindowsAPI
    {
        #region External API Callsq
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(int uiAction, int uiParam, String pvParam, int fWinIni);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(int uiAction, int uiParam, int pvParam, int fWinIni);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(int uiAction, int uiParam, ref bool pvParam, int fWinIni);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(int uiAction, int uiParam, ref int pvParam, int fWinIni);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool ExitWindowsEx(int uFlags, int dwReason);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern bool MoveFileEx(String lpExistingFileName, String lpNewFileName, int dwFlags);
        #endregion

        #region Background API Call
        // Constants for API call
        private const int SPI_SETDESKWALLPAPER = 0x14;
        private const int SPIF_UPDATEINIFILE = 0x01;
        private const int SPIF_SENDWININICHANGE = 0x02;

        public static void SetWallpaper(String path)
        {
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, path, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }
        #endregion

        #region SetScreenSaveActive
        // Constants for API Call
        private const int SPI_SETSCREENSAVEACTIVE = 0x0011;

        public static void SetScreenSaver(string path, bool value)
        {
            int Active = 0;
            if (value) { Active = 1; }

            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("Control Panel", true);
                key = key.OpenSubKey("desktop", true);
                key.SetValue("SCRNSAVE.EXE", path);
                //oKey.SetValue("ScreenSaveActive", "1");

                //unsafe
                {
                    SystemParametersInfo(SPI_SETSCREENSAVEACTIVE, 0, Active, 0);
                }
            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.ToString());
            }
        }
        #endregion

        #region GetScreenSaveActive
        // Constants for API Call
        private const int SPI_GETSCREENSAVEACTIVE = 0x0010;

        private static bool GetScreenSaverActive()
        {
            bool isActive = false;

            SystemParametersInfo(SPI_GETSCREENSAVEACTIVE, 0, ref isActive, 0);
            return isActive;
        }
        private static String GetScreenSaverPath()
        {
            String path = string.Empty;
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Control Panel", false);
            key = key.OpenSubKey("desktop", false);
            path = key.GetValue("SCRNSAVE.EXE", String.Empty) as String;
            return path;
        }

        #endregion

        #region SetScreenSaverTimout
        // Constants for API Call
        private const int SPI_SETSCREENSAVERTIMEOUT = 0x000F;

        private static void SetScreenSaverTimeout(int time)
        {
            SystemParametersInfo(SPI_SETSCREENSAVERTIMEOUT, time, 0, SPIF_SENDWININICHANGE);
        }
        #endregion

        #region GetScreenSaverTimeout
        // Constants for API Call
        private const int SPI_GETSCREENSAVETIMEOUT = 0x000E;

        private int GetScreenSaverTimeout()
        {
            int timeout = 0;
            SystemParametersInfo(SPI_GETSCREENSAVETIMEOUT, 0, ref timeout, 0);
            return timeout;
        }
        #endregion

        #region SetScreenSaverSecure
        // Constants for API Call
        private const int SPI_SETSCREENSAVESECURE = 0x0077;

        private static void SetScreenSaverSecure(bool value)
        {
            int Secure = 0;
            if (value) { Secure = 1; }

            SystemParametersInfo(SPI_SETSCREENSAVESECURE, Secure, 0, SPIF_SENDWININICHANGE);
        }
        #endregion

        #region GetScreenSaverSecure
        // Constants for API Call
        private const int SPI_GETSCREENSAVESECURE = 0x0076;

        private static bool GetScreenSaverSecure()
        {
            bool Secure = false;
            SystemParametersInfo(SPI_GETSCREENSAVESECURE, 0, ref Secure, 0);
            return Secure;
        }

        #endregion

        #region ForceRestartOfWindows
        // Constants for API Call
        private const int EWX_REBOOT = 0x02;
        private const int EWX_FORCE = 0x04;

        private static bool ForceRestartOfWindows()
        {
            if (!ExitWindowsEx(EWX_REBOOT | EWX_FORCE, 0))
            {
                return false;
            }
            return true;
        }
        #endregion

        #region DeleteFileOnReboot
        // Constants for API Call
        private const int MOVEFILE_DELAY_UNTIL_REBOOT = 0x4;

        private static bool DeleteFileOnReboot(string FileName)
        {
            if (!MoveFileEx(FileName, null, MOVEFILE_DELAY_UNTIL_REBOOT)) 
            { 
                return false; 
            }
            return true;
        }
        #endregion

        #region Execution Functions
        public static bool SetComputerName(String Name)
        {
            ManagementObject ob = new ManagementObject();
            //using (ManagementObject wmiObject = new ManagementObject(new ManagementPath(String.Format("Win32_ComputerSystem.Name='{0}'", System.Environment.MachineName))))
            //{
            //    ManagementBaseObject inputArgs = wmiObject.GetMethodParameters("Rename");
            //    inputArgs["Name"] = Name;

            //    ManagementBaseObject outParams = wmiObject.InvokeMethod("Rename", inputArgs, null);
            //    uint ret = (uint)(outParams.Properties["ReturnValue"].Value);
            //    if (ret == 0)
            //    {
            //        //worked
            //        return true;
            //    }
            //    else
            //    {
            //        //didn't work
            //        return false;
            //    }
            //}
            return false;
        }
        #endregion // Execution Functions
    }
}
