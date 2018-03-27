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
        public UserScreen()
        {
            this.InitializeComponent();     
            profilePicture.Source = new BitmapImage(new Uri(App.picture));
            Name.Text = App.name;
            FirstName.Text = App.given_name;
            LastName.Text = App.family_name;
            if (App.gender != null)
            {
                Gender.Text = App.gender;
            }
            else
            {
                Gender.Text = "unknown";
            }
            if (App.locale != null)
            {
                Location.Text = App.locale;
            }
            else
            {
                Location.Text = "unknown";
            }
            Id.Text = App.sub;
           
        }
       
    }
}
