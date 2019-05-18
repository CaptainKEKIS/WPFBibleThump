using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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


        private static readonly InterceptKeys.LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            _hookID = InterceptKeys.SetHook(_proc);
        }

        public static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                System.Windows.Forms.Keys key = (System.Windows.Forms.Keys)vkCode;

                if (key == System.Windows.Forms.Keys.LWin || key == System.Windows.Forms.Keys.RWin)
                    return (IntPtr)1; // Handled.
            }

            return InterceptKeys.CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

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
            var t3 = ((ctrl.RenderTransform as TransformGroup).Children[0] as ScaleTransform);
            t3.ScaleX += r.Next(-20, 20) / 50.0;
            t3.ScaleY += r.Next(-10, 10) / 20.0;
            t.Angle += r.Next(-50,50);
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
            int rnd = r.Next();
            var bytes = BitConverter.GetBytes(rnd);
            this.Background = new SolidColorBrush( Color.FromRgb(bytes[0], bytes[1], bytes[2]));
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

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
    }
}
