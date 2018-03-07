using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
        }

        Point lastClick;
        private void HomeButton_Click(object sender, EventArgs e)
        {
            SidePanel.Height = HomeButton.Height;
            SidePanel.Top = HomeButton.Top;
            NavigationLabel.Text = "Home";
            homeUC1.BringToFront();
        }

        private void UserButton_Click(object sender, EventArgs e)
        {
            SidePanel.Height = UserButton.Height;
            SidePanel.Top = UserButton.Top;
            NavigationLabel.Text = "User";
            userUC1.BringToFront();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            SidePanel.Height = AddButton.Height;
            SidePanel.Top = AddButton.Top;
            NavigationLabel.Text = "Add";
            addUC1.BringToFront();
        }

        private void ShareButton_Click(object sender, EventArgs e)
        {
            SidePanel.Height = ShareButton.Height;
            SidePanel.Top = ShareButton.Top;
            NavigationLabel.Text = "Share";
            shareUC1.BringToFront();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
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
            this.WindowState = FormWindowState.Minimized;
        }

    }
}
