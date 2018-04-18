using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace API.Classes
{
    public static class ManageFiles
    {
        public static string FolderId { get; set; }
        public static string FolderName { get; set; }
        private static string BasePath = @"C:\Users\Mykolas\Documents\Repos\Drobox\API\UsersData";

        public static void CreateUserFolder(string id, string name)
        {
            Directory.CreateDirectory(BasePath + @"\" + id);
            Directory.CreateDirectory(BasePath + @"\" + id + @"\" + name);
        }

        public static void CreateUserFolderGuid(string userId, string[] files)
        {
            Guid guidId = Guid.NewGuid();
            Directory.CreateDirectory(BasePath + @"\" + userId + @"\" + guidId.ToString());
        
            foreach (string f in files)
            {
                var fileName = f.Split('\\').Last();
                var destFile = System.IO.Path.Combine(BasePath + "\\" + userId + "\\" + guidId.ToString(), fileName);
                System.IO.File.Copy(f, destFile, true);
            }
        }
    }
}