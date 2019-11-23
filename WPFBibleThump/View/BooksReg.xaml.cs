using System;
using System.Windows;
using System.Windows.Controls;
using WPFBibleThump.Model;
using WPFBibleThump.ViewModel;

namespace WPFBibleThump.View
{
    /// <summary>
    /// Логика взаимодействия для BooksReg.xaml
    /// </summary>
    public partial class BooksReg : Window
    {
        public BooksReg(MOYABAZAEntities model, Книги book)
        {
            InitializeComponent();
            DataContext = new BooksRegViewModel(model, book);
        }

        private void Author_GotFocus(object sender, RoutedEventArgs e)
        {
            //   MessageBox.Show("fdsfsd");
            if ((sender as ComboBox).IsDropDownOpen == false)
            {
                (sender as ComboBox).IsDropDownOpen = true;
            }
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text == String.Empty || Publish.Text == String.Empty || City.Text == String.Empty || PublishingYear.Text == String.Empty || UDK.Text == String.Empty ||
                   CopiesNumber.Text == String.Empty || DGAuthors.HasItems == false /*|| BookCopies.HasItems == true проверка на наличие записей в таблице экземпляров книг*/)
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
