using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace FHSSystemRenameServiceHost
{
    public class ImageProcessing
    {
        public static Image CreateFourCornerBackground(int Height, int Width, System.Drawing.Image Image, string FileName, System.Drawing.Imaging.ImageFormat ImageFormat)
        {
            if (!Directory.Exists(Path.GetDirectoryName(FileName)))
            {
                throw new ArgumentException("The holding directory does not exist", "FileName");
            }
            Image background = CreateFourCornerBackground(Height, Width,Image);
            background.Save(FileName, ImageFormat);
            return background;
        }
        public static Image CreateFourCornerBackground(int Height, int Width, System.Drawing.Image Image)
        {
            if (Image == null)
            {
                throw new ArgumentNullException("Image","The image given is null");
            }
            if (Height < 0)
            {
                throw new ArgumentOutOfRangeException("Height", "The background Height needs to be positive.");
            }
            if (Width < 0)
            {
                throw new ArgumentOutOfRangeException("Width", "The background Width needs to be positive.");
            }
            if (Height < Image.Height*2)
            {
                throw new ArgumentOutOfRangeException("Height", "The Height of the background needs to be larger than the image");
            }
            if (Width < Image.Width*2)
            {
                throw new ArgumentOutOfRangeException("Width","The Width of the background needs to be larger than the image");
            }
            
            int margin = 0;

            // Create pointers for image work
            System.Drawing.Point zero = new Point(0, 0);
            System.Drawing.Point ULCorner = new Point(0, 0);
            System.Drawing.Point URCorner = new Point(Width - Image.Width, 0);
            System.Drawing.Point LLCorner = new Point(0, Height - Image.Height);
            System.Drawing.Point LRCorner = new Point(Width - Image.Width, Height - Image.Height);

            Image background = new Bitmap(Width, Height);
            using (Graphics gfx = Graphics.FromImage(background))
            {
                // Create the brush
                SolidBrush blackBrush = new SolidBrush(Color.Black);
                SolidBrush whiteBrush = new SolidBrush(Color.White);

                // Create Rectangle
                System.Drawing.Rectangle backgroundRect = new Rectangle(0, 0, Width, Height);
                System.Drawing.Rectangle ULRect = new Rectangle(0, 0, Image.Width + margin, Image.Height + margin);
                System.Drawing.Rectangle URRect = new Rectangle(Width - Image.Width - margin, 0, Image.Width + margin, Image.Height + margin);
                System.Drawing.Rectangle LLRect = new Rectangle(0, Height - Image.Height - margin, Image.Width + margin, Image.Height + margin);
                System.Drawing.Rectangle LRRect = new Rectangle(Width - Image.Width - margin, Height - Image.Height - margin, Image.Width + margin, Image.Height + margin);

                gfx.FillRectangle(blackBrush, backgroundRect);
                gfx.FillRectangle(whiteBrush, ULRect);
                gfx.FillRectangle(whiteBrush, URRect);
                gfx.FillRectangle(whiteBrush, LLRect);
                gfx.FillRectangle(whiteBrush, LRRect);

                gfx.DrawImage(Image, zero);
                gfx.DrawImage(Image, ULCorner);
                gfx.DrawImage(Image, URCorner);
                gfx.DrawImage(Image, LLCorner);
                gfx.DrawImage(Image, LRCorner);
            }
            return background;
        }
        public static void CreateBackGround(int height, int width, string FileName, System.Drawing.Imaging.ImageFormat ImageFormat)
        {
            Bitmap background = new Bitmap(width, height);
            using (Graphics gfx = Graphics.FromImage(background))
            {
                // Create the brush
                SolidBrush blackBrush = new SolidBrush(System.Drawing.Color.Black);

                // Create Rectangle
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, width, height);

                gfx.FillRectangle(blackBrush, rect);
            }
            background.Save(FileName, ImageFormat);
        }
        public static System.Drawing.Image AddImageBelowCenter(Image Background, Image OverLay)
        {
            if (Background == null)
            {
                throw new ArgumentNullException("Background", "The background image to alter is null");
            }
            if (OverLay == null)
            {
                throw new ArgumentNullException("Overlay", "The overlay image is null");
            }
            
            int margin = 0;

            using (Graphics gfx = Graphics.FromImage(Background))
            {
                // Create the brush
                SolidBrush WhiteBrush = new SolidBrush(Color.White);

                // Create the Point
                System.Drawing.Point MiddleOffset = new Point(Background.Width / 2 - OverLay.Width / 2, Background.Height - 3 * OverLay.Height);

                // Create Rectanlge
                System.Drawing.Rectangle Border = new Rectangle(Background.Width / 2 - OverLay.Width / 2 - margin, Background.Height - 3 * OverLay.Height - margin, OverLay.Width + margin, OverLay.Height + margin);

                // Draw
                gfx.FillRectangle(WhiteBrush, Border);
                gfx.DrawImageUnscaled(OverLay, MiddleOffset);
            }
            return Background;
        }
        public static System.Drawing.Image AddImageBelowCenter(Image Background, Image OverLay, string FileName, System.Drawing.Imaging.ImageFormat ImageFormat)
        {
            if (!Directory.Exists(Path.GetDirectoryName(FileName)))
            {
                throw new ArgumentException("The holding directory does not exist", "FileName");
            }
            Background = AddImageBelowCenter(Background, OverLay);
            Background.Save(FileName, ImageFormat);
            return Background;
        }
    }
}
