using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Interaction logic for Gallery.xaml
    /// </summary>
    public partial class Gallery : Page
    {
        private string galleryFolder = "D:\\Visual Studio\\Visual Studio repos\\WPF_PersonalDashboard\\GalleryImages\\";
        public Gallery()
        {
            InitializeComponent();
            addAllImages();
        }

        private void addAllImages()
        {
            GalleryWrapPanel.Children.Clear();
            int imgWidth = 175;

            string[] imgSource = Directory.GetFiles(galleryFolder, "*.jpg");

            for (int i = 0;  i < imgSource.Length; i++)
            {
                BitmapImage bitmap = new BitmapImage(new Uri(imgSource[i]));

                double aspectRatio = (double)bitmap.PixelWidth / bitmap.PixelHeight;
                double imgHeight = imgWidth / aspectRatio;

                string filePath = imgSource[i];

                Border imageBorder = new Border
                {
                    Width = imgWidth,
                    Height = imgHeight,
                    VerticalAlignment = VerticalAlignment.Top,
                    BorderBrush = Brushes.Transparent,
                    BorderThickness = new Thickness(2),
                    Margin = new Thickness(13),
                    Background = Brushes.LightGray,
                    Cursor = Cursors.Hand
                };

                Image img = new Image
                {
                    Width = 165,
                    Height = imgHeight,
                    Stretch = Stretch.Uniform,
                    Source = new BitmapImage(new Uri(imgSource[i]))
                };

                imageBorder.Child = img;
                imageBorder.MouseLeftButtonDown += (s, e) => OpenImage(filePath);

                GalleryWrapPanel.Children.Add(imageBorder);
            }
        }

        private void OpenImage(string filePath)
        {
            GalleryScrollViewer.Visibility = Visibility.Collapsed;
            EnlargedImageBorder.Visibility = Visibility.Visible;

            EnlargedImage.Source = new BitmapImage(new Uri(filePath));
        }

        private void CloseImage(object sender, RoutedEventArgs e)
        {
            GalleryScrollViewer.Visibility = Visibility.Visible;
            EnlargedImageBorder.Visibility = Visibility.Collapsed;
        }
    }
}
