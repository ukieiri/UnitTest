using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GurshchenkovaValette
{
    public interface IFilenameManipulation
    {
        // return the files inside the selected folder
        String[] getFileNames();

        // choose a folder to work in
        Boolean setFolder(String folder);
        String getFolder();

        // choose the base name of the file "IMAGE-rainboxfilter.png"
        Boolean setFileName(String fileName);
        String getFileName();

        // choose the string in between the base name and the filter's name
        Boolean setFileToken(String fileToken);
        String getFileToken();

        // choose the filter name
        Boolean setFileFilter(String fileFilter);
        String getFileFilter();

        // choose the image format (png, jpeg, bmp)
        Boolean setFormat(String fileFormat);
        String getFormat();

        // return the full path of the chosen file
        String getFullPath();
    }
}
