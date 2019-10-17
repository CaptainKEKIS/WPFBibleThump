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
    /// Логика взаимодействия для Readers.xaml
    /// </summary>
    public partial class Readers : Window
    {
        public Readers()
        {
            InitializeComponent();
            DataContext = new ReadersViewModel();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //System.Windows.Data.CollectionViewSource читателиViewSource1 = ((System.Windows.Data.CollectionViewSource)(this.FindResource("читателиViewSource1")));
            // Загрузите данные, установив свойство CollectionViewSource.Source:
            // читателиViewSource1.Source = [универсальный источник данных]
        }
    }
}
