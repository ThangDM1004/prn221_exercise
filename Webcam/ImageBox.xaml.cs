using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Webcam
{
    /// <summary>
    /// Interaction logic for ImageBox.xaml
    /// </summary>
    public partial class ImageBox : Window
    {
        public string pathFile { get; set; }
        public string fileName { get; set; }
        public int number { get; set; }
        public ListView listView { get; set; }
        BitmapImage bitmap = new BitmapImage();
        public ImageBox()
        {
            InitializeComponent();
            Loaded += LoadImage;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            string path = pathFile + "/" + fileName;
            MessageBoxResult message = System.Windows.MessageBox.Show("Are you delete ?", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (message == MessageBoxResult.OK)
            {

                ImageSource icon = GetImage("image.png");
                LoadView(icon, pathFile, fileName);
                imageBox.Source = null;
                try
                {
                    this.Loaded += null;
                    File.Delete(path);
                }
                catch (Exception ex)
                {

                }
                this.Close();

            }
        }

        private async void UploadBtn_Click(object sender, RoutedEventArgs e)
        {

            string path = pathFile + "/" + fileName;

            using (FileStream fileStream = System.IO.File.OpenRead(path))
            {
                var task = new FirebaseStorage("uploadimage-839f3.appspot.com")
                              .Child("images")
                              .Child(System.IO.Path.GetFileName(path))
                              .PutAsync(fileStream);

                string downloadUrl = await task;
                System.Windows.MessageBox.Show("Uploaded successfully !!!");

            }
        }
        private void LoadImage(object sender, RoutedEventArgs e)
        {
            string imageUrl = pathFile + "/" + fileName;
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imageUrl);
            bitmap.EndInit();
            imageBox.Source = bitmap;
        }

        private ImageSource GetImage(string path)
        {
            return new BitmapImage(new Uri("D:/Dowloads/" + path));
        }

        private void LoadView(ImageSource icon, String path, String filename)
        {
            listView.Items.Clear();
            string[] filesAndFolders = Directory.GetFileSystemEntries(path);
            foreach (string entry in filesAndFolders)
            {
                if (entry.Substring(path.Length + 1) != fileName)
                {
                    listView.Items.Add(new FileSystemItem { Icon = icon, Path = entry.Substring(path.Length + 1) });
                }
            }
        }
    }
}
