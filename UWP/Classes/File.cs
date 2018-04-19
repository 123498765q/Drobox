using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP.Classes
{
    public class File
    {
        public File() {}

        public File(string typeImage, string fileName, string filePath, string fileType, string coordinates, string content)
        {
            TypeImage = typeImage;
            FileName = fileName;
            FilePath = filePath;
            FileType = fileType;
            Coordinates = coordinates;
            Content = content;
        }

        public string TypeImage { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public string Coordinates { get; set; }
        public string Content { get; set; }
    }
}

