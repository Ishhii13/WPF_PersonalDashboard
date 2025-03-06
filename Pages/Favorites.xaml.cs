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
    /// Interaction logic for Favorites.xaml
    /// </summary>
    public partial class Favorites : Page
    {
        private string favsImagesFolder = "D:\\Visual Studio\\Visual Studio repos\\WPF_PersonalDashboard\\FavsImages\\";
        public Favorites()
        {
            InitializeComponent();
            populateFavs();
        }

        private string[] getTitles()
        {
            string[] titles = new string[6]; //change this when u get enough websites

            titles[0] = "RetroGames";
            titles[1] = "Infinite Craft";
            titles[2] = "Pokemon: Auto Chess";
            titles[3] = "Entertrained";
            titles[4] = "JPerm";
            titles[5] = "TypeRacer";

            return titles;
        }

        private string[] getHyperlinks()
        {
            string[] hyperlinks = new string[6];

            hyperlinks[0] = "https://www.retrogames.onl/";
            hyperlinks[1] = "https://neal.fun/infinite-craft/";
            hyperlinks[2] = "https://www.pokemon-auto-chess.com/";
            hyperlinks[3] = "https://entertrained.app/";
            hyperlinks[4] = "https://jperm.net/algs/oll";
            hyperlinks[5] = "https://play.typeracer.com/";

            return hyperlinks;
        }

        private Image[] getImages()
        {
            string[] imgSource = Directory.GetFiles(favsImagesFolder, "*.jpg");
            Image[] images = new Image[imgSource.Length];

            for (int i = 0; i < imgSource.Length; i++)
            {
                Image img = new Image
                {
                    Height = 165,
                    MaxWidth = 165,
                    MaxHeight = 165,
                    Margin = new Thickness(5),
                    Stretch = Stretch.Uniform,
                    Source = new BitmapImage(new Uri(imgSource[i]))
                };

                images[i] = img;
            }

            return images;
        }

        private void populateFavs()
        {
            string[] title = getTitles();
            string[] hyperlink = getHyperlinks();
            Image[] image = getImages();

            for (int i = 0; i<image.Length; i++)
            {
                Border cardBorder = new Border
                {
                    Width = 190,
                    Height = 250,
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(2),
                    Margin = new Thickness(6),
                    Background = Brushes.White,
                    CornerRadius = new CornerRadius(2)
                };

                StackPanel cardContainer = new StackPanel
                {
                    Orientation = Orientation.Vertical
                };

                TextBlock linkBlock = new TextBlock
                {
                    Margin = new Thickness(5),
                    TextAlignment = TextAlignment.Center
                };

                Hyperlink link = new Hyperlink
                {
                    NavigateUri = new Uri(hyperlink[i])
                };

                link.Inlines.Add(title[i]);
                link.RequestNavigate += (s, args) =>
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(args.Uri.AbsoluteUri) { UseShellExecute = true });
                };

                linkBlock.Inlines.Add(link);

                cardContainer.Children.Add(image[i]);
                cardContainer.Children.Add(linkBlock);

                cardBorder.Child = cardContainer;

                FavoritesWrapPanel.Children.Add(cardBorder);
            }
        }
    }
}
