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

namespace WPFBibleThump
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void CityTheseus_Click(object sender, RoutedEventArgs e)
        {
            CityTheseus cityT = new CityTheseus
            {
                Owner = this
            };
            cityT.Show();
        }

        private void StreetTheseus_Click(object sender, RoutedEventArgs e)
        {
            StreetTheseus streetT = new StreetTheseus
            {
                Owner = this
            };
            streetT.Show();
        }

        private void AuthorTheseus_Click(object sender, RoutedEventArgs e)
        {
            AuthorTheseus AuthorT = new AuthorTheseus
            {
                Owner = this
            };
            AuthorT.Show();
        }

        private void PublishTheseus_Click(object sender, RoutedEventArgs e)
        {
            PublishTheseus PublishT = new PublishTheseus
            {
                Owner = this
            };
            PublishT.Show();
        }

        private void SysCatalogue_Click(object sender, RoutedEventArgs e)
        {
            SysCatalogue SysCatalogueT = new SysCatalogue
            {
                Owner = this
            };
            SysCatalogueT.Show();
        }

        private void BooksTheseus_Click(object sender, RoutedEventArgs e)
        {
            BooksTheseus BooksT = new BooksTheseus
            {
                Owner = this
            };
            BooksT.Show();
        }

        private void IssuedBooks_Click(object sender, RoutedEventArgs e)
        {
            IssuedBooks IssuedBooksT = new IssuedBooks
            {
                Owner = this
            };
            IssuedBooksT.Show();
        }

        private void Readers_Click(object sender, RoutedEventArgs e)
        {
            Readers ReadersT = new Readers
            {
                Owner = this
            };
            ReadersT.Show();
        }

        private void ReadersReg_Click(object sender, RoutedEventArgs e)
        {
            ReadersReg ReadersR = new ReadersReg
            {
                Owner = this
            };
            ReadersR.Show();
        }

        private void IssuingBooks_Click(object sender, RoutedEventArgs e)
        {
            IssuingBooksForm IssuingB = new IssuingBooksForm
            {
                Owner = this
            };
            IssuingB.Show();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
