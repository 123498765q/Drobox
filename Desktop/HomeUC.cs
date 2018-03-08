using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Desktop
{
    public partial class HomeUC : UserControl
    {
        public HomeUC()
        {
            InitializeComponent();
            this.panel1.AutoScroll = true;
            /*for (int i = 0; i < 100; i++)
            {
                Label l = new Label();
                l.Text = "betkas";
                l.Location = new Point(10, i * 100);
                panel1.Controls.Add(l);
            }*/
 
        }

        private void addFile_Click(object sender, EventArgs e)
        {
            /*DialogResult result = this.openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
            }*/
            string fileName = "p.txt";
            string sourcePath = @"C:\Users\ASUS-pc\Desktop";
            string targetPath = @"C:\Users\ASUS-pc\Desktop\Cloud";

            // Use Path class to manipulate file and directory paths.
            string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
            string destFile = System.IO.Path.Combine(targetPath, fileName);

            // To copy a folder's contents to a new location:
            // Create a new target folder, if necessary.
            if (!System.IO.Directory.Exists(targetPath))
            {
                System.IO.Directory.CreateDirectory(targetPath);
            }

            // To copy a file to another location and 
            // overwrite the destination file if it already exists.
            System.IO.File.Copy(sourceFile, destFile, true);
        }
    }
}
