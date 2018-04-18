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
using Windows.UI.Xaml.Navigation;
using UWP.Classes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SharedScreen : Page
    {
        private List<Files> Books;

        public SharedScreen()
        {
            this.InitializeComponent();
            Books = BookManager.GetBooks();
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var book = (Files)e.ClickedItem;
            ResultTextBlock.Text = "You Selected: " + book.Title;
        }
    }
}
