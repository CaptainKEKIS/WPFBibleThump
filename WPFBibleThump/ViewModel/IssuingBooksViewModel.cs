using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
        public RelayCommand AddCommand { get; }

        public IssuingBooksViewModel(Читатели reader)
        {
            _reader = reader;
            //App.MOYABAZA.Читатели.Load();
            //ObservableCollection<Читатели> Readers = new ObservableCollection<Читатели>(App.MOYABAZA.Читатели.Local.Where(s => s == _selectedReader));
            AddCommand = new RelayCommand(
                (param) =>
                {
                    SelectedBook.Дата_возврата = DateTime.Parse("01.10.2019");
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.ReadersName && uo.E == 1) != 0 && param != null);
        }

        public string Name
        {
            get { return _reader.Имя; }
            set
            {
                _reader.Имя = value;
                OnPropertyChanged("Name");
            }
        }
        public string ReaderNumber {
            get { return _reader.Номер_читательского_билета; }
            set
            {
                _reader.Номер_читательского_билета = value;
                OnPropertyChanged("ReaderNumber");
            }
        }

        public string SName {
            get { return _reader.Фамилия; }
            set
            {
                _reader.Фамилия = value;
                OnPropertyChanged("SName");
            }
        }
        
        public string TName {
            get { return _reader.Отчество; }
            set
            {
                _reader.Отчество = value;
                OnPropertyChanged("TName");
            }
        }

        public string MobileNumber {
            get { return _reader.Телефон; }
            set
            {
                _reader.Телефон = value;
                OnPropertyChanged("MobileNumber");
            }
        }

        public Улицы Street {
            get { return _reader.Улицы; }
            set
            {
                _reader.Улицы = value;
                OnPropertyChanged("Street");
            }
        }

        public string HouseNumber {
            get { return _reader.Номер_дома; }
            set
            {
                _reader.Номер_дома = value;
                OnPropertyChanged("HouseNumber");
            }
        }

        public string Квартира {
            get { return _reader.Имя; }
            set
            {
                _reader.Имя = value;
                OnPropertyChanged("Name");
            }
        }

        public System.DateTime Дата_регистрации {
            get { return _reader.Имя; }
            set
            {
                _reader.Имя = value;
                OnPropertyChanged("Name");
            }
        }

        public Nullable<System.DateTime> Дата_перерегистрации {
            get { return _reader.Имя; }
            set
            {
                _reader.Имя = value;
                OnPropertyChanged("Name");
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
