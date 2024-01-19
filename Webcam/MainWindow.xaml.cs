using AForge.Video;
using AForge.Video.DirectShow;
using System.IO;
using Firebase.Storage;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Diagnostics;

namespace Webcam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        private Bitmap currentFrame;
        
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            Closing += MainWindow_Closing;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoDevices.Count > 0)
            {
                videoSource = new VideoCaptureDevice((videoDevices[0].MonikerString));
                videoSource.NewFrame += VideoSource_NewFrame;
                videoSource.Start();
            }
            else
            {
                System.Windows.MessageBox.Show("No video devices found.");
            }
        }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            webcamImage.Dispatcher.Invoke(() =>
            {
                webcamImage.Source = ToBitmapImage(eventArgs.Frame);
            });
        }

        private System.Windows.Media.Imaging.BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;

                System.Windows.Media.Imaging.BitmapImage bitmapImage = new System.Windows.Media.Imaging.BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.Stop();
            }
        }

        private void btnBrowser_Click(object sender, RoutedEventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();

                // Kiểm tra xem người dùng đã chọn một thư mục chưa
                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    // Hiển thị đường dẫn thư mục trong TextBox
                    txtSave.Text = folderBrowserDialog.SelectedPath;
                    ImageSource icon = GetImage("image.png");
                    LoadView(icon, txtSave.Text);

                }
            }
        }


        private void btnCapture_Click(object sender, RoutedEventArgs e)
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                // Dừng việc chụp hình tạm thời

                // Kiểm tra xem folder đã được chọn chưa
                if (!string.IsNullOrWhiteSpace(txtSave.Text) && webcamImage.Source is BitmapSource bitmapSource)
                {
                    // Tạo đường dẫn lưu ảnh
                    string fileName = $"picture_{DateTime.Now:yyyyMMddHHmmss}.png";
                    string filePath = System.IO.Path.Combine(txtSave.Text, fileName);

                    // Lưu ảnh vào thư mục đã chọn
                    SaveImage(filePath, bitmapSource);
                    ImageSource icon = GetImage("image.png");
                    LoadView(icon, txtSave.Text);
                    System.Windows.MessageBox.Show($"Picture saved to {filePath}");
                }
                else
                {
                    System.Windows.MessageBox.Show("Please select a folder before capturing a snapshot.");
                }

                // Bắt đầu lại việc chụp hình từ webcam
            }
            else
            {
                System.Windows.MessageBox.Show("Webcam is not running.");
            }
        }
        private void SaveImage(string filePath, BitmapSource bitmapSource)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                encoder.Save(fileStream);
            }
        }
        private void LoadView(ImageSource icon, String path)
        {
            listView.Items.Clear();
            string[] filesAndFolders = Directory.GetFileSystemEntries(path);
            foreach (string entry in filesAndFolders)
            {
                listView.Items.Add(new FileSystemItem { Icon = icon, Path = entry.Substring(path.Length + 1) });

            }
        }
        private void Open_event(object sender, RoutedEventArgs e)
        {
            FileSystemItem selectedItem = (FileSystemItem)listView.SelectedItem;
            string path = txtSave.Text + "\\" + selectedItem.Path;
            ImageBox imageBox = new ImageBox
            {
                fileName = selectedItem.Path,
                pathFile = txtSave.Text,
                listView = listView,
                
            };
            imageBox.ShowDialog();
        }

        private ImageSource GetImage(string path)
        {
            return new BitmapImage(new Uri("D:/Dowloads/" + path));
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            FileSystemItem selectedItem = (FileSystemItem)listView.SelectedItem;
            string path = txtSave.Text + "/" + selectedItem.Path;
            MessageBoxResult message = System.Windows.MessageBox.Show("Are you delete ?", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (message == MessageBoxResult.OK)
            {
                try
                {
                    File.Delete(path);
                    ImageSource icon = GetImage("image.png");
                    LoadView(icon, txtSave.Text);
                }
                catch (Exception ex)
                {
                    ImageSource icon = GetImage("image.png");
                    LoadView(icon, txtSave.Text);
                }

            }
        }

        private async void UploadBtn_Click(object sender, RoutedEventArgs e)
        {
            FileSystemItem selectedItem = (FileSystemItem)listView.SelectedItem;
            string path = txtSave.Text + "/" + selectedItem.Path;


            using (FileStream fileStream = System.IO.File.OpenRead(path))
            {

                var task = new FirebaseStorage("uploadimage-839f3.appspot.com")
                              .Child("images")
                              .Child(Path.GetFileName(path))
                              .PutAsync(fileStream);

                string downloadUrl = await task;
                System.Windows.MessageBox.Show("Uploaded successfully");
            }

        }
    }

}



