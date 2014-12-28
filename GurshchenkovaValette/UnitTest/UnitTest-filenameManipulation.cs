using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GurshchenkovaValette;
using System.Drawing;

namespace UnitTest
{
    [TestClass]
    public class UnitTest_filenameManipulation
    {
        // all test methods will test this class. So we instanciate it once
        IFilenameManipulation filenameManipulation = new FilenameManipulation();

        // We need a working folder with files for many methods.
        String validFolder = System.IO.Directory.GetCurrentDirectory() + "\\..\\..\\images";

        [TestMethod] // control that the folder name's functions are correctly checked
        public void folderName()
        {
            Boolean result = true;

            // try to add an unexisting path
            result = filenameManipulation.setFolder("Unexisting path");
            Assert.AreEqual(false, result);
            Assert.AreEqual("", filenameManipulation.getFolder());

            // add an existing path
            result = filenameManipulation.setFolder(validFolder);
            Assert.AreEqual(true, result);
            Assert.AreEqual(validFolder, filenameManipulation.getFolder());

            // change to a non-existing path. The previous path should have been kept
            result = filenameManipulation.setFolder("unexisting folder");
            Assert.AreEqual(false, result);
            Assert.AreEqual(validFolder, filenameManipulation.getFolder());
        }

        [TestMethod] // control the "get files" method
        public void getFiles()
        {
            String[] results = null;

            // invalid folder
            filenameManipulation.setFolder("invalid folder");

            results = filenameManipulation.getFileNames();
            Assert.AreEqual(null, results);

            // valid folder
            filenameManipulation.setFolder(validFolder);

            results = filenameManipulation.getFileNames();
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
            result = filenameManipulation.setFileName("/¦2");
            Assert.AreEqual(false, result);
            Assert.AreEqual("new file", filenameManipulation.getFileName());

            // set a valid file name
            result = filenameManipulation.setFileName(validName);
            Assert.AreEqual(true, result);
            Assert.AreEqual(validName, filenameManipulation.getFileName());

            // set invalid name, previous name should stay
            result = filenameManipulation.setFileName("cou/cou");
            Assert.AreEqual(false, result);
            Assert.AreEqual(validName, filenameManipulation.getFileName());
        }

        [TestMethod] // control the file token's methods
        public void fileToken()
        {
            Boolean result = true;
            String validToken = " (filter) ";

            // try to set an invalid file token
            result = filenameManipulation.setFileToken(" / ");
            Assert.AreEqual(false, result);
            Assert.AreEqual(" - ", filenameManipulation.getFileToken());

            // set a valid file token
            result = filenameManipulation.setFileToken(validToken);
            Assert.AreEqual(true, result);
            Assert.AreEqual(validToken, filenameManipulation.getFileToken());

            // set invalid token, previous token should stay
            result = filenameManipulation.setFileToken(" :filter: ");
            Assert.AreEqual(false, result);
            Assert.AreEqual(validToken, filenameManipulation.getFileToken());
        }

        [TestMethod] // control the file format's methods
        public void fileFormat()
        {
            Boolean result = true;
            String validFormat = ".jpeg";

            // try to set an invalid format
            result = filenameManipulation.setFormat(".docx");
            Assert.AreEqual(false, result);
            Assert.AreEqual(".jpg", filenameManipulation.getFormat());

            // set a valid format
            result = filenameManipulation.setFormat(validFormat);
            Assert.AreEqual(true, result);
            Assert.AreEqual(validFormat, filenameManipulation.getFormat());

            // set invalid format, previous format should stay
            result = filenameManipulation.setFormat(".xlsx");
            Assert.AreEqual(false, result);
            Assert.AreEqual(validFormat, filenameManipulation.getFormat());
        }

    } // end of class "UnitTest"
} // end of namespace
