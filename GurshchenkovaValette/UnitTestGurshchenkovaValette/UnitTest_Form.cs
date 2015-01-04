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
            string path = System.IO.Directory.GetCurrentDirectory() + "\\..\\..\\images\\a.jpg";
            Image img = Image.FromFile(path);
            Boolean result;

            fa = new Form1();
            result = fa.PopulatePictureBoxes(img, path);
            Assert.AreEqual(true, result);
        }        

        [TestMethod]
        public void DeleteIsNotPossible()
        {
            fa = new Form1();
            Boolean result;
            result = fa.DeletePicture();
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void GetAPath()
        {
            fa = new Form1();
            Boolean result;
            result = fa.GetAPath();
            Assert.AreEqual(true, result);
        }

         [TestMethod]
        public void ApplyFilter() {
            fa = new Form1();
            string path = System.IO.Directory.GetCurrentDirectory() + "\\..\\..\\images\\a.jpg";
            fa.setOrigin(Image.FromFile(path));
            Boolean result;
            result = fa.ApplyFilter("MiamiFilter");
            Assert.AreEqual(true, result);
            result = fa.ApplyFilter("NightFilter");
            Assert.AreEqual(true, result);
            result = fa.ApplyFilter("HellFilter");
            Assert.AreEqual(true, result);
            result = fa.ApplyFilter("ZenFilter");
            Assert.AreEqual(true, result);
            result = fa.ApplyFilter("BlackAndWhite");
            Assert.AreEqual(true, result);
            result = fa.ApplyFilter("SwapFilter");
            Assert.AreEqual(true, result);
            result = fa.ApplyFilter("BlaaFilter");
            Assert.AreEqual(false, result);
        }
         [TestMethod]
         public void SaveImage() {
             fa = new Form1();
             Boolean result;
             result = fa.SaveImage();
             Assert.AreEqual(true, result);
         }
         [TestMethod]
         public void FilterButtonsTest()
         {
             fa = new Form1();
             string path = System.IO.Directory.GetCurrentDirectory() + "\\..\\..\\images\\a.jpg";
             fa.setOrigin(Image.FromFile(path));
             Boolean result;

             result = fa.SaveImage();
             Assert.AreEqual(true, result);
         }

    }
}
