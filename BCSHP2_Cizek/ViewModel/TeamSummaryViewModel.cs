using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TeamsLibrary;

namespace BCSH2_Cizek.ViewModel
{
    [ObservableObject]
    public partial class TeamSummaryViewModel
    {
        [ObservableProperty]
        private Team team;

        [ObservableProperty]
        private ObservableCollection<Player> playersTeam;

        [ObservableProperty]
        private ObservableCollection<Match> matchesTeam;

        private readonly ITeamRepository _teamRepository;

        public TeamSummaryViewModel(Team team, ITeamRepository teamRepository)
        {
            this.team = team;
            _teamRepository = teamRepository;
            playersTeam = new ObservableCollection<Player>(team.Players);
            matchesTeam = new ObservableCollection<Match>(team.Matches);
        }

        [RelayCommand]
        public void RefreshMatches()
        {
            MatchesTeam.Clear();
            foreach (Match match in team.Matches) {
                MatchesTeam.Add(match);
            }
        }

        [RelayCommand]
        public void RefreshPlayers()
        {
            PlayersTeam.Clear();
            foreach (Player player in Team.Players)
            {
                PlayersTeam.Add(player);
            }
        }
    }
}
