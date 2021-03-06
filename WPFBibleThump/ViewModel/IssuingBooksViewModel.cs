﻿using System;
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
    class IssuingBooksViewModel : INotifyPropertyChanged
    {
        private string _searchText;
        private Выданные_книги _selectedBook;
        public ICollectionView Readers { get; set; }
        private Читатели _reader;
        public RelayCommand IssueBook { get; }

        public IssuingBooksViewModel(Читатели reader)
        {
            _reader = reader;

            IssueBook = new RelayCommand(
                (param) =>
                {
                    //if(App.MOYABAZA.Читатели.FirstOrDefault(r => r == _reader).Выданные_книги.FirstOrDefault(b => b.Инвентарный_номер == _selectedBook.Инвентарный_номер).Дата_выдачи > _selectedBook.Дата_возврата)
                    //{
                    //    MessageBox.Show($"Дата возврата не может быть меньше даты выдачи!!!!!!!! \n({_selectedBook.Экземпляры_книги.Книги.Название})");
                    //}
                    //else
                    //{
                        App.MOYABAZA.SaveChanges();
                    //}
                    CollectionViewSource.GetDefaultView(_reader.Выданные_книги).Refresh();
                },
                (param) => /*App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.ReadersName && uo.E == 1) != 0 &&*/ param != null);
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
        public string ReaderNumber
        {
            get { return _reader.Номер_читательского_билета; }
            set
            {
                _reader.Номер_читательского_билета = value;
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

        public string MobileNumber
        {
            get { return _reader.Телефон; }
            set
            {
                _reader.Телефон = value;
                OnPropertyChanged();
            }
        }

        public string Street
        {
            get { return _reader.Улицы.Название; }
            set
            {

            }
        }

        public string HouseNumber
        {
            get { return _reader.Номер_дома; }
            set
            {
                _reader.Номер_дома = value;
                OnPropertyChanged();
            }
        }

        public string Appartment
        {
            get { return _reader.Квартира; }
            set
            {
                _reader.Квартира = value;
                OnPropertyChanged();
            }
        }

        public DateTime RegistrationTime
        {
            get { return _reader.Дата_регистрации; }
            set
            {
                _reader.Дата_регистрации = value;
                OnPropertyChanged();
            }
        }

        public DateTime? ReRegistrationTime
        {
            get { return _reader.Дата_перерегистрации; }
            set
            {
                _reader.Дата_перерегистрации = value;
                OnPropertyChanged();
            }
        }

        public ICollection<Выданные_книги> IssuedBooks
        {
            get { return _reader.Выданные_книги; }
        }

        public Выданные_книги SelectedBook
        {
            get { return _selectedBook; }
            set
            {
                _selectedBook = value;
                OnPropertyChanged();
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                Readers.Refresh();
            }
        }

        bool FilterFunction(object o)
        {
            Читатели chitateli = o as Читатели;
            if (String.IsNullOrEmpty(SearchText) ||
                chitateli.Номер_читательского_билета.StartsWith(SearchText.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
