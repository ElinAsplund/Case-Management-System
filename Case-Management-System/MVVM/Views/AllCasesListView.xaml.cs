using Case_Management_System.MVVM.Models;
using Case_Management_System.MVVM.ViewModels;
using Case_Management_System.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Case_Management_System.MVVM.Views
{
    /// <summary>
    /// Interaction logic for AllCasesListView.xaml
    /// </summary>
    public partial class AllCasesListView : UserControl
    {
        public static Case clickedCase = null!;

        public AllCasesListView()
        {
            InitializeComponent();
        }

        private void ClickedListViewItem(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            var caseObject = item!.DataContext as Case;
            clickedCase = caseObject!;
        }

        private void Btn_Remove_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var _clickedCase = button!.DataContext as Case;

            if (MessageBox.Show("Är du säker på att du vill ta bort ärendet?",
                "Ta bort ärende",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {

                Task.Run(async() => await AllCasesListViewModel.RemoveCaseAsync(_clickedCase));
                clickedCase = null!;

                //I have trouble with ListView not updating when removing case, frontend.
            }
        }
    }
}
