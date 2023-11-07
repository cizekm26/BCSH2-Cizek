using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Goal
    {
        public int GoalId { get; set; }
        public int Minute { get; set; }
        public Player? Scorer { get; set; }

        public Goal(int minute, Player? scorer)
        {
            Minute = minute;
            Scorer = scorer;
        }
    }
}
