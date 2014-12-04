using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GurshchenkovaValette
{
    public class FileManipulation : IFileManipulation
    {
        // 
        public String[] getFileNames()
        {
            return null;
        }
        public Boolean setFolder(String folder)
        {
            return false;
        }
        public String getFolder()
        {
            return "";
        }
        public Boolean setFileName(String fileName)
        {
            return false;
        }
        public String getFileName()
        {
            return "";
        }
        public Boolean setFileToken(String fileToken)
        {
            return false;
        }
        public String getFileToken()
        {
            return "";
        }
        public Boolean setFileFilter(String fileFilter)
        {
            return false;
        }
        public String getFileFilter()
        {
            return "";
        }
        public Boolean setFormat(String fileFormat)
        {
            return false;
        }
        public String getFormat()
        {
            return "";
        }
        public Boolean save(Image img)
        {
            return false;
        }
        public Image openImage(String fileName)
        {
            return null;
        }
    }
}
