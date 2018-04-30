using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Data.Html;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace UWP.Classes
{
    static class DesignUtil
    {
        private static string path = "";
        private static string fName = "";
        private static string popupTitle = "";
        public static string SelectExtentionImage(string fileExtention)
        {
            switch (fileExtention)
            {
                case "png":
                    return "Assets/FileTypeImg/png.png";
                case "doc":
                    return "Assets/FileTypeImg/doc.png";
                case "exe":
                    return "Assets/FileTypeImg/exe.png";
                case "jpg":
                    return "Assets/FileTypeImg/jpg.png";
                case "mp3":
                    return "Assets/FileTypeImg/mp3.png";
                case "mp4":
                    return "Assets/FileTypeImg/mp4.png";
                case "pdf":
                    return "Assets/FileTypeImg/pdf.png";
                case "txt":
                    return "Assets/FileTypeImg/txt.png";
                case "xls":
                    return "Assets/FileTypeImg/xls.png";
                case "zip":
                case "rar":
                    return "Assets/FileTypeImg/doc.png";
            }
            return "Assets/FileTypeImg/folder.png";
        }

        public static string SetFileType(string fileExtention)
        {
            switch (fileExtention)
            {
                case "png":
                case "doc":
                case "exe":
                case "jpg":
                case "mp3":
                case "mp4":
                case "pdf":
                case "txt":
                case "xls":
                case "zip":
                case "rar":
                    return fileExtention;
            }
            return "folder";
        }

        //        public static async Task<string> InputTextDialogAsync(string title, string folderPath = "")
        //        {
        //            path = folderPath;
        //            TextBox inputTextBox = new TextBox();
        //            inputTextBox.AcceptsReturn = false;
        //            inputTextBox.Height = 32;
        //
        //            TextBlock output = new TextBlock();
        //            output.Text = "asd";
        //            
        //
        //            ContentDialog dialog = new ContentDialog();
        //            dialog.Content = inputTextBox;
        //            dialog.Title = title;
        //            dialog.IsSecondaryButtonEnabled = true;
        //            dialog.PrimaryButtonText = "Ok";
        //            dialog.SecondaryButtonText = "Cancel";
        //            dialog.PrimaryButtonClick += DialogOnPrimaryButtonClick;
        //            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
        //            {
        //                fName = inputTextBox.Text;
        //                return inputTextBox.Text;
        //            }
        //            return "";
        //        }

        public static async Task<string> InputTextDialogAsync(string title, string folderPath = "")
        {
            path = folderPath;
            popupTitle = title;

            TextBox inputTextBox = new TextBox();
            inputTextBox.AcceptsReturn = false;
            inputTextBox.Height = 32;
            TextBox email = new TextBox();
            email.AcceptsReturn = false;
            email.Height = 32;
            ContentDialog dialog = new ContentDialog();
            dialog.Content = inputTextBox;
            dialog.Title = title;
            dialog.IsSecondaryButtonEnabled = true;
            dialog.PrimaryButtonText = "Ok";
            dialog.SecondaryButtonText = "Cancel";
            dialog.PrimaryButtonClick += DialogOnPrimaryButtonClick;
            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
            {
                fName = inputTextBox.Text;
                return inputTextBox.Text;
            }
            else
                return "";
        }

        private async static void DialogOnPrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

            if (popupTitle == "Folder Name:")
            {
                var dialog = new MessageDialog("Link");
                dialog.Content = "Share link with your friends!";
                dialog.Commands.Add(new UICommand {Label = "Ok", Id = 0});
                var res = await dialog.ShowAsync();

                if ((int)res.Id == 0)
                {
                    var dataPackage = new DataPackage();
                    dataPackage.SetText(path + fName);

                    Clipboard.SetContent(dataPackage);
                }
            }
        }


    }
}
