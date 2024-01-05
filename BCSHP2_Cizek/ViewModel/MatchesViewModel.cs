using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TeamsLibrary;

namespace BCSH2_Cizek.ViewModel
{
    [ObservableObject]
    public partial class MatchesViewModel
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RemoveCommand))]
        [NotifyCanExecuteChangedFor(nameof(EditCommand))]
        private Match selectedMatch;

        [ObservableProperty]
        private string opponent;

        [ObservableProperty]
        private DateTime date = DateTime.Today;

        [ObservableProperty]
        private int goalsScored;

        [ObservableProperty]
        private int goalsAgainst;

        [ObservableProperty]
        private MatchType type;

        [ObservableProperty]
        private bool isFinished;

        [ObservableProperty]
        private ObservableCollection<Match> matches;

        [ObservableProperty]
        private ObservableCollection<Goal> goals;

        [ObservableProperty]
        public ObservableCollection<Player> players;

        private readonly Team team;
        private readonly ITeamRepository _teamRepository;

        public MatchesViewModel(Team team, ITeamRepository repository)
        {
            this.team= team;
            _teamRepository= repository;
            Matches= new ObservableCollection<Match>(this.team.Matches);
            Players= new ObservableCollection<Player>(this.team.Players);
            Goals=new ObservableCollection<Goal>();
            OnPropertyChanged(nameof(Players));
        }

        [RelayCommand]
        public void Add()
        {
            if (!InputIsValid())
            {
                ShowErrorMessage("Byly zadány neplatné údaje");
                return;
            }
            Match match;
            // pokud byl zápas dohrán, tak se uloží počet gólů a informace o gólech
            if (IsFinished)
            {
                List<Goal> goals = new List<Goal>();
                foreach (Goal goal in Goals)
                    goals.Add(new Goal(goal.Order, goal.Minute, goal.Scorer));
                match = new(Opponent, Date, GoalsScored, GoalsAgainst, goals, Type, IsFinished, team.ID);
            }
            else
                match = new(Opponent, Date, Type, IsFinished, team.ID);

            if (_teamRepository.AddMatch(match))
            {
                Matches.Add(match);
            }else
                ShowErrorMessage("Přidání zápasu se nepodařilo");
        }

        [RelayCommand(CanExecute = nameof(MatchIsSelected))]
        public void Remove()
        {
            if (SelectedMatch!= null)
            {
                if (_teamRepository.RemoveMatch(SelectedMatch))
                {
                    Matches.Remove(SelectedMatch);
                }
                else
                {
                    ShowErrorMessage("Odstranění se nepodařilo");
                }
            }
        }

        [RelayCommand(CanExecute = nameof(MatchIsSelected))]
        public void Edit()
        {
            if (!InputIsValid())
            {
                ShowErrorMessage("Byly zadány neplatné údaje");
                return;
            }
            if (SelectedMatch!= null)
            {
                SelectedMatch.Opponent = Opponent;
                SelectedMatch.Date = Date;
                SelectedMatch.Type = Type;
                SelectedMatch.IsFinished = IsFinished;
                SelectedMatch.GoalsAgainst = GoalsAgainst;
                SelectedMatch.GoalsScored = GoalsScored;
                SelectedMatch.Goals = Goals.ToList<Goal>();
                if (!_teamRepository.UpdateMatch(SelectedMatch))
                {
                    ShowErrorMessage("Editace se nepodařila");
                }
            }
        }

        public void OnSelectedItemChanged()
        {
            if (SelectedMatch!= null)
            {
                Opponent = SelectedMatch.Opponent;
                Date = SelectedMatch.Date;
                Type = SelectedMatch.Type;
                IsFinished = SelectedMatch.IsFinished;
                if (IsFinished)
                {
                    GoalsScored = SelectedMatch.GoalsScored;
                    GoalsAgainst = SelectedMatch.GoalsAgainst;
                    Goals.Clear();
                    foreach (Goal goal in SelectedMatch.Goals)
                    {
                        Goals.Add(goal);                    
                    }
                    Players.Clear();
                    foreach(Player player in team.Players)
                    {
                        Players.Add(player);
                    }
                }
                else
                {
                    GoalsScored = 0;
                    GoalsAgainst = 0;
                }
            }
        }

        public bool MatchIsSelected()
        {
            return SelectedMatch!= null;
        }

        public void OnTextGoalsChanged(String text)
        {
            // pokud se změní text se vstřelenými góly, tak se seznam gólů vymaže znovu naplní
            int newGoalCount = Int32.Parse(text);
            Goals.Clear();
            for (int i = 1; i <= newGoalCount; i++)
            {
                Goals.Add(new Goal(i, 0, null));
            }
            Players.Clear();
            foreach (Player player in team.Players)
            {
                Players.Add(player);
            }
        }

        private bool InputIsValid()
        {
            return Opponent != null && Opponent.Length > 0 && GoalsAgainst >= 0 && GoalsScored >= 0;
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }
}
