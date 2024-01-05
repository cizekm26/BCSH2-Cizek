using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using TeamsLibrary;

namespace BCSH2_Cizek.ViewModel
{
    [ObservableObject]
    public partial class PlayersViewModel
    {
        [ObservableProperty]
        private string firstName;

        [ObservableProperty]
        private string lastName;

        [ObservableProperty]
        private int age;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RemoveCommand))]
        [NotifyCanExecuteChangedFor(nameof(EditCommand))]
        private Player selectedPlayer;

        [ObservableProperty]
        private ObservableCollection<Player> players;

        private readonly Team team;
        private readonly ITeamRepository _teamRepository;

        public PlayersViewModel(Team team, ITeamRepository repository) { 
            this.team = team;
            _teamRepository = repository;
            players = new ObservableCollection<Player>(team.Players);
        }

        [RelayCommand]
        public void Add()
        {
            if (!InputIsValid()) {
                ShowErrorMessage("Byly zadány neplatné údaje");
                return;
            }
            Player player = new(FirstName, LastName, Age, team.ID);
            if (_teamRepository.AddPlayer(player))
            {
                Players.Add(player);
            }
            else
            {
                ShowErrorMessage("Přidání týmu se nepodařilo");
            }
            OnPropertyChanged(nameof(team.Players));
        }

        [RelayCommand(CanExecute = nameof(PlayerIsSelected))]
        public void Remove()
        {
            if(SelectedPlayer!= null)
            {
                if (_teamRepository.RemovePlayer(SelectedPlayer))
                {
                    Players.Remove(SelectedPlayer);
                    OnPropertyChanged(nameof(team.Players));
                }
                else
                {
                    ShowErrorMessage("Smazání se nepodařilo");
                }
            }
        }

        [RelayCommand(CanExecute = nameof(PlayerIsSelected))]
        public void Edit()
        {
            if (!InputIsValid())
            {
                ShowErrorMessage("Byly zadány neplatné údaje");
                return;
            }
            if (SelectedPlayer!= null)
            {
                SelectedPlayer.Age = Age;
                SelectedPlayer.FirstName = FirstName;
                SelectedPlayer.LastName = LastName;
                if (!_teamRepository.UpdatePlayer(SelectedPlayer))
                    ShowErrorMessage("Editace se nepodařila");
            }
        }

        public bool PlayerIsSelected()
        {
            return SelectedPlayer!= null;
        }
        public void OnSelectedItemChanged()
        {
            if (SelectedPlayer!= null)
            {
                FirstName = SelectedPlayer.FirstName;
                LastName = SelectedPlayer.LastName;
                Age = SelectedPlayer.Age;
            }
        }

        private bool InputIsValid()
        {
            return FirstName != null && LastName != null && FirstName.Length > 0 && LastName.Length > 0 && Age > 0;
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
