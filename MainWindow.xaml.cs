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
using System.Windows.Threading;
using WPF_PersonalDashboard.Pages;

namespace WPF_PersonalDashboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            startClock();
        }

        private void startClock()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            lbl_ClockTime.Content = DateTime.Now.ToString(@"hh\:mm");
            lbl_ClockDate.Content = DateTime.Now.ToString(@"D");
        }

        private void NavButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                switch (btn.Name)
                {
                    case "btn_Home":
                        MainFrame.Content = null;
                        break;
                    case "btn_Todo":
                        MainFrame.Navigate(new ToDoList());
                        break;
                    case "btn_Favs":
                        MainFrame.Navigate(new Favorites());
                        break;
                    case "btn_Gallery":
                        MainFrame.Navigate(new Gallery());
                        break;
                    case "btn_Notes":
                        MainFrame.Navigate(new Notes());
                        break;
                }
            }
        }
    }
}
