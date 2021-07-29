using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Gla
{
    public partial class Form1 : Form
    {
        private int curIndexImage = 0;
        private string[] files;
        private static string[] extensions = { "jpg", "jpeg", "png", "gif", "bmp" };
        private int coordX;
        private int coordY;

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ModifierKeys == Keys.Shift && e.Delta == 120) // zoom in
            {
                pictureBox1.Size = new Size(pictureBox1.Width + 15, pictureBox1.Height + 15);
                pictureBox1.Left = (this.ClientSize.Width - pictureBox1.Width) / 2;
                pictureBox1.Top = (this.ClientSize.Height - pictureBox1.Height) / 2;
            }
            else if (ModifierKeys == Keys.Shift && e.Delta == -120) // zoom out
            {
                pictureBox1.Size = new Size(pictureBox1.Width - 15, pictureBox1.Height - 15);
                pictureBox1.Left = (this.ClientSize.Width - pictureBox1.Width) / 2;
                pictureBox1.Top = (this.ClientSize.Height - pictureBox1.Height) / 2;
            }
            else if (ModifierKeys == Keys.Control && e.Delta == 120 && pictureBox1.Image != null) // show next
            {
                if (curIndexImage == files.Length - 1) { curIndexImage = 0; }
                else { curIndexImage++; }
                pictureBox1.Load(files[curIndexImage]);
                this.Text = "Glance at " + files[curIndexImage];
            }
            else if (ModifierKeys == Keys.Control && e.Delta == -120 && pictureBox1.Image != null) // show prev
            {
                if (curIndexImage == 0) { curIndexImage = files.Length - 1; }
                else { curIndexImage--; }
                pictureBox1.Load(files[curIndexImage]);
                this.Text = "Glance at " + files[curIndexImage];
            }
            else if (ModifierKeys == Keys.Alt && e.Delta == 120) // rotate forward
            {

            }
            else if (ModifierKeys == Keys.Alt && e.Delta == -120) // rotate backward
            {

            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    files = GetFiles(Path.GetDirectoryName(openFileDialog1.FileName));
                    pictureBox1.Load(openFileDialog1.FileName);
                    this.Text = "Glance at " + Path.GetFullPath(openFileDialog1.FileName);
                }
                catch
                {
                    pictureBox1.Image = null;
                    this.Text = "Glance";
                    MessageBox.Show("Неверный формат файла.");
                }
            }
        }

        private static String[] GetFiles(String folder)
        {
            List<String> images = new List<String>();
            foreach (var filter in extensions)
            {
                images.AddRange(Directory.GetFiles(folder, String.Format("*.{0}", filter)));
            }
            return images.ToArray();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Width = this.ClientSize.Width;
            pictureBox1.Height = this.ClientSize.Height;
            var args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                var img = args[args.Length - 1];
                if (File.Exists(img))
                {
                    pictureBox1.Load(img);
                    this.Text = "Glance at " + Path.GetFullPath(img);
                    files = GetFiles(Path.GetDirectoryName(img));
                    foreach (var i in files)
                    {
                        if (i != Path.GetFullPath(img)) { curIndexImage++; }
                    }
                }
            }
            else { files = GetFiles(Directory.GetCurrentDirectory()); }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            pictureBox1.Width = this.ClientSize.Width;
            pictureBox1.Height = this.ClientSize.Height;
            pictureBox1.Left = (this.ClientSize.Width - pictureBox1.Width) / 2;
            pictureBox1.Top = (this.ClientSize.Height - pictureBox1.Height) / 2;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            coordX = e.X;
            coordY = e.Y;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                pictureBox1.Location = new Point(pictureBox1.Location.X + e.X - coordX, pictureBox1.Location.Y + e.Y - coordY);
            }
        }
    }
}
