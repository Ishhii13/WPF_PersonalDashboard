using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
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
    /// Interaction logic for NoteEditor.xaml
    /// </summary>
    public partial class NoteEditor : Page
    {
        private string notesFolder = "D:\\Visual Studio\\Visual Studio repos\\WPF_PersonalDashboard\\Notes\\";
        private string currentFilePath;

        public NoteEditor()
        {
            InitializeComponent();
            currentFilePath = GenerateNewFilePath();
        }

        private void AddNote_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleTextBox.Text;
            string note = NoteTextBox.Text;

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(note))
            {
                MessageBox.Show("Please enter a title and content.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (StreamWriter sw = new StreamWriter(currentFilePath, false))
                {
                    sw.WriteLine(title); // First line is the title
                    sw.WriteLine(note);  // Second line is the content
                }

                TitleTextBox.Clear();
                NoteTextBox.Clear();

                // Navigate back to Notes
                MainFrame.Navigate(new Notes());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving note: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public NoteEditor(string filePath) : this()
        {
            currentFilePath = filePath;
            LoadNote(filePath);
        }

        private void LoadNote(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    string[] lines = File.ReadAllLines(filePath);
                    TitleTextBox.Text = lines.Length > 0 ? lines[0] : ""; // First line is the title
                    NoteTextBox.Text = lines.Length > 1 ? string.Join("\n", lines.Skip(1)) : ""; // Second line is the content
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading note: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private string GenerateNewFilePath()
        {
            int num = 1;
            string filePath;

            // Find the next available numbered filename
            do
            {
                filePath = System.IO.Path.Combine(notesFolder, $"{num}.txt");
                num++;
            } while (File.Exists(filePath));

            return filePath;
        }
    }
}
