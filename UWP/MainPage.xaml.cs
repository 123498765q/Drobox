using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using UWP.Classes;

namespace UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        
        public MainPage()
        {
            this.InitializeComponent();
            //GetFilesAsync();
            GetFoldersAsync();


        }
        
        private async void GetFilesAsync()
        {
            Windows.Storage.StorageFolder picturesFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            StringBuilder outputText = new StringBuilder();

            IReadOnlyList<StorageFile> fileList = await picturesFolder.GetFilesAsync();
            int i = 5;
            outputText.AppendLine("Files:");
            foreach (StorageFile file in fileList)
            {
                //outputText.Append(file.Name + "\n");
                Button button = new Button();
                button.Content = file.Name;
                button.Width = 200;
                button.Height = 30;
                button.Margin = new Thickness(30, 50 *i, 0, 100);
                //button.Padding = new Thickness(50, 50, 50, 50);
                button.Click += file_Click;
                RP.Children.Add(button);
                i++;
            }

            IReadOnlyList<StorageFolder> folderList = await picturesFolder.GetFoldersAsync();

            outputText.AppendLine("Folders:");
            foreach (StorageFolder folder in folderList)
            {
                outputText.Append(folder.DisplayName + "\n");
            }
            //tbout.Text = outputText.ToString();
            
        }
        private async void GetFoldersAsync()
        {
            Windows.Storage.StorageFolder picturesFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            //var picturesFolder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("");

            StringBuilder outputText = new StringBuilder();

            IReadOnlyList<IStorageItem> itemsList = await picturesFolder.GetItemsAsync();
            int i = 0;
            int j = 0;
            int k = 0;
            foreach (var item in itemsList)
            {
                if (item is StorageFolder)
                {
                    Button button = new Button();
                    button.Content = item.Name;
                    //ImageBrush brush1 = new ImageBrush();
                    //brush1.ImageSource = new BitmapImage(new Uri("ms-appx:///assets/folder.png"));
                    //button.Background = brush1;
                    button.Width = 120;
                    button.Height = 120;
                    button.Margin = new Thickness(0, 140 * i, 0, 100);
                    button.VerticalContentAlignment = VerticalAlignment.Bottom;
                    button.FontSize = 11;
                    button.Click += folder_Click;
                    RP.Children.Add(button);
                    i++;
                    

                }
                else
                {

                    Button button = new Button();
                    button.Content = item.Name;

                    button.FontSize = 11;
                    button.VerticalContentAlignment = VerticalAlignment.Bottom;
                    button.Width = 100;
                    button.Height = 100;
                    
                    button.Click += file_Click;
                    CP.Children.Add(button);
                    k++;
                    if (k == 2)
                    {
                        button.Margin = new Thickness(100 * k, 120 * j, 50, 50);
                        k = 0;
                        j++;
                    }
                    else
                    {
                        button.Margin = new Thickness(20 * k, 120 * j, 50, 50);
                    }
                    

                    //outputText.Append(item.Name + "\n");
                }
            }
            //tbout.Text = outputText.ToString();
        }

        private void folder_Click(object sender, RoutedEventArgs e)
        {
            string folderis = (sender as Button).Content.ToString();
            Kviesk_Folderi(folderis);
        }

        private void file_Click(object sender, RoutedEventArgs e)
        {
            string failas = (sender as Button).Content.ToString();
            Kviesk_Faila(failas);

        }
        private async void Kviesk_Faila(string failas)
        {
            StorageFile file = await Package.Current.InstalledLocation.GetFileAsync(failas);
            await Launcher.LaunchFileAsync(file);
        }
        private async void Kviesk_Folderi(string folderis)
        {
            StorageFolder folder = await Package.Current.InstalledLocation.GetFolderAsync(folderis);
            await Launcher.LaunchFolderAsync(folder);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string[] filePaths = {@"C:\Users\Mykolas\Desktop\files\bg5.jpg", @"C:\Users\Mykolas\Desktop\files\bg2.jpg" };
            Util.AddFile(filePaths);
        }
    }
}
