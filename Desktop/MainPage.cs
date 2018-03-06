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
        
    }
}
