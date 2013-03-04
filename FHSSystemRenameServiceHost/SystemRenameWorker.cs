using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Configuration;

namespace FHSSystemRenameServiceHost
{
    class SystemRenameWorker
    {
        #region Data
        private string LocalIP = NetOps.GetLocalIP();
        private string WorkingDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        private string MyPictures = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        private string Website;
        #endregion

        public void Startup()
        {
            Logging.log.Debug(LocalIP);
            Logging.log.Debug(WorkingDirectory);
            Logging.log.Debug(MyPictures);

            // set web address
            Website = "http://" + "www.barcodesinc.com/generator/image.php?code=" + LocalIP + "&style=197&C128&width=200&height=50&xres=1&font=3";

            // There should only be one file MyPictures
            // If the file exists delete it so all other other files can be moved
            if (File.Exists(MyPictures + "\\" + LocalIP + ".png"))
            {
                File.Delete(MyPictures + "\\" + LocalIP + ".png");
            }
            //MoveAll(MyPictures, WorkingDirectory + "\\" + "Temp Pictures");

            // create the barcode
            //NetOps.GetWebImage(Website, MyPictures + "\\" + LocalIP + ".png");

            // Save Screensaver settings


            // Turn on Screensaver
            WindowsAPI.SetScreenSaver("PhotoScreensaver.scr", true);
        }
        public void CleanUp()
        {
            // reset screensaver settings

            // Delete barcode image
            //File.Delete(MyPictures + "\\" + LocalIP + ".png");

            // Move images back
            //MoveAll(WorkingDirectory + "\\" + "Temp Pictures", MyPictures);

            // Delete holding folder
            //Directory.Delete(WorkingDirectory + "\\" + "Temp Pictures");

            // Remove service from list

            // Flag files for deletion
            //List<string> FileList = Directory.EnumerateFiles(WorkingDirectory).ToList<string>();
            //foreach (string s in FileList)
            //{
            //    WindowsAPI.DeleteFileOnReboot(s);
            //}
            //WindowsAPI.DeleteFileOnReboot(WorkingDirectory);

            // Force system reboot
            //WindowsAPI.ForceRestartOfWindows();
        }

        /// <summary>Process that moves all files and all folders from source to destination
        /// </summary>
        /// <param name="SourceDirectoryName">The Source Directory to move contents</param>
        /// <param name="DestinationDirectoryName">The Destination Directory</param>
        private void MoveAll(string SourceDirectoryName, string DestinationDirectoryName)
        {
            MoveFiles(SourceDirectoryName, DestinationDirectoryName);
            MoveSubdirectories(SourceDirectoryName, DestinationDirectoryName);
        }
        /// <summary>Process that moves all files from source to destination
        /// </summary>
        /// <param name="SourceDirectoryName">The Source Directory to move contents</param>
        /// <param name="DestinationDirectoryName">The Destination Directory</param>
        private void MoveFiles(string SourceDirectoryName, string DestinationDirectoryName)
        {
            Logging.log.Debug(SourceDirectoryName);
            Logging.log.Debug(DestinationDirectoryName);
            string FileName;
            // Check to see if destination directory exist
            if (!Directory.Exists(DestinationDirectoryName))
            {
                Directory.CreateDirectory(DestinationDirectoryName);
            }

            // Get the list of files in SourceDirectory
            List<string> FileList = Directory.EnumerateFiles(SourceDirectoryName).ToList<string>();

            // Move files out
            foreach (string s in FileList)
            {
                FileName = System.IO.Path.GetFileName(s);
                File.Move(s, DestinationDirectoryName + "\\" + FileName);
            }
        }
        /// <summary>Process that moves all folders from source to destination
        /// </summary>
        /// <param name="SourceDirectoryName">The Source Directory to move contents</param>
        /// <param name="DestinationDirectoryName">The Destination Directory</param>
        private void MoveSubdirectories(string SourceDirectoryName, string DestinationDirectoryName)
        {
            Logging.log.Debug(SourceDirectoryName);
            Logging.log.Debug(DestinationDirectoryName);
            string FolderName;
            // Check to see if destination directory exist
            if (!Directory.Exists(DestinationDirectoryName))
            {
                Directory.CreateDirectory(DestinationDirectoryName);
            }
            // Move SubDirectories
            // Get List of Folders in SourceDirectory
            List<string> SubDirectories = Directory.EnumerateDirectories(SourceDirectoryName).ToList<string>();

            // Move Folders
            foreach (string s in SubDirectories)
            {
                FolderName = System.IO.Path.GetFileName(s);
                Directory.Move(s, DestinationDirectoryName + "\\" + FolderName);
            }
        }
        private void SaveScreenSaverSettings()
        {
            // Call Windows API calls
            string ScreenSaverActive = WindowsAPI.GetScreenSaverActive().ToString();
            string ScreenSaverPath = WindowsAPI.GetScreenSaverPath();
            string ScreenSaverTimeout = WindowsAPI.GetScreenSaverTimeout().ToString();
            string ScreenSaverSecure = WindowsAPI.GetScreenSaverSecure().ToString();

            try
            {
                // Open the config
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                // check to see if the settings are current
                foreach (KeyValueConfigurationElement element in config.AppSettings.Settings)
                {
                    switch (element.Key)
                    {
                        case "ScreenSaverActive": element.Value = ScreenSaverActive;
                            break;
                        case "ScreenSaverPath": element.Value = ScreenSaverPath;
                            break;
                        case "ScreenSaverTimeout": element.Value = ScreenSaverTimeout;
                            break;
                        case "ScreenSaverSecure": element.Value = ScreenSaverSecure;
                            break;
                    }
                }
                config.Save();
            }
            catch (Exception ex)
            {
                Logging.log.Debug("An Error occured while trying to save the settings" + ex.Message, ex);
            }
        }
        private void LoadScreenSaverSettings()
        {
            ConfigurationManager.RefreshSection("appSettings");

        }
    }
}
