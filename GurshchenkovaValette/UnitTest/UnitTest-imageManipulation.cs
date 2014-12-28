using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using GurshchenkovaValette;
using System.Drawing;
using System.IO;

namespace UnitTest
{
    /// <summary>
    /// Summary description for UnitTest_imageManipulation
    /// </summary>
    [TestClass]
    public class UnitTest_imageManipulation
    {
        IFilenameManipulation _goodFilename;
        IFilenameManipulation _badFilename;
        IFilenameManipulation _existingFilename;
        String _path;
        IimageManipulation _imageGood;
        IimageManipulation _imageBad;
        IimageManipulation _imageExisting;

        public UnitTest_imageManipulation()
        {
            _path = System.IO.Directory.GetCurrentDirectory() + "\\..\\..\\images";

            // instanciation for successfull usage
            _goodFilename = Substitute.For<IFilenameManipulation>();
            _goodFilename.getFolder().Returns(_path);
            _goodFilename.getFullPath().Returns(_path + "\\new image - no filter.jpg");
            _goodFilename.getFormat().Returns(".jpg");

            // instanciation for unsuccessful usage
            _badFilename = Substitute.For<IFilenameManipulation>();
            _badFilename.getFolder().Returns("unexisting path");

            // instanciation for existing image
            _existingFilename = Substitute.For<IFilenameManipulation>();
            _existingFilename.getFolder().Returns(_path);
            _existingFilename.getFullPath().Returns(_path + "\\a.jpg");

            _imageGood = new ImageManipulation(_goodFilename);
            _imageBad = new ImageManipulation(_badFilename);
            _imageExisting = new ImageManipulation(_existingFilename);
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod] // control the save and remove image methods (behaviour if existing)
        public void imageGood()
        {
            Boolean result;
            String[] files;

            // take an image to save and delete
            Image img = Image.FromFile(_path + "\\a.jpg");

            // save in good folder : return true and new image added
            result = _imageGood.save(img);
            Assert.AreEqual(true, result);

            files = Directory.GetFiles(_goodFilename.getFolder());
            Assert.AreEqual(5, files.Length); // 5, because originally there are 3 pictures + 1 text file

            // remove an existing image : return true and old image deleted
            result = _imageGood.remove();
            Assert.AreEqual(true, result);

            files = Directory.GetFiles(_goodFilename.getFolder());
            Assert.AreEqual(4, files.Length);
        }

        [TestMethod] // control the save and remove image methods (behaviour if non-existing)
        public void imageBad()
        {
            Boolean result;
            String[] files;

            // take an image to save and delete
            Image img = Image.FromFile(_path + "\\a.jpg");

            // save in non-existing folder : return false and no image added
            result = _imageBad.save(img);
            Assert.AreEqual(false, result);

            files = Directory.GetFiles(_goodFilename.getFolder());
            Assert.AreEqual(4, files.Length);

            // remove a non-existing image : return false, no image removed
            result = _imageBad.remove();
            Assert.AreEqual(false, result);

            files = Directory.GetFiles(_goodFilename.getFolder());
            Assert.AreEqual(4, files.Length);
        }

        [TestMethod] // control the open image method
        public void openImage()
        {
            Image img;

            // open an existing image
            img = _imageExisting.openImage();
            Assert.AreNotEqual(null, img);

            // open a non-existing image from a non-existing folder
            img = _imageBad.openImage();
            Assert.AreEqual(null, img);

            // open a non-existing image from an existing folder
            img = _imageGood.openImage();
            Assert.AreEqual(null, img);
        }
    } // end of class
} // end of namespace
