using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WPFBibleThump.Model;

namespace WPFBibleThump.ViewModel
{
    class StreetsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Улицы> Street { get; set; }
	    public string SearchText { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}