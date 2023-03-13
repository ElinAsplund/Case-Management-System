using Case_Management_System.MVVM.Models;
using Case_Management_System.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Case_Management_System.MVVM.ViewModels;

public partial class AllCasesListViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Case> casesList = null!;

    public async Task populateCaseList()
    {
        CasesList = await DatabaseService.GetAllFromDbAsync();
    }

    public AllCasesListViewModel()
    {
        Task.Run(async () => await populateCaseList());
    }
}
