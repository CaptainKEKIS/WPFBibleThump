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
        private string _bookCopy;
        private string _buttonText;
        private string _title;

        public RelayCommand AuthorAddCommand { get; }
        public RelayCommand CopyAddCommand { get; }

        public BooksRegViewModel(MOYABAZAEntities model, Книги book)
        {
            _book = book;
            Authors = CollectionViewSource.GetDefaultView(App.MOYABAZA.Авторы.ToArray());
            Cities = CollectionViewSource.GetDefaultView(App.MOYABAZA.Города.ToArray());
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

                    }
                    else if (SelectedAuthor == null)
                    {
                    }
                    else
                    {
                        SelectedBookAuthors.Add(SelectedAuthor);
                    }
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.BooksThesaurusName && uo.W == 1) != 0);

            CopyAddCommand = new RelayCommand(
                (param) =>
                {
               /* if (BookCopies.Where(x => x.Инвентарный_номер == BookCopy).FirstOrDefault() != null)
                    {
                        MessageBox.Show("Этот инвентарный номер уже добавлен!");
                    }
                    else if (BookCopy == null)
                    {
                        MessageBox.Show("Инвентарный номер не может быть пустым!");
                    }
                    else
                    {
                        var Copy = new Экземпляры_книги();
                        Copy.Инвентарный_номер = BookCopy;
                        BookCopies.Add(Copy);
                    }*/
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

        public string UDK
        {
            get { return _book.УДК; }
            set
            {
                _book.УДК = value;
            }
        }

        public int CopiesNumber
        {
            get { return _book.Количество_экземпляров; }
            set
            {
                _book.Количество_экземпляров = value;
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
                    var bookCopies = new ObservableCollection<Экземпляры_книги>(_book.Экземпляры_книги);
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


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
