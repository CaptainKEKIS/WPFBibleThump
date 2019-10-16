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
        private string _cityTextBox;
        private bool _editAllowed;
        public ICollectionView Cities { get; set; }

        public RelayCommand AddCommand { get; }
        public RelayCommand AddModeCommand { get; }
        public RelayCommand ChangeModeCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand SaveCommand { get; }


        public CityViewModel()
        {
            MOYABAZAEntities model = App.MOYABAZA;
            model.Города.Load();
            Cities = CollectionViewSource.GetDefaultView(model.Города.Local);

            AddModeCommand = new RelayCommand(
                (param) =>
                {
                    SelectedCity = null;
                    EditAllowed = true;
                    //model.Города.Local.Add(city);
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.CityThesaurusName && uo.W == 1) != 0);
            
            ChangeModeCommand = new RelayCommand(
                (param) =>
                {
                    EditAllowed ^= true;
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.CityThesaurusName && uo.E == 1) != 0 && param != null);

            SaveCommand = new RelayCommand(
                (param) =>
                {
                    if (SelectedCity != null)
                    {
                        if (CityTextBox != null)    //Изменение существующего города
                        {
                            try
                            {
                                SelectedCity.Название = CityTextBox;
                                model.SaveChanges();
                                Cities.Refresh();
                                EditAllowed = false;
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show($"Такой город уже существует! \n {e.Message}");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Название не может быть пустым!");
                        }
                    }
                    else
                    {
                        if (CityTextBox != null)    //Добавление нового города
                        {
                            Города city = new Города();
                            try
                            {
                                city.Название = CityTextBox;
                                model.Города.Local.Add(city);
                                model.SaveChanges();
                                EditAllowed = false;
                                CityTextBox = String.Empty;
                            }
                            catch (DbUpdateException e)
                            {
                                model.Города.Local.Remove(city);
                                MessageBox.Show($"Такой город уже существует! \n {e.Message}");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Название не может быть пустым!");
                        }
                    }
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.CityThesaurusName && (uo.E == 1 || uo.R == 1)) != 0 && Convert.ToBoolean(param) == true);

            DeleteCommand = new RelayCommand(
                (param) =>
                {
                    if (MessageBox.Show("Уверен?", "Назад дороги не будет", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                    {
                        var deletedCity = _selectedCity;

                        try
                        {
                            if (deletedCity.Книги.Count != 0)
                            {
                                throw new DbUpdateException("В городе есть книги!!!!");
                            }
                            model.Города.Local.Remove(deletedCity);
                            model.SaveChanges();
                        }
                        catch (DbUpdateException ex)
                        {
                            MessageBox.Show($"Произошла ошибка при удалении данных: {Environment.CommandLine}{ex.Message}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                            model.Города.Local.Add(deletedCity);
                            Cities.MoveCurrentTo(deletedCity);
                            Cities.Refresh();
                        }
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
                EditAllowed = false;
                if (_selectedCity != null)
                {
                    CityTextBox = _selectedCity.Название;
                }
                else { CityTextBox = null; }
                OnPropertyChanged();
            }
        }

        public string CityTextBox
        {
            get { return _cityTextBox; }
            set
            {
                _cityTextBox = value;
                OnPropertyChanged();
            }
        }

        public bool EditAllowed
        {
            get { return _editAllowed; }
            set
            {
                _editAllowed = value;
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
