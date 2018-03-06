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

        Point lastClick; //Holds where the Form was clicked


        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            lastClick = new Point(e.X, e.Y); //We'll need this for when the Form starts to move
        }
        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            //Point newLocation = new Point(e.X - lastE.X, e.Y - lastE.Y);
            if (e.Button == MouseButtons.Left) //Only when mouse is clicked
            {
                //Move the Form the same difference the mouse cursor moved;
                this.Left += e.X - lastClick.X;
                this.Top += e.Y - lastClick.Y;
            }
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void userButton_Click(object sender, EventArgs e)
        {
            navigationLabel.Text = "User";
            CheckState();
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            navigationLabel.Text = "Home";
            CheckState();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            navigationLabel.Text = "Add";
            CheckState();
        }

        private void shareButton_Click(object sender, EventArgs e)
        {
            navigationLabel.Text = "Share";
            CheckState();
        }

        private void optionsButton_Click(object sender, EventArgs e)
        {
            navigationLabel.Text = "Settings";
            CheckState();
        }

        private void CheckState()
        {
            if (navigationLabel.Text == "User")
            {
                userButton.BackColor = (Color.FromArgb(255, 195, 106, 55));
            }
            else
            {
                userButton.BackColor = Color.Transparent;
            }
            if (navigationLabel.Text == "Home")
            {
                homeButton.BackColor = (Color.FromArgb(255, 195, 106, 55));
            }
            else
            {
                homeButton.BackColor = Color.Transparent;
            }
            if (navigationLabel.Text == "Add")
            {
                addButton.BackColor = (Color.FromArgb(255, 195, 106, 55));
            }
            else
            {
                addButton.BackColor = Color.Transparent;
            }
            if (navigationLabel.Text == "Share")
            {
                shareButton.BackColor = (Color.FromArgb(255, 195, 106, 55));
            }
            else
            {
                shareButton.BackColor = Color.Transparent;
            }
            if (navigationLabel.Text == "Settings")
            {
                optionsButton.BackColor = (Color.FromArgb(255, 195, 106, 55));
            }
            else
            {
                optionsButton.BackColor = Color.Transparent;
            }


        }

    }
}
