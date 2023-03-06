using Case_Management_System.MVVM.Models;
using Case_Management_System.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace Case_Management_System.MVVM.ViewModels;

public partial class AddACaseViewModel : ObservableObject
{
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
            CustomerPhoneNumber = PhoneNumber
        };

        await DatabaseService.SaveToDbAsync(task);

        ClearForm();
    }

}
