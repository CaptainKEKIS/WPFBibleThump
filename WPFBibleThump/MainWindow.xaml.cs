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
            cityT.ShowDialog();
        }

        private void StreetTheseus_Click(object sender, RoutedEventArgs e)
        {
            StreetTheseus streetT = new StreetTheseus
            {
                Owner = this
            };
            streetT.ShowDialog();
        }
    }
}
