using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WPFBibleThump.Model;

namespace WPFBibleThump.ViewModel
{
    class IssuingBooksViewModel
    {
        private string _searchText;
        private Выданные_книги _selectedBook;
        public ICollectionView Readers { get; set; }

        public RelayCommand AddCommand { get; }

        public IssuingBooksViewModel()
        {
            //App.MOYABAZA.Читатели.Load();
            //ObservableCollection<Читатели> Readers = new ObservableCollection<Читатели>(App.MOYABAZA.Читатели.Local.Where(s => s == _selectedReader));
            AddCommand = new RelayCommand(
                (param) =>
                {
                    SelectedBook.Дата_возврата = DateTime.Parse("01.10.2019");
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.ReadersName && uo.E == 1) != 0 && param != null);
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
    }
}
