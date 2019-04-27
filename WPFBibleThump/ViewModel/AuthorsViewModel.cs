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
    class AuthorsViewModel
    {
        private string _searchText;
        public ICollectionView Authors { get; set; }

        public AuthorsViewModel()
        {
            App.MOYABAZA.Авторы.Load();
            Authors = CollectionViewSource.GetDefaultView(App.MOYABAZA.Авторы.Local);
            //Authors.Filter = FilterFunction;
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                Authors.Refresh();
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
