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
        public static void GetWebImage(String url, String ImagePath)
        {
            try
            {
                Stream _imageStream = new WebClient().OpenRead(url);
                System.Drawing.Image _img = System.Drawing.Image.FromStream(_imageStream);
                _img.Save(ImagePath);
            }
            catch (Exception ex)
            {
                // TODO
                // Add error handling
            }
        }
        public static System.Drawing.Image GetWebImage(String url)
        {
            System.Drawing.Image image = null;
            try
            {
                using (Stream _imageStream = new WebClient().OpenRead(url))
                {
                    image = System.Drawing.Image.FromStream(_imageStream);
                }
            }
            catch (Exception ex)
            {

            }
            return image;
        }
    }
}
