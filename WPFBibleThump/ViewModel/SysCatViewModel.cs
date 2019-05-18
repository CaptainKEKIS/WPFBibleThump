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

        public RelayCommand AddCommand { get; }
        public RelayCommand ChangeCommand { get; }
        public RelayCommand DeleteCommand { get; }

        public SysCatViewModel()
        {
            App.MOYABAZA.Систематический_каталог.Load();
            SysCat = CollectionViewSource.GetDefaultView(App.MOYABAZA.Систематический_каталог.Local);

            AddCommand = new RelayCommand((param) => { }, (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.SysCatalogueName && uo.W == 1) != 0);
            ChangeCommand = new RelayCommand((param) => { },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.SysCatalogueName && uo.E == 1) != 0 && param != null);
            DeleteCommand = new RelayCommand((param) => { },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.SysCatalogueName && uo.D == 1) != 0 && param != null);
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
