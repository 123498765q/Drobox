using System;
using System.Collections.Generic;
using System.IO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using UWP.Classes;
using File = UWP.Classes.File;

namespace UWP
{
    public sealed partial class MainPage : Page
    {
        private List<File> files;
        public MainPage()
        {
            this.InitializeComponent();
            var a = Util.GetFileList();
            File f = new File
            {
                TypeImage = DesignUtils.SelectExtentionImage("png"),
                FileName = "AZSDASDASD",
                FilePath = "filePath",
                FileType = "fileType",
                Coordinates = "coordinates",
                Content = "content"
        };
            files = new List<File>();
            files.Add(f);
            
        }

        private List<File> GetAllFiles()
        {
            string userPath = App.BaseUrl + App.sub + "/" + App.given_name;

            string[] filePaths = Directory.GetFiles(@":\Users\Mykolas\Desktop\", "*.txt", SearchOption.TopDirectoryOnly);

            return null;
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var file = (File)e.ClickedItem;
            ResultTextBlock.Text = "You Selected: ";
        }

        private void ShareBtn_OnClick(object sender, RoutedEventArgs e)
        {
            string[] filePaths = { @"C:\Users\Mykolas\Desktop\files\b.png", @"C:\Users\Mykolas\Desktop\files\a.png" };
            Util.AddFile(filePaths);
        }

        private void DelBtn_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}