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
using System.Windows.Shapes;
using WPFBibleThump.ViewModel;

namespace WPFBibleThump
{
    /// <summary>
    /// Логика взаимодействия для BooksTheseus.xaml
    /// </summary>
    public partial class BooksTheseus : Window
    {
        public BooksTheseus()
        {
            InitializeComponent();
            DataContext = new BooksViewModel();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //System.Windows.Data.CollectionViewSource книгиViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("книгиViewSource")));
            // Загрузите данные, установив свойство CollectionViewSource.Source:
            // книгиViewSource.Source = [универсальный источник данных]
        }
    }
}
