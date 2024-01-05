using BCSH2_Cizek.ViewModel;
using BCSHP2_Cizek;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TeamsLibrary;

namespace BCSH2_Cizek.Views
{
    /// <summary>
    /// Interaction logic for TeamSummaryView.xaml
    /// </summary>
    public partial class TeamSummaryView : Window
    {
        public TeamSummaryView(Team team, ITeamRepository teamRepository)
        {
            if (team== null)
                throw new ArgumentNullException(nameof(team));
            if (teamRepository== null)
                throw new ArgumentNullException(nameof(teamRepository));

            InitializeComponent();
            DataContext = new TeamSummaryViewModel(team, teamRepository);
            PlayersView playersView = new(new PlayersViewModel(team, teamRepository));
            MatchesView matchesView = new(new MatchesViewModel(team, teamRepository));

            TabItem playersTab = new()
            {
                Content = playersView,
                Name = "PlayersTab",
                Header = "Spravovat hráče"
            };
            TabItem matchesTab = new()
            {
                Content = matchesView,
                Name="MatchesTab",
                Header = "Spravovat zápasy"
            };
            tabControl.Items.Add(playersTab);
            tabControl.Items.Add(matchesTab);
        }
    }
}
