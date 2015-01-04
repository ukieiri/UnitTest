using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GurshchenkovaValette
{
    public partial class Form1 : Form
    {
        private Image Origin;
        Bitmap map;
        FilenameManipulation fm;
        ImageManipulation im;
        string picturename = string.Empty;

        public void setOrigin(Image origin) {
            this.Origin = origin;
        }
        public Form1()
        {
            InitializeComponent();
            fm = new FilenameManipulation();
            im = new ImageManipulation(fm);
            DisableControles();
            DisableImportButton();
        }

        private void btLoad_Click(object sender, EventArgs e)
        {
            GetAPath();
        }
        //get a path and load the image
        public bool GetAPath()
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "Image Files (JPG,PNG,GIF)|*.JPG;*.PNG;*.GIF";
                DialogResult dr = op.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    string path = op.FileName;
                    Image img = Image.FromFile(path);
                    PopulatePictureBoxes(img, path);
                    //to free image after reloading
                    img.Dispose();
                }
                op.Dispose();
                return true;
            }
            catch {
                return false;
            }
        }
        public bool PopulatePictureBoxes(Image img, string path)
        {
            try{
                pbMainPicture.Image = img;
                pbOriginal.Image = img;

                //adjust the image
                Bitmap temp = new Bitmap(pbMainPicture.Image);
                pbMainPicture.Image = temp;
                pbOriginal.Image = temp;
                pbMainPicture.SizeMode = PictureBoxSizeMode.Zoom;
                pbOriginal.SizeMode = PictureBoxSizeMode.Zoom;

                //apply the filter and load preview images
                LoadPreviewImage(img, pbMiamiFilter);
                LoadPreviewImage(img, pbNightFilter);
                LoadPreviewImage(img, pbHellFilter);
                LoadPreviewImage(img, pbZenFilter);
                LoadPreviewImage(img, pbBlackAndWhite);
                LoadPreviewImage(img, pbSwapFilter);

                map = new Bitmap(pbMainPicture.Image);
                Origin = pbMainPicture.Image;
                //get the filename
                string filename = path.Split(new char[] { '\\' }).Last();
                string extension = path.Split(new char[] { '.' }).Last();
                picturename = filename.Replace("." + extension, "");
                tbImageName.Text = picturename;

                //get the foldername to populate the listbox
                string foldername = path.Replace(filename, "");
                fm.setFolder(foldername);
                fm.setFileName(filename);
                fm.setFileFilter(string.Empty);
                fm.setFileToken(string.Empty);
                fm.setFormat(string.Empty);
                PopulateListBox();
                EnableControles();
                return true;
            }
            catch {
                return false;
            }
        }

        public void LoadPreviewImage(Image image, PictureBox pictureBox)
        {
            pictureBox.Image = image;
            Bitmap temp = new Bitmap(pictureBox.Image);
            pictureBox.Image = temp;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            switch (pictureBox.Name)
            {
                case "pbMiamiFilter":
                    pictureBox.Image = ImageFilters.ApplyFilter(new Bitmap(pictureBox.Image), 1, 1, 10, 1); break;
                case "pbNightFilter": pictureBox.Image = ImageFilters.ApplyFilter(new Bitmap(pictureBox.Image), 1, 1, 1, 25); break;
                case "pbHellFilter": pictureBox.Image = ImageFilters.ApplyFilter(new Bitmap(pictureBox.Image), 1, 1, 10, 15); break;
                case "pbZenFilter": pictureBox.Image = ImageFilters.ApplyFilter(new Bitmap(pictureBox.Image), 1, 10, 1, 1); break;
                case "pbBlackAndWhite": pictureBox.Image = ImageFilters.BlackWhite(new Bitmap(pictureBox.Image)); break;
                case "pbSwapFilter": pictureBox.Image = ImageFilters.ApplyFilterSwap(new Bitmap(pictureBox.Image)); break;
                default: break;
            }
        }

        public void PopulateListBox() {
            //populate the listbox with images from chosen folder
            var files = fm.getFileNames();
            lbImages.DataSource = files;
        }
        

        public bool ApplyFilter(string filter) {
            try
            {
                pbMainPicture.Image = Origin;
                switch (filter)
                {
                    case "MiamiFilter":
                        pbMainPicture.Image = ImageFilters.ApplyFilter(new Bitmap(pbMainPicture.Image), 1, 1, 10, 1); break;
                    case "NightFilter": pbMainPicture.Image = ImageFilters.ApplyFilter(new Bitmap(pbMainPicture.Image), 1, 1, 1, 25); break;
                    case "HellFilter": pbMainPicture.Image = ImageFilters.ApplyFilter(new Bitmap(pbMainPicture.Image), 1, 1, 10, 15); break;
                    case "ZenFilter": pbMainPicture.Image = ImageFilters.ApplyFilter(new Bitmap(pbMainPicture.Image), 1, 10, 1, 1); break;
                    case "BlackAndWhite": pbMainPicture.Image = ImageFilters.BlackWhite(new Bitmap(pbMainPicture.Image)); break;
                    case "SwapFilter": pbMainPicture.Image = ImageFilters.ApplyFilterSwap(new Bitmap(pbMainPicture.Image)); break;
                    default: return false;
                }
                return true;
            }
            catch {
                return false;
            }
        }

        private void btMiamiFilter_Click(object sender, EventArgs e)
        {
            ApplyFilter("MiamiFilter");
        }

        private void pbMiamiFilter_Click(object sender, EventArgs e)
        {
            ApplyFilter("MiamiFilter");
        }

        private void btNightFilter_Click(object sender, EventArgs e)
        {
            ApplyFilter("NightFilter");
        }

        private void pbNightFilter_Click(object sender, EventArgs e)
        {
            ApplyFilter("NightFilter");
        }
        
        private void btHellFilter_Click(object sender, EventArgs e)
        {
            ApplyFilter("HellFilter");
        }

        private void pbHellFilter_Click(object sender, EventArgs e)
        {
            ApplyFilter("HellFilter");
        }

        private void btZenFilter_Click(object sender, EventArgs e)
        {
            ApplyFilter("ZenFilter");
        }

        private void pbZenFilter_Click(object sender, EventArgs e)
        {
            ApplyFilter("ZenFilter");
        }

        private void btBlackAndWhite_Click(object sender, EventArgs e)
        {
            ApplyFilter("BlackAndWhite");
        }

        private void pbBlackAndWhite_Click(object sender, EventArgs e)
        {
            ApplyFilter("BlackAndWhite");
        }

        private void btSwapFilter_Click(object sender, EventArgs e)
        {
            ApplyFilter("SwapFilter");
        }

        private void pbSwapFilter_Click(object sender, EventArgs e)
        {
            ApplyFilter("SwapFilter");
        }
        
        private void btOriginal_Click(object sender, EventArgs e)
        {
            pbMainPicture.Image = Origin;
        }

        private void pbOriginal_Click(object sender, EventArgs e)
        {
            pbMainPicture.Image = Origin;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            SaveImage();
        }
        public bool SaveImage()
        {
            try
            {
                Image img = pbMainPicture.Image;
                //give a unique name to image
                var dt = DateTime.Now;
                var ticks = dt.Ticks;
                var seconds = ticks / TimeSpan.TicksPerSecond;
                fm.setFileName(tbImageName.Text + seconds);
                im.save(img);
                PopulateListBox();
                MessageBox.Show("Saved successfully");
                return true;
            }
            catch {
                return false;
            }
        }

        private void btImport_Click(object sender, EventArgs e)
        {
            if (lbImages.SelectedItem != null)
            {
                //get the file name from listbox, exclude the extention
                string fileName = lbImages.SelectedItem.ToString();
                string extension = fileName.Split(new char[] { '.' }).Last();
                fileName = fileName.Replace("." + extension, "");
                fm.setFileName(fileName);
               // fm.setFormat("." + extension);
                fm.setFileFilter(string.Empty);
                fm.setFileToken(string.Empty);
                fm.setFormat(string.Empty);

                //open selected image and load it into picture  boxes
                im = new ImageManipulation(fm);
                var image = im.openImage();                
                PopulatePictureBoxes(image, fm.getFullPath());
                image.Dispose();
            }
        }

        private void bnDelete_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure you want to delete this picture? It will be deleted permanently from your computer",
                                     "Delete",
                                     MessageBoxButtons.YesNo);
           
                if (confirmResult == DialogResult.Yes)
                {
                    DeletePicture();
                }
            
        }
        public bool DeletePicture() {
        try{
            var deleted = im.remove();
            if (deleted == true)
            {
                RestoreTheOriginalForm();
                MessageBox.Show("Deleted successfully");
                DisableControles();
                return true;
            }
            else
            {
                MessageBox.Show("Deleting is not possible");
                return false;
            }
            
        }
        catch
        {
            return false;
        }
        }
        public void RestoreTheOriginalForm() {
            pbOriginal.Image = null;
            pbMainPicture.Image = null;
            pbMiamiFilter.Image = null;
            pbNightFilter.Image = null;
            pbHellFilter.Image = null;
            pbZenFilter.Image = null;
            pbBlackAndWhite.Image = null;
            pbSwapFilter.Image = null;
            tbImageName.Text = null;
            PopulateListBox();
        }
        public void DisableControles() {
            pbOriginal.Enabled = false;
            btOriginal.Enabled = false;
            pbMiamiFilter.Enabled = false;
            btMiamiFilter.Enabled = false;
            pbNightFilter.Enabled = false;
            btNightFilter.Enabled = false;
            pbHellFilter.Enabled = false;
            btHellFilter.Enabled = false;
            pbZenFilter.Enabled = false;
            btZenFilter.Enabled = false;
            pbBlackAndWhite.Enabled = false;
            btBlackAndWhite.Enabled = false;
            pbSwapFilter.Enabled = false;
            btSwapFilter.Enabled = false;
            btSave.Enabled = false;
            bnDelete.Enabled = false;
        }
        //has to be handle separatly as after deleting there is no need to disable it
        public void DisableImportButton() {
            btImport.Enabled = false;
        }
        public void EnableControles()
        {
            pbOriginal.Enabled = true;
            btOriginal.Enabled = true;
            pbMiamiFilter.Enabled = true;
            btMiamiFilter.Enabled = true;
            pbNightFilter.Enabled = true;
            btNightFilter.Enabled = true;
            pbHellFilter.Enabled = true;
            btHellFilter.Enabled = true;
            pbZenFilter.Enabled = true;
            btZenFilter.Enabled = true;
            pbBlackAndWhite.Enabled = true;
            btBlackAndWhite.Enabled = true;
            pbSwapFilter.Enabled = true;
            btSwapFilter.Enabled = true;
            btSave.Enabled = true;
            bnDelete.Enabled = true;
            btImport.Enabled = true;
        }
    }
}
