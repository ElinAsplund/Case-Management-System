using Case_Management_System.Contexts;
using Case_Management_System.MVVM.Models.Entities;
using Case_Management_System.MVVM.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Case_Management_System.MVVM.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private static ObservableObject currentViewModel = null!;

    private DatabaseContext _context = new DatabaseContext();

    //public MainViewModel()
    //{
    //    CurrentViewModel = new EmptyContactViewModel();
    //}

    //Syntax to change between views:
    //[RelayCommand]
    //public void GoToAddContact()
    //{
    //    CurrentViewModel = new AddContactViewModel();
    //}


    [ObservableProperty]
    private string title = "Lägg till ett nytt ärende";

    [ObservableProperty]
    private string firstName = string.Empty;

    [ObservableProperty]
    private string lastName = string.Empty;

    [ObservableProperty]
    private string email = string.Empty;
 
    [ObservableProperty]
    private string phoneNumber = string.Empty;

    [ObservableProperty]
    private string description = string.Empty;

    //[RelayCommand]
    //public async Task SaveAsync(Case task)
    //{
    //    CustomerEntity customerEntity = task;
    //    CaseEntity caseEntity = task;

    //    _context.Add(customerEntity);
    //    _context.Add(caseEntity);
    //    await _context.SaveChangesAsync();
    //}

    [RelayCommand]
    private void Click()
    {
        ClearForm();
    }

    private void ClearForm()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
        PhoneNumber = string.Empty;
        Description = string.Empty;
    }

    [RelayCommand]
    public async Task SaveAsync()
    {
        var task = new Case
        {
            Description = Description,
            CustomerFirstName = FirstName,
            CustomerLastName = LastName,
            CustomerEmail = Email,
            CustomerPhoneNumber = PhoneNumber,
        };

        CustomerEntity customerEntity = task;
        CaseEntity caseEntity = task;

        _context.Add(customerEntity);
        caseEntity.CustomerId = customerEntity.Id;
        _context.Add(caseEntity);
        await _context.SaveChangesAsync();
        ClearForm();
    }
    
    //[RelayCommand]
    //public void Saving()
    //{
    //    var task = new Case
    //    {
    //        Description = Description,
    //        CustomerFirstName = FirstName,
    //        CustomerLastName = LastName,
    //        CustomerEmail = Email,
    //        CustomerPhoneNumber = PhoneNumber,
    //    };

    //    CustomerEntity customerEntity = task;
    //    CaseEntity caseEntity = task;

    //    _context.Add(customerEntity);
    //    caseEntity.CustomerId = customerEntity.Id;
    //    _context.Add(caseEntity);
    //    _context.SaveChanges();
    //    ClearForm();
    //}
}
