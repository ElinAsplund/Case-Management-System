using Case_Management_System.MVVM.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Case_Management_System.MVVM.ViewModels;

public partial class AddCommentViewModel : ObservableObject
{
    [ObservableProperty]
    public Case currentTask = null!;

    [ObservableProperty]
    private string firstName = string.Empty;
    
    [ObservableProperty]
    private string description = string.Empty;


    public AddCommentViewModel()
    {
    }
    
    public AddCommentViewModel(Case currentCase)
    {
        currentTask = currentCase;

        FirstName = currentTask.CustomerFirstName;
        Description = currentTask.Description;
    }

}
