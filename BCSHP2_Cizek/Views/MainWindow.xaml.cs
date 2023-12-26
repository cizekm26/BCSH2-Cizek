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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TeamsLibrary;

namespace BCSH2_Cizek.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;
        public MainWindow()
        {
            TeamRepository teamRepository = new TeamRepository();
            _viewModel = new MainWindowViewModel(teamRepository);
            DataContext = _viewModel;
            InitializeComponent();
        }


    }
}
