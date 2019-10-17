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
    /// Логика взаимодействия для IssuedBooks.xaml
    /// </summary>
    public partial class IssuedBooks : Window
    {
        public IssuedBooks()
        {
            InitializeComponent();
            DataContext = new IssuedBViewModel();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //System.Windows.Data.CollectionViewSource выданные_книгиViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("выданные_книгиViewSource")));
            // Загрузите данные, установив свойство CollectionViewSource.Source:
            // выданные_книгиViewSource.Source = [универсальный источник данных]
            //System.Windows.Data.CollectionViewSource выданные_книгиViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("выданные_книгиViewSource")));
            // Загрузите данные, установив свойство CollectionViewSource.Source:
            // выданные_книгиViewSource.Source = [универсальный источник данных]
        }
    }
}
