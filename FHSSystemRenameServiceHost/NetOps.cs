using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace FHSSystemRenameServiceHost
{
    public class NetOps
    {
        public static string GetLocalIP()
        {
            IPHostEntry host;
            string localIP = string.Empty;
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }
        public static System.Drawing.Image GetWebImage(String url, String ImagePath)
        {
            Logging.log.Debug("Entered Get image and Save it function");
            System.Drawing.Image image = null;
            try
            {
                using (Stream _imageStream = new WebClient().OpenRead(url))
                {
                    Logging.log.Debug("Stream established.  Can Read: " + _imageStream.CanRead);
                    image = System.Drawing.Image.FromStream(_imageStream);
                    Logging.log.Debug("Image Size: " + image.Width + "X" + image.Height);
                    image.Save(ImagePath);   
                }
            }
            catch (Exception ex)
            {
                // TODO
                Logging.log.Error("Error getting Image from web: " + ex.Message);
            }
            return image;
        }
        public static System.Drawing.Image GetWebImage(String url)
        {
            Logging.log.Debug("Entered Get image function");
            System.Drawing.Image image = null;
            try
            {
                using (Stream _imageStream = new WebClient().OpenRead(url))
                {
                    Logging.log.Debug("Stream established.  Can Read: " + _imageStream.CanRead);
                    image = System.Drawing.Image.FromStream(_imageStream);
                    Logging.log.Debug("Image Size: " + image.Width + "X" + image.Height);
                }
            }
            catch (Exception ex)
            {
                Logging.log.Error("Error getting Image from web: " + ex.Message);
            }
            return image;
        }
    }
}
