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
using WPFBibleThump.Model;
using WPFBibleThump.ViewModel;

namespace WPFBibleThump
{
    /// <summary>
    /// Логика взаимодействия для ReadersReg.xaml
    /// </summary>
    public partial class ReadersReg : Window
    {
        public ReadersReg(MOYABAZAEntities model, Читатели reader)
        {
            InitializeComponent();
            RegDate.SelectedDate = DateTime.Now;
            DataContext = new ReadersRegViewModel(model, reader);
        }

        private void Street_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void Street_GotFocus(object sender, RoutedEventArgs e)
        {
         //   MessageBox.Show("fdsfsd");
            (sender as ComboBox).IsDropDownOpen = true;

        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            if (RTicket.Text == String.Empty || SName.Text == String.Empty || FName.Text == String.Empty || TName.Text == String.Empty || TBPhone.Text == String.Empty ||
                   Street.Text == String.Empty || HouseNumber.Text == String.Empty || ApprtNumber.Text == String.Empty)
            {
                MessageBox.Show("Не все поля заплнены!");
            }
            else
            {
                DialogResult = true;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
