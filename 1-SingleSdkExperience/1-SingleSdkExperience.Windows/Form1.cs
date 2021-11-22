using System;
using System.Drawing;
using System.Windows.Forms;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;

namespace _1_SingleSdkExperience.Windows
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            MediaCapture captureManager = new MediaCapture();
            await captureManager.InitializeAsync();

            ImageEncodingProperties imgFormat = ImageEncodingProperties.CreateJpeg();
            StorageFile file = await KnownFolders.CameraRoll.CreateFileAsync("TestPhoto.jpg",
                                           CreationCollisionOption.GenerateUniqueName);

            await captureManager.CapturePhotoToStorageFileAsync(imgFormat, file);

            var image = Image.FromFile(file.Path);
            pictureBox1.Image = image;
        }
    }
}