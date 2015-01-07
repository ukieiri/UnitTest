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
        public IControlsManipulation _cm;
        //to have access from other classes
        public static Form1 _Form1;

        public void setOrigin(Image origin) {
            this.Origin = origin;
        }
        public Form1()
        {
            InitializeComponent();
            _Form1 = this;
            _cm = new ControlsManipulation();
            DisableControles();
            DisableImportButton();
        }
        //the another constructor for testing purposes
        public Form1(IControlsManipulation cm) {
            InitializeComponent();
            _Form1 = this;
            _cm = cm;
            DisableControles();
            DisableImportButton();
        }

        private void btLoad_Click(object sender, EventArgs e)
        {
            if (GetAPath())
            {
                lbImages.DataSource = _cm.PopulateListBox();
                EnableControles();     
            }

        }

        //get a path and load the image
        public bool GetAPath()
        {
            Image img = null;
            ImageParameters imageparams = new ImageParameters();
            OpenFileDialog op = new OpenFileDialog();
            //set available formats
            op.Filter = "Image Files (JPG,PNG,GIF)|*.JPG;*.PNG;*.GIF";
            DialogResult dr = op.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string path = op.FileName;
                _cm.ConfigureFileManager(path);
                img = Image.FromFile(path);
                PopulatePictureBoxes(img, path);
                //to free image 
                img.Dispose();
            }
            op.Dispose();
            return true;
        }

        public bool PopulatePictureBoxes(Image img, string path)
        {
            try{
                pbMainPicture.Image = img;
                pbOriginal.Image = img;
                
                Bitmap temp = new Bitmap(pbMainPicture.Image);
                pbMainPicture.Image = temp;
                pbOriginal.Image = temp;
                //adjust the image
                pbMainPicture.SizeMode = PictureBoxSizeMode.Zoom;
                pbOriginal.SizeMode = PictureBoxSizeMode.Zoom;

                PopulatePreviews(img);

                string filename = path.Split(new char[] { '\\' }).Last();
                string extension = path.Split(new char[] { '.' }).Last();
                filename = filename.Replace("." + extension, "");
                tbImageName.Text = filename;

                map = new Bitmap(pbMainPicture.Image);
                Origin = pbMainPicture.Image;
                return true;
            }
            catch {
                return false;
            }
        }

        public void PopulatePreviews(Image img) {
            //apply the filter and load preview images
            _cm.LoadPreviewImage(img, pbMiamiFilter);
            _cm.LoadPreviewImage(img, pbNightFilter);
            _cm.LoadPreviewImage(img, pbHellFilter);
            _cm.LoadPreviewImage(img, pbZenFilter);
            _cm.LoadPreviewImage(img, pbBlackAndWhite);
            _cm.LoadPreviewImage(img, pbSwapFilter);
        }

        public bool OnFilterButtonClick(string filter) {
            var result = _cm.ApplyFilter(filter, pbMainPicture, tbImageName, Origin);
            if (result == true)
                return result;
            else return result;
        }

        private void btMiamiFilter_Click(object sender, EventArgs e)
        {
            OnFilterButtonClick("pbMiamiFilter");
        }

        private void pbMiamiFilter_Click(object sender, EventArgs e)
        {
            OnFilterButtonClick("pbMiamiFilter");
        }

        private void btNightFilter_Click(object sender, EventArgs e)
        {
            OnFilterButtonClick("pbNightFilter");
        }

        private void pbNightFilter_Click(object sender, EventArgs e)
        {
            OnFilterButtonClick("pbNightFilter");
        }
        
        private void btHellFilter_Click(object sender, EventArgs e)
        {
           OnFilterButtonClick("pbHellFilter");
        }

        private void pbHellFilter_Click(object sender, EventArgs e)
        {
            OnFilterButtonClick("pbHellFilter");
        }

        private void btZenFilter_Click(object sender, EventArgs e)
        {
            OnFilterButtonClick("pbZenFilter");
        }

        private void pbZenFilter_Click(object sender, EventArgs e)
        {
            OnFilterButtonClick("pbZenFilter");
        }

        private void btBlackAndWhite_Click(object sender, EventArgs e)
        {
            OnFilterButtonClick("pbBlackAndWhite");
        }

        private void pbBlackAndWhite_Click(object sender, EventArgs e)
        {
            OnFilterButtonClick("pbBlackAndWhite");
        }

        private void btSwapFilter_Click(object sender, EventArgs e)
        {
            OnFilterButtonClick("pbSwapFilter");
        }

        private void pbSwapFilter_Click(object sender, EventArgs e)
        {
            OnFilterButtonClick("pbSwapFilter");
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
            if (SaveImage())
            {
                MessageBox.Show("Saved successfully");
                lbImages.DataSource = _cm.PopulateListBox();
            }
        }

        public bool SaveImage()
        {
            Image img = pbMainPicture.Image;
            string picturename = tbImageName.Text;
            var result = _cm.SaveImage(img, picturename);
            return result;         
        }
  
        private void btImport_Click(object sender, EventArgs e)
        {
            if (OnImportClick(lbImages.SelectedItem.ToString()))
            EnableControles();
        }

        public bool OnImportClick(string image)
        {
            var result = _cm.ImportImage(image);
            return result;
        }
        
        private void bnDelete_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure you want to delete this picture? It will be deleted permanently from your computer",
                                       "Delete", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                if (OnDeleteClick())
                {
                    RestoreTheOriginalForm();
                    DisableControles();
                    MessageBox.Show("Deleted successfully");
                }
                else {
                    MessageBox.Show("Deleting is not possible");
                }
            }
           
        }

        public bool OnDeleteClick() {
            return _cm.DeletePicture();
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
            lbImages.DataSource = _cm.PopulateListBox();
        }

        /// <summary>
        /// Disabling and enabling controles
        /// </summary>
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
