using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UI.WebCam
{
    class Helper
    {
        public static void SaveImageCapture(System.Drawing.Image image)
        {

            SaveFileDialog s = new SaveFileDialog();
            s.FileName = "Image";// Default file name
            s.DefaultExt = ".Jpg";// Default file extension
            s.Filter = "Image (.jpg)|*.jpg"; // Filter files by extension
            s.RestoreDirectory = true;
            s.InitialDirectory = Path.Combine(Application.StartupPath, @"VisitorImages");

            // Show save file dialog box
            // Process save file dialog box results
            if (s.ShowDialog() == DialogResult.OK)
            {
                // Save Image
                string filename = s.FileName;
                FileStream fstream = new FileStream(filename, FileMode.Create);
                image.Save(fstream, System.Drawing.Imaging.ImageFormat.Jpeg);
                fstream.Close();
            }
        }

        public static void SaveImageCapture(System.Drawing.Image image, string fileName)
        {

            //SaveFileDialog s = new SaveFileDialog();
            //s.FileName = "Image";// Default file name
            //s.DefaultExt = ".Jpg";// Default file extension
            //s.Filter = "Image (.jpg)|*.jpg"; // Filter files by extension
            //s.RestoreDirectory = true;
            //s.InitialDirectory = Path.Combine(Application.StartupPath, @"VisitorImages");

            // Show save file dialog box
            // Process save file dialog box results
            //if (s.ShowDialog() == DialogResult.OK)
            //{
                // Save Image
                //string filename = s.FileName;
            
                FileStream fstream = new FileStream(fileName, FileMode.Create);
                image.Save(fstream, System.Drawing.Imaging.ImageFormat.Jpeg);
                fstream.Close();
            //}
        }
    }
}
