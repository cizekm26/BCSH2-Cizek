using LiteDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TeamsLibrary
{
    public class TeamRepository : ITeamRepository
    {
        private LiteDatabase db;
        private readonly IList<Team> teams;

        public TeamRepository()
        {
            db = new LiteDatabase("@teams.db");
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
                /*Team teamUpdate = teamCollection.FindById(team.ID);
                teamUpdate.Name = team.Name;
                teamUpdate.Ranking = team.Ranking;
                teamUpdate.Competition = team.Competition;*/
                teamCollection.Update(team);
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

        public bool AddPlayer(Player player)
        {
            
            if (teams.Where(t => t.ID == player.TeamID).Count() > 0)
            {
                ILiteCollection<Team> teamCollection = db.GetCollection<Team>("teams");
                ILiteCollection<Player> playerCollection = db.GetCollection<Player>("players");
                Team team = teams.Where(t => t.ID == player.TeamID).First();
                playerCollection.Insert(player);
                team.AddPlayer(player);
                teamCollection.Update(team);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RemovePlayer(Player player)
        {
            if (teams.Where(t => t.ID == player.TeamID).Count() > 0)
            {
                ILiteCollection<Team> teamCollection = db.GetCollection<Team>("teams");
                ILiteCollection<Player> playerCollection = db.GetCollection<Player>("players");
                Team team = teams.Where(t => t.ID == player.TeamID).First();
                team.RemovePlayer(player);
                teamCollection.Update(team);
                playerCollection.Delete(player.ID);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdatePlayer(Player player)
        {
            if(teams.Where(t => t.ID == player.TeamID).Count() > 0)
            {
                //ILiteCollection<Team> teamCollection = db.GetCollection<Team>("teams");
                ILiteCollection<Player> playerCollection = db.GetCollection<Player>("players");
                playerCollection.Update(player);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddMatch(Match match)
        {
            if (teams.Where(t => t.ID == match.TeamID).Count() > 0)
            {
                ILiteCollection<Team> teamCollection = db.GetCollection<Team>("teams");
                ILiteCollection<Match> matchCollection = db.GetCollection<Match>("matches");
                ILiteCollection<Player> playerCollection = db.GetCollection<Player>("players");
                ILiteCollection<Goal> goalCollection = db.GetCollection<Goal>("goals");
                matchCollection.Insert(match);
                foreach (Goal goal in match.Goals)
                {
                    if (goal.Scorer != null)
                    {
                        goal.Scorer.GoalsScored++;
                    }
                    goal.MatchID = match.ID;
                    goalCollection.Insert(goal);
                }
                Team team = teams.Where(t => t.ID == match.TeamID).First();
                team.AddMatch(match);
                teamCollection.Update(team);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RemoveMatch(Match match)
        {
            if (teams.Where(t => t.ID == match.TeamID).Count() > 0)
            {
                ILiteCollection<Team> teamCollection = db.GetCollection<Team>("teams");
                ILiteCollection<Goal> goalCollection = db.GetCollection<Goal>("goals");
                ILiteCollection<Player> playerCollection = db.GetCollection<Player>("players");
                ILiteCollection<Match> matchCollection = db.GetCollection<Match>("matches");
                Team team = teams.Where(t => t.ID == match.TeamID).First();
                team.RemoveMatch(match);
                IEnumerable<Goal> goals = goalCollection.Find(t => t.MatchID == match.ID);
                foreach (Goal goal in goals)
                {
                    if (goal.Scorer != null)
                    {
                        goal.Scorer.GoalsScored--;
                        playerCollection.Update(goal.Scorer);
                    }
                    goalCollection.Delete(goal.ID);
                }
                teamCollection.Update(team);
                matchCollection.Delete(match.ID);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateMatch(Match match)
        {
            if (teams.Where(t => t.ID == match.TeamID).Count() > 0)
            {
                //ILiteCollection<Team> teamCollection = db.GetCollection<Team>("teams");
                ILiteCollection<Match> matchCollection = db.GetCollection<Match>("matches");
                ILiteCollection<Goal> goalCollection = db.GetCollection<Goal>("goals");
                ILiteCollection<Player> playerCollection = db.GetCollection<Player>("players");

                IEnumerable<Goal> goals = goalCollection.Find(t => t.MatchID == match.ID);
                foreach (Goal goal in goals)
                {
                    if (goal.Scorer != null)
                    {
                        goal.Scorer.GoalsScored--;
                        playerCollection.Update(goal.Scorer);
                    }
                    goalCollection.Delete(goal.ID);
                }
                foreach(Goal goal in match.Goals)
                {
                    if (goal.Scorer != null)
                    {
                        goal.Scorer.GoalsScored++;
                        playerCollection.Update(goal.Scorer);
                    }
                    goal.MatchID = match.ID;
                    goalCollection.Insert(goal);
                }
                matchCollection.Update(match);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
