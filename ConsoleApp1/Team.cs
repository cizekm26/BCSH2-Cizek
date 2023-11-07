using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public int Ranking { get; set; }
        public string Competition { get; set; }

        public Team(string name, int ranking, string competition)
        {
            Name = name;
            Ranking = ranking;
            Competition = competition;
        }

    }
}
