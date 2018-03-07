using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            DialogResult result = this.openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                //this.textBox1.Text = this.openFileDialog1.FileName;
            }
        }
    }
}
