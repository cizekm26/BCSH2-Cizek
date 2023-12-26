using LiteDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamsLibrary
{
    public class TeamRepository : ITeamRepository
    {
        private LiteDatabase db;
        private readonly IList<Team> teams;

        public TeamRepository()
        {
            db = new LiteDatabase(@"C:\tmp\MyData.db");
            ILiteCollection<Team> teamCollection = db.GetCollection<Team>("teams");
            teams = new ObservableCollection<Team>(teamCollection.FindAll());
        }

        public IEnumerable<Team> Teams { get { return teams; } }

        public bool Add(Team team)
        {
            if (teams.IndexOf(team) < 0)
            {
                ILiteCollection<Team> teamCollection = db.GetCollection<Team>("teams");
                teamCollection.Insert(team);
                teams.Add(team);
                return true;
            }
            return false;
        }

        public bool Remove(Team team)
        {
            if (teams.IndexOf(team) >= 0)
            {
                ILiteCollection<Team> teamCollection = db.GetCollection<Team>("teams");
                teamCollection.Delete(team.ID);
                teams.Remove(team);
                return true;
            }
            return false;
        }

        public bool Update(Team team)
        {
            if(teams.IndexOf(team) >= 0)
            {
                ILiteCollection<Team> teamCollection = db.GetCollection<Team>("teams");
                Team teamUpdate = teamCollection.FindById(team.ID);
                teamUpdate.Name = team.Name;
                teamUpdate.Ranking = team.Ranking;
                teamUpdate.Competition = team.Competition;
                teamCollection.Update(teamUpdate);
                /*Team teamCol = teams.FirstOrDefault(team);
                teamCol.Name = team.Name;
                teamCol.Ranking = team.Ranking;
                teamCol.Competition = team.Competition;*/
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
