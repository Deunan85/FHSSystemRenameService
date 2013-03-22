using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Configuration;
using System.Drawing;
using System.Configuration.Install;


namespace FHSSystemRenameServiceHost
{
    class SystemRenameWorker
    {
        #region Data
        private string LocalIP = NetOps.GetLocalIP();
        private string WorkingDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        private string OEMBackgroundDirectory = @"%windir%\Sysnative\oobe\info\backgrounds";
        private string InfoDirectory = @"%windir%\Sysnative\oobe\info";
        private string Website;
        #endregion

        public void Startup()
        {
            Logging.log.Debug(LocalIP);
            Logging.log.Debug(WorkingDirectory);
            Logging.log.Debug(OEMBackgroundDirectory);
            Logging.log.Debug(InfoDirectory);
            //Logging.log.Debug(MyPictures);

            // resolve enviroment variables
            OEMBackgroundDirectory = Environment.ExpandEnvironmentVariables(OEMBackgroundDirectory);
            InfoDirectory = Environment.ExpandEnvironmentVariables(InfoDirectory);
            Logging.log.Debug("Expand vaiables");
            Logging.log.Debug(OEMBackgroundDirectory);
            Logging.log.Debug(InfoDirectory);

            // set web address
            Website = "http://" + "www.barcodesinc.com/generator/image.php?code=" + LocalIP + "&style=197&type=C128B&width=220&height=50&xres=1&font=3";

            // create the barcode
            Logging.log.Debug("Getting Image");
            System.Drawing.Image barcode = NetOps.GetWebImage(Website, WorkingDirectory + "\\barcode.jpg");
            Logging.log.Debug("Got the Image");

            // Get Screen size
            int height = (int)System.Windows.SystemParameters.PrimaryScreenHeight;
            int width = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
            Logging.log.Debug("Screen Size: " + width + "X" + height);

            // Create new background
            Logging.log.Debug("Creating the background image");
            Image background = ImageProcessing.CreateFourCornerBackground(height, width, barcode, WorkingDirectory + "\\FourCorners.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            Logging.log.Debug("Background image created");

            // Add Overlay
            Logging.log.Debug("Adding the Overlay");
            ImageProcessing.AddImageBelowCenter(background, barcode, WorkingDirectory + "\\backgroundDefault.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            // Create the directory to support the OEMbackground
            Logging.log.Debug("Create the OEM Directory");
            Directory.CreateDirectory(OEMBackgroundDirectory);

            // Copy the OEM Background to the new OEM directory
            Logging.log.Debug("Move the OEM background file over to the new directory");
            File.Copy(WorkingDirectory + "\\backgroundDefault.jpg", OEMBackgroundDirectory + "\\backgroundDefault.jpg", true);

            // Enable OEMBackground
            Logging.log.Debug("Enabling the OEM Background");
            RegistryWorker.EnableOEMBackground();
        }
        public void CleanUp()
        {
            // Disable OEMBackground
            Logging.log.Debug("Disabling the OEM Background");
            RegistryWorker.DisableOEMBackground();
            Logging.log.Debug("OEM Background disabled");

            // Uninstall the existing service
            Logging.log.Debug("Uninstalling the service");
            ManagedInstallerClass.InstallHelper(new string[] { "/u", Assembly.GetExecutingAssembly().Location });

            // Flag files for deletion
            List<string> FileList = Directory.EnumerateFiles(WorkingDirectory).ToList<string>();
            foreach (string s in FileList)
            {
                WindowsAPI.DeleteFileOnReboot(s);
            }
            WindowsAPI.DeleteFileOnReboot(WorkingDirectory);

            FileList = Directory.EnumerateFiles(OEMBackgroundDirectory).ToList<string>();
            foreach (string s in FileList)
            {
                WindowsAPI.DeleteFileOnReboot(s);
            }
            WindowsAPI.DeleteFileOnReboot(OEMBackgroundDirectory);
            WindowsAPI.DeleteFileOnReboot(InfoDirectory);

            
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
