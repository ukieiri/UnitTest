using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GurshchenkovaValette
{
    public interface IFileManipulation
    {
        String[] getFileNames();
        Boolean setFolder(String folder);
        Boolean setFileName(String fileName);
        Boolean setFileToken(String fileToken);
        Boolean setFileFilter(String fileFilter);
        Boolean setFormat(String fileFormat);
        Boolean save(Image img);
        Image openImage(String fileName);
    }
}
