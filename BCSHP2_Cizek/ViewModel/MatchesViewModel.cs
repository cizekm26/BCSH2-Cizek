using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TeamsLibrary;

namespace BCSH2_Cizek.ViewModel
{
    [ObservableObject]
    public partial class MatchesViewModel
    {
        [ObservableProperty]
        private Match selectedMatch;

        [ObservableProperty]
        private string opponent;

        [ObservableProperty]
        private DateTime date;

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

        private Team team;
        private readonly ITeamRepository _teamRepository;

        public MatchesViewModel(Team team, ITeamRepository repository)
        {
            this.team= team;
            _teamRepository= repository;
            Matches= new ObservableCollection<Match>(this.team.Matches);
            Players= new ObservableCollection<Player>(this.team.Players);
            Goals=new ObservableCollection<Goal>();
        }

        [RelayCommand]
        public void Add()
        {
            Match match;
            if (IsFinished)
            {
                match = new Match(Opponent, Date, GoalsScored, GoalsAgainst, Type, IsFinished);
                match.Goals = Goals.ToList<Goal>();
            }
            else
                match = new Match(Opponent, Date, Type, IsFinished);
            match.TeamID = team.ID;
            if (_teamRepository.AddMatch(match))
            {
                Matches.Add(match);
            }
        }

        [RelayCommand]
        public void Remove()
        {
            if (SelectedMatch!= null)
            {
                if (_teamRepository.RemoveMatch(SelectedMatch))
                {
                    Matches.Remove(SelectedMatch);
                }
            }
        }

        [RelayCommand]
        public void Edit()
        {
            if (SelectedMatch!= null)
            {
                //List<Goal> goalsOld = SelectedMatch.Goals.ToList();
                SelectedMatch.Opponent = Opponent;
                SelectedMatch.Date = Date;
                SelectedMatch.Type = type;
                SelectedMatch.IsFinished = IsFinished;
                SelectedMatch.GoalsAgainst = GoalsAgainst;
                SelectedMatch.GoalsScored = GoalsScored;
                SelectedMatch.Goals = Goals.ToList<Goal>();
                _teamRepository.UpdateMatch(SelectedMatch);
            }
        }

        public void OnSelectedItemChanged()
        {
            if (SelectedMatch!= null)
            {
                Opponent = SelectedMatch.Opponent;
                Date = SelectedMatch.Date;
                type = SelectedMatch.Type;
                IsFinished = SelectedMatch.IsFinished;
                if (IsFinished)
                {
                    GoalsScored = SelectedMatch.GoalsScored;
                    GoalsAgainst = SelectedMatch.GoalsAgainst;
                    Goals = new ObservableCollection<Goal>(SelectedMatch.Goals);
                }
                else
                {
                    GoalsScored = 0;
                    GoalsAgainst = 0;
                }
            }
        }

        public void OnTextGoalsChanged(String text)
        {
                int newGoalCount = Int32.Parse(text);
                Goals.Clear();
                for (int i = 1; i <= newGoalCount; i++)
                {
                    Goals.Add(new Goal(i, 0, null));
                }
        }

    }
}
