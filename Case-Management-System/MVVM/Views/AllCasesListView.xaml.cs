using Case_Management_System.MVVM.Models;
using Case_Management_System.MVVM.ViewModels;
using System.Windows;
using System.Windows.Controls;


namespace Case_Management_System.MVVM.Views
{
    /// <summary>
    /// Interaction logic for AllCasesListView.xaml
    /// </summary>
    public partial class AllCasesListView : UserControl
    {
        //public static Case clickedCase = null!;

        public AllCasesListView()
        {
            InitializeComponent();
        }

        public void AddComment_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var caseObject = button!.DataContext as Case;

        }

    }
}
