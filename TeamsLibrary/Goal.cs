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
        private int order;
        private int minute;
        private Player? scorer;
        public int ID { get; set; }
        public int MatchID { get; set; }
        public int Order {
            get => order; set { order = value; OnPropertyChanged(nameof(Order)); } 
        }
        public int Minute
        {
            get => minute; set { minute = value; OnPropertyChanged(nameof(Minute)); }
        }
        public Player? Scorer {
            get => scorer; set { scorer = value; OnPropertyChanged(nameof(Scorer)); }
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
