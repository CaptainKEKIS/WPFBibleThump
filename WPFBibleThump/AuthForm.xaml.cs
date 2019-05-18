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
using System.Windows.Threading;

namespace WPFBibleThump
{
    /// <summary>
    /// Логика взаимодействия для AuthForm.xaml
    /// </summary>
    public partial class AuthForm : Window
    {
        Random r = new Random();
        public AuthForm()
        {
            InitializeComponent();
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = new TimeSpan(0, 0, 0, 0, 50);
            dt.Tick += Dt_Tick;
            dt.IsEnabled = true;

        }
        void ChangeElementPosition( Control ctrl)
        {
            var t = ((ctrl.RenderTransform as TransformGroup).Children[2] as RotateTransform);
            var t2 = ((ctrl.RenderTransform as TransformGroup).Children[3] as TranslateTransform);
            t.Angle += 10;
            var nx = t2.X + r.Next(-40, 40);
            var ny = t2.Y + r.Next(-20, 20);
            if (!(nx > this.Width / 2 - btn.Width || nx < -this.Width / 2 + btn.Width))
            {
                t2.X = nx;
            }
            if (!(ny > this.Height - btn.Height || ny < -this.Height / 2 + btn.Height))
            {
                t2.Y = ny;
            }
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            foreach( var element in grid.Children)
            {
                ChangeElementPosition(element as Control);
            }
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
