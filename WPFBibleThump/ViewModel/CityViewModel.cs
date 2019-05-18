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

        public RelayCommand AddCommand { get; }
        public RelayCommand ChangeCommand { get; }
        public RelayCommand DeleteCommand { get; }

        public CityViewModel()
        {
            App.MOYABAZA.Города.Load();
            Cities = CollectionViewSource.GetDefaultView(App.MOYABAZA.Города.Local);

            AddCommand = new RelayCommand((param) => { }, (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.CityThesaurusName && uo.W == 1) != 0);
            ChangeCommand = new RelayCommand((param) => { },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.CityThesaurusName && uo.E == 1) != 0 && param != null);
            DeleteCommand = new RelayCommand((param) => { },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.CityThesaurusName && uo.D == 1) != 0 && param != null);
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
