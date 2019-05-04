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

        public RelayCommand AddCommand { get; }
        public RelayCommand DeleteCommand { get; }

        public AuthorsViewModel()
        {
            App.MOYABAZA.Авторы.Load();
            Authors = CollectionViewSource.GetDefaultView(App.MOYABAZA.Авторы.Local);

            AddCommand = new RelayCommand((param) => { }, (param) =>  App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.AuthorThesaurusName && uo.W == 1) != 0 );
            DeleteCommand = new RelayCommand((param) => { }, 
                (param) =>  App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.AuthorThesaurusName && uo.D == 1) != 0 && param != null );
            Authors.Filter = FilterFunction;
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
        
        bool FilterFunction(object o)
        {
            Авторы ulica = o as Авторы;
            if (String.IsNullOrEmpty(SearchText) || 
                ulica.Имя.StartsWith(SearchText.Trim(), StringComparison.OrdinalIgnoreCase)|| 
                ulica.Фамилия.StartsWith(SearchText.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }
        
    }
}
