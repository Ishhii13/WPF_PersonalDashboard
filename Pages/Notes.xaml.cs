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
    /// Interaction logic for Notes.xaml
    /// </summary>
    public partial class Notes : Page
    {
        private string notesFolder = "D:\\Visual Studio\\Visual Studio repos\\WPF_PersonalDashboard\\Notes\\";

        public Notes()
        {
            InitializeComponent();
            ShowNotes();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new NoteEditor());
        }

        private void ShowNotes()
        {
            NoteStackPanel.Children.Clear();

            //string[] noteFiles = Directory.GetFiles(notesFolder, "*.txt");

            string[] noteFiles = Directory.GetFiles(notesFolder, "*.txt")
                                  .OrderByDescending(File.GetLastWriteTime)
                                  .ToArray();

            List<Border> borderList = new List<Border>();

            foreach (string file in noteFiles)
            {
                string[] lines = File.ReadAllLines(file);
                string title = lines[0];
                string preview = lines[1];

                Border noteBorder = new Border
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Width = 500,
                    Height = 100,
                    Background = Brushes.LightBlue,
                    BorderThickness = new Thickness(2),
                    BorderBrush = Brushes.Black,
                    Margin = new Thickness(6),
                    CornerRadius = new CornerRadius(3)
                };

                Grid noteContainer = new Grid();

                TextBlock titleBlock = new TextBlock
                {
                    Text = title,
                    FontWeight = FontWeights.Bold,
                    FontSize = 14,
                    Margin = new Thickness(5, 5, 5, 0)
                };

                TextBlock previewBlock = new TextBlock
                {
                    Text = preview,
                    FontSize = 12,
                    Margin = new Thickness(5, 25, 5, 5),
                    TextWrapping = TextWrapping.Wrap
                };

                noteContainer.Children.Add(titleBlock);
                noteContainer.Children.Add(previewBlock);
                noteBorder.Child = noteContainer;

                noteBorder.MouseLeftButtonDown += (s, e) => OpenNote(file);

                borderList.Add(noteBorder);
            }

            //borderList.Reverse();

            foreach (Border border in borderList)
                NoteStackPanel.Children.Add(border);
        }

        private void OpenNote(string filePath)
        {
            MainFrame.Navigate(new NoteEditor(filePath));
        }
    }
}
