using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class UserScreen : Page
    {
        static int maxVieta = 3000;
       
        public UserScreen()
        {
            this.InitializeComponent();
            
            /*long length = Directory.GetFiles(@"C:\Users\ASUS-pc\Downloads", "*", SearchOption.AllDirectories).Sum(t => (new FileInfo(t).Length));
           double s = (length / 1024f) / 1024f;
           double skaiciavimai = (s * 100 / maxVieta);
           RadialProgressBarControl.Value = Convert.ToInt32(skaiciavimai);*/
            profilePicture.Source = new BitmapImage(new Uri(App.picture));
            Name.Text = App.name;
            FirstNameInfo.Text = App.given_name;
            LastName.Text = App.family_name;
            if (App.gender != null)
            {
                Gender.Text = App.gender;
            }
            else
            {
                Gender.Text = "Unknown";
            }
            if (App.locale != null)
            {
                
                if (App.locale == "lt")
                {
                    Location.Text = "Lithuania";
                }
                else if (App.locale == "en")
                {
                    Location.Text = "England";
                }
                else
                {
                    Location.Text = App.locale;
                }
            }
            else
            {
                Location.Text = "Unknown";
            }

        }

    }
}
