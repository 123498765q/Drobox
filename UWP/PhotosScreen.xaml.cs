using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices.WindowsRuntime;
using UWP.Classes;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PhotosScreen : Page
    {
        private string PhotoPath = "";

        public PhotosScreen()
        {
            this.InitializeComponent();
            komentaras.Visibility = Visibility.Collapsed;
            Save.Visibility = Visibility.Collapsed;
            //Photo.Visibility = Visibility.Collapsed;
            locationCB.Visibility = Visibility.Collapsed;
            lokacija.Visibility = Visibility.Collapsed;
            makePhoto();
            GPS gps = new GPS();
            string ip = gps.GetPublicIP();
            //string ip2 = gps.GetLocalIPAddress();
            //string ip = gps.GetLocalIPv4(NetworkInterfaceType.Ethernet);
            string location = gps.GetUserCountryByIp("188.69.192.58");
            lokacija.Text = location;
        }

        private async void Photo_Click(object sender, RoutedEventArgs e)
        {
            makePhoto();
        }

        
        public async void makePhoto()
        {
            CameraCaptureUI dialog = new CameraCaptureUI();
            dialog.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            dialog.PhotoSettings.CroppedSizeInPixels = new Size(700, 700);
            StorageFile file = await dialog.CaptureFileAsync(CameraCaptureUIMode.Photo);


            if (file != null)
            {
                BitmapImage bitmapImage = new BitmapImage();
                using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read))
                {
                    bitmapImage.SetSource(fileStream);

                }

                PhotoPath = file.Path;
                CapturePhoto.Source = bitmapImage;
                komentaras.Visibility = Visibility.Visible;
                Save.Visibility = Visibility.Visible;
                //Photo.Visibility = Visibility.Visible;
                locationCB.Visibility = Visibility.Visible;

            }
        }

        private void SavePhotoBtn(object sender, RoutedEventArgs e)
        {
            var imageName = PhotoPath.Split('\\').Last(); // "C:\\Users\\Mykolas\\AppData\\Local\\Packages\\fc57b5b6-58fe-4d9c-b7f6-cdcead7ef616_866atarb4ygr6\\TempState\\CCapture (19).jpg"
            var path = FileUtil.BaseApiPath + App.sub + "\\" + App.given_name + "\\" + imageName;

            string[] selectedPaths = new string[1];
            selectedPaths[0] = PhotoPath;
            FileUtil.AddFile(selectedPaths, App.given_name + "\\" + imageName);

            ImageInfo info = new ImageInfo(path, App.sub, lokacija.Text, komentaras.Text);
            FileUtil.PostImageInfo(info);
        }

        private void locationCB_Checked(object sender, RoutedEventArgs e)
        {
            lokacija.Visibility = Visibility.Visible;
        }

        private void locationCB_Unchecked(object sender, RoutedEventArgs e)
        {
            lokacija.Visibility = Visibility.Collapsed;
        }
    }
}
