using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WPFBibleThump.Model;

namespace WPFBibleThump.ViewModel
{
    class BooksViewModel
    {
        private string _searchText;
        public ICollectionView Books { get; set; }

        public RelayCommand AddCommand { get; }
        public RelayCommand ChangeCommand { get; }
        public RelayCommand DeleteCommand { get; }

        public BooksViewModel()
        {
            App.MOYABAZA.Книги.Load();
            Books = CollectionViewSource.GetDefaultView(App.MOYABAZA.Книги.Local);

            AddCommand = new RelayCommand((param) => {  }, 
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.BooksThesaurusName && uo.W == 1) != 0);
            ChangeCommand = new RelayCommand((param) => { },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.BooksThesaurusName && uo.E == 1) != 0 && param != null);
            DeleteCommand = new RelayCommand((param) => {  },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.BooksThesaurusName && uo.D == 1) != 0 && param != null);
            Books.Filter = FilterFunction;
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                Books.Refresh();
            }
        }
        
        bool FilterFunction(object o)
        {
            Книги ulica = o as Книги;
            if (String.IsNullOrEmpty(SearchText) ||
                ulica.Название.StartsWith(SearchText.Trim(), StringComparison.OrdinalIgnoreCase) ||
                ulica.Издательства.Название.StartsWith(SearchText.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }
        
    }
}
