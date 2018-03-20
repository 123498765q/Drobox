using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace Desktop
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        private ChromiumWebBrowser chrome;

        private void Form1_Load(object sender, EventArgs e)
        {
            CefSettings settings = new CefSettings();
            Cef.Initialize(settings);
            string url = "http://localhost:57769/GoogleLogin/index.html";
            chrome = new ChromiumWebBrowser(url);
            this.panel1.Controls.Add(chrome);
            chrome.Dock = DockStyle.Fill;
            
        }

        
    }
}
