using Case_Management_System.Contexts;
using Case_Management_System.MVVM.Models.Entities;
using Case_Management_System.MVVM.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace Case_Management_System.Services;

internal class DatabaseService
{
    private static DatabaseContext _context = new DatabaseContext();

    public static async Task SaveToDbAsync(Case task)
    {
        CustomerEntity customerEntity = task;
        CaseEntity caseEntity = task;

        _context.Add(customerEntity);
        caseEntity.CustomerId = customerEntity.Id;
        _context.Add(caseEntity);
        await _context.SaveChangesAsync();
    }

    public static async Task<ObservableCollection<Case>> GetAllFromDbAsync()
    {
        var _cases = new ObservableCollection<Case>();

        foreach (var _case in await _context.Cases.Include(x => x.Customer).ToListAsync())
        {
            CustomerEntity customerEntity = _case.Customer;
            CaseEntity caseEntity = _case;

            _cases.Add(new Case
            {
                Id = caseEntity.Id,
                Description = caseEntity.Description,
                EntryTime = caseEntity.EntryTime,
                Status = caseEntity.Status,
                CustomerFirstName = customerEntity.FirstName,
                CustomerLastName = customerEntity.LastName,
                CustomerEmail = customerEntity.Email,
                CustomerPhoneNumber = customerEntity.PhoneNumber
            });
        }

        return _cases;
    }
}
