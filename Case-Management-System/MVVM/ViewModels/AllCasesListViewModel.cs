using Case_Management_System.MVVM.Models;
using Case_Management_System.MVVM.Views;
using Case_Management_System.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Case_Management_System.MVVM.ViewModels;

public partial class AllCasesListViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Case> taskList = null!;
    

    public AllCasesListViewModel()
    {
    }

    public AllCasesListViewModel(ObservableCollection<Case> caseList)
    {
        taskList = caseList;
        Task.Run(async () => await populateCaseList());
    }

    public async Task populateCaseList()
    {
        TaskList = await DatabaseService.GetAllFromDbAsync();
    }

}
