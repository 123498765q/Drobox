using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP.Classes
{
    static class DesignUtils
    {
        public static string SelectExtentionImage(string fileExtention)
        {
            switch (fileExtention)
            {
                case "png":
                    return "Assets/FileTypeImg/png.png";
                case "doc":
                    return "Assets/FileTypeImg/doc.png";
                case "exe":
                    return "Assets/FileTypeImg/exe.png";
                case "jpg":
                    return "Assets/FileTypeImg/jpg.png";
                case "mp3":
                    return "Assets/FileTypeImg/mp3.png";
                case "mp4":
                    return "Assets/FileTypeImg/mp4.png";
                case "pdf":
                    return "Assets/FileTypeImg/pdf.png";
                case "txt":
                    return "Assets/FileTypeImg/txt.png";
                case "xls":
                    return "Assets/FileTypeImg/xls.png";
                case "zip":
                case "rar":
                    return "Assets/FileTypeImg/doc.png";
            }
            return "Assets/FileTypeImg/file.png";
        }
    }
}
