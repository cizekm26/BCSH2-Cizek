using LiteDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamsLibrary
{
    public class Player: INotifyPropertyChanged
    {
        private int id;
        private int teamId;
        private string firstName;
        private string lastName;
        private int age;
        private int goalsScored;

        public int ID { get => id; set { id=value; OnPropertyChanged(nameof(ID)); } }
        public int TeamID { get => teamId; set { teamId=value; OnPropertyChanged(nameof(TeamID)); } }
        public string FirstName { get => firstName; set { firstName=value; OnPropertyChanged(nameof(FirstName)); } }
        public string LastName { get => lastName; set { lastName=value; OnPropertyChanged(nameof(LastName)); } }
        public int Age { get => age; set { age=value; OnPropertyChanged(nameof(Age)); } }
        public int GoalsScored { get => goalsScored; set { goalsScored=value; OnPropertyChanged(nameof(GoalsScored)); } }
        public Player(string firstName, string lastName, int age, int teamId) 
        { 
            FirstName= firstName;
            LastName= lastName;
            Age= age;
            GoalsScored= 0;
            TeamID = teamId;
        }

        public Player() { 
        }



        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is Player player)
            {
                return ID == player.ID &&
                    FirstName == player.FirstName &&
                    LastName == player.LastName &&
                    Age == player.Age &&
                    TeamID == player.TeamID;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID,FirstName,LastName,Age,TeamID);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string? ToString() => FirstName + " " + LastName;

       
    }
}
