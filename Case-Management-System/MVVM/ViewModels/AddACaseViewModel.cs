using Case_Management_System.MVVM.Models;
using Case_Management_System.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace Case_Management_System.MVVM.ViewModels;

public partial class AddACaseViewModel : ObservableObject
{
    [ObservableProperty]
    private string firstName = string.Empty;

    [ObservableProperty]
    private string lastName = string.Empty;

    [ObservableProperty]
    private string email = string.Empty;

    [ObservableProperty]
    private string phoneNumber = string.Empty;

    [ObservableProperty]
    private string enteredDescription = string.Empty;

    private void ClearForm()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
        PhoneNumber = string.Empty;
        EnteredDescription = string.Empty;
    }

    [RelayCommand]
    public async Task SaveAsync()
    {
        //Checking that the entered case is not empty:
        if(EnteredDescription!="" && Email!="") 
        { 
            var newCase = new Case
            {
                Description = EnteredDescription,
                CustomerFirstName = FirstName,
                CustomerLastName = LastName,
                CustomerEmail = Email,
                CustomerPhoneNumber = PhoneNumber
            };

            await DatabaseService.SaveToDbAsync(newCase);

            ClearForm();
        }
    }
}
