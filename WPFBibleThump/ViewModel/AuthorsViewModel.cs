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
    class AuthorsViewModel : INotifyPropertyChanged
    {
        private string _searchText;
        private Авторы _selectedAuthor;
        private string _fName;
        private string _sName;
        private string _tName;
        private bool _editAllowed;
        public ICollectionView Authors { get; set; }

        public RelayCommand AddCommand { get; }
        public RelayCommand AddModeCommand { get; }
        public RelayCommand ChangeModeCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand SaveCommand { get; }

        public AuthorsViewModel()
        {
            MOYABAZAEntities model = App.MOYABAZA;
            model.Авторы.Load();
            Authors = CollectionViewSource.GetDefaultView(model.Авторы.Local);

            AddModeCommand = new RelayCommand(
                (param) =>
                {
                    SelectedAuthor = null;
                    EditAllowed = true;
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.AuthorThesaurusName && uo.W == 1) != 0);

            ChangeModeCommand = new RelayCommand(
                (param) =>
                {
                    EditAllowed ^= true;
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.AuthorThesaurusName && uo.E == 1) != 0 && param != null);

            SaveCommand = new RelayCommand(
                (param) =>
                {
                    if (SelectedAuthor != null)
                    {
                        if (FName != String.Empty && SName != String.Empty && TName != String.Empty)    //Изменение существующего города
                        {
                            try
                            {
                                SelectedAuthor.Имя = FName;
                                SelectedAuthor.Фамилия = SName;
                                SelectedAuthor.Отчество = TName;
                                model.SaveChanges();
                                Authors.Refresh();
                                EditAllowed = false;
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show($"Такой город уже существует! \n {e.Message}");
                            }
                        }
                        else
                        {
                            MessageBox.Show("ФИО не может быть пустым!");
                            FName = SelectedAuthor.Имя;
                            SName = SelectedAuthor.Фамилия;
                            TName = SelectedAuthor.Отчество;
                        }
                    }
                    else
                    {
                        if (FName != String.Empty && SName != String.Empty && TName != String.Empty)    //Добавление нового города
                        {
                            Авторы author = new Авторы();
                            try
                            {
                                author.Имя = FName;
                                author.Фамилия = SName;
                                author.Отчество = TName;
                                model.Авторы.Local.Add(author);
                                model.SaveChanges();
                                EditAllowed = false;
                                FName = String.Empty;
                                SName = String.Empty;
                                TName = String.Empty;
                            }
                            catch (DbUpdateException e)
                            {
                                model.Авторы.Local.Remove(author);
                                MessageBox.Show($"Такой автор уже существует! \n {e.Message}");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Название не может быть пустым!");
                        }
                    }
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.AuthorThesaurusName && (uo.E == 1 || uo.R == 1)) != 0 && Convert.ToBoolean(param) == true);

            DeleteCommand = new RelayCommand(
                (param) =>
                {
                    if (MessageBox.Show("Уверен?", "Назад дороги не будет", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                    {
                        var deletedAuthor = _selectedAuthor;

                        try
                        {
                            if (deletedAuthor.Книги.Count != 0)
                            {
                                throw new DbUpdateException("В авторе есть книги!!!!");
                            }
                            model.Авторы.Local.Remove(deletedAuthor);
                            model.SaveChanges();
                        }
                        catch (DbUpdateException ex)
                        {
                            model.Авторы.Local.Add(deletedAuthor);
                            Authors.MoveCurrentTo(deletedAuthor);
                            Authors.Refresh();
                            MessageBox.Show($"Произошла ошибка при удалении данных: {Environment.CommandLine}{ex.Message}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.AuthorThesaurusName && uo.D == 1) != 0 && param != null);
            Authors.Filter = FilterFunction;
        }

        public Авторы SelectedAuthor
        {
            get { return _selectedAuthor; }
            set
            {
                _selectedAuthor = value;
                EditAllowed = false;
                if (_selectedAuthor != null)
                {
                    FName = _selectedAuthor.Имя;
                    SName = _selectedAuthor.Фамилия;
                    TName = _selectedAuthor.Отчество;
                }
                else
                {
                    FName = String.Empty;
                    SName = String.Empty;
                    TName = String.Empty;
                }
                OnPropertyChanged();
            }
        }

        public string FName
        {
            get { return _fName; }
            set
            {
                _fName = value;
                OnPropertyChanged();
            }
        }
        public string SName
        {
            get { return _sName; }
            set
            {
                _sName = value;
                OnPropertyChanged();
            }
        }
        public string TName
        {
            get { return _tName; }
            set
            {
                _tName = value;
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
                Authors.Refresh();
            }
        }

        bool FilterFunction(object o)
        {
            Авторы ulica = o as Авторы;
            if (String.IsNullOrEmpty(SearchText) ||
                ulica.Имя.StartsWith(SearchText.Trim(), StringComparison.OrdinalIgnoreCase) ||
                ulica.Фамилия.StartsWith(SearchText.Trim(), StringComparison.OrdinalIgnoreCase))
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
