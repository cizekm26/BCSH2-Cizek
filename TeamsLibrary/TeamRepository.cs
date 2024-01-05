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
        private readonly LiteDatabase db;
        private readonly IList<Team> teams;

        public TeamRepository()
        {
            // reference na seznam zápasů a hráčů v týmu, z databáze se potom budou načítat všechny atributy objektů
            BsonMapper.Global.Entity<Team>()
                .DbRef(x => x.Matches, "matches")
                .DbRef(x => x.Players, "players");
            // reference na seznam gólů v zápase
            BsonMapper.Global.Entity<Match>()
                .DbRef(x => x.Goals, "goals");
            db = new LiteDatabase(@"../../../../teams.db");
            ILiteCollection<Team> teamCollection = db.GetCollection<Team>("teams");
            teams = new ObservableCollection<Team>(teamCollection
                .Include(t => t.Players)
                .Include(t => t.Matches)
                .Include(t => t.Matches.Select(m => m.Goals))
                .Include(t => t.Matches.Select(m => m.Goals.Select(z => z.Scorer)))
                .FindAll()
            );
            
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
                return teams.Remove(team);
            }
            return false;
        }

        public bool Update(Team team)
        {
            if(teams.IndexOf(team) >= 0)
            {
                ILiteCollection<Team> teamCollection = db.GetCollection<Team>("teams");
                return teamCollection.Update(team);
            }
            else
            {
                return false;
            }
        }

        public bool AddPlayer(Player player)
        {
            
            if (teams.Where(t => t.ID == player.TeamID).Any())
            {
                ILiteCollection<Team> teamCollection = db.GetCollection<Team>("teams");
                ILiteCollection<Player> playerCollection = db.GetCollection<Player>("players");

                Team team = teams.Where(t => t.ID == player.TeamID).First();
                playerCollection.Insert(player);
                team.AddPlayer(player);
                return teamCollection.Update(team);
            }
            else
            {
                return false;
            }
        }

        public bool RemovePlayer(Player player)
        {
            if (teams.Where(t => t.ID == player.TeamID).Any())
            {
                ILiteCollection<Team> teamCollection = db.GetCollection<Team>("teams");
                ILiteCollection<Player> playerCollection = db.GetCollection<Player>("players");

                Team team = teams.Where(t => t.ID == player.TeamID).First();
                if (team == null)
                    return false;
                team.RemovePlayer(player);
                playerCollection.Delete(player.ID);
                return teamCollection.Update(team);
            }
            else
            {
                return false;
            }
        }

        public bool UpdatePlayer(Player player)
        {
            if(teams.Where(t => t.ID == player.TeamID).Any())
            {
                ILiteCollection<Player> playerCollection = db.GetCollection<Player>("players");
                return playerCollection.Update(player);
            }
            else
            {
                return false;
            }
        }

        public bool AddMatch(Match match)
        {
            if (teams.Where(t => t.ID == match.TeamID).Any())
            {
                ILiteCollection<Team> teamCollection = db.GetCollection<Team>("teams");
                ILiteCollection<Match> matchCollection = db.GetCollection<Match>("matches");
                ILiteCollection<Goal> goalCollection = db.GetCollection<Goal>("goals");

                matchCollection.Insert(match);
                foreach (Goal goal in match.Goals)
                {
                    AddGoal(match, goal);
                }
                matchCollection.Update(match);
                Team team = teams.Where(t => t.ID == match.TeamID).First();
                team.AddMatch(match);
                return teamCollection.Update(team);
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
                ILiteCollection<Match> matchCollection = db.GetCollection<Match>("matches");

                foreach (Goal goal in match.Goals)
                {
                    DeleteGoal(goal);
                }
                matchCollection.Update(match);
                Team team = teams.Where(t => t.ID == match.TeamID).First();
                matchCollection.Delete(match.ID);
                team.RemoveMatch(match);
                return teamCollection.Update(team);
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
                ILiteCollection<Match> matchCollection = db.GetCollection<Match>("matches");
                ILiteCollection<Goal> goalCollection = db.GetCollection<Goal>("goals");
                ILiteCollection<Player> playerCollection = db.GetCollection<Player>("players");

                // nejprve jsou smazány všechny góly z upraveného zápasu a potom přidány nové
                IEnumerable<Goal> goals = goalCollection.Find(t => t.MatchID == match.ID);
                foreach (Goal goal in goals)
                {
                    if (goal.Scorer!= null)
                    {
                        Player scorer = playerCollection.FindById(goal.Scorer.ID);
                        if (scorer != null)
                        {
                            scorer.GoalsScored--;
                            playerCollection.Update(scorer);
                        }
                    }
                    goalCollection.Delete(goal.ID);
                }
                foreach (Goal goal in match.Goals)
                {
                    AddGoal(match, goal);
                }
                return matchCollection.Update(match);
            }
            else
            {
                return false;
            }
        }

        private void AddGoal(Match match, Goal goal)
        {
            ILiteCollection<Goal> goalCollection = db.GetCollection<Goal>("goals");
            ILiteCollection<Player> playerCollection = db.GetCollection<Player>("players");

             if (goal.Scorer != null)
             {
               int goalCount = playerCollection.FindById(goal.Scorer.ID).GoalsScored;
               goal.Scorer.GoalsScored = goalCount + 1;
               playerCollection.Update(goal.Scorer);
             }
             goal.MatchID = match.ID;
             goalCollection.Insert(goal);
        }

        private void DeleteGoal(Goal goal)
        {
            ILiteCollection<Goal> goalCollection = db.GetCollection<Goal>("goals");
            ILiteCollection<Player> playerCollection = db.GetCollection<Player>("players");

            if (goal.Scorer!= null)
            {
                    goal.Scorer.GoalsScored--;
                    playerCollection.Update(goal.Scorer);
            }
            goalCollection.Delete(goal.ID);
        }
    }
}
