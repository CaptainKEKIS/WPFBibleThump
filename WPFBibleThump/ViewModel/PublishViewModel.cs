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
    class PublishViewModel
    {
        private string _searchText;
        public ICollectionView Publishes { get; set; }

        public PublishViewModel()
        {
            App.MOYABAZA.Издательства.Load();
            Publishes = CollectionViewSource.GetDefaultView(App.MOYABAZA.Издательства.Local);
            Publishes.Filter = FilterFunction;
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                Publishes.Refresh();
            }
        }

        bool FilterFunction(object o)
        {
            Издательства publ = o as Издательства;
            if (String.IsNullOrEmpty(SearchText) || publ.Название.StartsWith(SearchText.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }
    }
}
