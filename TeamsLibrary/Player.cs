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
        private string firstName;
        private string lastName;
        private int age;
        private int goalsScored;

        public int ID { get; set; }
        public int TeamID { get; set; }

        public string FirstName { get => firstName; set { firstName=value; OnPropertyChanged(nameof(FirstName)); } }
        public string LastName { get => lastName; set { lastName=value; OnPropertyChanged(nameof(LastName)); } }
        public int Age { get => age; set { age=value; OnPropertyChanged(nameof(Age)); } }
        public int GoalsScored { get => goalsScored; set { goalsScored=value; OnPropertyChanged(nameof(GoalsScored)); } }
        public Player(string firstName, string lastName, int age) 
        { 
            FirstName= firstName;
            LastName= lastName;
            Age= age;
            GoalsScored= 0;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string? ToString() => FirstName + " " + LastName;
    }
}
