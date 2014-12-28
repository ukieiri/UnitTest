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
        String _path;
        IimageManipulation _imageGood;
        IimageManipulation _imageBad;

        public UnitTest_imageManipulation()
        {
            _path = System.IO.Directory.GetCurrentDirectory() + "\\..\\..\\images";

            // instanciation for successfull usage
            _goodFilename = Substitute.For<IFilenameManipulation>();
            _goodFilename.getFolder().Returns(_path);
            _goodFilename.getFullPath().Returns(_path + "new image - no filter.jpg");
            _goodFilename.getFolder().Returns(".jpg");

            // instanciation for unsuccessful usage
            _badFilename = Substitute.For<IFilenameManipulation>();
            _badFilename.getFolder().Returns("unexisting path");

            _imageGood = new ImageManipulation(_goodFilename);
            _imageBad = new ImageManipulation(_badFilename);
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

        [TestMethod] // control the save and remove image methods
        public void discManipulation()
        {
            Boolean result = true;

            // take an image to save and delete
            Image img = Image.FromFile(_path + "\\a.jpg");

            // save a good image : return true and new image added
            result = _imageGood.save(img);
            Assert.AreEqual(true, result);

            String[] files = Directory.GetFiles(_goodFilename.getFolder());
            Assert.AreEqual(4, files.Length);

            // remove a good image : return true and old image deleted
            result = _imageGood.remove();
        }
    }
}
