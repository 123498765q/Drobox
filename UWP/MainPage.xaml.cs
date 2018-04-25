using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
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
            files = new List<File>();
            InitListAsync();
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var file = (File)e.ClickedItem;
            if (file.FileType == "folder")
            {
                
            }

            btn.Content = file.FileName;
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

        private async void InitListAsync()
        {
            try
            {
                var paths = await Util.GetFileList();
                foreach (var p in paths)
                {
                    var fileName = p.Split(App.given_name + "\\")[1];
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
                        TypeImage = DesignUtils.SelectExtentionImage(fileExtention),
                        FilePath = p,
                        FileType = DesignUtils.SetFileType(fileExtention)
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

//        private void ListView1_OnDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
//        {
//            DependencyObject obj = (DependencyObject)e.OriginalSource;
//            var file = (File)e.OriginalSource;
//            if (file.FileType == "folder")
//            {
//            }
//
//            btn.Content = file.FileName;
//            System.Diagnostics.Process.Start(file.FilePath);
//        }

        private void ListView1_OnRightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            MenuFlyout myFlyout = new MenuFlyout();
            MenuFlyoutItem firstItem = new MenuFlyoutItem { Text = "Open" };
            MenuFlyoutItem secondItem = new MenuFlyoutItem { Text = "Delete" };
            myFlyout.Items.Add(firstItem);
            myFlyout.Items.Add(secondItem);

            FrameworkElement senderElement = sender as FrameworkElement;
            myFlyout.ShowAt(sender as UIElement, e.GetPosition(sender as UIElement));
        }

        private void ListView1_OnDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var file = (File) listView1.SelectedItem;
            if (file.FileType == "folder")
            {
            }

            System.Diagnostics.Process.Start(file.FilePath);
            btn.Content = file.FileName;
        }

        private void Btn_OnClick(object sender, RoutedEventArgs e)
        {
            var x = listView1.SelectedItems;
            int a = 45;
        }
    }
}