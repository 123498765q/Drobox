using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace API.Classes
{
    public static class CreateFolder
    {
        public static string FolderId { get; set; }
        public static string FolderName { get; set; }
        private static string BasePath = @"C:\Users\Mykolas\Documents\Repos\Drobox\API\UsersData";

        public static void CreateUserFolder(string id, string name)
        {
            Directory.CreateDirectory(BasePath + @"\" + id);
            Directory.CreateDirectory(BasePath + @"\" + id + @"\" + name);
        }
    }
}