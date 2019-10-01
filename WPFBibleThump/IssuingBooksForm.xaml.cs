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
using WPFBibleThump.Model;
using WPFBibleThump.ViewModel;

namespace WPFBibleThump
{
    /// <summary>
    /// Логика взаимодействия для IssuingBooksForm.xaml
    /// </summary>
    public partial class IssuingBooksForm : Window
    {
        public IssuingBooksForm(Читатели reader)
        {
            InitializeComponent();
            DataContext = reader;
            IssuedBooksDataGrid.ItemsSource = reader.Выданные_книги;
            //TODO: Добавить редактирование выданных книг(добавление даты возврата). Понять как это всё вообще сохранить.
        }
    }
}
