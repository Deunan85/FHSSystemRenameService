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
        private void MoveFiles(string SourceDirectoryName, string DestinationDirectoryName)
        {
            string FileName;
            string FolderName;

            // Check to see if destination directory exist
            if (!Directory.Exists(DestinationDirectoryName))
            {
                Directory.CreateDirectory(DestinationDirectoryName);
            }

            // Move SubDirectories

        }
    }
}
