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
        private Читатели _reader;
        private ObservableCollection<Выданные_книги> _giveOutBooks = new ObservableCollection<Выданные_книги>();
        public RelayCommand GiveBook { get; }
        public RelayCommand AddBook { get; }
        public RelayCommand DeleteBook { get; }

        public IssuingBooksReaderViewModel(Читатели reader)
        {
            MOYABAZAEntities model = App.MOYABAZA;
            model.Экземпляры_книги.Load();
            _reader = reader;

            GiveBook = new RelayCommand(
               (param) =>
               {
                   try
                   {
                       foreach (var book in GiveOutBooks)
                       {
                           book.Дата_выдачи = DateTime.Now;
                           _reader.Выданные_книги.Add(book);
                       }
                       App.MOYABAZA.SaveChanges();
                       MessageBox.Show("Книги выданы!");
                       GiveOutBooks.Clear();
                   }
                   catch (Exception e)
                   {
                       MessageBox.Show("Произошла ошибка!\n" + e.Message);
                   }
               },
               (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.ReadersName && uo.E == 1) != 0);

            AddBook = new RelayCommand(
               (param) =>
               {
                   //App.MOYABAZA.SaveChanges();
                   int inventaryNumber = int.Parse(InventaryNumber);
                   var bookCopy = model.Экземпляры_книги.FirstOrDefault(x => x.Инвентарный_номер == inventaryNumber);
                   var bookToGive = new Выданные_книги();
                   //bookToGive.Инвентарный_номер = bookCopy.Инвентарный_номер;

                   if (bookCopy == null)
                   {
                       MessageBox.Show("Инвентарный номер не найден!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                   }
                   else if (GiveOutBooks.FirstOrDefault(x => x.Экземпляры_книги.Инвентарный_номер == bookCopy.Инвентарный_номер) != null)
                   {
                       MessageBox.Show("Данный экземпляр книги уже добавлен!");
                   }
                   else
                   {
                       bookToGive.Экземпляры_книги = bookCopy;
                       bookToGive.Читатели = _reader;
                       GiveOutBooks.Add(bookToGive);
                   }
               },
               (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.ReadersName && uo.E == 1) != 0);

            DeleteBook = new RelayCommand(
               (param) =>
               {
                   GiveOutBooks.Remove(param as Выданные_книги);
               },
               (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.ReadersName && uo.E == 1) != 0 && param != null);
        }

        public string InventaryNumber { get; set; }

        public ObservableCollection<Выданные_книги> GiveOutBooks
        {
            get => _giveOutBooks;
            set { _giveOutBooks = value; }
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}