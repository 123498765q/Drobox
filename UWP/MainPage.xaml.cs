using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using UWP.Classes;
using File = UWP.Classes.File;

namespace UWP
{
    public sealed partial class MainPage : Page
    {
        
        public MainPage()
        {
            this.InitializeComponent();
        
             // cia reikia sita suda padaryt async kazkaip
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var file = (File)e.ClickedItem;
            if (file.FileType == "folder")
            {
                
            }
            System.Diagnostics.Process.Start(file.FilePath);
        }

        private void ShareBtn_OnClick(object sender, RoutedEventArgs e)
        {
            string[] filePaths = { @"C:\Users\Mykolas\Desktop\files\b.png", @"C:\Users\Mykolas\Desktop\files\a.png" };
            Util.AddFile(filePaths);
        }

        private void DelBtn_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private async Task InitListAsync()
        {
//            try
//            {
//                var paths = await Util.GetFileList();
//                foreach (var p in paths)
//                {
//                    var fileName = p.Split(App.given_name + "\\")[1];
//                    if (fileName.Contains('.'))
//                    {
//                        fileName = fileName.Split('.')[1];
//                    }
//
//                    File f = new File
//                    {
//                        Content = "",
//                        Coordinates = "",
//                        FileName = fileName,
//                        TypeImage = DesignUtils.SelectExtentionImage(fileName),
//                        FilePath = p,
//                        FileType = DesignUtils.SetFileType(fileName)
//                    };
//                    files.Add(f);
//                }
//            }
//
//            catch
//            {
//                // ignored
//            }
        }
        
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            List<File> files = new List<File>();
            try
            {
                var paths = await Util.GetFileList();
                foreach (var p in paths)
                {
                    var fileName = p.Split(App.given_name + "\\")[1];
                    if (fileName.Contains('.'))
                    {
                        fileName = fileName.Split('.')[1];
                    }

                    File f = new File
                    {
                        Content = "",
                        Coordinates = "",
                        FileName = fileName,
                        TypeImage = DesignUtils.SelectExtentionImage(fileName),
                        FilePath = p,
                        FileType = DesignUtils.SetFileType(fileName)
                    };
                    files.Add(f);
                }
                
                string txt = "";
                foreach (var t in files)
                {
                    ListViewItem item = new ListViewItem();
                    
                    list.Items.Add(files);
                }

                aaa.Text = txt;
            }

            catch
            {
                // ignored
            }
        }
    }
}