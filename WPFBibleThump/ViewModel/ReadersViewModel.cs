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
    class ReadersViewModel
    {
        private string _searchText;
        public ICollectionView Readers { get; set; }

        public ReadersViewModel()
        {
            App.MOYABAZA.Читатели.Load();
            Readers = CollectionViewSource.GetDefaultView(App.MOYABAZA.Читатели.Local);
            Readers.Filter = FilterFunction;
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
            Читатели readers = o as Читатели;
            if (String.IsNullOrEmpty(SearchText) || readers.Фамилия.StartsWith(SearchText.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }
    }
}
