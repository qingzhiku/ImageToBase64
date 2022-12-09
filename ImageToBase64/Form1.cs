using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImageToBase64
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }











        public string ConvertImageToBase64(Image file)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.Save(memoryStream, file.RawFormat);
                byte[] imageBytes = memoryStream.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }


        public Image ConvertBase64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                ms.Write(imageBytes, 0, imageBytes.Length);
                return Image.FromStream(ms, true);
            }
        }



        private void toBase64Btn_Click(object sender, EventArgs e)
        {
            if (picImage.Image == null)
                return;

            using (Image image = picImage.Image.Clone() as Image)
            {
                txtBase64.Text = ConvertImageToBase64(image);
            }
        }

        private void toImageBtn_Click(object sender, EventArgs e)
        {
            picImage.Image = ConvertBase64ToImage(txtBase64.Text);
        }

        private void openBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Picture|*.gif;*.jpg;*.jpeg;*.bmp;*.jfif;*.png;|All|*.*";
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                var filename = openFileDialog.FileName;

                if (File.Exists(filename))
                    picImage.ImageLocation = filename;
            }
        }


    }
}
