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
    /// Логика взаимодействия для CityTheseus.xaml
    /// </summary>
    public partial class StreetTheseus : Window
    {
        public StreetTheseus()
        {
            InitializeComponent();
            DataContext = new StreetsViewModel();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            Edit_TextBox.Focus();
        }

        private void Edit_TextBox_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((sender as TextBox).IsEnabled)
            {
                TextBox textBox = (sender as TextBox);
                textBox.Focus();
                textBox.CaretIndex = textBox.Text.Count();
            }
        }
    }
}
