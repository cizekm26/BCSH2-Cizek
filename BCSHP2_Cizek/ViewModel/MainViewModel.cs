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
        private readonly ITeamRepository _teamRepository;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RemoveCommand))]
        private Team? selectedTeam;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string competition;

        [ObservableProperty]
        private int ranking;
        public IEnumerable<Team> Teams => _teamRepository.Teams;

        public MainViewModel(TeamRepository teamRepository)
        {
            if (teamRepository== null)
                throw new ArgumentNullException(nameof(teamRepository));
            _teamRepository = teamRepository;
        }

        [RelayCommand]
        public void Add()
        {
            _teamRepository.Add(new Team(Name, Ranking, Competition));
        }

        [RelayCommand(CanExecute = nameof(RemoveCanExecute))]
        public void Remove()
        {
            if (SelectedTeam != null)
            {
                _teamRepository.Remove(SelectedTeam);
            }

        }

        [RelayCommand]
        public void Edit()
        {
            if (SelectedTeam != null)
            {
                SelectedTeam.Name = Name;
                SelectedTeam.Ranking = Ranking;
                SelectedTeam.Competition = Competition;
                _teamRepository.Update(SelectedTeam);
            }
        }

        [RelayCommand]
        public void ShowTeam()
        {
            if (SelectedTeam != null)
            {
                var window = new TeamSummaryView(SelectedTeam,_teamRepository);
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

        public void OnSelectedItemChanged()
        {
            if (SelectedTeam!= null)
            {
                Name = SelectedTeam.Name;
                Ranking = SelectedTeam.Ranking;
                Competition = SelectedTeam.Competition;
            }
        }
    }
}
