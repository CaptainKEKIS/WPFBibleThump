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

        public RelayCommand AddCommand { get; }
        public RelayCommand ChangeCommand { get; }
        public RelayCommand DeleteCommand { get; }

        public PublishViewModel()
        {
            App.MOYABAZA.Издательства.Load();
            Publishes = CollectionViewSource.GetDefaultView(App.MOYABAZA.Издательства.Local);

            AddCommand = new RelayCommand((param) => { }, (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.PublishThesaurusName && uo.W == 1) != 0);
            ChangeCommand = new RelayCommand((param) => { },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.PublishThesaurusName && uo.E == 1) != 0 && param != null);
            DeleteCommand = new RelayCommand((param) => { },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.PublishThesaurusName && uo.D == 1) != 0 && param != null);
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
