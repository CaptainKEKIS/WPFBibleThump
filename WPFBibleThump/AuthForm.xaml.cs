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

namespace WPFBibleThump
{
    /// <summary>
    /// Логика взаимодействия для AuthForm.xaml
    /// </summary>
    public partial class AuthForm : Window
    {
        public AuthForm()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var User = App.MOYABAZA.Пользователи
                .FirstOrDefault(s => s.Login == TBLogin.Text && s.Password == PBPasswd.Password);
            if (User != null && 
                String.IsNullOrEmpty( User.Login ) == false && 
                String.IsNullOrEmpty(User.Password) == false && 
                User.Пользователи_Объекты.Count( uo => uo.Объекты.SName == Constants.ApplicationName && uo.R == 1) != 0 )
            {
                App.ActiveUser = User;
                MainWindow MWindow = new MainWindow();
                this.Close();
                MWindow.Show();
            }
            else
            {
                MessageBox.Show("В данный момент приложение не работает.\nОбратитесь к системному администратору!", "Ошибка работы приложения", MessageBoxButton.OK);
            }
        }
    }
}
