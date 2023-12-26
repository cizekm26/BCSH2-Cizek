using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamsLibrary
{
    public class Team
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Ranking { get; set; }
        public string Competition { get; set; }

        public List<Player> Players { get; set; }
        public List<Match> Matches { get; set; }

        public Team(string name, int ranking, string competition)
        {
            Name = name;
            Ranking = ranking;
            Competition = competition;
            Players = new List<Player>();
            Matches = new List<Match>();
        }

    }
}
