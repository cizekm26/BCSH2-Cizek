using BCSH2_Cizek.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TeamsLibrary;

namespace BCSH2_Cizek.ViewModel
{
    [ObservableObject]
    public partial class TeamListViewModel
    {
        private readonly ITeamRepository _teamRepository;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RemoveCommand))]
        private Team? selectedTeam;

        [ObservableProperty]
        private string? name;

        [ObservableProperty]
        private int ranking;

        [ObservableProperty]
        private string? competition;

        public IEnumerable<Team> Teams => _teamRepository.Teams;


        public TeamListViewModel(TeamRepository teamRepository)
        {
            if (teamRepository== null)
                throw new ArgumentNullException(nameof(teamRepository));
            _teamRepository = teamRepository;
        }

        [RelayCommand]
        public void Add()
        {
            //CreateTeamView createTeamView = new CreateTeamView(_teamRepository);
            //createTeamView.Show();
            var team = new Team(Name, Ranking, Competition);
            _teamRepository.Add(team);
            // SelectedTeam = team;
            //OnPropertyChanged("SelectedTeam");
        }

        [RelayCommand(CanExecute = nameof(RemoveCanExecute))]
        public void Remove()
        {
            if (SelectedTeam != null)
                _teamRepository.Remove(SelectedTeam);

        }

        [RelayCommand]
        public void Edit()
        {
            if (SelectedTeam != null)
            {
                CreateTeamView createTeamView = new CreateTeamView(_teamRepository, SelectedTeam);
                if (createTeamView.ShowDialog())
                {

                }
                OnPropertyChanged("SelectedTeam");
                _teamRepository.Update(SelectedTeam);
            }
        }

        [RelayCommand]
        public void ShowTeam()
        {
            if (SelectedTeam != null)
            {
                var window = new TeamView(SelectedTeam);
                window.Show();
            }
        }


        public bool RemoveCanExecute()
        {
            return SelectedTeam!= null;
        }

        public bool ManageCanExecute()
        {
            return SelectedTeam!= null;
        }
    }
}
