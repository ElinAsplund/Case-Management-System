using Case_Management_System.Contexts;
using Case_Management_System.MVVM.Models.Entities;
using Case_Management_System.MVVM.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using Case_Management_System.MVVM.ViewModels;

namespace Case_Management_System.Services;

internal class DatabaseService
{
    public static DatabaseContext _context = new DatabaseContext();

    public static async Task SaveToDbAsync(Case newCase)
    {
        CustomerEntity customerEntity = newCase;
        CaseEntity caseEntity = newCase;
        CustomerEntity _currentCustomer = null!;

        //Check if there is any customer in the db with the entered email already
        var _allCustomers = await _context.Customers.ToListAsync();
        var customerNotUniqueEmail = _allCustomers.Where(x => x.Email == customerEntity.Email);

        foreach (var customer in customerNotUniqueEmail) {
            _currentCustomer = new CustomerEntity()
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber
            };
        }

        if(customerNotUniqueEmail.IsNullOrEmpty()) 
        {
            _context.Add(customerEntity);
            caseEntity.CustomerId = customerEntity.Id;
            _context.Add(caseEntity);
            await _context.SaveChangesAsync();            
        }
        else
        {
            //If email is not unique in the db, it sets the newCase to the customer with the matching email in db.
            caseEntity.CustomerId = _currentCustomer.Id;
            _context.Add(caseEntity);
            await _context.SaveChangesAsync();
        }
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

    public static async Task<ObservableCollection<Employee>> GetAllEmployeesAsync()
    {
        var _employees = new ObservableCollection<Employee>();

        foreach (var _employee in await _context.Employees.ToListAsync())
        {
            EmployeeEntity employeeEntity = _employee;

            _employees.Add(new Employee
            {
                Id = employeeEntity.Id,
                FirstName = employeeEntity.FirstName,
                LastName = employeeEntity.LastName,
                NameInitials = employeeEntity.NameInitials
            });
        }

        return _employees;
    }


    public static async Task<ObservableCollection<Comment>> GetSpecificCommentsFromDbAsync(Case currentCase)
    {
        var _allComments = new ObservableCollection<CommentEntity>();
        var _actualComments = new ObservableCollection<Comment>();
        var _currentCaseId = currentCase.Id;

        foreach (var _comment in await _context.Comments.ToListAsync())
        {
            _allComments.Add(_comment);
        };

        foreach(var _actualComment in _allComments.Where(x => x.CaseId == _currentCaseId))
        {
            Comment _comment = _actualComment;
            _actualComments.Add(_comment);
        }    

        return _actualComments;
    }

    public static async Task ChangeStatusAsync(Case currentCase)
    {
        var _dbCaseEntity = await _context.Cases.FirstOrDefaultAsync(x => x.Id == currentCase.Id);

        _dbCaseEntity!.Status = currentCase.Status;

        _context.Update(_dbCaseEntity);
        await _context.SaveChangesAsync();
    }

    public static async Task SaveCommentToDbAsync(Comment comment, int caseId)
    {
        CommentEntity commentEntity = comment;
        commentEntity.CaseId = caseId;
        _context.Add(commentEntity);
        await _context.SaveChangesAsync();
    }
    
    //public static async Task RemoveCaseAsync(Case clickedCase)
    //{
    //    var _dbCaseEntity = await _context.Cases.FirstOrDefaultAsync(x => x.Id == clickedCase.Id);
        
    //    if(_dbCaseEntity != null)
    //    {
    //        var _allComments = new List<CommentEntity>();

    //        foreach (var _comment in await _context.Comments.ToListAsync())
    //        {
    //            _allComments.Add(_comment);
    //        };

    //        foreach (var _associatedComment in _allComments.Where(x => x.CaseId == _dbCaseEntity.Id))
    //        {
    //            _context.Remove(_associatedComment);
    //        }

    //        _context.Remove(_dbCaseEntity);
    //        await _context.SaveChangesAsync();
    //    }
    //}
}
