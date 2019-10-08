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
    class ReadersViewModel
    {
        private string _searchText;
        private Читатели _selectedReader;
        public ICollectionView Readers { get; set; }

        public RelayCommand AddCommand { get; }
        public RelayCommand ChangeCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand IssueBookCommand { get; }

        public ReadersViewModel()
        {
            App.MOYABAZA.Читатели.Load();
            MOYABAZAEntities model = new MOYABAZAEntities();
            Readers = CollectionViewSource.GetDefaultView(model.Читатели.ToArray());
            AddCommand = new RelayCommand(
                (param) => 
                {
                    /*
                    Читатели reader = new Читатели
                    {
                        Id_улицы = 1,
                        Фамилия = "qew",
                        Имя = "saddsa",
                        Отчество = "sadasdaaaaaa",
                        Дата_регистрации = DateTime.Parse("19.09.2018"),
                        Квартира = "2",
                        Номер_дома = "3",
                        Номер_читательского_билета = "0000009",
                        Телефон = "+7952215555"
                    };
                    model.Читатели.Add(reader);
                    model.SaveChanges();
                    */
                    ReadersReg readersReg = new ReadersReg();
                    readersReg.Show();
                }, 
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.ReadersName && uo.W == 1) != 0);

            ChangeCommand = new RelayCommand((param) => { },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.ReadersName && uo.E == 1) != 0 && param != null);

            DeleteCommand = new RelayCommand(
                (param) => 
                {
                    model.Читатели.Remove(_selectedReader);
                    model.SaveChanges();
                    Readers.Refresh(); /////NE RABOTAET
                },
                (param) => App.ActiveUser.Пользователи_Объекты.Count(uo => uo.Объекты.SName == Constants.ReadersName && uo.D == 1) != 0 && param != null);

            IssueBookCommand = new RelayCommand(
                (param) => 
                {
                    IssuingBooksFormMVVM issuingBooksForm = new IssuingBooksFormMVVM(SelectedReader);
                    issuingBooksForm.Show();
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
