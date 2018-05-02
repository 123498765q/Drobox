using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using UWP.Classes;
using File = UWP.Classes.File;

namespace UWP
{
    public sealed partial class SharedScreen : Page
    {
        private readonly List<File> files;
        private string currentPath;

        public SharedScreen()
        {
            this.InitializeComponent();
            files = new List<File>();
            currentPath = "Shared";
            InitListAsync(currentPath);
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (currentPath == "Shared") return;
            var path = currentPath.Split('\\');

            currentPath = "";
            for (int i = 0; i < path.Length - 1; i++)
            {
                currentPath += (path[i] + "\\");
            }

            currentPath = currentPath.Remove(currentPath.Length - 1);
            InitListAsync(currentPath);
        }

        private async void Sync_OnClick(object sender, RoutedEventArgs e)
        {
            StorageFolder installedLocation = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var path = installedLocation.Path + "\\Files";
            var filesInDir = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly);
            FileUtil.AddFile(filesInDir, currentPath);

            DirectoryInfo di = new DirectoryInfo(path);
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }

            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = "Succes",
                Content = "Your files successfully synced with cloud.",
                CloseButtonText = "Ok"
            };
            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }

        private void ListView1_OnRightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            MenuFlyout myFlyout = new MenuFlyout();
            MenuFlyoutItem delete = new MenuFlyoutItem { Text = "Delete" };
            myFlyout.Items.Add(delete);

            FrameworkElement senderElement = sender as FrameworkElement;
            myFlyout.ShowAt(sender as UIElement, e.GetPosition(sender as UIElement));
            delete.Click += DeleteOnClick;
        }

        private async void DeleteOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                File f = (File)listView1.SelectedItems[0];
                FileUtil.DeleteFile(f.FilePath);
                InitListAsync(currentPath);
            }
            else
            {
                ContentDialog noWifiDialog = new ContentDialog
                {
                    Title = "Error",
                    Content = "Please select file.",
                    CloseButtonText = "Ok"
                };
                ContentDialogResult result = await noWifiDialog.ShowAsync();
            }
        }

        private void ListView1_OnDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var file = (File)listView1.SelectedItem;
            if (file.FileType == "folder")
            {
                currentPath += "\\" + file.FileName;
                InitListAsync(currentPath);
                return;
            }

            Util.OpenFile(file.FileName, currentPath);
        }

        private async void InitListAsync(string folder)
        {
            try
            {
                listView1.Items.Clear();
                files.Clear();
                PathDisplay.Text = currentPath;

                var paths = await FileUtil.GetFileList(folder);
                foreach (var p in paths)
                {
                    var fileName = p.Split(folder + "\\")[1];
                    string fileExtention = "";
                    if (fileName.Contains('.'))
                    {
                        fileExtention = fileName.Split('.')[1];
                    }

                    File f = new File
                    {
                        Content = "",
                        Coordinates = "",
                        FileName = fileName,
                        TypeImage = DesignUtil.SelectExtentionImage(fileExtention),
                        FilePath = p,
                        FileType = DesignUtil.SetFileType(fileExtention)
                    };
                    files.Add(f);
                    listView1.Items.Add(f);
                }
            }

            catch
            {
                // ignored
            }
        }

        private void GetSharedFolder_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sharedPath.Text != "")
                {
                    FileUtil.CopyDir(sharedPath.Text, App.sub + "\\Shared\\");
                    InitListAsync(currentPath);
                }
            }
            catch
            {
                // ignored
            }
        }
    }
}
