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

namespace CarsharingApp
{
    /// <summary>
    /// Interaction logic for AuthPage.xaml
    /// </summary>

    public partial class AuthPage : Page
    {
        private Image firstButton;

        public AuthPage()
        {
            InitializeComponent();
            LoadPuzzle();
        }

        private void LoadPuzzle()
        {
            var pieces = Enumerable.Range(1, 4).OrderBy(x => new Random().Next()).ToList();
            pieces.ForEach(x =>
            {
                var img = new Image
                {
                    Source = new BitmapImage(new Uri($"Images/{x}.png", UriKind.Relative)),
                    Tag = x,
                    Stretch = Stretch.Fill
                };
                img.MouseLeftButtonUp += Pieces_Click;
                Puzzlegrid.Children.Add(img);
            });
        }

        private void CheckPuzzle()
        {
            if (Puzzlegrid.Children.OfType<Image>()
                .Select((img, i) => i + 1 == (int)img.Tag)
                .All(x => x))
            {
                MessageBox.Show("Капча решена!");
            }
        }

        private void Pieces_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image clicked)
            {
                if (firstButton == null)
                {
                    firstButton = clicked;
                    firstButton.Opacity = 0.5;
                    return;
                }

                if (clicked != firstButton)
                {
                    (firstButton.Source, clicked.Source) = (clicked.Source, firstButton.Source);
                    (firstButton.Tag, clicked.Tag) = (clicked.Tag, firstButton.Tag);
                }

                firstButton.Opacity = 1;
                firstButton = null;
                CheckPuzzle();
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new CarsPage());
        }


    }
}