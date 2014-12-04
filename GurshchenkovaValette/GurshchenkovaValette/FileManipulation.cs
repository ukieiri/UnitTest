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
            return new String[]();
        }
        public Boolean setFolder(String folder)
        {
            return false;
        }
        public Boolean setFileName(String fileName)
        {
            return false;
        }
        public Boolean setFileToken(String fileToken)
        {
            return false;
        }
        public Boolean setFileFilter(String fileFilter)
        {
            return false;
        }
        public Boolean setFormat(String fileFormat)
        {
            return false;
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
