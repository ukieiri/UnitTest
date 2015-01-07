using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GurshchenkovaValette
{
    public interface IControlsManipulation
    {
        bool ConfigureFileManager(string path);
        void LoadPreviewImage(Image image, PictureBox pictureBox);
        bool ApplyFilter(string filter, PictureBox picturebox, TextBox textbox, Image Origin);
        bool SaveImage(Image img, string picturename);
        bool ImportImage(string imageName);
        bool DeletePicture();
        string[] PopulateListBox();
        void setFm(IFilenameManipulation fm);
        bool OpenImage(IFilenameManipulation fm);
        bool SetFilter(string filter, PictureBox picturebox);
    }
}
