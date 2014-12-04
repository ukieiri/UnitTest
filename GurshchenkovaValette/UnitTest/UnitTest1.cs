using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GurshchenkovaValette;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        
        [TestMethod] // control that the file name's parts are correctly checked
        public void setFileNames()
        {
            IFileManipulation fileManipulation = new FileManipulation();

            // try to add an unexisting path
            Boolean result = fileManipulation.setFolder("Unexisting path");
            Assert.AreEqual( false, result );
            Assert.AreEqual( "", fileManipulation.getFolder());

            // add an existing path
            String actualFolder = System.IO.Directory.GetCurrentDirectory() + "/images";
            Console.WriteLine(actualFolder);
            result = fileManipulation.setFolder(System.IO.Directory.GetCurrentDirectory() + "/images");
        }
    }
}
