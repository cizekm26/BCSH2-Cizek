using BCSH2_Cizek.ViewModel;
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
    /// Interaction logic for MatchesView.xaml
    /// </summary>
    public partial class MatchesView : UserControl
    {
        private readonly MatchesViewModel viewModel;
        public MatchesView(MatchesViewModel viewModel)
        {
            this.viewModel = viewModel;
            InitializeComponent();
            DataContext = this.viewModel;
            comboType.ItemsSource = Enum.GetValues(typeof(MatchType));
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.OnSelectedItemChanged();
        }

        private void TextGoals_TextChanged(object sender, TextChangedEventArgs e)
        {
            // po změně počtu gólů se znovu naplní seznam gólů (dataGrid)
            TextBox? textBox = sender as TextBox;
            if (textBox != null && textBox.Text.Length > 0)
            {
                viewModel.OnTextGoalsChanged(textBox.Text);
            }
        }
    }
}
