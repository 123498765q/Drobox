using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using UWP.Classes;
using File = UWP.Classes.File;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI.Xaml.Controls.Primitives;

namespace UWP
{
    public sealed partial class MainPage : Page
    {
        private readonly List<File> files;
        private string currentPath;

        public MainPage()
        {
            this.InitializeComponent();
            files = new List<File>();
            currentPath = App.given_name;
            InitListAsync(currentPath);
        }

//        private void ShareBtn_OnClick(object sender, RoutedEventArgs e)
//        {
//            string[] filePaths = { @"C:\Users\Mykolas\Desktop\files\b.png", @"C:\Users\Mykolas\Desktop\files\a.png" };
//            FileUtil.AddFile(filePaths);
//        }

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

        private void ListView1_OnRightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            MenuFlyout myFlyout = new MenuFlyout();
            MenuFlyoutItem addFile = new MenuFlyoutItem { Text = "Add file" };
            MenuFlyoutItem createFolder = new MenuFlyoutItem { Text = "Create folder" };
            MenuFlyoutItem share = new MenuFlyoutItem { Text = "Share" };
            MenuFlyoutItem delete = new MenuFlyoutItem { Text = "Delete" };
            myFlyout.Items.Add(addFile);
            myFlyout.Items.Add(createFolder);
            myFlyout.Items.Add(share);
            myFlyout.Items.Add(delete);

            FrameworkElement senderElement = sender as FrameworkElement;
            myFlyout.ShowAt(sender as UIElement, e.GetPosition(sender as UIElement));
            
            addFile.Click += AddOnClick;
            createFolder.Click += CreateFolderOnClick;
            share.Click += ShareOnClick;
            delete.Click += DeleteOnClick;
        }

        private async void CreateFolderOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            string text = await DesignUtil.InputTextDialogAsync("Name");
            FileUtil.CreateFolder(App.sub + "\\" + currentPath + "\\" + text);
            InitListAsync(currentPath);
        }

        private async void AddOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            // C:\Users\Mykolas\Documents\Repos\Drobox\UWP\bin\x86\Debug\AppX\IsCiaKelti
            try
            {
                var picker = new Windows.Storage.Pickers.FileOpenPicker();
                picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
                picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
                picker.FileTypeFilter.Add("*");
                StorageFile file = await picker.PickSingleFileAsync();
                if (file != null)
                {           
                    var filesToSave = new string[1];
                    filesToSave[0] = file.Path;
                    FileUtil.AddFile(filesToSave, currentPath);
                    var c = currentPath;
                    InitListAsync(currentPath);
                }
            }
            catch
            {
                // ignored
            }
        }

        private async void DeleteOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                File f = (File) listView1.SelectedItems[0];
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

        private async void ShareOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                File f = (File)listView1.SelectedItems[0];
                string text = await DesignUtil.InputTextDialogAsync("Folder name");
                string destPath = App.sub + "\\Shared\\" + text;
                FileUtil.CreateFolder(destPath);
                FileUtil.ShareFiles(f.FilePath, destPath);
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
            var file = (File) listView1.SelectedItem;
            if (file.FileType == "folder")
            {
                currentPath += "\\" + file.FileName;
                InitListAsync(currentPath);
                return;
            }

            Util.OpenFile(file.FileName, currentPath);
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            // MrM\nuotraukos\bendras
            if (currentPath == App.given_name) return;
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
    }
}