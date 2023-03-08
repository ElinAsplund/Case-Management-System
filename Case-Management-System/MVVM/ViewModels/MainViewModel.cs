using Case_Management_System.MVVM.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using Case_Management_System.Services;
using System.Collections.ObjectModel;
using Case_Management_System.MVVM.Views;

namespace Case_Management_System.MVVM.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    public static ObservableObject currentViewModel = null!;

    [ObservableProperty]
    private ObservableCollection<Case> casesList = null!;

    [ObservableProperty]
    private ObservableCollection<Employee> employeesList = null!;

    public async Task populateCasesList()
    {
        CasesList = await DatabaseService.GetAllFromDbAsync();
    }

    public async Task populateEmployeesList()
    {
        EmployeesList = await DatabaseService.GetAllEmployeesAsync();
    }

    public MainViewModel()
    {
        Task.Run(async () => await populateCasesList());
        Task.Run(async () => await populateEmployeesList());
        CurrentViewModel = new AddACaseViewModel();
    }


    //Navigation commands
    [RelayCommand]
    public void GoToAllCasesList()
    {
        CurrentViewModel = new AllCasesListViewModel(CasesList);
    }

    [RelayCommand]
    public void GoToAddACase()
    {
        CurrentViewModel = new AddACaseViewModel();
    }

    [RelayCommand]
    public void GoToAddComment()
    {
        Case currentCase = AllCasesListView.clickedCase;

        if (currentCase != null)
            CurrentViewModel = new AddCommentViewModel(currentCase, EmployeesList);
        else
            CurrentViewModel = new AddCommentViewModel();
    }
}
