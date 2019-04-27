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
    class CityViewModel
    {
        private string _searchText;
        public ICollectionView Cities { get; set; }

        public CityViewModel()
        {
            App.MOYABAZA.Города.Load();
            Cities = CollectionViewSource.GetDefaultView(App.MOYABAZA.Города.Local);
            Cities.Filter = FilterFunction;
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                Cities.Refresh();
            }
        }

        bool FilterFunction(object o)
        {
            Города сity = o as Города;
            if (String.IsNullOrEmpty(SearchText) || сity.Название.StartsWith(SearchText.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }
    }
}
