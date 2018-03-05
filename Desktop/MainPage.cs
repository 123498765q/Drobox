using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop
{
    public partial class MainPage : Form
    {
        public MainPage()
        {
            InitializeComponent();

        }





        private void MainPage_MouseCaptureChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("asd");
        }
    }
}
