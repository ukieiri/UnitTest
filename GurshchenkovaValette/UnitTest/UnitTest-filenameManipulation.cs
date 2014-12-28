using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GurshchenkovaValette;
using System.Drawing;

namespace UnitTest
{
    [TestClass]
    public class UnitTest_filenameManipulation
    {
        IFilenameManipulation _filenameManipulation;
        String _validFolder;

        public UnitTest_filenameManipulation()
        {
            // all test methods will test this class. So we instanciate it once
            _filenameManipulation = new FilenameManipulation();

            // We need a working folder with files for many methods.
            _validFolder = System.IO.Directory.GetCurrentDirectory() + "\\..\\..\\images";

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

        [TestMethod] // control that the folder name's functions are correctly checked
        public void folderName()
        {
            Boolean result = true;

            // try to add an unexisting path
            result = _filenameManipulation.setFolder("Unexisting path");
            Assert.AreEqual(false, result);
            Assert.AreEqual("", _filenameManipulation.getFolder());

            // add an existing path
            result = _filenameManipulation.setFolder(_validFolder);
            Assert.AreEqual(true, result);
            Assert.AreEqual(_validFolder, _filenameManipulation.getFolder());

            // change to a non-existing path. The previous path should have been kept
            result = _filenameManipulation.setFolder("unexisting folder");
            Assert.AreEqual(false, result);
            Assert.AreEqual(_validFolder, _filenameManipulation.getFolder());
        }

        [TestMethod] // control the "get files" method
        public void getFiles()
        {
            String[] results = null;

            // invalid folder
            _filenameManipulation.setFolder("invalid folder");

            results = _filenameManipulation.getFileNames();
            Assert.AreEqual(null, results);

            // valid folder
            _filenameManipulation.setFolder(_validFolder);

            results = _filenameManipulation.getFileNames();
            Assert.AreNotEqual(null, results);
            Assert.AreEqual(3, results.Length);

            String[] expected = { "a.jpg", "b.jpg", "c.jpg" };

            for (int i = 0, j = results.Length; i < j; i++)
                Assert.AreEqual(expected[i], results[i]);
        }

        [TestMethod] // control the file name's methods
        public void fileName()
        {
            Boolean result = true;
            String validName = "valid file name";

            // try to set an invalid file name
            result = _filenameManipulation.setFileName("/¦2");
            Assert.AreEqual(false, result);
            Assert.AreEqual("new file", _filenameManipulation.getFileName());

            // set a valid file name
            result = _filenameManipulation.setFileName(validName);
            Assert.AreEqual(true, result);
            Assert.AreEqual(validName, _filenameManipulation.getFileName());

            // set invalid name, previous name should stay
            result = _filenameManipulation.setFileName("cou/cou");
            Assert.AreEqual(false, result);
            Assert.AreEqual(validName, _filenameManipulation.getFileName());
        }

        [TestMethod] // control the file token's methods
        public void fileToken()
        {
            Boolean result = true;
            String validToken = " (filter) ";

            // try to set an invalid file token
            result = _filenameManipulation.setFileToken(" / ");
            Assert.AreEqual(false, result);
            Assert.AreEqual(" - ", _filenameManipulation.getFileToken());

            // set a valid file token
            result = _filenameManipulation.setFileToken(validToken);
            Assert.AreEqual(true, result);
            Assert.AreEqual(validToken, _filenameManipulation.getFileToken());

            // set invalid token, previous token should stay
            result = _filenameManipulation.setFileToken(" :filter: ");
            Assert.AreEqual(false, result);
            Assert.AreEqual(validToken, _filenameManipulation.getFileToken());
        }

        [TestMethod] // control the file format's methods
        public void fileFormat()
        {
            Boolean result = true;
            String validFormat = ".jpeg";

            // try to set an invalid format
            result = _filenameManipulation.setFormat(".docx");
            Assert.AreEqual(false, result);
            Assert.AreEqual(".jpg", _filenameManipulation.getFormat());

            // set a valid format
            result = _filenameManipulation.setFormat(validFormat);
            Assert.AreEqual(true, result);
            Assert.AreEqual(validFormat, _filenameManipulation.getFormat());

            // set invalid format, previous format should stay
            result = _filenameManipulation.setFormat(".xlsx");
            Assert.AreEqual(false, result);
            Assert.AreEqual(validFormat, _filenameManipulation.getFormat());
        }

    } // end of class "UnitTest"
} // end of namespace
