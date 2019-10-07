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
        public RelayCommand IssueBook { get; }

        public IssuingBooksViewModel(Читатели reader)
        {
            _reader = reader;
            App.MOYABAZA.Читатели.Load();
            MOYABAZAEntities model = new MOYABAZAEntities();
            //ObservableCollection<Читатели> Readers = new ObservableCollection<Читатели>(App.MOYABAZA.Читатели.Local.Where(s => s == _selectedReader));
            IssueBook = new RelayCommand(
                (param) =>
                {
                    model.Выданные_книги.FirstOrDefault(b => b.Инвентарный_номер == _selectedBook.Инвентарный_номер).Дата_возврата = DateTime.Now;
                    model.SaveChanges();
                },
                (param) => /*App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.ReadersName && uo.E == 1) != 0 &&*/ param != null);
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
        public string ReaderNumber
        {
            get { return _reader.Номер_читательского_билета; }
            set
            {
                _reader.Номер_читательского_билета = value;
                OnPropertyChanged("ReaderNumber");
            }
        }

        public string SName
        {
            get { return _reader.Фамилия; }
            set
            {
                _reader.Фамилия = value;
                OnPropertyChanged("SName");
            }
        }

        public string TName
        {
            get { return _reader.Отчество; }
            set
            {
                _reader.Отчество = value;
                OnPropertyChanged("TName");
            }
        }

        public string MobileNumber
        {
            get { return _reader.Телефон; }
            set
            {
                _reader.Телефон = value;
                OnPropertyChanged("MobileNumber");
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
                OnPropertyChanged("HouseNumber");
            }
        }

        public string Appartment
        {
            get { return _reader.Квартира; }
            set
            {
                _reader.Квартира = value;
                OnPropertyChanged("Appartment");
            }
        }

        public System.DateTime RegistrationTime
        {
            get { return _reader.Дата_регистрации; }
            set
            {
                _reader.Дата_регистрации = value;
                OnPropertyChanged("RegistrationTime");
            }
        }

        public Nullable<System.DateTime> ReRegistrationTime
        {
            get { return _reader.Дата_перерегистрации; }
            set
            {
                _reader.Дата_перерегистрации = value;
                OnPropertyChanged("ReRegistrationTime");
            }
        }

        public ICollection<Выданные_книги> IssuedBooks
        {
            get { return _reader.Выданные_книги; }
        }
        /*
        public int IssuedDaysAgo
        {
            get { return ssuedDaysAgo; }
            set
            {
                var books = _reader.Выданные_книги.ToArray();
                foreach (var book in books)
                {
                    TimeSpan ssuedDaysAgo = book.Дата_выдачи.Date - DateTime.Today;
                }
                OnPropertyChanged("IssuedDaysAgo");
            }
        }
        */
        public Выданные_книги SelectedBook
        {
            get { return _selectedBook; }
            set
            {
                _selectedBook = value;
                OnPropertyChanged("SelectedBook");
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
