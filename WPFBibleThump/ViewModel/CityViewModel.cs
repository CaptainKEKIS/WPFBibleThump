using System;
using System.Collections.Generic;
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
    class CityViewModel : INotifyPropertyChanged
    {
        private string _searchText;
        private Города _selectedCity;
        public ICollectionView Cities { get; set; }

        public RelayCommand AddCommand { get; }
        public RelayCommand ChangeCommand { get; }
        public RelayCommand DeleteCommand { get; }

        public CityViewModel()
        {
            App.MOYABAZA.Города.Load();
            MOYABAZAEntities model = new MOYABAZAEntities();
            Cities = CollectionViewSource.GetDefaultView(model.Города.ToArray());

            AddCommand = new RelayCommand(
                (param) => 
                {
                    Города city = new Города
                    {
                        Название = "Дефолт Сити"
                    };
                    model.Города.Add(city);
                    model.SaveChanges();
                }, 
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.CityThesaurusName && uo.W == 1) != 0);

            ChangeCommand = new RelayCommand(
                (param) => 
                {

                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.CityThesaurusName && uo.E == 1) != 0 && param != null);

            DeleteCommand = new RelayCommand(
                (param) => 
                {
                    model.Города.Remove(_selectedCity);
                    model.SaveChanges();
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.CityThesaurusName && uo.D == 1) != 0 && param != null);

            Cities.Filter = FilterFunction;
        }

        public Города SelectedCity
        {
            get { return _selectedCity; }
            set
            {
                _selectedCity = value;
                OnPropertyChanged();
            }
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

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
