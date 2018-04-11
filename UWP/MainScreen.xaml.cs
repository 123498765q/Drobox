using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UWP.Classes;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainScreen : Page
    {
        public MainScreen()
        {
            this.InitializeComponent();
            /*var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.BackgroundColor = Windows.UI.Colors.DarkRed;*/
            //ContentFrame.Navigate(typeof(MainPage));
            
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                ContentFrame.Navigate(typeof(SettingsPage));
                NavView.Header = "Settings";

            }
            else
            {
                NavigationViewItem item = args.SelectedItem as NavigationViewItem;
                switch (item.Tag.ToString())
                {
                    case "home":
                        ContentFrame.Navigate(typeof(MainPage));
                        NavView.Header = "Home";
                        break;
                    case "user":
                        ContentFrame.Navigate(typeof(UserScreen));
                        NavView.Header = "User";
                        break;
                    case "photos":
                        ContentFrame.Navigate(typeof(PhotosScreen));
                        NavView.Header = "Photos";
                        break;
                    case "shared":
                        ContentFrame.Navigate(typeof(SharedScreen));
                        NavView.Header = "Shared";
                        break;
                    case "login":
                        break;

                }
            }
            
        }

        private async void addFile_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add("*");
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                System.IO.FileInfo fInfo = new System.IO.FileInfo(file.Name);
                string strFileName = fInfo.Name;
                string strFilePath = file.Path;
                string targetPath = @"C:\";
                if (!System.IO.Directory.Exists(targetPath))
                {
                    System.IO.Directory.CreateDirectory(targetPath);
                }
                System.IO.File.Copy(strFilePath , targetPath + '\\' + strFileName, true);
                this.NavView.Header = "Picked photo: " + file.Name;
            }
            else
            {
                this.NavView.Header = "Operation cancelled.";
            }
        }

        private void logOut_Click(object sender, RoutedEventArgs e)
        {
            Dialogs logout = new Dialogs();
            logout.DisplayLogoutDialog(Frame);
        }

       
    }
}
