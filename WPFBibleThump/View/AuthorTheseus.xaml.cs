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
using WPFBibleThump.ViewModel;

namespace WPFBibleThump
{
    /// <summary>
    /// Логика взаимодействия для AuthorTheseus.xaml
    /// </summary>
    public partial class AuthorTheseus : Window
    {
        public AuthorTheseus()
        {
            InitializeComponent();
            DataContext = new AuthorsViewModel();
        }

        private void SName_TextBox_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
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
