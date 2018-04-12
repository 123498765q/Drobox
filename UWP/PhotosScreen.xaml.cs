using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        public PhotosScreen()
        {
            this.InitializeComponent();
            komentaras.Visibility = Visibility.Collapsed;
            Save.Visibility = Visibility.Collapsed;

        }

       
        private async void Photo_Click(object sender, RoutedEventArgs e)
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
              

                CapturePhoto.Source = bitmapImage;
                komentaras.Visibility = Visibility.Visible;
                Save.Visibility = Visibility.Visible;
                 


            }
            else
            {
                var dialog1 = new MessageDialog("Are you sure you want to quit?");
                UICommand okBtn = new UICommand("Yes");
                dialog1.Commands.Add(okBtn);
              

                dialog1.ShowAsync();
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
