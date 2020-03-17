using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using WPFBibleThump.Model;

namespace WPFBibleThump.ViewModel
{
    class BooksRegViewModel : INotifyPropertyChanged
    {
        public ICollectionView Authors
        {
            get;
            set;
        }
        public ICollectionView Cities
        {
            get;
            set;
        }
        public ICollectionView UDKs
        {
            get;
            set;
        }
        public ICollectionView Publishes
        {
            get;
            set;
        }

        public string BookCopy
        {
            get => _bookCopy;
            set
            {
                _bookCopy = value;
            }
        }

        private string _searchText;
        private Книги _book;
        private Авторы _selectedAuthor;
        private Авторы _dataGridSelectedAuthor;
        private Экземпляры_книги _dataGridSelectedCopy;
        private string _bookCopy;
        private string _buttonText;
        private string _title;

        public RelayCommand AuthorAddCommand { get; }
        public RelayCommand AuthorDeleteCommand { get; }
        public RelayCommand CopyAddCommand { get; }
        public RelayCommand CopyDeleteCommand { get; }

        public BooksRegViewModel(MOYABAZAEntities model, Книги book)
        {
            _book = book;
            Authors = CollectionViewSource.GetDefaultView(App.MOYABAZA.Авторы.ToArray());
            Cities = CollectionViewSource.GetDefaultView(App.MOYABAZA.Города.ToArray());
            UDKs = CollectionViewSource.GetDefaultView(App.MOYABAZA.Систематический_каталог.ToArray());
            Publishes = CollectionViewSource.GetDefaultView(App.MOYABAZA.Издательства.ToArray());
            if (book.Название == null)
            {
                Title = "Добавление книги";
                ButtonText = "Добавить";
            }
            else
            {
                Title = "Изменение книги";
                ButtonText = "Изменить";
            }

            AuthorAddCommand = new RelayCommand(
                (param) =>
                {
                    if (SelectedBookAuthors.Contains(SelectedAuthor))
                    {
                        MessageBox.Show("Этот автор уже добавлен!");
                    }
                    else if (SelectedAuthor == null)
                    {
                        MessageBox.Show("Автор не найден!");
                    }
                    else
                    {
                        SelectedBookAuthors.Add(SelectedAuthor);
                    }
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.BooksThesaurusName && uo.W == 1) != 0);


            AuthorDeleteCommand = new RelayCommand(
                (param) =>
                {
                    if (DataGridSelectedAuthor == null)
                    {
                        MessageBox.Show("Автор не выбран!");
                    }
                    else
                    {
                        SelectedBookAuthors.Remove(DataGridSelectedAuthor);
                    }
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.BooksThesaurusName && uo.W == 1) != 0);


            CopyAddCommand = new RelayCommand(
                (param) =>
                {
                    var maxInvNumber = model.Экземпляры_книги.Max(b => b.Инвентарный_номер);
                    int newInvNumbersCount = 0;
                    int.TryParse(BookCopy, out newInvNumbersCount);
                    for (int i = maxInvNumber; i < maxInvNumber + newInvNumbersCount; i++)
                    {
                        var Copy = new Экземпляры_книги();
                        Copy.Инвентарный_номер = i + 1;
                        BookCopies.Add(Copy);
                    }
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.BooksThesaurusName && uo.W == 1) != 0);

            CopyDeleteCommand = new RelayCommand(
                (param) =>
                {
                    if (DataGridSelectedCopy == null)
                    {
                        MessageBox.Show("Экземпляр не выбран!");
                    }
                    else
                    {
                        BookCopies.FirstOrDefault(bc => bc == DataGridSelectedCopy).Архивировано = true;
                        BookCopies.Remove(DataGridSelectedCopy);
                    }
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.BooksThesaurusName && uo.W == 1) != 0);

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
            Авторы authors = o as Авторы;
            if (String.IsNullOrEmpty(SearchText) ||
                authors.FullName.Equals(SearchText) ||
                authors.Имя.StartsWith(SearchText.Trim(), StringComparison.OrdinalIgnoreCase) ||
                authors.Фамилия.StartsWith(SearchText.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
            }
        }

        public string ButtonText
        {
            get { return _buttonText; }
            set
            {
                _buttonText = value;
            }
        }

        public string Name
        {
            get { return _book.Название; }
            set
            {
                _book.Название = value;
            }
        }
        public Издательства Publish
        {
            get { return _book.Издательства; }
            set
            {
                _book.Издательства = value;
            }
        }

        public Города City
        {
            get { return _book.Города; }
            set
            {
                _book.Города = value;
            }
        }

        public string PublishingYear
        {
            get { return _book.Год_издания; }
            set
            {
                _book.Год_издания = value;
            }
        }

        public Систематический_каталог UDK
        {
            get { return App.MOYABAZA.Систематический_каталог.FirstOrDefault(x => x.Код == _book.УДК); }
            set
            {
                _book.УДК = value.Код;
            }
        }

        public int CopiesNumber
        {
            get { return 1; }
            set
            {

            }
        }

        private ICollection<Авторы> _selectedBookAuthors = null;
        public ICollection<Авторы> SelectedBookAuthors
        {
            get
            {
                if (_selectedBookAuthors == null)
                {
                    var selectedBookAuthors = new ObservableCollection<Авторы>(_book.Авторы);
                    selectedBookAuthors.CollectionChanged += SelectedBookAuthors_CollectionChanged;
                    _selectedBookAuthors = selectedBookAuthors;
                }
                return _selectedBookAuthors;
            }

        }

        private void SelectedBookAuthors_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                _book.Авторы.Remove(e.OldItems[0] as Авторы);
            }
            else
            {
                _book.Авторы.Add(e.NewItems[0] as Авторы);
            }
        }

        private ICollection<Экземпляры_книги> _bookCopies = null;
        public ICollection<Экземпляры_книги> BookCopies
        {
            get
            {
                if (_bookCopies == null)
                {
                    var bookCopies = new ObservableCollection<Экземпляры_книги>(_book.Экземпляры_книги.Where(e => e.Архивировано == false));
                    bookCopies.CollectionChanged += BookCopies_CollectionChanged;
                    _bookCopies = bookCopies;
                }
                return _bookCopies;
            }

        }

        private void BookCopies_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                _book.Экземпляры_книги.Remove(e.OldItems[0] as Экземпляры_книги);
            }
            else
            {
                _book.Экземпляры_книги.Add(e.NewItems[0] as Экземпляры_книги);
            }
        }

        public Авторы SelectedAuthor
        {
            get { return _selectedAuthor; }
            set
            {
                _selectedAuthor = value;
            }
        }

        public Авторы DataGridSelectedAuthor
        {
            get { return _dataGridSelectedAuthor; }
            set
            {
                _dataGridSelectedAuthor = value;
            }
        }


        public Экземпляры_книги DataGridSelectedCopy
        {
            get { return _dataGridSelectedCopy; }
            set
            {
                _dataGridSelectedCopy = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
