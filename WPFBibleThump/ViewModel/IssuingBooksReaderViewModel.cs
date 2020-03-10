using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using WPFBibleThump.Model;

namespace WPFBibleThump.ViewModel
{
    internal class IssuingBooksReaderViewModel : INotifyPropertyChanged
    {
        private Экземпляры_книги _selectedBook;
        private Читатели _reader;
        private ICollection<Экземпляры_книги> GiveOutBooks;
        public RelayCommand GiveBook { get; }
        public RelayCommand AddBook { get; }

        public IssuingBooksReaderViewModel(Читатели reader)
        {
            _reader = reader;

            GiveBook = new RelayCommand(
               (param) =>
               {
                    App.MOYABAZA.SaveChanges();
                    CollectionViewSource.GetDefaultView(_reader.Выданные_книги).Refresh();
               },
               (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.ReadersName && uo.E == 1) != 0 && param != null);

            AddBook = new RelayCommand(
               (param) =>
               {
                   App.MOYABAZA.SaveChanges();
                   CollectionViewSource.GetDefaultView(_reader.Выданные_книги).Refresh();
               },
               (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.ReadersName && uo.E == 1) != 0);
        }

        public string ReaderNumber
        {
            get { return _reader.Номер_читательского_билета; }
            set
            {
                _reader.Номер_читательского_билета = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _reader.Имя; }
            set
            {
                _reader.Имя = value;
                OnPropertyChanged();
            }
        }

        public string SName
        {
            get { return _reader.Фамилия; }
            set
            {
                _reader.Фамилия = value;
                OnPropertyChanged();
            }
        }

        public string TName
        {
            get { return _reader.Отчество; }
            set
            {
                _reader.Отчество = value;
                OnPropertyChanged();
            }
        }

        public Экземпляры_книги SelectedBook
        {
            get { return _selectedBook; }
            set
            {
                _selectedBook = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}