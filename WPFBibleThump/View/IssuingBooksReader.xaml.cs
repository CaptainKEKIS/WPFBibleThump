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

namespace WPFBibleThump.View
{
    /// <summary>
    /// Логика взаимодействия для IssuingBooksReader.xaml
    /// </summary>
    public partial class IssuingBooksReader : Window
    {
        public IssuingBooksReader(Читатели reader)
        {
            InitializeComponent();
            DataContext = new IssuingBooksReaderViewModel(reader);
        }
    }
}
