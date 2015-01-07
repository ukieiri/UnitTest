using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GurshchenkovaValette;
using NSubstitute;
using System.Drawing;
using System.Windows.Forms;
namespace UnitTest
{
    [TestClass]
    public class UnitTest_ControlsManipulator
    {
        Form1 fa;
        String _path;
        IFilenameManipulation _existingFilename;

        IControlsManipulation _controls;
        IControlsManipulation _controlSubs;


        IimageManipulation _imageExisting;
        IimageManipulation _imageGood;

        public UnitTest_ControlsManipulator() {
            _path = System.IO.Directory.GetCurrentDirectory() + "\\..\\..\\images";

            _imageGood = Substitute.For<IimageManipulation>();
            _imageGood.save(Arg.Any<Image>()).Returns(true);
            _imageGood.remove().Returns(true);

            _controlSubs = Substitute.For<IControlsManipulation>();
            _controlSubs.ApplyFilter(Arg.Any<string>(), Arg.Any<PictureBox>(), Arg.Any<TextBox>(), Arg.Any<Image>()).Returns(true);

            // instanciation for existing image
            _existingFilename = Substitute.For<IFilenameManipulation>();
            _existingFilename.getFolder().Returns(_path);
            _existingFilename.getFullPath().Returns(_path + "\\a.jpg");
            _existingFilename.getFileName().Returns("a");

            _imageExisting = new ImageManipulation(_existingFilename);
        }

        [TestMethod]
        public void SavePicture()
        {
            _controls = new ControlsManipulation(_imageGood);
            IFilenameManipulation fm = new FilenameManipulation();
            fm.setFormat(".jpg");
            fm.setFolder(System.IO.Directory.GetCurrentDirectory() + "\\..\\..\\images");
            fm.setFileName("a");
            _controls.setFm(fm);
            _controls.SaveImage(Arg.Any<Image>(), null).Returns(true);

            Form1 form = new Form1(_controls);
            
            var result = form.SaveImage();
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void DeletePicture()
        {
            _controls = new ControlsManipulation(_imageGood);
            _controls.DeletePicture().Returns(true);

            Form1 form = new Form1(_controls);

            var result = form.OnDeleteClick();
            Assert.AreEqual(true, result);

            form = new Form1();

            result = form.OnDeleteClick();
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void ApplyFilter()
        {
            Form1 form = new Form1(_controlSubs);
            var result = form.OnFilterButtonClick("pbMiamiFilter");
            Assert.AreEqual(true, result);
            form = new Form1();
            result = form.OnFilterButtonClick("pbMiamiFilter");
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void GetAPath()
        {
            Form1 form = new Form1(_controlSubs);
            var result = form.GetAPath();
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void ImportImage()
        {
            _controlSubs = Substitute.For<IControlsManipulation>();
            _controlSubs.ImportImage(Arg.Any<string>()).Returns(true);
            Form1 form = new Form1(_controlSubs);

            var result = form.OnImportClick("a.jpg");
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void OpenImage()
        {
            IControlsManipulation cm = new ControlsManipulation();
            var result = cm.OpenImage(null);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void PopulateListBox()
        {
            IControlsManipulation cm = new ControlsManipulation(_imageExisting);
            IFilenameManipulation fm = new FilenameManipulation();
            fm.setFileName("a");
            cm.setFm(fm);
            var result = cm.PopulateListBox();
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void ImportImageCM()
        {
            IControlsManipulation cm = new ControlsManipulation();  
            var result = cm.ImportImage("a.jpg");
            Assert.AreEqual(true, result);
        }
         [TestMethod]
        public void ConfigureFileManager()
        {
            IControlsManipulation cm = new ControlsManipulation();
            var result = cm.ConfigureFileManager(_path);
            Assert.AreEqual(true, result);
        }
        [TestMethod]
         public void ApplyFilterCM()
        {
            IControlsManipulation cm = new ControlsManipulation();
            PictureBox pbMiamiFilter = new PictureBox();
             TextBox tbImageName = new TextBox();
             Image img = Image.FromFile(_path + "\\a.jpg");
             var result = cm.ApplyFilter("pbMiamiFilter", pbMiamiFilter, tbImageName, img);
            Assert.AreEqual(true, result);
        }
        
        [TestMethod]
        public void PopulatePictureBoxes()
        {
            string path = System.IO.Directory.GetCurrentDirectory() + "\\..\\..\\images\\a.jpg";
            Image img = Image.FromFile(path);

            fa = new Form1();
            Boolean result = fa.PopulatePictureBoxes(img, path);
            Assert.AreEqual(true, result);

            fa = new Form1();
            result = fa.PopulatePictureBoxes(null, null);
            Assert.AreEqual(false, result);
        }
    }
}
