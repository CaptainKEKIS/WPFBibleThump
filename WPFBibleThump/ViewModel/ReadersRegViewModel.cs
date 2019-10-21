using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using WPFBibleThump.Model;

namespace WPFBibleThump.ViewModel
{
    class ReadersRegViewModel
    {
        public ICollectionView Streets
        {
            get;
            set;
        }
        private string _searchText;
        private Читатели _reader;

        public RelayCommand AddCommand { get; }
        public RelayCommand ChangeCommand { get; }
        public RelayCommand DeleteCommand { get; }

        public ReadersRegViewModel(MOYABAZAEntities model, Читатели reader)
        {
            //App.MOYABAZA.Улицы.Load();
            _reader = reader;
            Streets = CollectionViewSource.GetDefaultView(App.MOYABAZA.Улицы.ToArray());

            AddCommand = new RelayCommand(
                (param) =>
                {
                    
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.AuthorThesaurusName && uo.W == 1) != 0);

            Streets.Filter = FilterFunction;
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
            Улицы streets = o as Улицы;
            string Name = streets.Название;
            if (String.IsNullOrEmpty(SearchText) || streets.Название.StartsWith(SearchText.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public string Name
        {
            get { return _reader.Имя; }
            set
            {
                _reader.Имя = value;
            }
        }
        public string ReaderNumber
        {
            get { return _reader.Номер_читательского_билета; }
            set
            {
                _reader.Номер_читательского_билета = value;
            }
        }

        public string SName
        {
            get { return _reader.Фамилия; }
            set
            {
                _reader.Фамилия = value;
            }
        }

        public string TName
        {
            get { return _reader.Отчество; }
            set
            {
                _reader.Отчество = value;
            }
        }

        public string MobileNumber
        {
            get { return _reader.Телефон; }
            set
            {
                _reader.Телефон = value;
            }
        }

        public Улицы Street
        {
            get { return _reader.Улицы; }
            set
            {
                _reader.Улицы = value;
            }
        }

        public string HouseNumber
        {
            get { return _reader.Номер_дома; }
            set
            {
                _reader.Номер_дома = value;
            }
        }

        public string Appartment
        {
            get { return _reader.Квартира; }
            set
            {
                _reader.Квартира = value;
            }
        }

        public DateTime RegistrationTime
        {
            get { return _reader.Дата_регистрации; }
            set
            {
                _reader.Дата_регистрации = value;
            }
        }

        public Nullable<System.DateTime> ReRegistrationTime
        {
            get { return _reader.Дата_перерегистрации; }
            set
            {
                _reader.Дата_перерегистрации = value;
            }
        }

    }
}
