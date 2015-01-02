using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GurshchenkovaValette;
using NSubstitute;
using System.Drawing;
namespace UnitTest
{
    [TestClass]
    public class UnitTest_Form
    {
        Form1 fa;

        [TestMethod]
        public void PopulatePictureBoxes()
        {
            string path = System.IO.Directory.GetCurrentDirectory() + "\\..\\..\\images\\sue.png";
            Image img = Image.FromFile(path);
            Boolean result;

            fa = new Form1();
            result = fa.PopulatePictureBoxes(img, path);
            Assert.AreEqual(true, result);
        }
    }
}
