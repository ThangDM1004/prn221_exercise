using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Diagnostics;

namespace BT2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //FolderBrowserDialog dialog = new FolderBrowserDialog();
            //dialog.ShowDialog();

            //String folder_name = dialog.SelectedPath;
            //int indexToRemoveBefore = 3; 
            //string resultString="";
            //if (indexToRemoveBefore >= 0 && indexToRemoveBefore < folder_name.Length)
            //{
            //     resultString = folder_name.Substring(indexToRemoveBefore);
            //    Console.WriteLine(resultString);
            //}

            //folder.Text = resultString;

            //string path = dialog.SelectedPath;
            //string[] files = Directory.GetFileSystemEntries(path);

            //foreach (string file in files)
            //{
            //}
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string selectedPath = dialog.SelectedPath;
                folder.Text = selectedPath;
                int count = folder.Text.Length;

                string[] filesAndFolders = Directory.GetFileSystemEntries(dialog.SelectedPath);


                foreach (string entry in filesAndFolders)
                {
                    if ((File.GetAttributes(entry) & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        ImageSource icon = GetImage("folder.jpg");
                        listView.Items.Add(new FileSystemItem { Icon = icon, Path = entry.Substring(count + 1) });
                    }

                }
                foreach (string entry in filesAndFolders)
                {
                    if ((File.GetAttributes(entry) & FileAttributes.Directory) != FileAttributes.Directory)
                    {
                        ImageSource icon = GetImage("file.png");
                        listView.Items.Add(new FileSystemItem { Icon = icon, Path = entry.Substring(count + 1) });
                    }

                }
            }
        }
        private ImageSource GetImage(string path)
        {
            return new BitmapImage(new Uri("D:/Dowloads/" + path));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreatePopup createPopup = new CreatePopup
            {
                folderPath = folder.Text,
                listView = listView
            };
            createPopup.ShowDialog();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            FileSystemItem selectedItem = (FileSystemItem)listView.SelectedItem;
            string path = folder.Text + "/" + selectedItem.Path;
            MessageBoxResult message = System.Windows.MessageBox.Show("Are you delete ?", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (message == MessageBoxResult.OK)
            {
                try
                {
                    File.Delete(path);
                    LoadView(folder.Text);
                }
                catch (Exception ex)
                {
                    Directory.Delete(path, true);
                    LoadView(folder.Text);
                }

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

        private void Open_event(object sender, RoutedEventArgs e)
        {
            FileSystemItem selectedItem = (FileSystemItem)listView.SelectedItem;
            string path = folder.Text + "\\" + selectedItem.Path;
            if (File.Exists(path))
            {
                Process.Start("explorer.exe", path);
            }
            else if (Directory.Exists(path))
            {
                folder.Text = path;
                LoadView(path);

            }
        }

        private void changeEvent(object sender, RoutedEventArgs e)
        {
            create_btn.IsEnabled = true;
            delete_btn.IsEnabled = true;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string url = folder.Text;
            int numberOfLevelsToRemove = 1;

            for (int i = 0; i < numberOfLevelsToRemove; i++)
            {
                // Xóa phần cuối cùng của chuỗi (được hiểu là một cấp độ thư mục)
                int lastBackslashIndex = url.LastIndexOf('\\');
                if (lastBackslashIndex >= 0)
                {
                    url = url.Substring(0, lastBackslashIndex);
                    folder.Text = url;
                    LoadView(url);
                    break;
                }

            }
        }

        private void Delete_event(object sender, RoutedEventArgs e)
        {
            FileSystemItem selectedItem = (FileSystemItem)listView.SelectedItem;
            string path = folder.Text + "/" + selectedItem.Path;
            MessageBoxResult message = System.Windows.MessageBox.Show("Are you delete ?", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (message == MessageBoxResult.OK)
            {
                try
                {
                    File.Delete(path);
                    LoadView(folder.Text);
                }
                catch (Exception ex)
                {
                    Directory.Delete(path, true);
                    LoadView(folder.Text);
                }

            }
        }
    }
}
