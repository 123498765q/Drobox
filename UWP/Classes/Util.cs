using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace UWP.Classes
{
    static class Util
    {
        public static async void OpenFile(string fileName, string currentPath)
        {
            try
            {
                FileUtil.GetFile(currentPath + "\\" + fileName);
                StorageFolder installedLocation = Windows.ApplicationModel.Package.Current.InstalledLocation;
                var file = await StorageFile.GetFileFromPathAsync(installedLocation.Path + "\\Files\\" + fileName);
                await Windows.System.Launcher.LaunchFileAsync(file);
            }
            catch
            {
                // ignored
            }
        }
    }
}
