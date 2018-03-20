using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop
{
    public partial class RedesignedMainPage : Form
    {
        
        public RedesignedMainPage()
        {
            InitializeComponent();
            SidePanel.Height = HomeButton.Height;
            SidePanel.Top = HomeButton.Top;
            homeUC1.BringToFront();
            circularProgressBar1.Minimum = 0;
            circularProgressBar1.Maximum = 100;
            checkProgressBarPercents();
            Rounded();
        }

        static int maxVieta = 5;
        public void checkProgressBarPercents()
        {
            long length = Directory.GetFiles(@"C:\Users\ASUS-pc\Desktop\Cloud", "*", SearchOption.AllDirectories).Sum(t => (new FileInfo(t).Length));
            double s = (length / 1024f) / 1024f;
            double skaiciavimai = (s * 100 / maxVieta);
            circularProgressBar1.Value = Convert.ToInt32(skaiciavimai);
            valueLabel.Text = circularProgressBar1.Value + "%";
        }

        Point lastClick;
        private void HomeButton_Click(object sender, EventArgs e)
        {
            checkProgressBarPercents();
            SidePanel.Height = HomeButton.Height;
            SidePanel.Top = HomeButton.Top;
            NavigationLabel.Text = "Home";
            homeUC1.BringToFront();
            valueLabel.Text = circularProgressBar1.Value + "%";
        }

        private void UserButton_Click(object sender, EventArgs e)
        {
            checkProgressBarPercents();
            SidePanel.Height = UserButton.Height;
            SidePanel.Top = UserButton.Top;
            NavigationLabel.Text = "User";
            userUC1.BringToFront();
            //circularProgressBar1.Style = ProgressBarStyle.Blocks;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            checkProgressBarPercents();
            SidePanel.Height = AddButton.Height;
            SidePanel.Top = AddButton.Top;
            //NavigationLabel.Text = "Add";
            //addUC1.BringToFront();
        }

        private void ShareButton_Click(object sender, EventArgs e)
        {
            checkProgressBarPercents();
            SidePanel.Height = ShareButton.Height;
            SidePanel.Top = ShareButton.Top;
            //NavigationLabel.Text = "Share";
            //shareUC1.BringToFront();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            checkProgressBarPercents();
            SidePanel.Height = SettingsButton.Height;
            SidePanel.Top = SettingsButton.Top;
            NavigationLabel.Text = "Settings";
            settingsUC2.BringToFront();
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            lastClick = new Point(e.X, e.Y);
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) 
            {
                this.Left += e.X - lastClick.X;
                this.Top += e.Y - lastClick.Y;
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            this.WindowState = FormWindowState.Minimized;
        }

        private void RedesignedMainPage_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            panel1.Visible = true;
        }
        public void Rounded()
        {
            Rectangle r = new Rectangle(0, 0, pictureBox2.Width, pictureBox2.Height);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            int d = 55;
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            pictureBox2.Region = new Region(gp);
        }
    }
}
