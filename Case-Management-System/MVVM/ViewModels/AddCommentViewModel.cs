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
    public Case currentTask = null!;

    [ObservableProperty]
    public int? id;

    [ObservableProperty]
    private string description = string.Empty;

    [ObservableProperty]
    public string? entryTime;

    [ObservableProperty]
    public string status;

    [ObservableProperty]
    private string firstName = string.Empty;

    [ObservableProperty]
    public string lastName = string.Empty;

    [ObservableProperty]
    public string email = string.Empty;

    [ObservableProperty]
    public string? phoneNumber = string.Empty;

    [ObservableProperty]
    public string enteredComment = string.Empty;

    [ObservableProperty]
    public string selectedStatus = "Välj en ny status:";

    [ObservableProperty]
    public Employee selectedEmployee = null!;

    [ObservableProperty]
    private ObservableCollection<Employee> employeesList = null!;
    
    [ObservableProperty]
    private ObservableCollection<Comment> commentsList = null!;

    public AddCommentViewModel()
    {
        Task.Run(async () => await populateEmployeesList());
    }

    public async Task populateLists(Case currentTask)
    {
        EmployeesList = await DatabaseService.GetAllEmployeesAsync();
        CommentsList = await DatabaseService.GetSpecificCommentsFromDbAsync(currentTask);
    }
    public async Task populateEmployeesList()
    {
        EmployeesList = await DatabaseService.GetAllEmployeesAsync();
    }

    public AddCommentViewModel(Case currentCase, ObservableCollection<Employee> employees)
    {

        employeesList = employees;
        //Task.Run(async () => await populateEmployeesList());
        Task.Run(async () => await populateLists(currentTask));

        currentTask = currentCase;

        Id = currentTask.Id;
        Description = currentTask.Description;
        EntryTime = currentTask.EntryTime.ToString("dd/MM/yyyy HH:mm");

        //Convert the enum to a string, in swedish:
        if (currentTask.Status == CaseStatus.NotStarted)
            Status = "Ej påbörjad";
        else if (currentTask.Status == CaseStatus.InProgress)
            Status = "Pågående";
        else if (currentTask.Status == CaseStatus.Completed)
            Status = "Avslutad";
       
        FirstName = currentTask.CustomerFirstName;
        LastName = currentTask.CustomerLastName;
        Email = currentTask.CustomerEmail;

        //Checks if there is a phonenumber in the db and if there isn't any,
        //sets it to a string-message for the frontend:
        if (currentTask.CustomerPhoneNumber == "             ")
            PhoneNumber = "Inget telefonnummer angivet.";
        else
            PhoneNumber = currentTask.CustomerPhoneNumber;

    }

    [RelayCommand]
    public async Task UpdateStatusAsync()
    {
        if (SelectedStatus == "Ej påbörjad")
            CurrentTask.Status = CaseStatus.NotStarted;
        else if (SelectedStatus == "Pågående")
            CurrentTask.Status = CaseStatus.InProgress;
        else if (SelectedStatus == "Avslutad")
            CurrentTask.Status = CaseStatus.Completed;


        if (SelectedStatus != "Välj en ny status:")
        {
            await DatabaseService.ChangeStatusAsync(CurrentTask);

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

        await DatabaseService.SaveCommentToDbAsync(_comment, CurrentTask.Id);
        
        CommentsList.Add(_comment);
        EnteredComment = "";
        SelectedEmployee = EmployeesList[0];
    }
}
