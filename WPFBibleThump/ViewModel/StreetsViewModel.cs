﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WPFBibleThump.Model;

namespace WPFBibleThump.ViewModel
{
    class StreetsViewModel
    {
        private string _searchText;
        public ICollectionView Streets { get; set; }

        public StreetsViewModel()
        {
            App.MOYABAZA.Улицы.Load();
            Streets = CollectionViewSource.GetDefaultView(App.MOYABAZA.Улицы.Local);
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
            Улицы ulica = o as Улицы;
            if (String.IsNullOrEmpty(SearchText) || ulica.Название.StartsWith(SearchText.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }
    }
}