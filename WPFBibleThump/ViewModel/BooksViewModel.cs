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
using WPFBibleThump.View; 

namespace WPFBibleThump.ViewModel
{
    class BooksViewModel
    {
        private string _searchText;
        private Книги _selectedBook;
        public ICollectionView Books { get; set; }

        public RelayCommand AddCommand { get; }
        public RelayCommand ChangeCommand { get; }
        public RelayCommand DeleteCommand { get; }

        public BooksViewModel()
        {
            MOYABAZAEntities model = App.MOYABAZA;
            model.Книги.Load();
            Books = CollectionViewSource.GetDefaultView(model.Книги.Local);

            AddCommand = new RelayCommand((param) =>
            {
                Книги book = new Книги();
                BooksReg readersReg = new BooksReg(model, book);
                readersReg.ShowDialog();
                if (readersReg.DialogResult == true)
                {
                    model.Книги.Local.Add(book);
                    try
                    {
                        model.SaveChanges();
                    }
                    catch (DbUpdateException e)
                    {
                        model.Книги.Local.Remove(book);
                        MessageBox.Show($"Такой книга уже существует! \n {e.Message}");
                    }
                    Books.Refresh();
                }
            },
            (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.BooksThesaurusName && uo.W == 1) != 0);

            ChangeCommand = new RelayCommand((param) =>
            {
                BooksReg readersReg = new BooksReg(model, _selectedBook);
                readersReg.ShowDialog();
                if (readersReg.DialogResult == true)
                {
                    try
                    {
                        model.SaveChanges();
                    }
                    catch (DbUpdateException e)
                    {
                        model.Entry(_selectedBook).State = EntityState.Unchanged;
                        MessageBox.Show($"Такой book уже существует! \n {e.Message}");
                    }
                    Books.Refresh();
                }
                else
                {
                    //model.Entry(_selectedBook).Collection(p => p.Авторы).Load();
                   
                    ((IObjectContextAdapter)model).ObjectContext.Refresh(System.Data.Entity.Core.Objects.RefreshMode.ClientWins, _selectedBook.Авторы);
                    model.Entry(_selectedBook).State = EntityState.Unchanged;

                }
            },
            (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.BooksThesaurusName && uo.E == 1) != 0 && param != null);

            DeleteCommand = new RelayCommand((param) =>
            {
                if (MessageBox.Show("Уверен?", "Назад дороги не будет", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    var deletedBook = _selectedBook;

                    try
                    {
                        if (deletedBook.Экземпляры_книги.Count != 0)
                        {
                            throw new DbUpdateException("У книги есть экземпляры!!!!");
                        }
                        model.Книги.Local.Remove(deletedBook);
                        model.SaveChanges();
                    }
                    catch (DbUpdateException ex)
                    {
                        model.Книги.Local.Add(deletedBook);
                        Books.MoveCurrentTo(deletedBook);
                        Books.Refresh();
                        MessageBox.Show($"Произошла ошибка при удалении данных: {Environment.CommandLine}{ex.Message}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            },
            (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.BooksThesaurusName && uo.D == 1) != 0 && param != null);

            Books.Filter = FilterFunction;
        }

        public Книги SelectedBook
        {
            get { return _selectedBook; }
            set
            {
                _selectedBook = value;
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                Books.Refresh();
            }
        }

        bool FilterFunction(object o)
        {
            Книги book = o as Книги;
            if (String.IsNullOrEmpty(SearchText) ||
                book.Название.StartsWith(SearchText.Trim(), StringComparison.OrdinalIgnoreCase) ||
                book.Издательства.Название.StartsWith(SearchText.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

    }
}
