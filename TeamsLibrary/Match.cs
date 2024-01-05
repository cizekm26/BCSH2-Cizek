using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TeamsLibrary
{
    public enum MatchType
    {
        Doma, Venku
    }
    public class Match:INotifyPropertyChanged
    {
        private int id;
        private int teamId;
        private DateTime date;
        private string opponent;
        private int goalsScored;
        private int goalsAgainst;
        private bool isFinished;
        private List<Goal> goals;
        private MatchType type;

        public int ID { 
            get => id; set { id = value; OnPropertyChanged(nameof(ID)); }
        }
        public int TeamID { 
            get => teamId; set { teamId = value; OnPropertyChanged(nameof(TeamID)); } 
        }
        public string Opponent { 
            get => opponent; set { opponent=value; OnPropertyChanged(nameof(Opponent)); } 
        }
        public DateTime Date { 
            get => date; set { date=value; OnPropertyChanged(nameof(Date)); }
        }
        // vstřelené góly
        public int GoalsScored { 
            get => goalsScored; set { goalsScored=value; OnPropertyChanged(nameof(GoalsScored)); } 
        }
        // obdržené góly
        public int GoalsAgainst { 
            get => goalsAgainst; set { goalsAgainst=value; OnPropertyChanged(nameof(GoalsAgainst)); } 
        }
        // údaje o vstřelených gólech
        public List<Goal> Goals {
            get => goals; set { goals=value; OnPropertyChanged(nameof(Goals)); }
        }
        // domácí/venkovní zápas
        public MatchType Type { 
            get => type; set { type=value; OnPropertyChanged(nameof(Type)); }
        }
        // zápas byl dohrán
        public bool IsFinished {
            get => isFinished; set { isFinished=value; OnPropertyChanged(nameof(IsFinished)); }
        }

        public Match(string opponent, DateTime matchDate, MatchType matchType, bool isFinished, int teamId):
            this(opponent, matchDate, 0, 0, new List<Goal>(),matchType, isFinished, teamId)
        {
        }

        public Match(string opponent, DateTime date, int goalsScored, int goalsAgainst, List<Goal> goals, MatchType type, bool isFinished, int teamId)
        {
            Opponent=opponent;
            Date=date;
            GoalsScored=goalsScored;
            GoalsAgainst=goalsAgainst;
            Goals = goals;
            Type=type;
            IsFinished=isFinished;
            TeamID = teamId;
        }

        public Match()
        {

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
