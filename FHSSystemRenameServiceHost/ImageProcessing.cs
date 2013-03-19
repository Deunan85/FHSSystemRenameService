using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FHSSystemRenameServiceHost
{
    public class ImageProcessing
    {
        public static void CreateFourCornerBackground(int height, int width, System.Drawing.Image barcode, string FileName, System.Drawing.Imaging.ImageFormat imageFormat)
        {
            int margin = 5;

            // Create pointers for image work
            System.Drawing.Point zero = new Point(0, 0);
            System.Drawing.Point ULCorner = new Point(0, 0);
            System.Drawing.Point URCorner = new Point(width - barcode.Width, 0);
            System.Drawing.Point LLCorner = new Point(0, height - barcode.Height);
            System.Drawing.Point LRCorner = new Point(width - barcode.Width, height - barcode.Height);

            Bitmap background = new Bitmap(width, height);
            using (Graphics gfx = Graphics.FromImage(background))
            {
                // Create the brush
                SolidBrush blackBrush = new SolidBrush(Color.Black);
                SolidBrush whiteBrush = new SolidBrush(Color.White);

                // Create Rectangle
                System.Drawing.Rectangle backgroundRect = new Rectangle(0, 0, width, height);
                System.Drawing.Rectangle ULRect = new Rectangle(0, 0, barcode.Width + margin, barcode.Height + margin);
                System.Drawing.Rectangle URRect = new Rectangle(width - barcode.Width - margin, 0, barcode.Width + margin, barcode.Height + margin);
                System.Drawing.Rectangle LLRect = new Rectangle(0, height - barcode.Height - margin, barcode.Width + margin, barcode.Height + margin);
                System.Drawing.Rectangle LRRect = new Rectangle(width - barcode.Width - margin, height - barcode.Height - margin, barcode.Width + margin, barcode.Height + margin);

                gfx.FillRectangle(blackBrush, backgroundRect);
                gfx.FillRectangle(whiteBrush, ULRect);
                gfx.FillRectangle(whiteBrush, URRect);
                gfx.FillRectangle(whiteBrush, LLRect);
                gfx.FillRectangle(whiteBrush, LRRect);

                gfx.DrawImage(barcode, zero);
                gfx.DrawImage(barcode, ULCorner);
                gfx.DrawImage(barcode, URCorner);
                gfx.DrawImage(barcode, LLCorner);
                gfx.DrawImage(barcode, LRCorner);
            }
            background.Save(FileName, imageFormat);
        }
        public static void CreateBackGround(int height, int width, string FileName, System.Drawing.Imaging.ImageFormat imageFormat)
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
            background.Save(FileName, imageFormat);
        }
    }
}
