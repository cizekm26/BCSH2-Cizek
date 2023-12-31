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
    public partial class PlayersViewModel
    {
        [ObservableProperty]
        private string firstName;

        [ObservableProperty]
        private string lastName;

        [ObservableProperty]
        private int age;

        [ObservableProperty]
        private Player selectedPlayer;

        [ObservableProperty]
        private ObservableCollection<Player> players;

        private Team team;
        private ITeamRepository _teamRepository;

        public PlayersViewModel(Team team, ITeamRepository repository) { 
            this.team = team;
            _teamRepository = repository;
            players = new ObservableCollection<Player>(team.Players);
        }

        [RelayCommand]
        public void Add()
        {
            Player player = new Player(FirstName, LastName, Age);
            player.TeamID = team.ID;
            if (_teamRepository.AddPlayer(player))
            {
                Players.Add(player);
            }
            OnPropertyChanged(nameof(team.Players));
        }

        [RelayCommand]
        public void Remove()
        {
            if(SelectedPlayer!= null)
            {
                if (_teamRepository.RemovePlayer(SelectedPlayer))
                {
                    Players.Remove(SelectedPlayer);
                    OnPropertyChanged(nameof(team.Players));
                }
            }
        }

        [RelayCommand]
        public void Edit()
        {
            if (SelectedPlayer!= null)
            {
                SelectedPlayer.Age = Age;
                SelectedPlayer.FirstName = FirstName;
                SelectedPlayer.LastName = LastName;
                _teamRepository.UpdatePlayer(SelectedPlayer);
            }
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
    }
}
