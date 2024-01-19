using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Repositories.Repository;
using DataAccessObject;
using BusinessObject.Models;
using static System.Formats.Asn1.AsnWriter;
using System.Diagnostics;

namespace VN_Highschool_Exam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ISchoolYearRepository sy = new SchoolYearRepository();
        IStudentRepository st = new StudentRepository();
        IScoreRepository sc = new ScoreRepository();
        ISubjectRepository sb = new SubjectRepository();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void selectBtn_Click(object sender, RoutedEventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    path.Text = filePath;
                    string fileName = System.IO.Path.GetFileName(filePath);
                    pathFile.Text = fileName;
                }
            }
        }

        private void importBtn_Click(object sender, RoutedEventArgs e)
        {
            int firstIndex = -1;
            int lastIndex = -1;
            try
            {
                string[] lines = File.ReadAllLines(path.Text);
                string yearSelect = selectYear.Text;
                if (CheckContentInYear(yearSelect))
                {
                    System.Windows.MessageBox.Show(yearSelect + " have exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (!sy.checkExistYear(yearSelect))
                    {
                        sy.saveSchoolYear(new SchoolYear
                        {
                            Name = yearSelect,
                            ExamYear = yearSelect,
                            Status = null
                        });
                    }
                    for (int i = 1; i < lines.Length; i++)
                    {
                        string[] values = lines[i].Split(',');

                        if (values[6] == selectYear.Text)
                        {
                            if (firstIndex == -1)
                            {
                                firstIndex = i;
                            }
                            lastIndex = i;

                        }

                    }
                    saveStudent(lines, yearSelect, firstIndex, lastIndex);
                    saveScore(lines, yearSelect, firstIndex, lastIndex);
                    System.Windows.MessageBox.Show("Import successfully!!!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        private void saveStudent(string[] lines, string yearSelect, int firstIndex, int lastIndex)
        {
            List<Student> students = new List<Student>();
            for (int i = firstIndex; i <= lastIndex; i++)
            {
                string[] values = lines[i].Split(',');

                students.Add(new Student
                {
                    StudentCode = int.Parse(values[0]),
                    SchoolYearId = sy.getIdByYear(values[6]),
                    Status = null
                });

            }
            st.saveListStudent(students);
        }
        private void saveScore(string[] lines, string yearSelect, int firstIndex, int lastIndex)
        {
            List<Score> scores = new List<Score>();
            int yearId = sy.getIdByYear(yearSelect);
            int count = 0;
            for (int i = firstIndex; i <= lastIndex; i++)
            {
                string[] values = lines[i].Split(',');

                var studentId = st.getStudentByCode(int.Parse(values[0]), yearId);
                var subjectId = sb.getIdByName("Toan");
                scores.Add(new Score
                {
                    Score1 = values[1].Length > 0 ? double.Parse(values[1]) : 0,
                    StudentId = studentId,
                    SubjectId = 1
                });
                count++;
                Debug.WriteLine($"Count: " + count);
            }
            sc.saveListScore(scores);
        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            var list = st.getStudent(sy.getIdByYear(selectYear.Text));
            for (int i = 0; i < list.Count; i++)
            {
                try
                {
                    sc.deleteSubject(list[i].Id);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            st.deleteStudent(sy.getIdByYear(selectYear.Text));
            System.Windows.MessageBox.Show("Clear successfully!!!");
        }
        private bool CheckContentInYear(String yearSelect)
        {
            int year = sy.getIdByYear(yearSelect);
            bool Is_exist = st.haveContent(year);
            return Is_exist;
        }
    }
}
