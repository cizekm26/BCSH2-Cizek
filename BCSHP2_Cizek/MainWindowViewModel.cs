using BCSH2_Cizek;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiteDB;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsLibrary;

namespace BCSHP2_Cizek
{
    [ObservableObject]
   public partial class MainWindowViewModel
    {

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RemoveCommand))]
        private Team selectedTeam;

        [ObservableProperty]
        private ObservableCollection<Team> teams;

        public MainWindowViewModel()
        {
            Teams = new ObservableCollection<Team>();
        }

        [RelayCommand]
        public void Add()
        {
            Teams.Add(new Team(0,"",0,""));
        }

        [RelayCommand(CanExecute = nameof(RemoveCanExecute))]
        public void Remove()
        {
            Teams.Remove(SelectedTeam);
        }

        [RelayCommand]
        public void Manage()
        {
            var window = new TeamView();
            window.Show();
        }


        public bool RemoveCanExecute()
        {
            return SelectedTeam!= null;
        }

        public bool ManageCanExecute()
        {
            return SelectedTeam!= null;
        }
    }
}
