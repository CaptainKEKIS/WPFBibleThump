using System;
using System.Collections.Generic;
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
        private string _searchText;
        private Книги _book;
        public Авторы _selectedAuthor;
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
                    Author.Add(SelectedAuthor);
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.BooksThesaurusName && uo.W == 1) != 0);

            CopyAddCommand = new RelayCommand(
                (param) =>
                {
                    if (!book.Экземпляры_книги.Contains(BookCopy))
                    {
                        ICollection<Экземпляры_книги> ek;
                        ek.Add(BookCopy);
                        BookCopies = ek;
                    }
                    else
                    {
                        MessageBox.Show("экземпляр книги с таким инвнтарным номером уже существует!");
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

        public ICollection<Авторы> Author
        {
            get { return _book.Авторы; }
            set
            {
                _book.Авторы = value;
                OnPropertyChanged();
            }
        }
        
        public ICollection<Экземпляры_книги> BookCopies
        {
            get { return _book.Экземпляры_книги; }
            set
            {
                _book.Экземпляры_книги = value;
                OnPropertyChanged();
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

        public Экземпляры_книги BookCopy { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
