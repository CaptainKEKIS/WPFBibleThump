using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPFBibleThump.Model;

namespace WPFBibleThump
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MOYABAZAEntities MOYABAZA = new MOYABAZAEntities();
        public static Пользователи ActiveUser { get; set; } = App.MOYABAZA.Пользователи
                .FirstOrDefault(s => s.Login == "Admin" && s.Password == "Admin");
    }
}
