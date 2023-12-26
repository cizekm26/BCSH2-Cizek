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
        IEnumerable<Team> Teams { get; }
    }
}
