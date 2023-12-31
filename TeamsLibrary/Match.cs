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
        Home, Away
    }
    public class Match:INotifyPropertyChanged
    {
        private string opponent;
        private int goalsScored;
        private int goalsAgainst;
        private bool isFinished;
        public int ID { get; set; }
        public int TeamID { get; set; }

        public string Opponent { get => opponent; set { opponent=value; OnPropertyChanged(nameof(Opponent)); } }
        public DateTime Date { get; set; }
        public int GoalsScored { get => goalsScored; set { goalsScored=value; OnPropertyChanged(nameof(GoalsScored)); } }
        public int GoalsAgainst { get => goalsAgainst; set { goalsAgainst=value; OnPropertyChanged(nameof(GoalsAgainst)); } }
        public List<Goal> Goals { get; set; }
        public MatchType Type { get; set; }
        public bool IsFinished { get => isFinished; set { isFinished=value; OnPropertyChanged(nameof(IsFinished)); } }

        public Match(string opponent, DateTime matchDate, MatchType matchType, bool isFinished): this(opponent, matchDate, 0, 0, matchType, isFinished)
        {
        }

        public Match(string opponent, DateTime date, int goalsScored, int goalsAgainst, MatchType type, bool isFinished)
        {
            Opponent=opponent;
            Date=date;
            GoalsScored=goalsScored;
            GoalsAgainst=goalsAgainst;
            Goals = new List<Goal>();
            Type=type;
            IsFinished=isFinished;
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
