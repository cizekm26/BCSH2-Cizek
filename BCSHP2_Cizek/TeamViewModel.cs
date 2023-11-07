using CommunityToolkit.Mvvm.ComponentModel;
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

        public TeamViewModel()
        {
            team = null;
        }
        public TeamViewModel(Team team)
        {
            this.team = team;
        }
    }
}
