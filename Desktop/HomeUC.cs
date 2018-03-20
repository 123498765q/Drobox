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
using System.IO;
using System.Linq;


namespace Desktop
{
    public partial class HomeUC : UserControl
    {
        public HomeUC()
        {
            InitializeComponent();
            this.panel1.AutoScroll = true;

            DirectoryInfo d = new DirectoryInfo(@"C:\Users\ASUS-pc\Desktop\Cloud");
            FileInfo[] Files = d.GetFiles();
            string str = "";
            Point newLoc = new Point(50, 50);
          
            string[] t;
            int i = 0;
            int RowCount = 3;

            //foreach (FileInfo file in Files)
            //{
            //    str = str + ", " + file.Name;
            //    int column = 0;
            //    for (; column < RowCount; column++)
            //    {
            //        string fileExt = System.IO.Path.GetExtension(file.ToString());
            //        Button button = new Button();
            //        button.Text = "valio";//Files[i].ToString();
            //        //button.Left = column * 100;
            //        button.Top = column * 120;
            //        button.Width = 50;
            //        button.Height = 50;
            //        button.Location = new Point(column * 80, i * 120);
            //        button.TextImageRelation = TextImageRelation.ImageAboveText;
            //        button.TabStop = false;
            //        button.FlatStyle = FlatStyle.Flat;
            //        button.FlatAppearance.BorderSize = 0;
            //        //button.TextAlign =
            //        button.Click += Button_Click;

            //        //if (fileExt == ".txt")
            //        //{
            //        //    button.Image = Properties.Resources.txt;
            //        //}
            //        //else if (fileExt == ".png")
            //        //{
            //        //    button.Image = Image.FromFile(@"C:\Users\ASUS-pc\Desktop\Cloud\dalintis.png");
            //        //}
            //        panel1.Controls.Add(button);
            //    }
            //    i++;
            //}
            
            for (int y = 0; y < Files.Length; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    string fileExt = System.IO.Path.GetExtension(Files[i].ToString());
                    Button button = new Button();
                    button.Text = Files[i].ToString();
                    button.Width = 75;
                    button.Height = 50;
                    button.Location = new Point(x * 80, y * 20);
                    panel1.Controls.Add(button);
                    button.Click += Button_Click;
                    if (fileExt == ".txt")
                    {
                        button.Image = Properties.Resources.txt;
                    }
                    else if (fileExt == ".png")
                    {
                        //button.Image = Image.FromFile(@"C:\Users\ASUS-pc\Desktop\Cloud\dalintis.png");
                    }
                    i++;
                }
                y += 3;   
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {

            string failas = (sender as Button).Text;
            System.Diagnostics.Process.Start(@"C:\Users\ASUS-pc\Desktop\Cloud\" + failas);
        }

        private void Panel1_Scroll(object sender, ScrollEventArgs e)
        {
            addFile.Location = new Point(662 + this.AutoScrollPosition.X, 316 + this.AutoScrollPosition.Y);
        }

        private void addFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fDialog = new OpenFileDialog();
            if (fDialog.ShowDialog() != DialogResult.OK)
                return;
            System.IO.FileInfo fInfo = new System.IO.FileInfo(fDialog.FileName);
            string strFileName = fInfo.Name;
            string strFilePath = fInfo.DirectoryName;
            string targetPath = @"C:\Users\ASUS-pc\Desktop\Cloud";
            if (!System.IO.Directory.Exists(targetPath))
                {
                    System.IO.Directory.CreateDirectory(targetPath);
                }
                System.IO.File.Copy(strFilePath + '\\' + strFileName, targetPath + '\\' + strFileName, true);
                
            
        }
        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            string strFilePath = FileList[0];
            string fileName = Path.GetFileName(strFilePath);
            string targetPath = @"C:\Users\ASUS-pc\Desktop\Cloud";
            if (!fileName.Contains("."))
            {
                CloneDirectory(strFilePath, targetPath);
            }
            else
            {
                if (!System.IO.Directory.Exists(targetPath))
                {
                    System.IO.Directory.CreateDirectory(targetPath);
                }
                System.IO.File.Copy(strFilePath, targetPath + '\\' + fileName, true);
            }
  
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private static void CloneDirectory(string root, string dest)
        {
            foreach (var directory in Directory.GetDirectories(root))
            {
                string dirName = Path.GetFileName(directory);
                if (!Directory.Exists(Path.Combine(dest, dirName)))
                {
                    Directory.CreateDirectory(Path.Combine(dest, dirName));
                }
                CloneDirectory(directory, Path.Combine(dest, dirName));
            }
            foreach (var file in Directory.GetFiles(root))
            {
                File.Copy(file, Path.Combine(dest, Path.GetFileName(file)));
            }
        }
    }
    }

