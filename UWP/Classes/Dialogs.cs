﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace UWP.Classes
{
    class Dialogs
    {
        public async void DisplayLogoutDialog(Frame frame)
        {
            ContentDialog LogoutDialog = new ContentDialog
            {
                Title = "Logout",
                Content = "Are you sure want to log out?",
                PrimaryButtonText = "Yes",
                CloseButtonText = "No"
            };

            ContentDialogResult result = await LogoutDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                frame.Navigate(typeof(Login));
            }
            else
            {

            }
        }

        public async void ExitCamera()
        {
            ContentDialog ExitCamera = new ContentDialog
            {
                Title = "Close camera",
                Content = "Are you sure want to close camera?",
                PrimaryButtonText = "Yes",
                CloseButtonText = "No"
            };

            ContentDialogResult result = await ExitCamera.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                
            }
            else
            {

            }
        }

    }
}
