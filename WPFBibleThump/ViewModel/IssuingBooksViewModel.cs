using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPFBibleThump.ViewModel
{
    class IssuingBooksViewModel
    {
        public ICollectionView IssuingBooks { get; set; }
        public IssuingBooksViewModel()
        {
            App.MOYABAZA.Выданные_книги.Load();
            IssuingBooks = CollectionViewSource.GetDefaultView(App.MOYABAZA.Выданные_книги.Local);
        }
    }
}
