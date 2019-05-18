using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WPFBibleThump.Model;

namespace WPFBibleThump.ViewModel
{
    class StreetsViewModel
    {
        private string _searchText;
        public ICollectionView Streets { get; set; }

        public RelayCommand AddCommand { get; }
        public RelayCommand ChangeCommand { get; }
        public RelayCommand DeleteCommand { get; }

        public StreetsViewModel()
        {
            App.MOYABAZA.Улицы.Load();
            Streets = CollectionViewSource.GetDefaultView(App.MOYABAZA.Улицы.Local);

            AddCommand = new RelayCommand((param) => { }, (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.StreetThesaurusName && uo.W == 1) != 0);
            ChangeCommand = new RelayCommand((param) => { },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.StreetThesaurusName && uo.E == 1) != 0 && param != null);
            DeleteCommand = new RelayCommand((param) => { },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.StreetThesaurusName && uo.D == 1) != 0 && param != null);
            Streets.Filter = FilterFunction;
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                Streets.Refresh();
            }
        }

        bool FilterFunction(object o)
        {
            Улицы ulica = o as Улицы;
            if (String.IsNullOrEmpty(SearchText) || ulica.Название.StartsWith(SearchText.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }
    }
}