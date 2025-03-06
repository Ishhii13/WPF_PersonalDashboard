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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_PersonalDashboard.Pages
{
    /// <summary>
    /// Interaction logic for ToDoList.xaml
    /// </summary>
    public partial class ToDoList : Page
    {
        private string toDoList = "D:\\Visual Studio\\Visual Studio repos\\WPF_PersonalDashboard\\ToDoList\\";

        public ToDoList()
        {
            InitializeComponent();
        }

        private void cal_ToDoCal_SelectedDatesChanged (object sender, EventArgs e)
        {
            if (cal_ToDoCal.SelectedDate.HasValue)
                PopulateToDoList(cal_ToDoCal.SelectedDate.Value);
        }

        private void PopulateToDoList(DateTime date)
        {
            ToDoListPanel.Children.Clear();

            string filePath = System.IO.Path.Combine(toDoList, date.ToString("yyyy-MM-dd") + ".txt");

            if (File.Exists(filePath))
            {
                string[] tasks = File.ReadAllLines(filePath);
                foreach (string task in tasks)
                {
                    bool isChecked = task.StartsWith("+");
                    string taskText = task.Substring(1);

                    AddNewTask(taskText, false, isChecked);
                }
            }
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            if (!cal_ToDoCal.SelectedDate.HasValue)
            {
                MessageBox.Show("No date selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string task = tbx_NewTask.Text;

            if (string.IsNullOrWhiteSpace(task))
                return;

            AddNewTask(task, true, false);
            tbx_NewTask.Clear();
        }

        private void AddNewTask(string text, bool add, bool isChecked)
        {
            CheckBox checkBox = new CheckBox
            {
                Content = text,
                FontSize = 20,
                Margin = new Thickness(5),
                IsChecked = isChecked
            };

            if (checkBox.IsChecked == true)
                checkBox.Foreground=Brushes.Gray;
            else
                checkBox.Foreground=Brushes.Black;

            checkBox.Checked += (s, e) =>
            {
                checkBox.Foreground = Brushes.Gray;
                SaveToDoList();
            };

            checkBox.Unchecked += (s, e) =>
            {
                checkBox.Foreground = Brushes.Black;
                SaveToDoList();
            };

            ToDoListPanel.Children.Add(checkBox);

            if (add && cal_ToDoCal.SelectedDate.HasValue)
            {
                SaveToDoList();
            }
        }

        private void SaveToDoList()
        {
            if (!cal_ToDoCal.SelectedDate.HasValue) return;

            DateTime selectedDate = cal_ToDoCal.SelectedDate.Value;
            string filePath = System.IO.Path.Combine(toDoList, selectedDate.ToString("yyyy-MM-dd") + ".txt");

            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                string status = "-"; //default false
                foreach (CheckBox item in ToDoListPanel.Children)
                {
                    if (item.IsChecked == true)
                        status = "+";
                    else
                        status= "-";

                    sw.WriteLine(status+item.Content);
                }
            }
        }
    }
}
