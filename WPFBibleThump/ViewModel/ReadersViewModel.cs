using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using WPFBibleThump.Model;

namespace WPFBibleThump.ViewModel
{
    class ReadersViewModel
    {
        private string _searchText;
        private Читатели _selectedReader;
        public ICollectionView Readers { get; set; }

        public RelayCommand AddCommand { get; }
        public RelayCommand ChangeCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand ReturnBookCommand { get; }
        public RelayCommand IssueBookCommand { get; }

        public ReadersViewModel()
        {
            MOYABAZAEntities model = App.MOYABAZA;
            model.Читатели.Load();
            Readers = CollectionViewSource.GetDefaultView(model.Читатели.Local);

            AddCommand = new RelayCommand(
                (param) =>
                {
                    Читатели reader = new Читатели();
                    ReadersReg readersReg = new ReadersReg(model, reader);
                    readersReg.ShowDialog();
                    if(readersReg.DialogResult == true)
                    {
                        model.Читатели.Local.Add(reader);
                        try
                        {
                            model.SaveChanges();
                        }
                        catch (DbUpdateException e)
                        {
                            model.Читатели.Local.Remove(reader);
                            MessageBox.Show($"Такой reader уже существует! \n {e.Message}");
                        }
                        Readers.Refresh();
                    }
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.ReadersName && uo.W == 1) != 0);

            ChangeCommand = new RelayCommand(
                (param) =>
                {
                    ReadersReg readersReg = new ReadersReg(model, _selectedReader);
                    readersReg.ShowDialog();
                    if(readersReg.DialogResult == true)
                    {
                        try
                        {
                            model.SaveChanges();
                        }
                        catch (DbUpdateException e)
                        {
                            model.Entry(_selectedReader).State = EntityState.Unchanged;
                            MessageBox.Show($"Такой reader уже существует! \n {e.Message}");
                        }
                        Readers.Refresh();
                    }
                    else
                    {
                        model.Entry(_selectedReader).State = EntityState.Unchanged;
                    }
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.ReadersName && uo.E == 1) != 0 && param != null);

            DeleteCommand = new RelayCommand(
                (param) =>
                {
                    if (MessageBox.Show("Уверен?", "Назад дороги не будет", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            if (_selectedReader.Выданные_книги.Count != 0)
                            {
                                throw new DbUpdateException("У читателя есть выданные книги!!!!");
                            }
                            model.Читатели.Remove(_selectedReader);
                            model.SaveChanges();
                        }
                        catch (DbUpdateException ex)
                        {
                            MessageBox.Show($"Произошла ошибка при удалении данных: {Environment.CommandLine}{ex.Message}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.ReadersName && uo.D == 1) != 0 && param != null);

            ReturnBookCommand = new RelayCommand(
                (param) =>
                {
                    IssuingBooksFormMVVM issuingBooksForm = new IssuingBooksFormMVVM(SelectedReader);
                    issuingBooksForm.ShowDialog();
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.ReadersName && uo.E == 1) != 0 && param != null); // Возможно нужно добавить ещё уровень доступа

            IssueBookCommand = new RelayCommand(
                (param) =>
                {
                    IssuingBooksFormMVVM issuingBooksForm = new IssuingBooksFormMVVM(SelectedReader);
                    issuingBooksForm.ShowDialog();
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.ReadersName && uo.E == 1) != 0 && param != null); // Возможно нужно добавить ещё уровень доступа

            Readers.Filter = FilterFunction;
        }

        public Читатели SelectedReader
        {
            get { return _selectedReader; }
            set
            {
                _selectedReader = value;
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                Readers.Refresh();
            }
        }

        bool FilterFunction(object o)
        {
            Читатели readers = o as Читатели;
            string FIO = readers.Фамилия + " " + readers.Имя + " " + readers.Отчество;
            if (String.IsNullOrEmpty(SearchText)
                || readers.Номер_читательского_билета.StartsWith(SearchText.Trim(), StringComparison.OrdinalIgnoreCase)
                || FIO.StartsWith(SearchText.Trim(), StringComparison.OrdinalIgnoreCase)
                )
            {
                return true;
            }
            return false;
        }
    }
}
