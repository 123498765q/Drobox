using System;
using System.Collections.Generic;
using System.IO;
using Windows.UI.Xaml.Controls;
using UWP.Classes;

namespace UWP
{
    public sealed partial class MainPage : Page
    {
        private List<Files> Files;
        public MainPage()
        {
            this.InitializeComponent();
            //Files
        }

        private List<Files> GetAllFiles()
        {
            string userPath = App.BaseUrl + App.sub + "/" + App.given_name;

            string[] filePaths = Directory.GetFiles(@":\Users\Mykolas\Desktop\", "*.txt", SearchOption.TopDirectoryOnly);

            return null;
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var book = (Files)e.ClickedItem;
            ResultTextBlock.Text = "You Selected: ";
        }
    }
}


//        private void Button_Click(object sender, RoutedEventArgs e)
//        {
//            string[] filePaths = { @"C:\Users\Mykolas\Desktop\files\bg5.jpg", @"C:\Users\Mykolas\Desktop\files\bg2.jpg" };
//            Util.AddFile(filePaths);
//        }
//
//        private void Button_Click_1(object sender, RoutedEventArgs e)
//        {
//            string[] filePaths = { @"C:\Users\Mykolas\Desktop\files\bg5.jpg", @"C:\Users\Mykolas\Desktop\files\bg2.jpg" };
//        }