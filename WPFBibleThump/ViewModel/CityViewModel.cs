using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using WPFBibleThump.Model;

namespace WPFBibleThump.ViewModel
{
    class CityViewModel : INotifyPropertyChanged
    {
        private string _searchText;
        private Города _selectedCity;
        private bool _editMode;
        public ICollectionView Cities { get; set; }

        public RelayCommand AddCommand { get; }
        public RelayCommand ChangeCommand { get; }
        public RelayCommand DeleteCommand { get; }

        public CityViewModel()
        {
            MOYABAZAEntities model = App.MOYABAZA;
            model.Города.Load();
            Cities = CollectionViewSource.GetDefaultView(model.Города.Local);

            AddCommand = new RelayCommand(
                (param) =>
                {
                    Города city = new Города
                    {
                        Название = SelectedCity.Название
                    };
                    model.Города.Local.Add(city);
                    model.SaveChanges();
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.CityThesaurusName && uo.W == 1) != 0);

            ChangeCommand = new RelayCommand(
                (param) =>
                {
                    EditMode ^= true;
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.CityThesaurusName && uo.E == 1) != 0 && param != null);

            DeleteCommand = new RelayCommand(
                (param) =>
                {
                    var deletedCity = _selectedCity;
                    
                    try
                    {
                        if(deletedCity.Книги.Count != 0)
                        {
                            throw new DbUpdateException("В городе есть книги!!!!");
                        }
                        model.Города.Local.Remove(deletedCity);
                        model.SaveChanges();
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                    {
                        MessageBox.Show($"Произошла ошибка при удалении данных: {Environment.CommandLine}{ex.Message}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                        model.Города.Local.Add(deletedCity);
                        Cities.MoveCurrentTo(deletedCity);
                        Cities.Refresh();
                    }
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
                EditMode = false;
                OnPropertyChanged();
            }
        }

        public bool EditMode
        {
            get { return _editMode; }
            set
            {
                _editMode = value;
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
