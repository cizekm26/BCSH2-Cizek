using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamsLibrary
{
    public class Team:INotifyPropertyChanged
    {
        private string name;
        private int ranking;
        private string competition;
        private List<Match> match;
        private List<Player> player;


        public int ID { get; set; }
        public string Name { get => name; set { name=value; OnPropertyChanged(nameof(Name)); } }
        public int Ranking { get => ranking; set { ranking=value; OnPropertyChanged(nameof(Ranking)); } }
        public string Competition { get => competition; set { competition=value; OnPropertyChanged(nameof(Competition)); } }

        public List<Match> Matches { get => match; set { match = value; OnPropertyChanged(nameof(Matches)); } }

        public List<Player> Players { get => player; set { player = value; OnPropertyChanged(nameof(Players)); } }

        public Team(string name, int ranking, string competition)
        {
            Name = name;
            Ranking = ranking;
            Competition = competition;
            Players = new List<Player>();
            Matches = new List<Match>();
        }

        public Team() {
            Players = new List<Player>();
            Matches = new List<Match>();
        }

        public void AddPlayer(Player player)
        {
            if(player != null)
                Players.Add(player);
        }

        public void RemovePlayer(Player player)
        {
            if (player != null)
                Players.Remove(player);
        }

        public void AddMatch(Match match)
        {
            if (match != null)
                Matches.Add(match);
        }

        public void RemoveMatch(Match match)
        {
            if (match != null)
                Matches.Remove(match);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
