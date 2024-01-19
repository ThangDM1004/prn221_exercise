using System;
using System.Collections.Generic;
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

namespace BT2
{
    /// <summary>
    /// Interaction logic for CreatePopup.xaml
    /// </summary>
    public partial class CreatePopup : Window
    {
        public String folderPath { get;set; }
        public ListView listView { get; set; }
        public CreatePopup()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string path = folderPath + "/" + folderName.Content;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                LoadView(folderPath);
                this.Close();
            }
            else
            {
                System.Windows.MessageBox.Show("Folder exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void LoadView(String path)
        {
            listView.Items.Clear();
            string[] filesAndFolders = Directory.GetFileSystemEntries(path);


            foreach (string entry in filesAndFolders)
            {
                if ((File.GetAttributes(entry) & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    ImageSource icon = GetImage("folder.jpg");
                    listView.Items.Add(new FileSystemItem { Icon = icon, Path = entry.Substring(path.Length + 1) });
                }

            }
            foreach (string entry in filesAndFolders)
            {
                if ((File.GetAttributes(entry) & FileAttributes.Directory) != FileAttributes.Directory)
                {
                    ImageSource icon = GetImage("file.png");
                    listView.Items.Add(new FileSystemItem { Icon = icon, Path = entry.Substring(path.Length + 1) });
                }

            }
        }
        private ImageSource GetImage(string path)
        {
            return new BitmapImage(new Uri("D:/Dowloads/" + path));
        }
    }
}
