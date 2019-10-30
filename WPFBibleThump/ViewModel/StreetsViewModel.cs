using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    class StreetsViewModel : INotifyPropertyChanged
    {
        private string _searchText;
        private Улицы _selectedStreet;
        private string _streetTextBox;
        private bool _editAllowed;
        public ICollectionView Streets { get; set; }

        public RelayCommand AddCommand { get; }
        public RelayCommand AddModeCommand { get; }
        public RelayCommand ChangeModeCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand SaveCommand { get; }


        public StreetsViewModel()
        {
            MOYABAZAEntities model = App.MOYABAZA;
            model.Улицы.Load();
            Streets = CollectionViewSource.GetDefaultView(model.Улицы.Local);

            AddModeCommand = new RelayCommand(
                (param) =>
                {
                    SelectedStreet = null;
                    EditAllowed = true;
                    //model.Города.Local.Add(city);
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.StreetThesaurusName && uo.W == 1) != 0);

            ChangeModeCommand = new RelayCommand(
                (param) =>
                {
                    EditAllowed ^= true;
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.StreetThesaurusName && uo.E == 1) != 0 && param != null);

            SaveCommand = new RelayCommand(
                (param) =>
                {
                    if (SelectedStreet != null)
                    {
                        if (StreetTextBox != String.Empty)    //Изменение существующего города
                        {
                            try
                            {
                                SelectedStreet.Название = StreetTextBox;
                                model.SaveChanges();
                                Streets.Refresh();
                                EditAllowed = false;
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show($"Такая улица уже существует! \n {e.Message}");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Название не может быть пустым!");
                            StreetTextBox = SelectedStreet.Название;
                        }
                    }
                    else
                    {
                        if (StreetTextBox != String.Empty)    //Добавление нового города
                        {
                            Улицы street = new Улицы();
                            try
                            {
                                street.Название = StreetTextBox;
                                model.Улицы.Local.Add(street);
                                model.SaveChanges();
                                EditAllowed = false;
                                StreetTextBox = String.Empty;
                            }
                            catch (DbUpdateException e)
                            {
                                model.Улицы.Local.Remove(street);
                                MessageBox.Show($"Такая улица уже существует! \n {e.Message}");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Название не может быть пустым!");
                        }
                    }
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.StreetThesaurusName && (uo.E == 1 || uo.R == 1)) != 0 && Convert.ToBoolean(param) == true);

            DeleteCommand = new RelayCommand(
                (param) =>
                {
                    if (MessageBox.Show("Уверен?", "Назад дороги не будет", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                    {
                        var deletedStreet = _selectedStreet;

                        try
                        {
                            if (deletedStreet.Читатели.Count != 0)
                            {
                                throw new DbUpdateException("В улице есть читатели!!!!");
                            }
                            model.Улицы.Local.Remove(deletedStreet);
                            model.SaveChanges();
                        }
                        catch (DbUpdateException ex)
                        {

                            model.Улицы.Local.Add(deletedStreet);
                            Streets.MoveCurrentTo(deletedStreet);
                            Streets.Refresh();
                            MessageBox.Show($"Произошла ошибка при удалении данных: {Environment.CommandLine}{ex.Message}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.StreetThesaurusName && uo.D == 1) != 0 && param != null);
            Streets.Filter = FilterFunction;
        }

        public Улицы SelectedStreet
        {
            get { return _selectedStreet; }
            set
            {
                _selectedStreet = value;
                EditAllowed = false;
                if (_selectedStreet != null)
                {
                    StreetTextBox = _selectedStreet.Название;
                }
                else { StreetTextBox = null; }
                OnPropertyChanged();
            }
        }

        public string StreetTextBox
        {
            get { return _streetTextBox; }
            set
            {
                _streetTextBox = value;
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

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}