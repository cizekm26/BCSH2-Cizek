using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Match
    {
        public int MatchId { get; set; }
        public string Opponent { get; set; }
        public DateTime MatchStart { get; set; }
        public int TeamGoalsCount { get; set; }
        public int OppGoalsCount { get; set; }
        public List<Goal> TeamGoals { get; set; }
        public bool IsFinished { get; set; }

        public Match(string opponent, DateTime matchStart)
        {
            Opponent = opponent;
            MatchStart = matchStart;
            TeamGoalsCount = 0;
            OppGoalsCount = 0;
            IsFinished = false;
            TeamGoals = new List<Goal>();
        }
    }
}
