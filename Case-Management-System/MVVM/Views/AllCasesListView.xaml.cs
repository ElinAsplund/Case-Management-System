using Case_Management_System.MVVM.Models;
using Case_Management_System.MVVM.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
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
    }
}
