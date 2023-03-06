using Case_Management_System.MVVM.Models;
using Case_Management_System.MVVM.ViewModels;
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

        //public void GetObject_Click(object sender, RoutedEventArgs e)
        //{
        //    var button = sender as Button;
        //    var caseObject = button!.DataContext as Case;
        //    clickedCase = caseObject!;
        //}

        private void ListViewItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            var caseObject = item!.DataContext as Case;
            clickedCase = caseObject!;

            // Do something with the caseObject, e.g. navigate to a new view
        }
    }
}
