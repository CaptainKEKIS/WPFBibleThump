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
    class PublishViewModel : INotifyPropertyChanged
    {
        private string _searchText;
        private Издательства _selectedPublish;
        private string _publishTextBox;
        private bool _editAllowed;
        public ICollectionView Publishes { get; set; }

        public RelayCommand AddCommand { get; }
        public RelayCommand AddModeCommand { get; }
        public RelayCommand ChangeModeCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand SaveCommand { get; }


        public PublishViewModel()
        {
            MOYABAZAEntities model = App.MOYABAZA;
            model.Издательства.Load();
            Publishes = CollectionViewSource.GetDefaultView(model.Издательства.Local);

            AddModeCommand = new RelayCommand(
                (param) =>
                {
                    SelectedPublish = null;
                    EditAllowed = true;
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.PublishThesaurusName && uo.W == 1) != 0);

            ChangeModeCommand = new RelayCommand(
                (param) =>
                {
                    EditAllowed ^= true;
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.PublishThesaurusName && uo.E == 1) != 0 && param != null);

            SaveCommand = new RelayCommand(
                (param) =>
                {
                    if (SelectedPublish != null)
                    {
                        if (PublishTextBox != String.Empty)    //Изменение существующего
                        {
                            try
                            {
                                SelectedPublish.Название = PublishTextBox;
                                model.SaveChanges();
                                Publishes.Refresh();
                                EditAllowed = false;
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show($"Такое издательство уже существует! \n {e.Message}");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Название не может быть пустым!");
                            PublishTextBox = SelectedPublish.Название;
                        }
                    }
                    else
                    {
                        if (PublishTextBox != String.Empty)    //Добавление нового
                        {
                            Издательства publish = new Издательства();
                            try
                            {
                                publish.Название = PublishTextBox;
                                model.Издательства.Local.Add(publish);
                                model.SaveChanges();
                                EditAllowed = false;
                                PublishTextBox = String.Empty;
                            }
                            catch (DbUpdateException e)
                            {
                                model.Издательства.Local.Remove(publish);
                                MessageBox.Show($"Такое издательство уже существует! \n {e.Message}");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Название не может быть пустым!");
                        }
                    }
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.PublishThesaurusName && (uo.E == 1 || uo.R == 1)) != 0 && Convert.ToBoolean(param) == true);

            DeleteCommand = new RelayCommand(
                (param) =>
                {
                    if (MessageBox.Show("Уверен?", "Назад дороги не будет", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                    {
                        var deletedPublish = _selectedPublish;

                        try
                        {
                            if (deletedPublish.Книги.Count != 0)
                            {
                                throw new DbUpdateException("В издательстве есть книги!!!!");
                            }
                            model.Издательства.Local.Remove(deletedPublish);
                            model.SaveChanges();
                        }
                        catch (DbUpdateException ex)
                        {
                            model.Издательства.Local.Add(deletedPublish);
                            Publishes.MoveCurrentTo(deletedPublish);
                            Publishes.Refresh();
                            MessageBox.Show($"Произошла ошибка при удалении данных: {Environment.CommandLine}{ex.Message}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.PublishThesaurusName && uo.D == 1) != 0 && param != null);
            Publishes.Filter = FilterFunction;
        }

        public Издательства SelectedPublish
        {
            get { return _selectedPublish; }
            set
            {
                _selectedPublish = value;
                EditAllowed = false;
                if (_selectedPublish != null)
                {
                    PublishTextBox = _selectedPublish.Название;
                }
                else { PublishTextBox = null; }
                OnPropertyChanged();
            }
        }

        public string PublishTextBox
        {
            get { return _publishTextBox; }
            set
            {
                _publishTextBox = value;
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

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
