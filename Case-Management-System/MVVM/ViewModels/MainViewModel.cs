using Case_Management_System.MVVM.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Case_Management_System.MVVM.Views;

namespace Case_Management_System.MVVM.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    public static ObservableObject currentViewModel = null!;

    public MainViewModel()
    {
        CurrentViewModel = new FirstViewModel();
    }
    
    //Navigation commands:
    [RelayCommand]
    public void GoToAllCasesList()
    {
        CurrentViewModel = new AllCasesListViewModel();
    }

    [RelayCommand]
    public void GoToAddACase()
    {
        CurrentViewModel = new AddACaseViewModel();
    }

    [RelayCommand]
    public void GoToAddComment()
    {
        Case _currentCase = AllCasesListView.clickedCase;

        if (_currentCase != null)
            CurrentViewModel = new AddCommentViewModel(_currentCase);
        else
            CurrentViewModel = new EmptyDetailsViewModel();
    }
}
