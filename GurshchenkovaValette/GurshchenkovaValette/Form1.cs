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
        System.Drawing.Image Origin;
        Bitmap map;

        public Form1()
        {
            InitializeComponent();
        }

        private void btLoad_Click(object sender, EventArgs e)
        {
            GetAPath();
        }
        public void GetAPath()
        {
            OpenFileDialog op = new OpenFileDialog();
            DialogResult dr = op.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string path = op.FileName;

                Image img = Image.FromFile(path);

                pbMainPicture.Load(path);

                Bitmap temp = new Bitmap(pbMainPicture.Image);
                pbMainPicture.Image = temp;
                pbMainPicture.SizeMode = PictureBoxSizeMode.Zoom;

                LoadPreviewImage(path, pbMiamiFilter);
                LoadPreviewImage(path, pbNightFilter);
                LoadPreviewImage(path, pbHellFilter);
                LoadPreviewImage(path, pbZenFilter);
                LoadPreviewImage(path, pbBlackAndWhite);
                LoadPreviewImage(path, pbSwapFilter);

                map = new Bitmap(pbMainPicture.Image);
                Origin = pbMainPicture.Image;

                string filename = path.Split(new char[] { '\\' }).Last();
                tbImageName.Text = filename;
            }
        }
        public void LoadPreviewImage(string path, PictureBox pictureBox) 
        {
            pictureBox.Load(path);
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

        private void btMiamiFilter_Click(object sender, EventArgs e)
        {
            ApplyMiamiFilter();
        }

        private void pbMiamiFilter_Click(object sender, EventArgs e)
        {
            ApplyMiamiFilter();
        }

        private void ApplyMiamiFilter() {
            pbMainPicture.Image = Origin;
            pbMainPicture.Image = ImageFilters.ApplyFilter(new Bitmap(pbMainPicture.Image), 1, 1, 10, 1);
        }

        private void btNightFilter_Click(object sender, EventArgs e)
        {
            ApplyNightFilter();
        }

        private void pbNightFilter_Click(object sender, EventArgs e)
        {
            ApplyNightFilter();
        }

        private void ApplyNightFilter()
        {
            pbMainPicture.Image = Origin;
            pbMainPicture.Image = ImageFilters.ApplyFilter(new Bitmap(pbMainPicture.Image), 1, 1, 1, 25);
        }

        private void btHellFilter_Click(object sender, EventArgs e)
        {
            ApplyHellFilter();
        }

        private void pbHellFilter_Click(object sender, EventArgs e)
        {
            ApplyHellFilter();
        }

        private void ApplyHellFilter()
        {
            pbMainPicture.Image = ImageFilters.ApplyFilter(new Bitmap(pbMainPicture.Image), 1, 1, 10, 15);
        }

        private void btZenFilter_Click(object sender, EventArgs e)
        {
            ApplyZenFilter();
        }

        private void pbZenFilter_Click(object sender, EventArgs e)
        {
            ApplyZenFilter();
        }

        private void ApplyZenFilter()
        {
            pbMainPicture.Image = Origin;
            pbMainPicture.Image = ImageFilters.ApplyFilter(new Bitmap(pbMainPicture.Image), 1, 10, 1, 1);
        }

        private void btBlackAndWhite_Click(object sender, EventArgs e)
        {
            ApplyBlackAndWhite();
        }

        private void pbBlackAndWhite_Click(object sender, EventArgs e)
        {
            ApplyBlackAndWhite();
        }

        private void ApplyBlackAndWhite()
        {
            pbMainPicture.Image = Origin;
            pbMainPicture.Image = ImageFilters.BlackWhite(new Bitmap(pbMainPicture.Image));
        }

        private void btSwapFilter_Click(object sender, EventArgs e)
        {
            ApplySwapFilter();
        }

        private void pbSwapFilter_Click(object sender, EventArgs e)
        {
            ApplySwapFilter();
        }

        private void ApplySwapFilter()
        {
            pbMainPicture.Image = Origin;
            pbMainPicture.Image = ImageFilters.ApplyFilterSwap(new Bitmap(pbMainPicture.Image));
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            SaveImage();
        }
        public void SaveImage()
        {
            pbMainPicture.SizeMode = PictureBoxSizeMode.Zoom;
            FolderBrowserDialog fl = new FolderBrowserDialog();
            if (fl.ShowDialog() != DialogResult.Cancel)
            {

                pbMainPicture.Image.Save(fl.SelectedPath + @"\" + tbImageName.Text + @".png", System.Drawing.Imaging.ImageFormat.Png);
            };
            pbMainPicture.SizeMode = PictureBoxSizeMode.Zoom;
        }

        

        

        

        

        
        
    }
}
