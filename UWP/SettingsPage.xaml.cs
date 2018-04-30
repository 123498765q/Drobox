using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage.Search;
using Windows.Storage.FileProperties;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.Email;

using System.Net.Mail;
using System.Net;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 

    public sealed partial class SettingsPage : Page
    {

        public SettingsPage()
        {
            this.InitializeComponent();
            GetFileSize();
        }


        public async Task GetFileSize()
        {
            // Query all files in the folder. Make sure to add the CommonFileQuery
            // So that it goes through all sub-folders as well
            string root = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
            string path = root + @"\Cloud";
            StorageFolder folders = await StorageFolder.GetFolderFromPathAsync(path);

            // long folders = Directory.GetFiles(@"C:\Users\Mantas\source\repos\Drobox\UWP\bin\x86\Debug\AppX\Cloud", "*", SearchOption.AllDirectories).Sum(t => (new FileInfo(t).Length));

            // Await the query, then for each file create a new Task which gets the size
            var fileSizeTasks = (await folders.GetFilesAsync()).Select(async file => (await file.GetBasicPropertiesAsync()).Size);

            // Wait for all of these tasks to complete. WhenAll thankfully returns each result
            // as a whole list
            var sizes = await Task.WhenAll(fileSizeTasks);

            // Sum all of them up. You have to convert it to a long because Sum does not accept ulong.
            var folderSize = sizes.Sum(l => (long)l);
            Calculatesize(folderSize);
            textBlock.Text = "Used " + Calculatesize(folderSize) + " from 2 GB";

        }
                
        public string Calculatesize(double sizeInBytes)
        {
            const double terabyte = 1099511627776;
            const double gigabyte = 1073741824;
            const double megabyte = 1048576;
            const double kilobyte = 1024;

            string result;
            double theSize = 0;
            string units;

            if (sizeInBytes <= 0.1)
            {
                result = "0" + " " + "bytes";
                return result;
            }

            if (sizeInBytes >= terabyte)
            {
                theSize = sizeInBytes / terabyte;
                units = " TB";
            }
            else
            {
                if (sizeInBytes >= gigabyte)
                {
                    theSize = sizeInBytes / gigabyte;
                    units = " GB";
                }
                else
                {
                    if (sizeInBytes >= megabyte)
                    {
                        theSize = sizeInBytes / megabyte;
                        units = " MB";
                    }
                    else
                    {
                        if (sizeInBytes >= kilobyte)
                        {
                            theSize = sizeInBytes / kilobyte;
                            units = " KB";
                        }
                        else
                        {
                            theSize = sizeInBytes;
                            units = " bytes";
                        }
                    }
                }
            }

            if (units != "bytes")
            {
                result = theSize.ToString("0.00") + " " + units;
            }
            else
            {
                result = theSize.ToString("0.0") + " " + units;
            }
            return result;
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            string text = textBox2.Text;

            await sendEmailAsync(text, "inc.drobox@gmail.com");

        }

        public async Task sendEmailAsync(string text, string email)
        {
            if (String.IsNullOrEmpty(text) == true)
            {
                await new MessageDialog("Email text is empty!", "Error").ShowAsync();
            }
            else
            {
                var fromAddress = new MailAddress("inc.drobox@gmail.com", "From customer");
                var toAddress = new MailAddress(email, "For admin");
                string fromPassword = "drobox123";
                string subject = "Storage space";
                string body = text;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                    Timeout = 20000
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
            }
        }

//        private async void button_Click(object sender, RoutedEventArgs e)
//        {    
//            string text = textBox2.Text;
//            if (String.IsNullOrEmpty(text) == true)
//            {
//                await new MessageDialog("Email text is empty!", "Error").ShowAsync(); 
//            }
//            else
//            { 
//                var fromAddress = new MailAddress("inc.drobox@gmail.com", "From customer");
//                var toAddress = new MailAddress("inc.drobox@gmail.com", "For admin");
//                string fromPassword = "drobox123";
//                string subject = "Storage space";
//                string body = text;
//
//                var smtp = new SmtpClient
//                {
//                    Host = "smtp.gmail.com",
//                    Port = 587,
//                    EnableSsl = true,
//                    DeliveryMethod = SmtpDeliveryMethod.Network,
//                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
//                    Timeout = 20000
//                };
//                using (var message = new MailMessage(fromAddress, toAddress)
//                {
//                    Subject = subject,
//                    Body = body
//                })
//                {
//                    smtp.Send(message);
//                }
//            }
//        }   
    }
}

