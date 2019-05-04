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
    class SysCatViewModel
    {
        private string _searchText;
        public ICollectionView SysCat { get; set; }

        public SysCatViewModel()
        {
            App.MOYABAZA.Систематический_каталог.Load();
            SysCat = CollectionViewSource.GetDefaultView(App.MOYABAZA.Систематический_каталог.Local);
            SysCat.Filter = FilterFunction;
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                SysCat.Refresh();
            }
        }

        bool FilterFunction(object o)
        {
            Систематический_каталог cat = o as Систематический_каталог;
            if (String.IsNullOrEmpty(SearchText) || cat.Название.StartsWith(SearchText.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }
    }
}
