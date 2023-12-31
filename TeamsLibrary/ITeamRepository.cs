using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamsLibrary
{
    public interface ITeamRepository
    {
        bool Add(Team team);
        bool Update(Team team);
        bool Remove(Team team);

        bool AddPlayer(Player player);
        bool RemovePlayer(Player player);
        bool UpdatePlayer(Player player);

        bool AddMatch(Match match);
        bool RemoveMatch(Match match);
        bool UpdateMatch(Match match);
        IEnumerable<Team> Teams { get; }
    }
}
