﻿using System;
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
    /// Логика взаимодействия для SysCatalogue.xaml
    /// </summary>
    public partial class SysCatalogue : Window
    {
        public SysCatalogue()
        {
            InitializeComponent();
            DataContext = new SysCatViewModel();
        }
    }
}
