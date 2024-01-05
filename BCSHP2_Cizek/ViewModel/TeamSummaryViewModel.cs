using BCSH2_Cizek.Views;
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
        private ObservableCollection<Player> teamPlayers;

        [ObservableProperty]
        private ObservableCollection<Match> teamMatches;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ShowMatchCommand))]
        private Match selectedMatch;

        private readonly ITeamRepository _teamRepository;

        public TeamSummaryViewModel(Team team, ITeamRepository teamRepository)
        {
            this.team = team;
            _teamRepository = teamRepository;
            teamPlayers = new ObservableCollection<Player>(team.Players);
            teamMatches = new ObservableCollection<Match>(team.Matches);
        }

        [RelayCommand(CanExecute = nameof(MatchIsSelected))]
        public void ShowMatch()
        {
            MatchDetailView view = new MatchDetailView(SelectedMatch);
            view.Show();
        }

        [RelayCommand]
        public void RefreshMatches()
        {
            TeamMatches.Clear();
            foreach (Match match in Team.Matches) {
                TeamMatches.Add(match);
            }
        }

        [RelayCommand]
        public void RefreshPlayers()
        {
            TeamPlayers.Clear();
            foreach (Player player in Team.Players)
            {
                TeamPlayers.Add(player);
            }
        }

        public bool MatchIsSelected()
        {
            return SelectedMatch!= null;
        }
    }
}
