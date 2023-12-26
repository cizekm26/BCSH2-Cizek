using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamsLibrary
{
    public enum MatchType
    {
        Home, Away
    }
    public class Match
    {
        public string Opponent { get; set; }
        public DateTime Date { get; set; }
        public int TeamGoals { get; set; }
        public int OppGoals { get; set; }
        public List<Goal> Goals { get; set; }
        public MatchType Type { get; set; }
        public bool IsFinished { get; set; }

        public Match(string opponent, DateTime matchDate, MatchType matchType)
        {
            Opponent = opponent;
            Date = matchDate;
            TeamGoals = 0;
            OppGoals = 0;
            IsFinished = false;
            Goals = new List<Goal>();
        }
    }
}
