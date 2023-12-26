using BCSH2_Cizek;
using BCSH2_Cizek.ViewModel;
using BCSH2_Cizek.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiteDB;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TeamsLibrary;

namespace BCSHP2_Cizek
{
    [ObservableObject]
    public partial class MainViewModel
    {
        public object CurrentViewModel { get; set; }

        public MainViewModel() {
            CurrentViewModel = new TeamListViewModel(new TeamRepository());
        }
    }
}
