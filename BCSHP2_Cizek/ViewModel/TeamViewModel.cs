using BCSH2_Cizek;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsLibrary;

namespace BCSHP2_Cizek
{
    [ObservableObject]
    public partial class TeamViewModel
    {
        [ObservableProperty]
        private Team team;
        public TeamViewModel(Team team)
        {
            this.team = team;
        }

        [RelayCommand]
        public void ShowMainWindow()
        {
            var window = new MainWindow();
            window.Show();
        }
    }
}
