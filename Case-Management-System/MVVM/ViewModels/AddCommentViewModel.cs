using Case_Management_System.MVVM.Models;
using Case_Management_System.MVVM.Models.Entities;
using Case_Management_System.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Case_Management_System.MVVM.ViewModels;

public partial class AddCommentViewModel : ObservableObject
{
    [ObservableProperty]
    private int? id;

    [ObservableProperty]
    private string description = string.Empty;

    [ObservableProperty]
    private string? entryTime;

    [ObservableProperty]
    private string status;

    [ObservableProperty]
    private string firstName = string.Empty;

    [ObservableProperty]
    private string lastName = string.Empty;

    [ObservableProperty]
    private string email = string.Empty;

    [ObservableProperty]
    private string? phoneNumber = string.Empty;

    [ObservableProperty]
    private string enteredComment = string.Empty;

    [ObservableProperty]
    private string selectedStatus = "Välj en ny status:";

    [ObservableProperty]
    private Employee selectedEmployee = null!;

    [ObservableProperty]
    private Case _currentCase = null!;

    [ObservableProperty]
    private ObservableCollection<Employee> employeesList = null!;
    
    [ObservableProperty]
    private ObservableCollection<Comment> commentsList = null!;

    public AddCommentViewModel()
    {
        Task.Run(async () => await populateEmployeesList());
    }

    public async Task populateLists(Case currentCase)
    {
        EmployeesList = await DatabaseService.GetAllEmployeesAsync();
        CommentsList = await DatabaseService.GetSpecificCommentsFromDbAsync(currentCase);
    }
    public async Task populateEmployeesList()
    {
        EmployeesList = await DatabaseService.GetAllEmployeesAsync();
    }

    public AddCommentViewModel(Case currentCase)
    {
        Task.Run(async () => await populateLists(_currentCase));

        _currentCase = currentCase;

        Id = _currentCase.Id;
        Description = _currentCase.Description;
        EntryTime = _currentCase.EntryTime.ToString("dd/MM/yyyy HH:mm");

        //Convert the enum to a string, in swedish:
        if (_currentCase.Status == CaseStatus.NotStarted)
            Status = "Ej påbörjad";
        else if (_currentCase.Status == CaseStatus.InProgress)
            Status = "Pågående";
        else if (_currentCase.Status == CaseStatus.Completed)
            Status = "Avslutad";
       
        FirstName = _currentCase.CustomerFirstName;
        LastName = _currentCase.CustomerLastName;
        Email = _currentCase.CustomerEmail;

        //Checks if there is a phonenumber in the db and if there isn't any,
        //sets it to a string-message for the frontend:
        if (_currentCase.CustomerPhoneNumber == "             ")
            PhoneNumber = "Inget telefonnummer angivet.";
        else
            PhoneNumber = _currentCase.CustomerPhoneNumber;

    }

    [RelayCommand]
    public async Task UpdateStatusAsync()
    {
        if (SelectedStatus == "Ej påbörjad")
            _currentCase.Status = CaseStatus.NotStarted;
        else if (SelectedStatus == "Pågående")
            _currentCase.Status = CaseStatus.InProgress;
        else if (SelectedStatus == "Avslutad")
            _currentCase.Status = CaseStatus.Completed;


        if (SelectedStatus != "Välj en ny status:")
        {
            await DatabaseService.ChangeStatusAsync(_currentCase);

            //This updates the frontend's current status to the new status
            Status = SelectedStatus;
        }
        else
            SelectedStatus = "Välj en ny status:";
    }

    [RelayCommand]
    public async Task AddCommentAsync()
    {
        Comment _comment = new Comment {
            CommentString = EnteredComment,
            SigningEmployee = SelectedEmployee
        };

        await DatabaseService.SaveCommentToDbAsync(_comment, _currentCase.Id);
        
        //Resetting and updating frontend
        CommentsList.Add(_comment);
        EnteredComment = "";
        SelectedEmployee = EmployeesList[0];
    }
}
