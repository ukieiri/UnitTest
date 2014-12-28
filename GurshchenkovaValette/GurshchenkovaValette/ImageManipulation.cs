using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace GurshchenkovaValette
{
    public class ImageManipulation : IimageManipulation
    {
        IFilenameManipulation _filename;

        // initializes properties
        public ImageManipulation(IFilenameManipulation filename) {
            _filename = filename;
        }

        // save an image on the disc
        public Boolean save(Image img)
        {
            // get the path if exists
            String path = _filename.getFullPath();
            if (path == null)
                return false;

            // choose the right format
           ImageFormat format = null;

            if (_filename.getFormat() == ".png")
                format = ImageFormat.Png;
            else
                format = ImageFormat.Jpeg;

            // save the image in the right place, at the right format
            try
            {
                System.Diagnostics.Trace.WriteLine(path);
                img.Save(path, format);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // remove an image from the disc
        public Boolean remove()
        {
            try
            {
                File.Delete( _filename.getFullPath() );
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // open an image from the disc
        public Image openImage()
        {
            // check the file exists
            String filePath = _filename.getFullPath();
            if (!File.Exists(filePath))
                return null;

            try
            {
                Image img = Image.FromFile(filePath);
                return img;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
