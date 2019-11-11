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
                Читатели reader = new Читатели();
                ReadersReg readersReg = new ReadersReg(model, reader);
                readersReg.ShowDialog();
                if (readersReg.DialogResult == true)
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
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.BooksThesaurusName && uo.W == 1) != 0);
            ChangeCommand = new RelayCommand((param) =>
            {
                ReadersReg readersReg = new ReadersReg(model, _selectedReader);
                readersReg.ShowDialog();
                if (readersReg.DialogResult == true)
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
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.BooksThesaurusName && uo.E == 1) != 0 && param != null);
            DeleteCommand = new RelayCommand((param) =>
            {
                model.Читатели.Remove(_selectedReader);
                model.SaveChanges();
            },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.BooksThesaurusName && uo.D == 1) != 0 && param != null);
            Books.Filter = FilterFunction;
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
            Книги ulica = o as Книги;
            if (String.IsNullOrEmpty(SearchText) ||
                ulica.Название.StartsWith(SearchText.Trim(), StringComparison.OrdinalIgnoreCase) ||
                ulica.Издательства.Название.StartsWith(SearchText.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

    }
}
