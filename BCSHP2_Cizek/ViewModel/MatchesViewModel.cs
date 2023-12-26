using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsLibrary;

namespace BCSH2_Cizek.ViewModel
{
    [ObservableObject]
    partial class MatchViewModel
    {
        [ObservableProperty]
        private Match SelectedMatch;
    }
}
