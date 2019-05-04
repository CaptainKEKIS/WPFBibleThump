using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPFBibleThump.ViewModel
{
    class IssuedBViewModel
    {
        private string _searchText;
        public ICollectionView IssuedBooks { get; set; }

        public IssuedBViewModel()
        {
            App.MOYABAZA.Выданные_книги.Load();
            IssuedBooks = CollectionViewSource.GetDefaultView(App.MOYABAZA.Выданные_книги.Local);
            //Authors.Filter = FilterFunction;
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                IssuedBooks.Refresh();
            }
        }
        /*
        bool FilterFunction(object o)
        {
            Авторы ulica = o as Авторы;
            if (String.IsNullOrEmpty(SearchText) || ulica..StartsWith(SearchText.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }
        */
    }
}
