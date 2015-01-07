using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GurshchenkovaValette
{
    public class ControlsManipulation : IControlsManipulation
    {
        IImageFilters imgf;
        IFilenameManipulation fm;
        IimageManipulation _im;

        public void setFm(IFilenameManipulation fm)
        {
            this.fm = fm;
        }
        public ControlsManipulation()
        {
            imgf = new ImageFilters();
            fm = new FilenameManipulation();
            _im = new ImageManipulation(fm);
        }
        public ControlsManipulation(IimageManipulation im)
        {
            imgf = new ImageFilters();
            _im = im;
        }

        public bool ConfigureFileManager(string path) {
            //get the filename
            string filename = path.Split(new char[] { '\\' }).Last();
            string extension = path.Split(new char[] { '.' }).Last();
            string name = filename.Replace("." + extension, "");

            //get the foldername to populate the listbox
            string foldername = path.Replace(filename, "");
            fm.setFolder(foldername);
            fm.setFileName(name);
            fm.setFileFilter(string.Empty);
            fm.setFileToken(string.Empty);
            fm.setFormat("." + extension);
            return true;
        }
        public bool SaveImage(Image img, string picturename)
        {
            //give a unique name to image
            var dt = DateTime.Now;
            var ticks = dt.Ticks;
            var seconds = ticks / TimeSpan.TicksPerSecond;
            fm.setFileName(picturename + seconds);
            var saved = _im.save(img);
            return saved;
        }

        public void LoadPreviewImage(Image image,PictureBox pictureBox)
        {
                pictureBox.Image = image;
                Bitmap temp = new Bitmap(pictureBox.Image);
                pictureBox.Image = temp;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                SetFilter(pictureBox.Name, pictureBox);
        }

        public bool SetFilter(string filter, PictureBox picturebox) {
            Bitmap bitmap = new Bitmap(picturebox.Image);
            switch (filter)
            {
                case "pbMiamiFilter":
                    picturebox.Image = imgf.ApplyFilter(bitmap, 1, 1, 10, 1); break;
                case "pbNightFilter": picturebox.Image = imgf.ApplyFilter(bitmap, 1, 1, 1, 25); break;
                case "pbHellFilter": picturebox.Image = imgf.ApplyFilter(bitmap, 1, 1, 10, 15); break;
                case "pbZenFilter": picturebox.Image = imgf.ApplyFilter(bitmap, 1, 10, 1, 1); break;
                case "pbBlackAndWhite": picturebox.Image = imgf.BlackWhite(bitmap); break;
                case "pbSwapFilter": picturebox.Image = imgf.ApplyFilterSwap(bitmap); break;          
            }
            return true;
        }

        public bool ApplyFilter(string filter, PictureBox picturebox, TextBox textbox, Image Origin)
        {
            try
            {
                picturebox.Image = Origin;
                SetFilter(filter, picturebox);                
                string filename = fm.getFileName();
                textbox.Text = filename + "_" + filter;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ImportImage(string imageName)
        {
            //get the file name from listbox, exclude the extention
            string fileName = imageName;
            string extension = fileName.Split(new char[] { '.' }).Last();
            fileName = fileName.Replace("." + extension, "");
            fm.setFileName(fileName);
            fm.setFileFilter(string.Empty);
            fm.setFileToken(string.Empty);
            fm.setFormat("." +extension);
            OpenImage(fm);
            return true;
        }
        public bool OpenImage(IFilenameManipulation fm)
        {
            try
            {
                //open selected image and load it into picture  boxes
                _im = new ImageManipulation(fm);
                var image = _im.openImage();
                Form1._Form1.PopulatePictureBoxes(image, fm.getFileName());
                image.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeletePicture()
        {
            return _im.remove();    
        }

        public string[] PopulateListBox()
        {
            //populate the listbox with images from chosen folder
            var files = fm.getFileNames();
            return files;
        }
    }
}
