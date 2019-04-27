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

        public BooksViewModel()
        {
            App.MOYABAZA.Книги.Load();
            Books = CollectionViewSource.GetDefaultView(App.MOYABAZA.Книги.Local);
            //Authors.Filter = FilterFunction;
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
