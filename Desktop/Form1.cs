
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
         
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.Navigate(Application.StartupPath + "http://localhost:57769/GoogleLogin/index.html");

        }

        //private void button1_Click(object sender, EventArgs e)
        //{

        //    ProcessStartInfo startInfo = new ProcessStartInfo("http://localhost:57769/GoogleLogin/index.html");
        //    Process.Start(startInfo);
        //    System.Diagnostics.Process.Start("http://localhost:57769/GoogleLogin/index.html");
        //}

    }
}
