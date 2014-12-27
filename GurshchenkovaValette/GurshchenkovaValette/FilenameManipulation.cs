using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GurshchenkovaValette
{
    public class FilenameManipulation : IFilenameManipulation
    {
        private String  _folderName,
                        _fileName,
                        _fileToken,
                        _fileFilter,
                        _fileExtension;

        private String[] _acceptedFormats = { ".jpg", ".jpeg", ".png" };

        // initalize all properties
        public FilenameManipulation()
        {
            _folderName = "";
            _fileName = "new file";
            _fileToken = " - ";
            _fileFilter = "no filter";
            _fileExtension = ".jpg";
        }

        // return all file's names from the selected directory
        public String[] getFileNames()
        {
            if ( !Directory.Exists(_folderName) )
                return null;

            List<String> result = new List<String>();
            List<String> filesToSee = new List<String>();

            // read all files in the directory, with the appropriate extension
            DirectoryInfo di = new DirectoryInfo(_folderName);
            FileInfo[] fi = di.GetFiles().Where( f => _acceptedFormats.Contains(f.Extension) ).ToArray();

            foreach (FileInfo file in fi)
                result.Add(file.Name);

            return result.ToArray();
        }

        // assign a new folder, return false if doesn't exist
        public Boolean setFolder(String folder)
        {
            if( !Directory.Exists(folder) )
                return false;

            _folderName = folder;
            return true;
        }
        public String getFolder()
        {
            return _folderName;
        }

        public Boolean setFileName(String fileName)
        {
            if( !fileNameValid(fileName) )
                return false;

            _fileName = fileName;
            return true;
        }
        public String getFileName()
        {
            return _fileName;
        }

        // set the separator between the file name and the filter used
        public Boolean setFileToken(String fileToken)
        {
            if (!fileNameValid(fileToken))
                return false;

            _fileToken = fileToken;
            return true;
        }
        public String getFileToken()
        {
            return _fileToken;
        }

        // set the filter used on the file
        public Boolean setFileFilter(String fileFilter)
        {
            if( !fileNameValid(fileFilter) )
                return false;

            _fileFilter = fileFilter;
            return true;
        }
        public String getFileFilter()
        {
            return "";
        }

        // set the format of the picture
        public Boolean setFormat(String fileFormat)
        {
            if( !_acceptedFormats.Contains(fileFormat) )
                return false;

            _fileExtension = fileFormat;
            return true;
        }
        public String getFormat()
        {
            return _fileExtension;
        }

        // check if the string could be used in a file name
        private Boolean fileNameValid(String file)
        {
            // regex for all invalid characters in file name
            String regexString = "[" + Regex.Escape(new String(Path.GetInvalidFileNameChars())) + "]";
            Regex invChar = new Regex(regexString);

            // check no invalid character was used
            if (invChar.IsMatch(file))
                return false;

            return true;
        }

        // returns the full path of the file
        public String getFullPath() {
            
            // check the folder exists
            if (!Directory.Exists(getFolder()))
                return null;

            // returns the full path, from folder to extension
            return getFolder() + "\\" + getFileName() + getFileToken() + getFileFilter() + getFormat();
        }
    }
}
