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
using System.Windows.Controls;
using TeamsLibrary;

namespace BCSHP2_Cizek
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly ITeamRepository _teamRepository;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RemoveCommand))]
        [NotifyCanExecuteChangedFor(nameof(EditCommand))]
        [NotifyCanExecuteChangedFor(nameof(ShowTeamCommand))]
        private Team selectedTeam;

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
            if (!InputIsValid())
            {
                ShowErrorMessage("Byly zadány neplatné údaje");
                return;
            }
            if (!_teamRepository.Add(new Team(Name, Ranking, Competition)))
                ShowErrorMessage("Přidání týmu se nepodařilo");
        }

        [RelayCommand(CanExecute = nameof(TeamIsSelected))]
        public void Remove()
        {
            if (SelectedTeam != null)
            {
                if(!_teamRepository.Remove(SelectedTeam))
                {
                    ShowErrorMessage("Odstranění týmu se nepodařilo");
                }
            }
            

        }
        
        [RelayCommand(CanExecute = nameof(TeamIsSelected))]
        public void Edit()
        {
            if (!InputIsValid())
            {
                ShowErrorMessage("Byly zadány neplatné údaje");
                return;
            }
            if (SelectedTeam != null)
            {
                SelectedTeam.Name = Name;
                SelectedTeam.Ranking = Ranking;
                SelectedTeam.Competition = Competition;
                if (!_teamRepository.Update(SelectedTeam))
                {
                    ShowErrorMessage("Editace se nepodařila");
                }
            }
        }

        [RelayCommand(CanExecute = nameof(TeamIsSelected))]
        public void ShowTeam()
        {
            if (SelectedTeam != null)
            {
                var window = new TeamSummaryView(SelectedTeam,_teamRepository);
                window.ShowDialog();
            }
        }

        public bool TeamIsSelected()
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

        private bool InputIsValid()
        {
            return Name != null && Competition != null && Name.Length > 0 && Competition.Length > 0 && Ranking > 0;
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
