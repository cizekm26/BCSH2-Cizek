using LiteDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamsLibrary
{
    public class Goal:INotifyPropertyChanged
    {
        private int id;
        private int matchId;
        private int order;
        private int minute;
        private Player? scorer;

        public int ID { 
            get=>id; 
            set { id=value; OnPropertyChanged(nameof(ID)); }
        }
        public int MatchID { 
            get=>matchId; 
            set { matchId=value; OnPropertyChanged(nameof(MatchID)); }
        }
        public int Order {
            get => order; 
            set { order = value; OnPropertyChanged(nameof(Order)); } 
        }
        public int Minute
        {
            get => minute; 
            set { minute = value; OnPropertyChanged(nameof(Minute)); }
        }

        [BsonRef("players")]
        public Player? Scorer { 
            get=>scorer;
            set { scorer = value; OnPropertyChanged(nameof(Scorer)); }
        }

        public Goal(int order, int minute, Player? scorer)
        {
            Order = order;
            Minute = minute;
            Scorer = scorer;
        }

        public Goal()
        {

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
