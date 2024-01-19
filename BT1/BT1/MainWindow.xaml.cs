using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BT1
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
            float height = float.Parse(height_number.Text)/100;
            float weight = float.Parse(weight_number.Text);
            float BMI = weight / (height * height);
            string round = Math.Round(BMI, 1).ToString();
            if (BMI < 18.5)
            {
                bmi.Text = round;
                status.Text = "Underweight";
                bmi.Background = Brushes.Aqua;
            }
            else if (BMI > 18.5 && BMI < 24.9)
            {
                bmi.Text = round;
                status.Text = "Normal";
                bmi.Background = Brushes.LightGreen;
            }
            else if (BMI > 25 && BMI < 29.9)
            {
                bmi.Text = round;
                status.Text = "Overweight";
                bmi.Background = Brushes.Yellow;
            }
            else if (BMI > 30 && BMI < 34.9)
            {
                status.Text = "Obese";
                bmi.Text = round;
                bmi.Background = Brushes.Orange;
            }
            else if (BMI > 35)
            {
                status.Text = "Extremely Obese";
                bmi.Text = round;
                bmi.Background = Brushes.Red;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            height_number.Clear();
            weight_number.Clear();
            bmi.Text = "";
            status.Text = "";
            bmi.Background = Brushes.White;
        }
    }
}
