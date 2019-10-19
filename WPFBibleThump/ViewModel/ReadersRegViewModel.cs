using System;
using System.Collections;
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
    class ReadersRegViewModel
    {
        public ICollectionView Streets
        {
            get;
            set;
        }
        private string _searchText;

        public RelayCommand AddCommand { get; }
        public RelayCommand ChangeCommand { get; }
        public RelayCommand DeleteCommand { get; }

        public ReadersRegViewModel()
        {
            //App.MOYABAZA.Улицы.Load();
            Streets = CollectionViewSource.GetDefaultView(App.MOYABAZA.Улицы.ToArray());
            AddCommand = new RelayCommand((param) => { }, (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.AuthorThesaurusName && uo.W == 1) != 0);

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
            Улицы streets = o as Улицы;
            string Name = streets.Название;
            if (String.IsNullOrEmpty(SearchText) || streets.Название.StartsWith(SearchText.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

    }
}
