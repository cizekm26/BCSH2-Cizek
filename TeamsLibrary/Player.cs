using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamsLibrary
{
    public class Player
    {
        public int MathId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int GoalsScored { get; set; }
        public Player(string firstName, string lastName, int age) 
        { 
            FirstName= firstName;
            LastName= lastName;
            Age= age;
            GoalsScored= 0;
        }
    }
}
