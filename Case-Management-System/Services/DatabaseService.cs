using Case_Management_System.Contexts;
using Case_Management_System.MVVM.Models.Entities;
using Case_Management_System.MVVM.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace Case_Management_System.Services;

internal class DatabaseService
{
    public static DatabaseContext _context = new DatabaseContext();

    public static async Task<bool> SaveToDbAsync(Case newCase)
    {
        CustomerEntity _customerEntity = newCase;
        CaseEntity _caseEntity = newCase;
        CustomerEntity _foundCustomer = null!;

        //Checks if a customer with the entered email exists in the db:
        var _allCustomersInDb = await _context.Customers.ToListAsync();
        var _customerEmailFound = _allCustomersInDb.Where(x => x.Email == _customerEntity.Email);

        //Mapping up the _foundCustomer:
        foreach (var _customer in _customerEmailFound) {
            _foundCustomer = new CustomerEntity()
            {
                Id = _customer.Id,
                FirstName = _customer.FirstName,
                LastName = _customer.LastName,
                Email = _customer.Email,
                PhoneNumber = _customer.PhoneNumber
            };
        }

        //If no customer is found in the db with the entered email, adds a new customer:
        if(_customerEmailFound.IsNullOrEmpty()) 
        {
            //Checks that the FirstName and LastName aren't null:
            if(_customerEntity.FirstName != "" || _customerEntity.LastName != "")
            {
                _context.Add(_customerEntity);
                _caseEntity.CustomerId = _customerEntity.Id;
                _context.Add(_caseEntity);
                await _context.SaveChangesAsync();

                return true;
            }
        }
        else
        {
            //If email is found in the db, it sets the newCase to the foundCustomer with the matching email in db:
            _caseEntity.CustomerId = _foundCustomer.Id;
            _context.Add(_caseEntity);
            await _context.SaveChangesAsync();

            return true;
        }

        return false;
    }

    public static async Task<ObservableCollection<Case>> GetAllFromDbAsync()
    {
        var _cases = new ObservableCollection<Case>();

        foreach (var _case in await _context.Cases.Include(x => x.Customer).ToListAsync())
        {
            CustomerEntity customerEntity = _case.Customer;
            CaseEntity caseEntity = _case;

            //Mapping of the db entities (customer and case) to the a case model:
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

            //Mapping of the db entity (employee) to the a employee model:
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
        var _speficicComments = new ObservableCollection<Comment>();

        foreach (var _comment in await _context.Comments.ToListAsync())
        {
            _allComments.Add(_comment);
        };

        foreach(var _comment in _allComments.Where(x => x.CaseId == currentCase.Id))
        {
            //Casting from CommentEntity to Comment:
            Comment _speficicComment = _comment;
            _speficicComments.Add(_speficicComment);
        }    

        return _speficicComments;
    }

    public static async Task ChangeStatusAsync(Case currentCase)
    {
        var _currentCaseEntity = await _context.Cases.FirstOrDefaultAsync(x => x.Id == currentCase.Id);

        _currentCaseEntity!.Status = currentCase.Status;

        _context.Update(_currentCaseEntity);
        await _context.SaveChangesAsync();
    }

    public static async Task SaveCommentToDbAsync(Comment comment, int caseId)
    {
        //Casting from Comment to CommentEntity:
        CommentEntity _commentEntity = comment;
        _commentEntity.CaseId = caseId;
        _context.Add(_commentEntity);
        await _context.SaveChangesAsync();
    }

    public static async Task RemoveCaseAsync(Case clickedCase)
    {
        var _clickedCaseEntity = await _context.Cases.FirstOrDefaultAsync(x => x.Id == clickedCase.Id);

        //Becuase I have trouble making the list of cases, in the frontend, update correctly when removing a case,
        //I have to check if the _clickedCaseEntity is present:
        if (_clickedCaseEntity != null)
        {
            var _allComments = new List<CommentEntity>();

            //Lists all the comments in the db:
            foreach (var _comment in await _context.Comments.ToListAsync())
            {
                _allComments.Add(_comment);
            };

            //Remove all the associated comments to the clicked case, one by one:
            foreach (var _associatedComment in _allComments.Where(x => x.CaseId == _clickedCaseEntity.Id))
            {
                _context.Remove(_associatedComment);
            }

            //Removes the clicked case:
            _context.Remove(_clickedCaseEntity);
            await _context.SaveChangesAsync();
        }
    }
}
