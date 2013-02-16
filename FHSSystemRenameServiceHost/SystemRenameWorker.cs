using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using FHSSystemRenameService;

namespace FHSSystemRenameServiceHost
{
    class SystemRenameWorker
    {
        public void Startup()
        {
            //// Get the list of files in MyPictures
            //List<string> FileList = Directory.EnumerateFiles(Environment.GetFolderPath(
            //    Environment.SpecialFolder.MyPictures)).ToList<string>();

            //// Check to see if barcode image is present
            //// image will be at ..\MyPictures\{localIP}.png
            //if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) +
            //    "\\" + NetOps.GetLocalIP() + ".png"))
            //{
            //    NetOps.GetWebImage("website", 
            //        Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) +
            //    "\\" + NetOps.GetLocalIP() + ".png");
            //}
        }
        public void End()
        {

        }
        public void Run()
        {

        }

        /// <summary>Rename process that moves all files and all folders from source to destination
        /// </summary>
        /// <param name="SourceDirectoryName">The Source Directory to move contents</param>
        /// <param name="DestinationDirectoryName">The Destination Directory</param>
        private void MoveFiles(string SourceDirectoryName, string DestinationDirectoryName)
        {
            string FileName;
            string FolderName;

            // Check to see if destination directory exist
            if (!Directory.Exists(DestinationDirectoryName))
            {
                Directory.CreateDirectory(DestinationDirectoryName);
            }

            // Get the list of files in MyPictures
            List<string> FileList = Directory.EnumerateFiles(SourceDirectoryName).ToList<string>();

            // Move files out
            foreach (string s in FileList)
            {
                FileName = System.IO.Path.GetFileName(s);
                File.Move(s, DestinationDirectoryName + "\\" + FileName);
            }
            // Move SubDirectories
            // Get List of Folders in MyPictures
            List<string> SubDirectories = Directory.EnumerateDirectories(SourceDirectoryName).ToList<string>();

            // Move Folders
            foreach (string s in SubDirectories)
            {
                FolderName = System.IO.Path.GetFileName(s);
                Directory.Move(s, DestinationDirectoryName + "\\" + FolderName);
            }
        }
    }
}
